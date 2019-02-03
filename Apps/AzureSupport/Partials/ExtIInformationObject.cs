﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AaltoGlobalImpact.OIP;
using TheBall;
using TheBall.CORE;
using TheBall.CORE.Storage;

namespace TheBall.CORE
{
    public static partial class OwnerInitializer
    {
        public static async Task InitializeAndConnectMastersAndCollections(this IContainerOwner owner)
        {
            Type myType = typeof(OwnerInitializer);
            var myMethods = myType.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            var initTasks = new List<Task>();
            foreach (var myMethod in myMethods.Where(method => method.Name.StartsWith("DOMAININIT_")))
            {
                var task = (Task) myMethod.Invoke(null, new object[] { owner });
                initTasks.Add(task);
            }
            await Task.WhenAll(initTasks);
            await owner.ReconnectMastersAndCollectionsForOwner();
        }
    }

    public static partial class ExtIContainerOwner
    {
        public static bool IsEqualOwner(this IContainerOwner owner, IContainerOwner comparingToOwner)
        {
            return owner.ContainerName == comparingToOwner.ContainerName && owner.LocationPrefix == comparingToOwner.LocationPrefix;
        }

        public static string ToFolderName(this IContainerOwner owner)
        {
            return owner.ToParseableString();
        }

        public static string ToParseableString(this IContainerOwner owner)
        {
            return owner.ContainerName + "/" + owner.LocationPrefix;
        }

        public static string PrefixWithOwnerLocation(this IContainerOwner owner, string location)
        {
            string ownerLocationPrefix = owner.ContainerName + "/" + owner.LocationPrefix + "/";
            if (location.StartsWith(ownerLocationPrefix))
                return location;
            return ownerLocationPrefix + location;
        }

        public static bool IsAccountContainer(this IContainerOwner owner)
        {
            return owner.ContainerName == "acc" && owner.LocationPrefix.Length == StorageSupport.GuidLength;
        }

        public static bool IsGroupContainer(this IContainerOwner owner)
        {
            return (owner.ContainerName == "grp" || owner.ContainerName == "dev") && owner.LocationPrefix.Length == StorageSupport.GuidLength;
        }

        public static bool IsSystemOwner(this IContainerOwner owner)
        {
            return owner.IsSameOwner(SystemSupport.SystemOwner);
        }

        public static async Task ReconnectMastersAndCollectionsForOwner(this IContainerOwner owner)
        {
            //string myLocalAccountID = "0c560c69-c3a7-4363-b125-ba1660d21cf4";
            //string acctLoc = "acc/" + myLocalAccountID + "/";

            string ownerLocation = owner.ContainerName + "/" + owner.LocationPrefix + "/";

            /*
            var informationObjects = BlobStorage.
                StorageSupport.CurrActiveContainer.GetInformationObjects(ownerLocation, name => name.Contains("TheBall.CORE/RequestResourceUsage") == false, 
                                                                                              nonMaster =>
                                                                                              nonMaster.
                                                                                                  IsIndependentMaster ==
                                                                                              false && (nonMaster is TBEmailValidation == false)).ToArray();
                                                                                              */
            throw new NotImplementedException();
            IInformationObject[] informationObjects = null;
            foreach (var iObj in informationObjects)
            {
                try
                {
                    await iObj.ReconnectMastersAndCollectionsAsync(true);
                }
                catch (Exception ex)
                {
                    bool ignoreException = false;
                    if (ignoreException == false)
                        throw;
                }
            }
        }

    }
}