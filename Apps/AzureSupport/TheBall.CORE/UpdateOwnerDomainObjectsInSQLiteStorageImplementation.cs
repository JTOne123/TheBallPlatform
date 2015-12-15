using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AzureSupport;
using Microsoft.WindowsAzure.StorageClient;
using SQLiteSupport;
using TheBall.CORE.InstanceSupport;

namespace TheBall.CORE
{
    public class UpdateOwnerDomainObjectsInSQLiteStorageImplementation
    {
        public static string GetTarget_OwnerRootPath(IContainerOwner owner)
        {
            return StorageSupport.GetOwnerRootAddress(owner);
        }

        public static CloudBlockBlob[] GetTarget_BlobsToSync(IContainerOwner owner, string semanticDomain)
        {
            var blobListing = owner.GetOwnerBlobListing(semanticDomain, true);
            return blobListing.Cast<CloudBlockBlob>().ToArray();
        }

        public static void ExecuteMethod_PerformSyncing(Type dataContextType, string databaseAttachOrCreateMethodName, string sqLiteDbLocationFileName, string ownerRootPath, CloudBlockBlob[] blobsToSync)
        {
            // For now clear the datacontext type
            if (dataContextType == null)
                return;
            if(databaseAttachOrCreateMethodName == null)
                throw new ArgumentNullException("databaseAttachOrCreateMethodName");
            if (sqLiteDbLocationFileName == null) throw new ArgumentNullException("sqLiteDbLocationFileName");
            if (ownerRootPath == null) throw new ArgumentNullException("ownerRootPath");
            if (blobsToSync == null) throw new ArgumentNullException("blobsToSync");
            //dataContextType.InvokeMember()
            using (
                IStorageSyncableDataContext dbContext = (IStorageSyncableDataContext)dataContextType.InvokeMember(databaseAttachOrCreateMethodName, BindingFlags.InvokeMethod, null, null, new object[] { sqLiteDbLocationFileName })
                    //SQLite.TheBall.Payments.TheBallDataContext.CreateOrAttachToExistingDB(sqLiteDbLocationFileName)
                )
            {

                bool anyChangesApplied = SQLiteSync.ApplyStorageChangesToSQLiteDB(ownerRootPath, dbContext,
                    rootPath =>
                    {
                        List<InformationObjectMetaData> metaDatas = new List<InformationObjectMetaData>();
                        foreach (CloudBlockBlob blob in blobsToSync)
                        {
                            if (Path.GetExtension(blob.Name) != String.Empty)
                                continue;
                            var nameComponents = blob.Name.Split('/');
                            string objectID = nameComponents[nameComponents.Length - 1];
                            string objectType = nameComponents[nameComponents.Length - 2];
                            string semanticDomain = nameComponents[nameComponents.Length - 3];
                            var metaData = new InformationObjectMetaData
                            {
                                CurrentStoragePath = blob.Name.Substring(ownerRootPath.Length),
                                FileLength = blob.Properties.Length,
                                LastWriteTime = blob.Properties.LastModifiedUtc.ToString("s"),
                                MD5 = blob.Properties.ContentMD5,
                                ETag = blob.Properties.ETag,
                                SemanticDomain = semanticDomain,
                                ObjectType = objectType,
                                ObjectID = objectID
                            };
                            metaDatas.Add(metaData);
                        }
                        return metaDatas.ToArray();
                    });
            }
        }

        public static string GetTarget_SQLiteDBLocationDirectory(IContainerOwner owner)
        {
            string currContainerName = InformationContext.Current.InitializedContainerName;
            string dbDirectory = SecureConfig.Current.CoreShareWithFolderName + "\\" + currContainerName  + "\\" + owner.ContainerName + "\\" +
                                 owner.LocationPrefix;
            return dbDirectory;

        }

        public static void ExecuteMethod_CreateDBLocationDirectoryIfMissing(string sqLiteDbLocationDirectory)
        {
            if (!Directory.Exists(sqLiteDbLocationDirectory))
                Directory.CreateDirectory(sqLiteDbLocationDirectory);
        }

        public static string GetTarget_SQLiteDBLocationFileName(string semanticDomain, string sqLiteDbLocationDirectory)
        {
            string fullFileName = Path.Combine(sqLiteDbLocationDirectory, semanticDomain + ".sqlite");
            if (fullFileName.StartsWith(@"\\"))
                fullFileName = @"\\" + fullFileName;
            return fullFileName;
        }

        public static string GetTarget_DataContextFullTypeName(string semanticDomain)
        {
            return String.Format("SQLite.{0}.TheBallDataContext", semanticDomain);
        }

        public static Type GetTarget_DataContextType(string dataContextFullTypeName)
        {
            //Type dataContextType = Type.GetType(dataContextFullTypeName);
            Type dataContextType = ReflectionSupport.GetSQLiteDataContextType(dataContextFullTypeName);
            return dataContextType;
        }

        public static string GetTarget_DatabaseAttachOrCreateMethodName()
        {
            return "CreateOrAttachToExistingDB";
        }

    }
}