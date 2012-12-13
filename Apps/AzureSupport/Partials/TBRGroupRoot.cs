﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.StorageClient;
using TheBall;

namespace AaltoGlobalImpact.OIP
{
    partial class TBRGroupRoot
    {
        public static string[] GetAllGroupIDs()
        {
            string blobPath = "AaltoGlobalImpact.OIP/TBRGroupRoot/";
            string searchPath = StorageSupport.CurrActiveContainer.Name + "/" + blobPath;
            int substringLen = blobPath.Length;
            var blobList = StorageSupport.CurrBlobClient.ListBlobsWithPrefix(searchPath).OfType<CloudBlob>();
            return blobList.Select(blob => blob.Name.Substring(substringLen)).ToArray();
        }

        public static TBRGroupRoot CreateNewWithGroup()
        {
            TBRGroupRoot groupRoot = TBRGroupRoot.CreateDefault();
            groupRoot.Group.ID = groupRoot.ID;
            return groupRoot;
        }

        public static TBRGroupRoot CreateLegacyNewWithGroup(string legacyID)
        {
            TBRGroupRoot groupRoot = TBRGroupRoot.CreateDefault();
            groupRoot.ID = legacyID;
            groupRoot.UpdateRelativeLocationFromID();
            groupRoot.Group.ID = groupRoot.ID;
            return groupRoot;
        }

        public static void DeleteEntireGroup(string groupID)
        {
            TBRGroupRoot groupToDelete = TBRGroupRoot.RetrieveFromDefaultLocation(groupID);
            throw new NotImplementedException("Call remove group membership for each member, then delete to recycle bin");
            foreach(var member in groupToDelete.Group.Roles.CollectionContent)
            {
                string emailRootID = TBREmailRoot.GetIDFromEmailAddress(member.Email.EmailAddress);
                TBREmailRoot emailRoot = TBREmailRoot.RetrieveFromDefaultLocation(emailRootID);
                emailRoot.Account.GroupRoleCollection.CollectionContent.RemoveAll(
                    candidate => candidate.GroupID == groupToDelete.Group.ID);
                emailRoot.Account.StoreAccountToRoot();
            }
            StorageSupport.DeleteInformationObject(groupToDelete);
            //WorkerSupport.DeleteEntireOwner(groupToDelete.Group);
            OperationRequest operationRequest = new OperationRequest
                                                    {
                                                        DeleteEntireOwner = DeleteEntireOwnerOperation.CreateDefault()
                                                    };
            operationRequest.DeleteEntireOwner.ContainerName = groupToDelete.Group.ContainerName;
            operationRequest.DeleteEntireOwner.LocationPrefix = groupToDelete.Group.LocationPrefix;
            //QueueSupport.PutToOperationQueue(operationRequest);
            InformationContext.Current.AddOperationRequestToFinalizingQueue(operationRequest);
        }
    }
}