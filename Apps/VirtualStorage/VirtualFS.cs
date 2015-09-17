using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using PCLStorage;
using ProtoBuf;

namespace TheBall.Support.VirtualStorage
{

    [ProtoContract]
    public class VirtualFS
    {
        public readonly object MyLock = new object();

        [ProtoMember(1)]
        public readonly string StorageFolderLocation;
        [ProtoMember(2)]
        public Dictionary<string, VFSItem> FileLocationDictionary = new Dictionary<string, VFSItem>();
        [ProtoMember(3)]
        public Dictionary<string, VFSItem[]> ContentHashDictionary = new Dictionary<string, VFSItem[]>();

        public static VirtualFS Current { get; private set; }

        public VirtualFS()
        {
            
        }

        public static async Task InitializeVFS()
        {
            var localPersonalPath = FileSystem.Current.LocalStorage.Path;
            var virtualFSPath = Path.Combine(localPersonalPath, "VFS");
            if (await FileSystem.Current.LocalStorage.CheckExistsAsync(virtualFSPath) == ExistenceCheckResult.NotFound)
                await FileSystem.Current.LocalStorage.CreateFolderAsync(virtualFSPath, CreationCollisionOption.FailIfExists);
            await InitializeVFS(virtualFSPath);
        }

        private string VFSMetaFileName
        {
            get { return Path.Combine(StorageFolderLocation, "VirtualFS.protobuf"); }
        }

        private static async Task InitializeVFS(string storageFolderLocation)
        {
            Current = new VirtualFS(storageFolderLocation);
            Current = await Current.LoadFrom();
        }

        private async Task<VirtualFS> LoadFrom()
        {
            string vfsMetaFileName = VFSMetaFileName;
            VirtualFS result;
            if (await FileSystem.Current.LocalStorage.CheckExistsAsync(vfsMetaFileName) == ExistenceCheckResult.NotFound)
            {
                result = this;
            }
            else
            {
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(vfsMetaFileName);
                using (var stream = await file.OpenAsync(FileAccess.Read))
                {
                    result = Serializer.Deserialize<VirtualFS>(stream);
                }
            }
            return result;
        }

        [ProtoContract]
        public class VFSItem
        {
            [ProtoMember(1)]
            public string FileName;
            [ProtoMember(2)]
            public string StorageFileName;
            [ProtoMember(3)]
            public string ContentMD5;
            [ProtoMember(4)]
            public long ContentLength;
            //public DateTime LastModifiedTime;
        }


        private VirtualFS(string storageFolderLocation)
        {
            StorageFolderLocation = storageFolderLocation;
        }

        public ContentItemLocationWithMD5[] GetContentRelativeFromRoot(string rootLocation)
        {
            lock (MyLock)
            {
                var allFileNames = FileLocationDictionary.Keys;
                var filesBelongingToRootLQ = allFileNames.Where(fileName => fileName.StartsWith(rootLocation));
                var contentItems = filesBelongingToRootLQ.Select(fileName =>
                {
                    var vfsItem = FileLocationDictionary[fileName];
                    var relativeFileName = fileName.Substring(rootLocation.Length);
                    return new ContentItemLocationWithMD5
                    {
                        ContentLocation = relativeFileName,
                        ContentMD5 = vfsItem.ContentMD5
                    };
                }).ToArray();
                return contentItems;
            }
        }

        public void RemoveLocalContentByMD5(string contentMd5)
        {
            lock (MyLock)
            {
                var fsItems = ContentHashDictionary[contentMd5];
                foreach (var fsItem in fsItems)
                    FileLocationDictionary.Remove(fsItem.FileName);
                ContentHashDictionary.Remove(contentMd5);
                var storageFile = fsItems.First().StorageFileName;
                var fileGet = FileSystem.Current.LocalStorage.GetFileAsync(storageFile);
                fileGet.Wait();
                var file = fileGet.Result;
                var deletion = file.DeleteAsync();
                deletion.Wait();
                var saveTask = SaveChanges();
                saveTask.Wait();
            }
        }

        public void RemoveLocalContent(string targetFullName)
        {
            lock (MyLock)
            {
                var fsItem = FileLocationDictionary[targetFullName];
                var contentHashKey = fsItem.ContentMD5;
                var allContentLinks = ContentHashDictionary[contentHashKey];
                FileLocationDictionary.Remove(targetFullName);
                if (allContentLinks.Length == 1)
                {
                    ContentHashDictionary.Remove(contentHashKey);
                    var fileGet = FileSystem.Current.LocalStorage.GetFileAsync(fsItem.StorageFileName);
                    fileGet.Wait();
                    var file = fileGet.Result;
                    var deletion = file.DeleteAsync();
                    deletion.Wait();
                }
                else
                {
                    ContentHashDictionary[contentHashKey] =
                        allContentLinks.Where(link => link.FileName != targetFullName).ToArray();
                }
                var saveTask = SaveChanges();
                saveTask.Wait();
            }
        }

