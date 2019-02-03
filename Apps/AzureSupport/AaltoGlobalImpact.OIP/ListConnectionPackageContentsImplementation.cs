using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob;
using TheBall;
using TheBall.CORE;
using TheBall.CORE.Storage;

namespace AaltoGlobalImpact.OIP
{
    public class ListConnectionPackageContentsImplementation
    {
        public static string GetTarget_ConnectionID(Process process)
        {
            string connectionID = process.InitialArguments.First(arg => arg.ItemFullType == "ConnectionID").ItemValue;
            return connectionID;
        }

        public static PickCategorizedContentToConnectionParameters CallPickCategorizedContentConnection_GetParameters(string connectionId)
        {
            return new PickCategorizedContentToConnectionParameters {ConnectionID = connectionId};
        }

        public static string[] CallPickCategorizedContentConnection_GetOutput(PickCategorizedContentToConnectionReturnValue operationReturnValue, string connectionId)
        {
            return operationReturnValue.ContentLocations;
        }

        public static async Task ExecuteMethod_SetContentsAsProcessOutputAsync(Process process, string[] callPickCategorizedContentConnectionOutput)
        {
/*
 *                 var contentLocation = processItem.Outputs.First(item => item.ItemFullType == "ContentLocation").ItemValue;
                var contentMD5 = processItem.Outputs.First(item => item.ItemFullType == "ContentMD5").ItemValue;
*/
            process.ProcessItems.Clear();
            foreach (string contentLocation in callPickCategorizedContentConnectionOutput)
            {
                bool isMediaContentType = InformationObjectSupport.IsContentGivenType(contentLocation, typeof (MediaContent).FullName);
                if (isMediaContentType)
                {
                    string extension = Path.GetExtension(contentLocation);
                    string fullLocationWithoutExtension = contentLocation.Substring(0, contentLocation.Length - extension.Length);
                    var mediaContentBlobs = await BlobStorage.GetBlobItemsA(InformationContext.CurrentOwner, fullLocationWithoutExtension);
                    foreach (var mediaContentBlob in mediaContentBlobs)
                    {
                        SemanticInformationItem semanticItemForLocation = new SemanticInformationItem
                        {
                            ItemFullType = "ContentLocation",
                            ItemValue = mediaContentBlob.Name
                        };
                        SemanticInformationItem semanticItemForMD5 = new SemanticInformationItem
                        {
                            ItemFullType = "ContentMD5",
                            ItemValue = mediaContentBlob.ContentMD5
                        };
                        ProcessItem processItem = new ProcessItem();
                        processItem.Outputs.Add(semanticItemForLocation);
                        processItem.Outputs.Add(semanticItemForMD5);
                        process.ProcessItems.Add(processItem);
                    }
                }
                else
                {
                    var blob = StorageSupport.GetOwnerBlobReference(contentLocation);
                    await blob.FetchAttributesAsync();
                    SemanticInformationItem semanticItemForLocation = new SemanticInformationItem
                        {
                            ItemFullType = "ContentLocation",
                            ItemValue = contentLocation
                        };
                    SemanticInformationItem semanticItemForMD5 = new SemanticInformationItem
                        {
                            ItemFullType = "ContentMD5",
                            ItemValue = blob.Properties.ContentMD5
                        };
                    ProcessItem processItem = new ProcessItem();
                    processItem.Outputs.Add(semanticItemForLocation);
                    processItem.Outputs.Add(semanticItemForMD5);
                    process.ProcessItems.Add(processItem);
                }
            }
        }
    }
}