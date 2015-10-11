﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheBall.Support.VirtualStorage;

namespace TheBall.Support.DeviceClient
{
    public static class ClientExecute
    {
        public static Connection GetConnection(string connectionName)
        {
            var connection = UserSettings.CurrentSettings.Connections.Single(conn => conn.Name == connectionName);
            if(connection == null)
                throw new ArgumentException("Connection not found: " + connectionName);
            return connection;
        }

        public static FolderSyncItem GetFolderSyncItem(string connectionName, string syncName)
        {
            var connection = GetConnection(connectionName);
            var syncItem = connection.FolderSyncItems.SingleOrDefault(item => item.SyncItemName == syncName);
            if(syncItem == null)
                throw new ArgumentException("Sync item not found: " + syncName);
            return syncItem;
        }

        public static void UpSync(string connectionName, string syncItemName)
        {
            var connection = GetConnection(connectionName);
            var syncItem = GetFolderSyncItem(connectionName, syncItemName);
            UpSync(connection, syncItem).Wait();
        }

        public static string[] GetAccountGroups(Connection connection)
        {
            var device = connection.Device;
            var dod = device.ExecuteDeviceOperation(new DeviceOperationData
            {
                OperationRequestString = "GETACCOUNTGROUPS"
            });
            if(dod.OperationResult == false)
                throw new OperationCanceledException("Error on remote call operation: GETACCOUNTGROUPS");
            return dod.OperationReturnValues;
        }

        public static async Task UpSync(Connection connection, FolderSyncItem upSyncItem)
        {
            var rootFolder = upSyncItem.LocalFullPath;
            var sourceList = await FileSystemSupport.GetContentRelativeFromRoot(rootFolder);
            string destinationPrefix = upSyncItem.SyncType == "DEV" ? "DEV_" : "";
            string destinationCopyRoot = destinationPrefix + upSyncItem.RemoteEntry;
            if (destinationCopyRoot.EndsWith("/") == false)
                destinationCopyRoot += "/";
            ContentItemLocationWithMD5[] remoteContentBasedActionList = getConnectionToCopyMD5s(connection, sourceList, destinationCopyRoot);

            var itemsToCopy = remoteContentBasedActionList.Where(item => item.ItemDatas.Any(iData => iData.DataName == "OPTODO" && iData.ItemTextData == "COPY")).ToArray();
            var itemsDeleted = remoteContentBasedActionList.Where(item => item.ItemDatas.Any(iData => iData.DataName == "OPDONE" && iData.ItemTextData == "DELETED")).ToArray();
            var device = connection.Device;
            SyncSupport.SynchronizeSourceListToTargetFolder(
                itemsToCopy, new ContentItemLocationWithMD5[0],
                delegate(ContentItemLocationWithMD5 source, ContentItemLocationWithMD5 target)
                    {
                        string fullLocalName = Path.Combine(rootFolder, source.ContentLocation);
                        string destinationContentName = destinationCopyRoot + source.ContentLocation;
                        DeviceSupport.PushContentToDevice(device, fullLocalName, destinationContentName);
                        Console.WriteLine("Uploaded: " + source.ContentLocation);
                    },
                target =>
                    {

                    }, 10);
            var dod = device.ExecuteDeviceOperation(new DeviceOperationData
                {
                    OperationParameters = new[] {destinationCopyRoot},
                    OperationRequestString = "COPYSYNCEDCONTENTTOOWNER"
                });
            if(dod.OperationResult == false)
                throw new OperationCanceledException("Error on remote call operation");
            Console.WriteLine("Finished copying data to owner location: " + destinationCopyRoot);
        }

        public static async Task DownSync(string connectionName, string syncItemName)
        {
            var connection = GetConnection(connectionName);
            var syncItem = GetFolderSyncItem(connectionName, syncItemName);
            await DownSync(connection, syncItem);
        }

        public static RelativeContentItemRetriever LocalContentItemRetriever =
            FileSystemSupport.GetContentRelativeFromRoot;

        public static TargetStreamRetriever LocalTargetStreamRetriever = 
            FileSystemSupport.GetLocalTargetAsIs;

        public static TargetContentWriteFinalizer LocalTargetContentWriteFinalizer = 
            FileSystemSupport.TargetContentWriteFinalizer;

        public static TargetContentRemover LocalTargetRemover =
            FileSystemSupport.RemoveLocalTarget;