        private async Task SaveChanges()
        {
            var realFile = VFSMetaFileName;
            var tmpFile = realFile + "_tmp";
            var file = await FileSystem.Current.LocalStorage.CreateFileAsync(tmpFile, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                Serializer.Serialize(stream, this);
                await stream.FlushAsync();
            }
            await file.MoveAsync(realFile, NameCollisionOption.ReplaceExisting);
            //File.Copy(tmpFile, realFile, true);
        }

        private static string toFileNameSafeMD5(string md5)
        {
            return md5.Replace("+", "-").Replace("/", "_");
        }

        private static string toRealMD5(string fileNameSafeMD5)
        {
            return fileNameSafeMD5.Replace("-", "+").Replace("_", "/");
        }

        public async Task<Stream> GetLocalTargetStreamForRead(string targetFullName)
        {
            VFSItem fsItem;
            if (!FileLocationDictionary.TryGetValue(targetFullName, out fsItem))
            {
                string[] allKeys = FileLocationDictionary.Keys.Cast<string>().ToArray();
                return null;
            }
            var storageFullName = getStorageFullPath(fsItem.StorageFileName);
            var file = await FileSystem.Current.LocalStorage.GetFileAsync(storageFullName);
            return await file.OpenAsync(FileAccess.Read);
        }

        public async Task<Stream> GetLocalTargetStreamForWrite(string contentHash)
        {
            var storageFileName = toFileNameSafeMD5(contentHash);
            var file =
                await
                    FileSystem.Current.LocalStorage.CreateFileAsync(storageFileName,
                        CreationCollisionOption.ReplaceExisting);
            return await file.OpenAsync(FileAccess.ReadAndWrite);
        }

        public async Task<Stream> GetLocalTargetStreamForWrite(ContentItemLocationWithMD5 targetLocationItem)
        {
            if (fileExists(targetLocationItem.ContentLocation))
                RemoveLocalContent(targetLocationItem.ContentLocation);
            var fsItem = new VFSItem
            {
                ContentMD5 = targetLocationItem.ContentMD5,
                FileName = targetLocationItem.ContentLocation,
                StorageFileName =
                    toFileNameSafeMD5(targetLocationItem.ContentMD5) +
                    Path.GetExtension(targetLocationItem.ContentLocation)
            };
            FileLocationDictionary.Add(targetLocationItem.ContentLocation, fsItem);
            var contentHashKey = targetLocationItem.ContentMD5;

            VFSItem[] existingItems;
            VFSItem[] toAdd = new VFSItem[] {fsItem};
            bool hasExistingContent = false;
            if (ContentHashDictionary.TryGetValue(contentHashKey, out existingItems))
            {
                ContentHashDictionary[contentHashKey] = existingItems.Concat(toAdd).ToArray();
                hasExistingContent = true;
            }
            else
                ContentHashDictionary.Add(contentHashKey, toAdd);
            await SaveChanges();

            if (hasExistingContent)
                return null;

            var storageFileName = getStorageFullPath(fsItem.StorageFileName);
            //return File.Create(storageFileName);
            var file = await FileSystem.Current.LocalStorage.CreateFileAsync(storageFileName,
                CreationCollisionOption.ReplaceExisting);
            return await file.OpenAsync(FileAccess.ReadAndWrite);
        }

        private string getStorageFullPath(string storageFileName)
        {
            return Path.Combine(StorageFolderLocation, storageFileName);
        }

        private bool fileExists(string contentLocation)
        {
            return FileLocationDictionary.ContainsKey(contentLocation);
        }

        public async Task UpdateMetadataAfterWrite(ContentItemLocationWithMD5 targetLocationItem)
        {
            var fsItem = FileLocationDictionary[targetLocationItem.ContentLocation];
            //var file = await FileSystem.Current.LocalStorage.GetFileAsync(fsItem.StorageFileName);
            //var fileInfo = new FileInfo(getStorageFullPath(fsItem.StorageFileName));
            //fsItem.ContentLength = FileSystem.Current.LocalStorage.;
            var contentHashKey = fsItem.ContentMD5;
            VFSItem[] allLinkItems;
            if (ContentHashDictionary.TryGetValue(contentHashKey, out allLinkItems))
            {
                allLinkItems = allLinkItems.Concat(new VFSItem[] {fsItem}).ToArray();
                ContentHashDictionary[contentHashKey] = allLinkItems;
            }
            else
            {
                ContentHashDictionary.Add(contentHashKey, new VFSItem[] {fsItem});
            }
            await SaveChanges();
        }

