﻿using System;
using System.Diagnostics;
using System.Linq;
using AaltoGlobalImpact.OIP;
using TheBall;

namespace TheBallTool
{
    public static class DataPatcher
    {
        public static void SetAllInvitedViewerMembersAsFullCollaborators()
        {
            var accountIDs = TBRAccountRoot.GetAllAccountIDs();
            foreach(var acctID in accountIDs)
            {
                TBRAccountRoot accountRoot = TBRAccountRoot.RetrieveFromDefaultLocation(acctID);
                TBAccount account = accountRoot.Account;
                foreach(var grpRole in account.GroupRoleCollection.CollectionContent)
                {
                    if (TBCollaboratorRole.IsRoleStatusValidMember(grpRole.RoleStatus) == false)
                        grpRole.RoleStatus = TBCollaboratorRole.RoleStatusMemberValue;
                    if (grpRole.GroupRole == TBCollaboratorRole.ViewerRoleValue)
                        grpRole.GroupRole = TBCollaboratorRole.CollaboratorRoleValue;
                }
                account.StoreAndPropagate();
            }

        }

        public static void UpdateReferenceInformationsInAllAcountsAndGroups()
        {
            var ownerLocations = GetAllOwnerLocations();
            int totalCount = ownerLocations.Length;
            int currIX = 0;
            foreach(var ownerLocation in ownerLocations)
            {
                Console.WriteLine("Updating number " + (++currIX) + " out of " + totalCount);
                VirtualOwner owner = VirtualOwner.FigureOwner(ownerLocation);
                var informationObjects = StorageSupport.CurrActiveContainer.
                    GetInformationObjects(ownerLocation,
                                          iObj =>
                                          iObj is Blog ||
                                          iObj is Activity ||
                                          iObj is AddressAndLocation);
                foreach (var iObj in informationObjects)
                {
                    try
                    {
                        StorageSupport.StoreInformationMasterFirst(iObj, owner);
                        InformationContext.ProcessAndClearCurrent();
                        InformationContext.Current.InitializeCloudStorageAccess(Properties.Settings.Default.CurrentActiveContainerName);
                    } catch(Exception ex)
                    {
                        bool letThrow = false;
                        if (letThrow)
                            throw;
                    }
                }

            }
        }

        public static void EnsureAndRefreshMasterCollections()
        {
            var accountIDs = TBRAccountRoot.GetAllAccountIDs();
            foreach (string accountID in accountIDs)
            {
                string acctLocation = "acc/" + accountID + "/";
                VirtualOwner owner = VirtualOwner.FigureOwner(acctLocation);
                owner.EnsureMasterCollections();
                owner.RefreshMasterCollections();
            }
            var groupIDs = TBRGroupRoot.GetAllGroupIDs();
            foreach (string groupID in groupIDs)
            {
                string grpLocation = "grp/" + groupID + "/";
                VirtualOwner owner = VirtualOwner.FigureOwner(grpLocation);
                owner.EnsureMasterCollections();
                owner.RefreshMasterCollections();
            }
        }

        public static string[] GetAllOwnerLocations()
        {
            var accountIDs = TBRAccountRoot.GetAllAccountIDs();
            var accountLocs = accountIDs.Select(accID => "acc/" + accID + "/");
            var groupLocs = GetAllGroupLocations();
            return accountLocs.Union(groupLocs).ToArray();
        }

        public static string[] GetAllGroupLocations()
        {
            var groupIDs = TBRGroupRoot.GetAllGroupIDs();
            var groupLocs = groupIDs.Select(grpID => "grp/" + grpID + "/");
            return groupLocs.ToArray();
        }

        public static string[] GetAllAccountLocations()
        {
            var accountIDs = TBRAccountRoot.GetAllAccountIDs();
            var accountLocs = accountIDs.Select(accID => "acc/" + accID + "/");
            return accountLocs.ToArray();
        }

        public static void ReconnectGroupsMastersAndCollections()
        {
            var groupLocs = GetAllGroupLocations();
            foreach(var grpLoc in groupLocs)
                ReconnectMastersAndCollections(grpLoc);
        }

        public static void ReconnectAccountsMastersAndCollections()
        {
            var acctLocs = GetAllAccountLocations();
            foreach (var acctLoc in acctLocs)
                ReconnectMastersAndCollections(acctLoc);
        }

        private static void ReconnectMastersAndCollections(string ownerLocation)
        {
            //string myLocalAccountID = "0c560c69-c3a7-4363-b125-ba1660d21cf4";
            //string acctLoc = "acc/" + myLocalAccountID + "/";

            VirtualOwner me = VirtualOwner.FigureOwner(ownerLocation);

            var informationObjects = StorageSupport.CurrActiveContainer.GetInformationObjects(ownerLocation,
                                                                                              nonMaster =>
                                                                                              nonMaster.
                                                                                                  IsIndependentMaster ==
                                                                                              false && (nonMaster is TBEmailValidation == false)).ToArray();
            foreach (var iObj in informationObjects)
            {
                try
                {
                    iObj.ReconnectMastersAndCollections(true);
                } catch(Exception ex)
                {
                    bool ignoreException = false;
                    if (ignoreException == false)
                        throw;
                }
            }
        }

        private static void DoCustomCleanup(string groupLoc)
        {
            var defaultBlogToDelete = StorageSupport.CurrActiveContainer.
                GetInformationObjects(groupLoc,
                                      item => item is Blog && item.RelativeLocation.EndsWith("/default")).ToArray();
            foreach (Blog blog in defaultBlogToDelete)
            {
                StorageSupport.DeleteInformationObject(blog);
            }
            InformationContext.ProcessAndClearCurrent();
            InformationContext.Current.InitializeCloudStorageAccess(Properties.Settings.Default.CurrentActiveContainerName);
        }

        private static void SyncWwwPublicFromDefaultGroup()
        {
            string publicSite = "demowww.aaltoglobalimpact.org";
            string[] folderList = new[] {"bootstrap-default", "oip-additions", "www-public"};
            foreach(string folder in folderList)
            {
                string sourceFolder = folder;
                //if (sourceFolder == "www-public")
                //    sourceFolder = "oip-public";
                var operationRequest = RenderWebSupport.SyncTemplatesToSite(StorageSupport.CurrActiveContainer.Name, "grp/9798daca-afc4-4046-a99b-d0d88bb364e0/wwwsite/" + sourceFolder,
                                                     publicSite.Replace('.', '-'), folder, true, false);
                QueueSupport.PutToOperationQueue(operationRequest);
            }
        }

        private static void RefreshAllAccounts()
        {
            var accountIDs = TBRAccountRoot.GetAllAccountIDs();
            foreach (var accountID in accountIDs)
            {
                var accountRoot = TBRAccountRoot.RetrieveFromDefaultLocation(accountID);
                accountRoot.Account.StoreAndPropagate();
            }

        }

        public static bool DoPatching()
        {
            return false;
            Debugger.Break();
            bool skip = false;
            if(skip == false)
                throw new NotSupportedException("Skip this with debugger");
            //EnsureAndRefreshMasterCollections();
            //ReconnectAccountsMastersAndCollections();
            //ReconnectGroupsMastersAndCollections();
            SyncWwwPublicFromDefaultGroup();
            return true;
        }
    }
}