        public static RelativeContentItemRetriever VirtualContentItemRetriever = async location =>
        {
            var result = await SQLiteFS.Current.GetContentRelativeFromRoot(location);
            var propertypes = result.Select(contentItem =>
            {
                return new ContentItemLocationWithMD5
                {
                    ContentLocation = contentItem.ContentLocation,
                    ContentMD5 = contentItem.ContentMD5
                };
            }).ToArray();
            return propertypes;
        };

        public static TargetStreamRetriever VirtualTargetStreamRetriever = async targetLocationItem =>
        {
            var properTypeItem = new VirtualStorage.ContentItemLocationWithMD5
            {
                ContentLocation = targetLocationItem.ContentLocation,
                ContentMD5 = targetLocationItem.ContentMD5
            };
            var stream = await SQLiteFS.Current.GetLocalTargetStreamForWrite(properTypeItem);
            return stream;
        };

        public static TargetContentWriteFinalizer VirtualTargetContentWriteFinalizer = async targetLocationItem =>
        {
            var properTypeItem = new VirtualStorage.ContentItemLocationWithMD5
            {
                ContentLocation = targetLocationItem.ContentLocation,
                ContentMD5 = targetLocationItem.ContentMD5
            };
            //await VirtualFS.Current.UpdateMetadataAfterWrite(properTypeItem);
        };


        public static TargetContentRemover VirtualTargetRemover = async targetLocation =>
        {
            await SQLiteFS.Current.RemoveLocalContent(targetLocation);
        };


        public delegate Task<ContentItemLocationWithMD5[]> RelativeContentItemRetriever(string rootLocation);
        public delegate Task TargetContentWriteFinalizer(ContentItemLocationWithMD5 targetContentItem);
        public delegate Task<Stream> TargetStreamRetriever(ContentItemLocationWithMD5 targetContentItem);
        public delegate Task TargetContentRemover(string targetFullName);

        public static async Task DownSync(Connection connection, FolderSyncItem downSyncItem, string ownerPrefix = null)
        {
            var rootItem = downSyncItem.LocalFullPath;
            var myDataContents = await LocalContentItemRetriever(rootItem);
            foreach (var myDataItem in myDataContents)
            {
                if(!downSyncItem.IsFile)
                    myDataItem.ContentLocation = downSyncItem.RemoteEntry + myDataItem.ContentLocation;
            }
            ContentItemLocationWithMD5[] remoteContentSourceList = getConnectionContentMD5s(connection, new[] { downSyncItem.RemoteEntry }, ownerPrefix);
            var device = connection.Device;
            int stripRemoteFolderIndex = downSyncItem.IsFile ? 0 : downSyncItem.RemoteEntry.Length;
            SyncSupport.SynchronizeSourceListToTargetFolder(
                remoteContentSourceList, myDataContents,
                async delegate(ContentItemLocationWithMD5 source, ContentItemLocationWithMD5 target)
                    {
                        string targetFullName = downSyncItem.IsFile ? rootItem : Path.Combine(rootItem, target.ContentLocation.Substring(stripRemoteFolderIndex));
                        var targetLocalContentItem = new ContentItemLocationWithMD5
                        {
                            ContentLocation = targetFullName,
                            ContentMD5 = source.ContentMD5
                        };
                        var targetStream = await LocalTargetStreamRetriever(targetLocalContentItem);
                        if (targetStream != null)
                        {
                            using (targetStream)
                            {
                                DeviceSupport.FetchContentFromDevice(device, source.ContentLocation,
                                                                     targetStream, ownerPrefix);
                                targetStream.Close();
                            }
                            if (LocalTargetContentWriteFinalizer != null)
                                await LocalTargetContentWriteFinalizer(targetLocalContentItem);
                        }
                        //Console.WriteLine(" ... done");
                        var copyItem = ownerPrefix != null
                            ? ownerPrefix + "/" + source.ContentLocation
                            : source.ContentLocation;
                        Console.WriteLine("Copied: " + copyItem);
                    }, delegate(ContentItemLocationWithMD5 target)
                        {
                            string targetContentLocation = target.ContentLocation.Substring(stripRemoteFolderIndex);
                            string targetFullName = downSyncItem.IsFile ? rootItem : Path.Combine(rootItem, targetContentLocation);
                            LocalTargetRemover(targetFullName);
                            var deleteItem = ownerPrefix != null
                                ? ownerPrefix + "/" + targetContentLocation
                                : targetContentLocation;
                            Console.WriteLine("Deleted: " + deleteItem);
                        }, 10);
        }