        public ContentSyncRequest CreateFullSyncRequest(string stagingRoot, string[] requestedFolders, Func<byte[], byte[]> md5HashComputer)
        {
            lock (MyLock)
            {
                var allFiles = FileLocationDictionary.Keys.Where(key => key.StartsWith(stagingRoot)).ToArray();
                Uri rootUri;
                if(stagingRoot.EndsWith("\\") || stagingRoot.EndsWith("/"))
                    rootUri = new Uri(stagingRoot);
                else
                    rootUri = new Uri(stagingRoot + "/");
                var syncItems = allFiles.Select(fileName =>
                    new
                    {
                        RelativeName = rootUri.MakeRelativeUri(new Uri(fileName)).OriginalString,
                        FullName = fileName
                    }).ToArray();
                var groupAndAccountFiles = syncItems.Where(item => item.RelativeName.StartsWith("grp") || item.RelativeName.StartsWith("acc")).ToArray();
                var ownerGrp = groupAndAccountFiles.GroupBy(item =>
                {
                    if (item.RelativeName.StartsWith("acc"))
                        return "account";
                    var paths = item.RelativeName.Split('/');
                    return Path.Combine(paths[0], paths[1]).Replace('\\', '/');
                });
                var ownerFolderGrouped = ownerGrp.Select(groupItems =>
                {
                    var ownerPrefix = groupItems.Key;

                    var tempFolderGrp = groupItems.GroupBy(item =>
                    {
                        var paths = item.RelativeName.Split('/');
                        return paths[2];
                    });
                    var folderContent = tempFolderGrp.Select(folderGrp =>
                    {
                        var folderName = folderGrp.Key;
                        var folderContents = folderGrp.Select(item => new RemoteSyncSupport.FolderContent
                        {
                            ContentMD5 = FileLocationDictionary[item.FullName].ContentMD5,
                            RelativeName = item.RelativeName.Substring(item.RelativeName.IndexOf('/', 4) + 1)
                        });
                        var fullMD5Hash = RemoteSyncSupport.GetFolderMD5Hash(folderContents, md5HashComputer);
                        return new
                        {
                            FolderName = folderName,
                            FullMD5Hash = fullMD5Hash
                        };

                    });
                    return new
                    {
                        OwnerPrefix = ownerPrefix,
                        Folders = folderContent
                    };
                }).ToArray();

                var allMD5s =
                    allFiles.Select(fileName => FileLocationDictionary[fileName].ContentMD5).Distinct().ToArray();

                var contentOwners = ownerFolderGrouped.Select(ownerItem => new ContentSyncRequest.ContentOwner
                {
                    OwnerPrefix = ownerItem.OwnerPrefix,
                    ContentFolders = ownerItem.Folders.Select(folderItem => new ContentSyncRequest.ContentFolder
                    {
                        Name = folderItem.FolderName,
                        FullMD5Hash = folderItem.FullMD5Hash
                    }).ToArray()
                }).ToArray();
                var syncRequest = new ContentSyncRequest
                {
                    ContentMD5s = allMD5s,
                    RequestedFolders = requestedFolders,
                    ContentOwners = contentOwners
                };
                return syncRequest;
            }
        }

        public void UpdateContentNameData(string contentMd5, string[] fullNames, bool initialAdd = false, long initialAddContentLength = -1)
        {
            lock (MyLock)
            {
                VFSItem[] existingContents = null;
                string[] namesToAdd;
                long contentLength;
                string storageFileName;
                VFSItem[] itemsToDelete = null;
                var hasExistingContent = ContentHashDictionary.TryGetValue(contentMd5, out existingContents);
                if(!initialAdd && !hasExistingContent)
                    throw new InvalidOperationException("Non-existent content must be with initial add flag");
                if (hasExistingContent)
                {
                    existingContents = ContentHashDictionary[contentMd5];
                    var fsItemTemplate = existingContents.First();
                    contentLength = fsItemTemplate.ContentLength;
                    storageFileName = fsItemTemplate.StorageFileName;
                    namesToAdd =
                        fullNames.Where(name => existingContents.All(fsContent => fsContent.FileName != name)).ToArray();
                    itemsToDelete = existingContents.Where(fsItem => !fullNames.Contains(fsItem.FileName)).ToArray();
                    foreach (var itemToDelete in itemsToDelete)
                        FileLocationDictionary.Remove(itemToDelete.FileName);
                }
                else
                {
                    namesToAdd = fullNames;
                    contentLength = initialAddContentLength;
                    storageFileName = toFileNameSafeMD5(contentMd5);
                }
                var itemsToAdd = namesToAdd.Select(fileName =>
                    new VFSItem
                    {
                        FileName = fileName,
                        ContentLength = contentLength,
                        ContentMD5 = contentMd5,
                        StorageFileName = storageFileName
                    }).ToArray();

                if (hasExistingContent)
                    ContentHashDictionary[contentMd5] =
                        existingContents.Except(itemsToDelete).Concat(itemsToAdd).ToArray();
                else
                    ContentHashDictionary.Add(contentMd5, itemsToAdd);

                // Make modifications
                foreach (var itemToAdd in itemsToAdd)
                    FileLocationDictionary.Add(itemToAdd.FileName, itemToAdd);

                SaveChanges().Wait();
            }
        }
    }
}