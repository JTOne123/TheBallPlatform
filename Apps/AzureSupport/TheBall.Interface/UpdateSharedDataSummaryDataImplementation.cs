using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TheBall.CORE;
using TheBall.CORE.Storage;
using TheBall.Interface.INT;

namespace TheBall.Interface
{
    public class UpdateSharedDataSummaryDataImplementation
    {
        private static string ShareInfoDirectory = BlobStorage.CombinePath("TheBall.Interface", "ShareInfo");


        public static bool GetTarget_IsCompleteUpdate(CollaborationPartner partner)
        {
            return partner?.PartnerID == null;
        }

        public static async Task<IContainerOwner[]> GetTarget_CollaborationPartnersAsync(CollaborationPartner partner, bool isCompleteUpdate)
        {
            if (!isCompleteUpdate)
                return new IContainerOwner[] { new VirtualOwner(partner.PartnerType, partner.PartnerID, true) };

            var accountShareDir = BlobStorage.CombinePath(ShareInfoDirectory, "acc");
            var accountFolders = await BlobStorage.GetOwnerFoldersA(accountShareDir);

            var groupShareDir = BlobStorage.CombinePath(ShareInfoDirectory, "grp");
            var groupFolders = await BlobStorage.GetOwnerFoldersA(groupShareDir);

            var accountOwners = accountFolders.Select(folder => new VirtualOwner("acc", folder.FolderName, true)).ToArray();
            var groupOwners = groupFolders.Select(folder => new VirtualOwner("grp", folder.FolderName, true)).ToArray();

            var collabPartners = accountOwners.Concat(groupOwners).ToArray();
            return collabPartners;
        }


        /*
         *             var collabPartnerSummaryPath = BlobStorage.CombinePath("TheBall.Interface", "ShareInfo",
                "CollaborationPartnerSummary.json");

         */

        private static bool isMetadata(string fileName)
        {
            return fileName.StartsWith("_") && fileName.EndsWith(".json");
        }

        public static async Task<Tuple<IContainerOwner, bool>> updatePartnerSummary(IContainerOwner partner, string summariesFolder)
        {
            ShareInfoSummary summaryObject = new ShareInfoSummary();

            var partnerShareRootFolder = BlobStorage.CombinePath(ShareInfoDirectory, partner.ContainerName,
                partner.LocationPrefix);
            var partnerSharedBlobs = await BlobStorage.GetOwnerBlobsA(partnerShareRootFolder);
            var sharedWithMeBlobs = partnerSharedBlobs.Where(blob => isMetadata(blob.FileName) == false).ToArray();

            var shareInfoStripLength =
                BlobStorage.GetOwnerContentLocation(InformationContext.CurrentOwner, ShareInfoDirectory).Length + 1;

            summaryObject.SharedForMe = sharedWithMeBlobs.Select(blob => 
                new ShareInfo
                {
                    Length = blob.Length,
                    ContentMD5 = blob.ContentMD5,
                    ItemName = blob.Name.Substring(shareInfoStripLength),
                    Modified = blob.LastModified
                }).ToArray();

            // TODO: add shared by me
            summaryObject.SharedByMe = new ShareInfo[0];

            bool hadData = partnerSharedBlobs.Length > 0;
            var summaryFileName = BlobStorage.CombinePath(summariesFolder, getSummaryFileName(partner.ContainerName, partner.LocationPrefix));
            await BlobStorage.StoreBlobJsonContentA(summaryFileName, summaryObject);
            return new Tuple<IContainerOwner, bool>(partner, hadData);
        }

        public static async Task<Tuple<IContainerOwner, bool>[]> ExecuteMethod_UpdatePartnerSummariesAsync(IContainerOwner[] collaborationPartners, bool isCompleteUpdate)
        {
            var summariesFolder = BlobStorage.CombinePath(ShareInfoDirectory, "Summaries");

            var updateTasks = collaborationPartners.Select(partner => updatePartnerSummary(partner, summariesFolder)).ToArray();
            await Task.WhenAll(updateTasks);
            var result = updateTasks.Select(task => task.Result).ToArray();

            if (isCompleteUpdate)
            {
                var existingSummaryBlobs = await BlobStorage.GetOwnerBlobsA(summariesFolder);
                var existingDataSummaryNames =
                    result.Where(item => item.Item2)
                        .Select(
                            item => getSummaryFileName(item.Item1.ContainerName, item.Item1.LocationPrefix)).ToArray();
                var blobsToDelete =
                    existingSummaryBlobs.Where(blob => existingDataSummaryNames.Contains(blob.FileName) == false)
                        .ToArray();
                var deleteTasks = blobsToDelete.Select(blob => BlobStorage.DeleteBlobA(blob.Name)).ToArray();
                await Task.WhenAll(deleteTasks);
            }
            return result;
        }

        public static async Task ExecuteMethod_UpdateCompleteShareSummaryAsync(Tuple<IContainerOwner, bool>[] updatePartnerSummariesOutput, bool isCompleteUpdate)
        {
            var shareStatusData = updatePartnerSummariesOutput;
            var summaryDataPath = BlobStorage.CombinePath(ShareInfoDirectory, "CollaborationPartnerSummary.json");
            CollaborationPartnerSummary summaryObject;
            if (isCompleteUpdate)
            {
                summaryObject = new CollaborationPartnerSummary();
                summaryObject.Partners = shareStatusData
                    .Where(item => item.Item2).Select(item => getPartnerFromOwner(item.Item1))
                    .ToArray();
            }
            else
            {
                summaryObject = await BlobStorage.GetBlobJsonContentA<CollaborationPartnerSummary>(summaryDataPath) ??
                                new CollaborationPartnerSummary
                                {
                                    Partners = new CollaborationPartner[0]
                                };
                var partnersWithData = shareStatusData.Where(item => item.Item2).Select(item => getPartnerFromOwner(item.Item1)).Select(partnerToTuple).ToArray();
                var partnersWithoutData = shareStatusData.Where(item => item.Item2 == false).Select(item => getPartnerFromOwner(item.Item1)).Select(partnerToTuple).ToArray();
                var existingPartners = summaryObject.Partners.Select(partnerToTuple);
                var setPartners =
                    existingPartners.Union(partnersWithData)
                        .Except(partnersWithoutData)
                        .Select(tupleToPartner)
                        .OrderBy(partner => partner.PartnerType + "_" + partner.PartnerID)
                        .ToArray();
                summaryObject.Partners = setPartners;
            }
            await BlobStorage.StoreBlobJsonContentA(summaryDataPath, summaryObject);
        }

        private static string getSummaryFileName(string partnerType, string partnerID)
        {
            return $"{partnerType}_{partnerID}.json";
        }

        private static Tuple<string, string> partnerToTuple(CollaborationPartner partner)
        {
            return new Tuple<string, string>(partner.PartnerType, partner.PartnerID);
        }

        private static CollaborationPartner tupleToPartner(Tuple<string, string> tuple)
        {
            return new CollaborationPartner
            {
                PartnerType = tuple.Item1,
                PartnerID = tuple.Item2
            };
        }

        private static CollaborationPartner getPartnerFromOwner(IContainerOwner owner)
        {
            return new CollaborationPartner
                {
                    PartnerType = owner.ContainerName,
                    PartnerID = owner.LocationPrefix
                };
        }

    }
}