        public static ContentItemLocationWithMD5[] getConnectionContentMD5s(Connection connection, string[] downSyncFolders, string ownerPrefix)
        {
            DeviceOperationData dod = new DeviceOperationData
                {
                    OperationRequestString = "GETCONTENTMD5LIST",
                    OperationParameters = downSyncFolders
                };
            dod = connection.Device.ExecuteDeviceOperation(dod, ownerPrefix);
            return dod.OperationSpecificContentData;
        }

        public static ContentItemLocationWithMD5[] getConnectionToCopyMD5s(Connection connection, ContentItemLocationWithMD5[] localContentToSyncTargetFrom, string destinationCopyRoot)
        {
            DeviceOperationData dod = new DeviceOperationData
                {
                    OperationRequestString = "SYNCCOPYCONTENT",
                    OperationSpecificContentData = localContentToSyncTargetFrom,
                    OperationParameters = new[] { destinationCopyRoot}
                };
            dod = connection.Device.ExecuteDeviceOperation(dod);
            return dod.OperationSpecificContentData;
        }

        public static FolderSyncItem AddSyncFolder(string connectionName, string syncName, string syncType, string syncDirection, string localFullPath, string remoteFolder)
        {
            localFullPath = Path.GetFullPath(localFullPath);
            var connection = GetConnection(connectionName);
            if(connection.FolderSyncItems.Any(item => item.SyncItemName == syncName))
                throw new ArgumentException("Sync folder already exists: " + syncName);
            var syncFolderItem = new FolderSyncItem
                {
                    SyncItemName = syncName,
                    SyncDirection = syncDirection,
                    SyncType = syncType,
                    LocalFullPath = localFullPath,
                    RemoteEntry = remoteFolder
                };
            syncFolderItem.Validate();
            connection.FolderSyncItems.Add(syncFolderItem);
            return syncFolderItem;
        }

        public static void RemoveSyncFolder(string connectionName, string syncItemName)
        {
            var connection = GetConnection(connectionName);
            int removed = connection.FolderSyncItems.RemoveAll(item => item.SyncItemName == syncItemName);
            if(removed == 0)
                throw new ArgumentException("Sync item to remove not found: " + syncItemName);
        }

        public static void DeleteConnection(string connectionName, bool forceDeleteWhenRemoteDeleteFails)
        {
            var connection = UserSettings.CurrentSettings.Connections.FirstOrDefault(conn => conn.Name == connectionName);
            if(connection == null)
                throw new ArgumentException("ConnectionName is invalid");
            try
            {
                DeviceSupport.ExecuteRemoteOperationVoid(connection.Device, "TheBall.CORE.RemoteDeviceCoreOperation", new DeviceOperationData
                    {
                        OperationRequestString = "DELETEREMOTEDEVICE"
                    });
            }
            catch(Exception)
            {
                if (forceDeleteWhenRemoteDeleteFails == false)
                    throw;
            }
            UserSettings.CurrentSettings.Connections.Remove(connection);
        }

        public static void CreateConnection(string hostName, string groupIDOrAccountEmailAddress, string connectionName)
        {
            byte[] sharedSecretFullPayload = GetSharedSecretPayload(hostName);
            var sharedSecret = sharedSecretFullPayload.Take(32).ToArray();
            var sharedSecretPayload = sharedSecretFullPayload.Skip(32).ToArray();
            string protocol = hostName.StartsWith("localdev") ? "ws" : "wss";
            string connectionProtocol = hostName.StartsWith("localdev") ? "http" : "https";
            bool isEmailAddress = groupIDOrAccountEmailAddress.Contains("@");
            string groupID = isEmailAddress ? null : groupIDOrAccountEmailAddress;
            string emailAddress = isEmailAddress ? groupIDOrAccountEmailAddress : null;
            bool isAccountConnection = isEmailAddress;
            string connectionTargetParameter = isEmailAddress
                ? $"accountEmail={emailAddress}"
                : $"groupID={groupID}";

            var result = SecurityNegotiationManager.PerformEKEInitiatorAsBob(protocol + "://" + hostName + "/websocket/NegotiateDeviceConnection?" + connectionTargetParameter,
                                                                             sharedSecret, "Connection from Tool with name: " + connectionName, sharedSecretPayload);
            string connectionUrl = isAccountConnection ? String.Format("{1}://{0}/auth/account/DEV", hostName, connectionProtocol)
                : String.Format("{2}://{0}/auth/grp/{1}/DEV", hostName, groupID, connectionProtocol);
            var connection = new Connection
                {
                    Name = connectionName,
                    HostName = hostName,
                    GroupID = groupID,
                    Device = new Device
                        {
                            AESKey = result.AESKey,
                            ConnectionURL = connectionUrl,
                            EstablishedTrustID = result.EstablishedTrustID,
                            AccountEmail = emailAddress
                        },
                    EstablishedTrustID = result.EstablishedTrustID
                };
            UserSettings.CurrentSettings.Connections.Add(connection);
        }

