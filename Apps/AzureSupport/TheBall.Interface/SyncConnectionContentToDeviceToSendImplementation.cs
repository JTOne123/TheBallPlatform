using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using TheBall.Core;
using TheBall.Core.INT;

namespace TheBall.Interface
{
    public class SyncConnectionContentToDeviceToSendImplementation
    {
        public static string GetTarget_PackageContentListingProcessID(Connection connection)
        {
            return connection.ProcessIDToListPackageContents;
        }

        public static async Task ExecuteMethod_ExecuteContentListingProcessAsync(string packageContentListingProcessId)
        {
            await ExecuteProcess.ExecuteAsync(new ExecuteProcessParameters {ProcessID = packageContentListingProcessId});
        }

        public static async Task<Process> GetTarget_PackageContentListingProcessAsync(string packageContentListingProcessId)
        {
            return await ObjectStorage.RetrieveFromOwnerContentA<Process>(InformationContext.CurrentOwner, packageContentListingProcessId);
        }

        public static ContentItemLocationWithMD5[] GetTarget_ContentListingResult(Process packageContentListingProcess)
        {
            List<ContentItemLocationWithMD5> contentList = new List<ContentItemLocationWithMD5>();
            foreach (var processItem in packageContentListingProcess.ProcessItems)
            {
                var contentLocation = processItem.Outputs.First(item => item.ItemFullType == "ContentLocation").ItemValue;
                var contentMD5 = processItem.Outputs.First(item => item.ItemFullType == "ContentMD5").ItemValue;
                contentList.Add(new ContentItemLocationWithMD5
                    {
                        ContentLocation = contentLocation,
                        ContentMD5 = contentMD5
                    });
            }
            return contentList.ToArray();
        }

        public static string GetTarget_SyncTargetRootFolder(Connection connection)
        {
            if(string.IsNullOrEmpty(connection.DeviceID))
                throw new InvalidDataException("Connection requires device ID");
            var ownerTargetDeviceLocation = string.Format("TheBall.Core/AuthenticatedAsActiveDevice/{0}/",
                                                          connection.DeviceID);
            return ownerTargetDeviceLocation;
        }

        public static void ExecuteMethod_CopyContentsToSyncRoot(ContentItemLocationWithMD5[] contentListingResult, string syncTargetRootFolder)
        {
            SyncSupport.SynchronizeSourceListToTargetFolder(SyncSupport.RelativeRootFolderValue, contentListingResult, syncTargetRootFolder);
        }
    }
}