        public static byte[] GetSharedSecretPayload(string hostName)
        {
            string connectionProtocol = hostName.StartsWith("localdev") ? "http" : "https";
            string sharedSecretRequestUrl = string.Format("{0}://{1}/websocket/RequestSharedSecret", connectionProtocol, hostName);
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(sharedSecretRequestUrl);
            request.Method = "POST";
            // Hack on writing 0 length below, because Mono on Mac didn't send server proper "Content-Length=0"
            // when the content length was only set
            using(BinaryWriter writer = new BinaryWriter(request.GetRequestStream()))
                writer.Write(new byte[0]);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            if(response.StatusCode != HttpStatusCode.OK)
                throw new WebException("Invalid response from remote secret request");
            using (MemoryStream memStream = new MemoryStream())
            {
                var responseStream = response.GetResponseStream();
                responseStream.CopyTo(memStream);
                return memStream.ToArray();
            }
        }

        public static async Task SyncFolder(string connectionName, string syncItemName)
        {
            var connection = GetConnection(connectionName);
            var syncItem = GetFolderSyncItem(connectionName, syncItemName);
            if(syncItem.SyncDirection == "UP")
                await UpSync(connection, syncItem);
            else if(syncItem.SyncDirection == "DOWN")
                await DownSync(connection, syncItem);
            else 
                throw new NotSupportedException("Sync direction not supported: " + syncItem.SyncDirection);
        }

        public static void ExecuteWithSettings(Action<UserSettings> executionAction, Action<Exception> exceptionHandling)
        {
            var currentSettings = UserSettings.GetCurrentSettings();
            try
            {
                executionAction(currentSettings);
            }
            catch (Exception ex)
            {
                if (exceptionHandling != null)
                    exceptionHandling(ex);
                else
                    throw;
            }
            finally
            {
                UserSettings.SaveCurrentSettings();
            }
        }

        public static async Task ExecuteWithSettingsAsync(Func<UserSettings, Task> executionAction, Action<Exception> exceptionHandling)
        {
            var currentSettings = UserSettings.GetCurrentSettings();
            try
            {
                await executionAction(currentSettings);
            }
            catch (Exception ex)
            {
                if (exceptionHandling != null)
                    exceptionHandling(ex);
                else
                    throw;
            }
            finally
            {
                UserSettings.SaveCurrentSettings();
            }
        }


        public static async Task StageOperation(string connectionName, bool getData, bool putDev, bool putLive, bool getFullAccount, bool useVirtualFS, object sqlitePlatform = null)
        {
            if(useVirtualFS && !getFullAccount)
                throw new NotSupportedException("VirtualFS is only supported on getFA option");
            var connection = GetConnection(connectionName);
            var stageDef = connection.StageDefinition;
            if(stageDef == null)
                throw new InvalidDataException("Staging definition not found for connection: " + connectionName);
            string stagingRootFolder = stageDef.LocalStagingRootFolder;
            if(String.IsNullOrEmpty(stagingRootFolder))
                throw new InvalidDataException("Staging root folder definition is missing");
            if (getFullAccount)
            {
                if(getData || putDev || putLive)
                    throw new InvalidOperationException("StageOperation: getFA is exclusive and cannot be combined with other get/put operations");
                var localTargetContentWriteFinalizer = LocalTargetContentWriteFinalizer;
                var localContentItemRetriever = LocalContentItemRetriever;
                var localTargetRemover = LocalTargetRemover;
                var localTargetStreamRetriever = LocalTargetStreamRetriever;
                try
                {
                    if (useVirtualFS)
                    {
                        await SQLiteFS.InitializeSQLiteFS(sqlitePlatform);
                        LocalTargetContentWriteFinalizer = VirtualTargetContentWriteFinalizer;
                        LocalContentItemRetriever = VirtualContentItemRetriever;
                        LocalTargetRemover = VirtualTargetRemover;
                        LocalTargetStreamRetriever = VirtualTargetStreamRetriever;
                    }

                    var dataFolders = stageDef.DataFolders;
                    await downSyncFullAccount(connection, stagingRootFolder, dataFolders.ToArray());
                    return;
                }
                finally
                {
                    LocalTargetContentWriteFinalizer = localTargetContentWriteFinalizer;
                    LocalContentItemRetriever = localContentItemRetriever;
                    LocalTargetRemover = localTargetRemover;
                    LocalTargetStreamRetriever = localTargetStreamRetriever;

                }
            }
            if (getData)
            {
                var dataFolders = stageDef.DataFolders;
                await downSyncData(connection, stagingRootFolder, dataFolders);
            }
            List<FolderSyncItem> upsyncItems = new List<FolderSyncItem>();
            if (putDev)
            {
                addUpsyncItems(stagingRootFolder, upsyncItems, "DEV_*");
            }
            if (putLive)
            {
                addUpsyncItems(stagingRootFolder, upsyncItems, "LIVE_*");   
            }
            foreach (var upSyncItem in upsyncItems)
            {
                await UpSync(connection, upSyncItem);
            }
        }

        private static async Task downSyncFullAccount(Connection connection, string stagingRootFolder, string[] dataFolders)
        {
            if (dataFolders.Length == 0)
                throw new InvalidDataException("Staging data folders are not defined (getdata is not possible)");

            var syncRootFolder = stagingRootFolder;
            MD5 md5 = MD5.Create();
            var syncHandler = await RemoteSyncSupport.GetFileSystemSyncHandler(syncRootFolder, dataFolders, md5.ComputeHash);
            await DeviceSupport.ExecuteRemoteOperationAsync(connection.Device, "TheBall.CORE.DeviceSyncFullAccountOperation", syncHandler.RequestStreamHandler, syncHandler.ResponseStreamHandler);
        }

        private static async Task downSyncData(Connection connection, string stagingRootFolder, List<string> dataFolders, string ownerPrefix = null)
        {
            if (dataFolders.Count == 0)
                throw new InvalidDataException("Staging data folders are not defined (getdata is not possible)");
            var folderSyncItems = dataFolders.Select(syncEntry =>
            {
                bool isFile = syncEntry.StartsWith("F:");
                if (!isFile)
                {
                    string fullName = Path.Combine(stagingRootFolder, syncEntry);
                    if (!Directory.Exists(fullName))
                    {
                        Directory.CreateDirectory(fullName);
                    }
                }
                return FolderSyncItem.CreateFromStagingRemoteDataFolder(stagingRootFolder, syncEntry);
            }).Where(fsi => fsi != null).ToArray();
            foreach (var folderSyncItem in folderSyncItems)
            {
                await DownSync(connection, folderSyncItem, ownerPrefix);
            }
        }

        private static void addUpsyncItems(string stagingRootFolder, List<FolderSyncItem> upsyncItems, string folderSearchPattern)
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(stagingRootFolder);
            var subDirectories = rootDirectory.GetDirectories(folderSearchPattern);
            foreach (var subDir in subDirectories)
            {
                var fsi = FolderSyncItem.CreateFromStagingFolderDirectory(stagingRootFolder, subDir.Name);
                if (fsi != null)
                    upsyncItems.Add(fsi);
            }
        }

        public static StageDefinition SetStaging(string connectionName, string stagingFolderFullPath, string dataFolders)
        {

            var connection = GetConnection(connectionName);
            stagingFolderFullPath = Path.GetFullPath(stagingFolderFullPath);
            var stageDef = connection.StageDefinition;
            if (stageDef == null)
            {
                stageDef = new StageDefinition();
                connection.StageDefinition = stageDef;
            }
            if(stagingFolderFullPath != null)
                stageDef.LocalStagingRootFolder = stagingFolderFullPath;
            if (dataFolders != null)
                stageDef.DataFolders = dataFolders.Split(',').ToList();
            return stageDef;
        }

        public static void DetachStaging(string connectionName)
        {
            var connection = GetConnection(connectionName);
            connection.StageDefinition = null;
        }
    }
}
