﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Web;
using PersonalWeb.Diosphere;
using TheBall.Admin;
using TheBall.CORE;
using AaltoGlobalImpact.OIP;
using AzureSupport;
using TheBall.Interface;

namespace TheBall
{
    public static class ModifyInformationSupport
    {
        public static object ExecuteOwnerWebPOST(IContainerOwner containerOwner, NameValueCollection form, HttpFileCollection fileContent)
        {
            bool reloadPageAfter = form["NORELOAD"] == null;

            bool isCancelButton = form["btnCancel"] != null;
            if (isCancelButton)
                return reloadPageAfter;

            string operationName = form["ExecuteOperation"];
            if (operationName != null)
            {
                var operationResult = executeOperationWithFormValues(containerOwner, operationName, form, fileContent);
                if(operationResult != null)
                    return operationResult;
                return reloadPageAfter;
            }

            string adminOperationName = form["ExecuteAdminOperation"];
            if (adminOperationName != null)
            {
                var adminResult = executeAdminOperationWithFormValues(containerOwner, adminOperationName, form, fileContent);
                if(adminResult != null)
                    return adminResult;
                return reloadPageAfter;
            }

            string contentSourceInfo = form["ContentSourceInfo"];
            var rootSourceAction = form["RootSourceAction"];
            if (rootSourceAction != null && rootSourceAction != "Save")
                return reloadPageAfter;
            var filterFields = new string[] { "ContentSourceInfo", "RootSourceAction", "NORELOAD" };
            string[] contentSourceInfos = contentSourceInfo.Split(',');
            var filteredForm = filterForm(form, filterFields);
            foreach (string sourceInfo in contentSourceInfos)
            {
                string relativeLocation;
                string oldETag;
                retrieveDataSourceInfo(sourceInfo, out relativeLocation, out oldETag);
                VirtualOwner verifyOwner = VirtualOwner.FigureOwner(relativeLocation);
                if (verifyOwner.IsSameOwner(containerOwner) == false)
                    throw new SecurityException("Mismatch in ownership of data submission");
                IInformationObject rootObject = StorageSupport.RetrieveInformation(relativeLocation, oldETag,
                                                                                   containerOwner);
                if (oldETag != rootObject.ETag)
                {
                    throw new InvalidDataException("Information under editing was modified during display and save");
                }
                // TODO: Proprely validate against only the object under the editing was changed (or its tree below)

                SetObjectTreeValues.Execute(new SetObjectTreeValuesParameters
                    {
                        RootObject = rootObject,
                        HttpFormData = filteredForm,
                        HttpFileData = fileContent
                    });
            }
            return reloadPageAfter;
        }

        private static object executeAdminOperationWithFormValues(IContainerOwner containerOwner, string operationName, NameValueCollection form, HttpFileCollection fileContent)
        {
            var filterFields = new string[] { "ExecuteOperation", "ObjectDomainName", "ObjectName", "ObjectID", "NORELOAD" };
            string adminGroupID = InstanceConfiguration.AdminGroupID;
            if(containerOwner.LocationPrefix != adminGroupID)
                throw new SecurityException("Only Admin Group can execute these operations");
            switch (operationName)
            {
                case "FixAllGroupsMastersAndCollections":
                    {
                        var allGroupIDs = TBRGroupRoot.GetAllGroupIDs();
                        foreach (var groupID in allGroupIDs)
                        {
                            Debug.WriteLine("Fixing group: " + groupID);
                            FixGroupMastersAndCollectionsParameters parameters = new FixGroupMastersAndCollectionsParameters
                                ()
                            {
                                GroupID = groupID
                            };
                            FixGroupMastersAndCollections.Execute(parameters);
                        }
                        break;
                    }
                case "FixGroupMastersAndCollections":
                    {
                        FixGroupMastersAndCollectionsParameters parameters = new FixGroupMastersAndCollectionsParameters
                            ()
                            {
                                GroupID = form["GroupID"]
                            };
                        FixGroupMastersAndCollections.Execute(parameters);
                        break;
                    }
                default:
                    throw new NotSupportedException("Operation not (yet) supported: " + operationName);
            }
            return null;
        }

        private static void requireGroup(IContainerOwner owner, string errorMessage)
        {
            if(owner.IsGroupContainer() == false)
                throw new InvalidOperationException(errorMessage);
        }

        private static object executeOperationWithFormValues(IContainerOwner containerOwner, string operationName, NameValueCollection form, HttpFileCollection fileContent)
        {
            var filterFields = new string[] {"ExecuteOperation", "ObjectDomainName", "ObjectName", "ObjectID", "NORELOAD"};
            switch (operationName)
            {
                case "FetchURLAsGroupContent":
                {
                    if(containerOwner.IsGroupContainer() == false)
                        throw new NotSupportedException("CreateOrUpdateCustomUI is only supported for groups");
                    var groupID = containerOwner.LocationPrefix;
                    FetchURLAsGroupContent.Execute(new FetchURLAsGroupContentParameters
                    {
                        DataURL = form["DataURL"],
                        FileName = form["FileName"],
                        GroupID = groupID
                    });
                    break;
                }
                case "PersonalWeb.Diosphere.SaveRoomData":
                {
                    PersonalWeb.Diosphere.SaveRoomData.Execute(new SaveRoomDataParameters
                    {
                        JSONData = form["JSONData"],
                        RoomID = form["RoomID"]
                    });
                    break;
                }

                case "SetGroupAsDefaultForAccount":
                {
                    SetGroupAsDefaultForAccountParameters parameters = new SetGroupAsDefaultForAccountParameters
                    {
                        GroupID = form["GroupID"]
                    };
                    SetGroupAsDefaultForAccount.Execute(parameters);
                    break;
                }
                case "ClearDefaultGroupFromAccount":
                {
                    ClearDefaultGroupFromAccount.Execute();
                    break;
                }

                case "PublishToConnection":
                    {
                        var parameters = new PublishCollaborationContentOverConnectionParameters
                            {
                                ConnectionID = form["ConnectionID"]
                            };
                        PublishCollaborationContentOverConnection.Execute(parameters);
                        break;
                    }
                case "FinalizeConnectionAfterGroupAuthorization":
                    {
                        var parameters = new FinalizeConnectionAfterGroupAuthorizationParameters
                            {
                                ConnectionID = form["ConnectionID"]
                            };
                        FinalizeConnectionAfterGroupAuthorization.Execute(parameters);
                        break;
                    }
                case "DeleteConnection":
                    {
                        var parameters = new DeleteConnectionWithStructuresParameters
                            {
                                ConnectionID = form["ConnectionID"],
                                IsLaunchedByRemoteDelete = false
                            };
                        DeleteConnectionWithStructures.Execute(parameters);
                        break;
                    }
                case "SynchronizeConnectionCategories":
                    {
                        var parameters = new SynchronizeConnectionCategoriesParameters
                        {
                            ConnectionID = form["ConnectionID"]
                        };
                        SynchronizeConnectionCategories.Execute(parameters);
                        break;
                    }

                case "UpdateConnectionThisSideCategories":
                    {
                        ExecuteConnectionProcess.Execute(new ExecuteConnectionProcessParameters
                            {
                                ConnectionID = form["ConnectionID"],
                                ConnectionProcessToExecute = "UpdateConnectionThisSideCategories"
                            });
                        break;
                    }
                case "InitiateIntegrationConnection":
                    {
                        InitiateIntegrationConnectionParameters parameters = new InitiateIntegrationConnectionParameters
                            {
                                Description = form["Description"],
                                TargetBallHostName = form["TargetBallHostName"],
                                TargetGroupID = form["TargetGroupID"]
                            };
                        InitiateIntegrationConnection.Execute(parameters);
                        break;
                    }
                case "DeleteCustomUI":
                    {
                        if (containerOwner.IsGroupContainer() == false)
                            throw new NotSupportedException("CreateOrUpdateCustomUI is only supported for groups");
                        DeleteCustomUIParameters parameters = new DeleteCustomUIParameters
                            {
                                CustomUIName = form["CustomUIName"],
                                Owner = containerOwner
                            };
                        DeleteCustomUI.Execute(parameters);
                        break;
                    }
                case "CreateOrUpdateCustomUI":
                    {
                        if(containerOwner.IsGroupContainer() == false)
                            throw new NotSupportedException("CreateOrUpdateCustomUI is only supported for groups");
                        var customUIContent = fileContent["CustomUIContents"];
                        if(customUIContent == null)
                            throw new ArgumentException("CustomUIContent field is required to contain the zip contents of custom UI.");
                        CreateOrUpdateCustomUIParameters parameters = new CreateOrUpdateCustomUIParameters
                            {
                                CustomUIName = form["CustomUIName"],
                                Owner = containerOwner,
                                ZipArchiveStream = customUIContent.InputStream,
                            };
                        CreateOrUpdateCustomUI.Execute(parameters);
                        break;
                    }
                case "AddCategories":
                    {
                        string categoryNames = form["CategoryList"];
                        string[] categoryList = categoryNames.Split(new string[] { Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries); 
                        foreach (string categoryName in categoryList)
                        {
                            NameValueCollection values = new NameValueCollection();
                            values.Add("Title", categoryName);
                            CreateSpecifiedInformationObjectWithValuesParameters parameters = new CreateSpecifiedInformationObjectWithValuesParameters
                                {
                                    ObjectDomainName = "AaltoGlobalImpact.OIP",
                                    ObjectName = "Category",
                                    HttpFormData = values,
                                    Owner = containerOwner
                                };
                            CreateSpecifiedInformationObjectWithValues.Execute(parameters);
                        }
                        break;
                    }
                case "PublishGroupToWww":
                    {
                        PublishGroupToWwwParameters parameters = new PublishGroupToWwwParameters
                            {
                                Owner = containerOwner
                            };
                        PublishGroupToWww.Execute(parameters);
                        break;
                    }
                case "UpdateUsageMonitoringItems":
                    {
                        UpdateUsageMonitoringItemsParameters parameters = new UpdateUsageMonitoringItemsParameters
                            {
                                MonitoringIntervalInMinutes = 5,
                                MonitoringItemTimeSpanInMinutes = 60,
                                Owner = containerOwner
                            };
                        UpdateUsageMonitoringItems.Execute(parameters);
                        UpdateUsageMonitoringSummariesParameters summaryParameters = new UpdateUsageMonitoringSummariesParameters
                            {
                                AmountOfDays = 31,
                                Owner = containerOwner
                            };
                        UpdateUsageMonitoringSummaries.Execute(summaryParameters);
                        break;
                    }
                case "ProcessAllResourceUsagesToOwnerCollections":
                    {
                        ProcessAllResourceUsagesToOwnerCollectionsParameters parameters = new ProcessAllResourceUsagesToOwnerCollectionsParameters
                            {
                                ProcessBatchSize = 500
                            };
                        ProcessAllResourceUsagesToOwnerCollections.Execute(parameters);
                        break;
                    }
                case "CreateInformationOutput":
                    {
                        CreateInformationOutputParameters parameters = new CreateInformationOutputParameters
                            {
                                Owner = containerOwner,
                                AuthenticatedDeviceID = form["AuthenticatedDeviceID"],
                                DestinationContentName = form["DestinationContentName"],
                                DestinationURL = form["DestinationURL"],
                                LocalContentURL = form["LocalContentURL"],
                                OutputDescription = form["OutputDescription"]
                            };
                        var createdInformationOutput = CreateInformationOutput.Execute(parameters);
                        var owningAccount = containerOwner as TBAccount;
                        TBCollaboratingGroup owningGroup = null;
                        if (owningAccount == null)
                        {
                            TBRGroupRoot groupRoot =
                                TBRGroupRoot.RetrieveFromDefaultLocation(containerOwner.LocationPrefix);
                            owningGroup = groupRoot.Group;
                        }
                        CreateAndSendEmailValidationForInformationOutputConfirmationParameters emailParameters = new CreateAndSendEmailValidationForInformationOutputConfirmationParameters
                        {
                            OwningAccount = owningAccount,
                            OwningGroup = owningGroup,
                            InformationOutput = createdInformationOutput.InformationOutput
                        };
                        CreateAndSendEmailValidationForInformationOutputConfirmation.Execute(emailParameters);
                        break;
                    }
                case "DeleteInformationOutput":
                    {
                        DeleteInformationOutputParameters parameters = new DeleteInformationOutputParameters
                            {
                                Owner = containerOwner,
                                InformationOutputID = form["InformationOutputID"]
                            };
                        DeleteInformationOutput.Execute(parameters);
                        break;
                    }
                
                case "PushToInformationOutput":
                    {
                        PushToInformationOutputParameters parameters = new PushToInformationOutputParameters
                            {
                                Owner = containerOwner,
                                InformationOutputID = form["InformationOutputID"]
                            };
                        PushToInformationOutput.Execute(parameters);
                        break;
                    }
                case "DeleteInformationInput":
                    {
                        DeleteInformationInputParameters parameters = new DeleteInformationInputParameters
                            {
                                Owner = containerOwner,
                                InformationInputID = form["InformationInputID"]
                            };
                        DeleteInformationInput.Execute(parameters);
                        break;
                    }
                case "FetchInputInformation":
                    {
                        FetchInputInformationParameters parameters = new FetchInputInformationParameters
                            {
                                Owner = containerOwner,
                                InformationInputID = form["InformationInputID"],
                                QueryParameters = form["QueryParameters"]
                            };
                        FetchInputInformation.Execute(parameters);
                        break;
                    }
                case "DeleteDeviceMembership":
                    {
                        DeleteDeviceMembershipParameters parameters = new DeleteDeviceMembershipParameters
                            {
                                Owner = containerOwner,
                                DeviceMembershipID = form["DeviceMembershipID"]
                            };
                        DeleteDeviceMembership.Execute(parameters);
                        break;
                    }
                case "DeleteAuthenticatedAsActiveDevice":
                    {
                        DeleteAuthenticatedAsActiveDeviceParameters parameters = new DeleteAuthenticatedAsActiveDeviceParameters
                            {
                                Owner = containerOwner,
                                AuthenticatedAsActiveDeviceID = form["AuthenticatedAsActiveDeviceID"]
                            };
                        DeleteAuthenticatedAsActiveDevice.Execute(parameters);
                        break;
                    }
                case "PerformNegotiationAndValidateAuthenticationAsActiveDevice":
                    {
                        PerformNegotiationAndValidateAuthenticationAsActiveDeviceParameters parameters = new PerformNegotiationAndValidateAuthenticationAsActiveDeviceParameters
                            {
                                Owner = containerOwner,
                                AuthenticatedAsActiveDeviceID = form["AuthenticatedAsActiveDeviceID"],
                            };
                        PerformNegotiationAndValidateAuthenticationAsActiveDevice.Execute(parameters);
                        break;
                    }
                case "CreateAuthenticatedAsActiveDevice":
                    {
                        requireGroup(containerOwner, "Create as authenticated device only supported by group for now");
                        CreateAuthenticatedAsActiveDeviceParameters parameters = new CreateAuthenticatedAsActiveDeviceParameters
                            {
                                Owner = containerOwner,
                                AuthenticationDeviceDescription = form["AuthenticationDeviceDescription"],
                                // SharedSecret = form["SharedSecret"],
                                TargetBallHostName = form["TargetBallHostName"],
                                TargetGroupID = form["TargetGroupID"]
                            };
                        CreateAuthenticatedAsActiveDevice.Execute(parameters);
                        break;
                    }
                case "RemoveCollaboratorFromGroup":
                    {
                        if (containerOwner.IsGroupContainer() == false)
                            throw new InvalidOperationException("Collaborator removal is only supported in group context");
                        if(!TBCollaboratorRole.HasModeratorRights(InformationContext.Current.CurrentGroupRole))
                            throw new SecurityException("Collaborator removal is only doable by moderators/initiators");
                        string accountID = form["ExecutorAccountID"];
                        RemoveMemberFromGroupParameters parameters = new RemoveMemberFromGroupParameters
                            {
                                AccountID = accountID,
                                GroupID = containerOwner.LocationPrefix
                            };
                        RemoveMemberFromGroup.Execute(parameters);
                        break;
                    }

                case "InviteMemberToGroupAndPlatform":
                {
                    if(containerOwner.IsGroupContainer() == false)
                        throw new InvalidOperationException("Group invitation is only supported in group context");
                    string emailAddress = form["emailAddress"];
                    string groupID = containerOwner.LocationPrefix;
                    InviteNewMemberToPlatformAndGroup.Execute(new InviteNewMemberToPlatformAndGroupParameters
                    {
                        GroupID = groupID,
                        MemberEmailAddress = emailAddress
                    });
                    break;
                }
                case "InviteMemberToGroup":
                    {
                        if (containerOwner.IsGroupContainer() == false)
                            throw new InvalidOperationException("Group invitation is only supported in group context");
                        string emailAddress = form["EmailAddress"];
                        string emailRootID = TBREmailRoot.GetIDFromEmailAddress(emailAddress);
                        TBREmailRoot emailRoot = TBREmailRoot.RetrieveFromDefaultLocation(emailRootID);
                        if(emailRoot == null)
                            throw new NotSupportedException("Email used for group invitation is not yet registered to the system");
                        string groupID = containerOwner.LocationPrefix;
                        InviteMemberToGroup.Execute(new InviteMemberToGroupParameters { GroupID = groupID, MemberEmailAddress = emailAddress });
                        break;
                    }

                case "CreateGroupWithTemplates":
                    {
                        var owningAccount = containerOwner as TBAccount;
                        if(owningAccount == null)
                            throw new NotSupportedException("Creating a group is only supported by account");
                        CreateGroupWithTemplatesParameters parameters = new CreateGroupWithTemplatesParameters
                            {
                                AccountID = owningAccount.ID,
                                GroupName = form["GroupName"],
                                RedirectUrlAfterCreation = form["RedirectUrlAfterCreation"],
                                TemplateNameList = form["TemplateNameList"]
                            };
                        CreateGroupWithTemplates.Execute(parameters);
                        break;
                    }
                case "InitiateAccountMergeFromEmail":
                    {
                        var owningAccount = containerOwner as TBAccount;
                        if(owningAccount == null)
                            throw new NotSupportedException("Email address based account merge is only supported for accounts");
                        InitiateAccountMergeFromEmailParameters parameters = new InitiateAccountMergeFromEmailParameters
                            {
                                CurrentAccountID = owningAccount.ID,
                                RedirectUrlAfterValidation = form["RedirectUrlAfterValidation"],
                                EmailAddress = form["EmailAddress"],
                            };
                        InitiateAccountMergeFromEmail.Execute(parameters);
                        break;
                    }

                case "UnregisterEmailAddress":
                    {
                        var owningAccount = containerOwner as TBAccount;
                        if(owningAccount == null)
                            throw new NotSupportedException("Unregistering email address is only supported for accounts");
                        UnregisterEmailAddressParameters parameters = new UnregisterEmailAddressParameters
                            {
                                AccountID = owningAccount.ID,
                                EmailAddress = form["EmailAddress"],
                            };
                        UnregisterEmailAddress.Execute(parameters);
                        break;
                    }
                case "BeginAccountEmailAddressRegistration":
                    {
                        var owningAccount = containerOwner as TBAccount;
                        if(owningAccount == null)
                            throw new NotSupportedException("Email address registration is only supported for accounts");
                        BeginAccountEmailAddressRegistrationParameters parameters = new BeginAccountEmailAddressRegistrationParameters
                            {
                                AccountID = owningAccount.ID,
                                RedirectUrlAfterValidation = form["RedirectUrlAfterValidation"],
                                EmailAddress = form["EmailAddress"],
                            };
                        BeginAccountEmailAddressRegistration.Execute(parameters);
                        break;
                    }
                case "CreateInformationInput":
                    {
                        CreateInformationInputParameters parameters = new CreateInformationInputParameters
                            {
                                InputDescription = form["InputDescription"],
                                LocationURL = form["LocationURL"],
                                LocalContentName = form["LocalContentName"],
                                AuthenticatedDeviceID = form["AuthenticatedDeviceID"],
                                Owner = containerOwner
                            };
                        var createdInformationInput = CreateInformationInput.Execute(parameters);
                        var owningAccount = containerOwner as TBAccount;
                        TBCollaboratingGroup owningGroup = null;
                        if (owningAccount == null)
                        {
                            TBRGroupRoot groupRoot =
                                TBRGroupRoot.RetrieveFromDefaultLocation(containerOwner.LocationPrefix);
                            owningGroup = groupRoot.Group;
                        }
                        CreateAndSendEmailValidationForInformationInputConfirmationParameters emailParameters = new CreateAndSendEmailValidationForInformationInputConfirmationParameters
                            {
                                OwningAccount = owningAccount,
                                OwningGroup = owningGroup,
                                InformationInput = createdInformationInput.InformationInput,
                            };
                        CreateAndSendEmailValidationForInformationInputConfirmation.Execute(emailParameters);
                        break;
                    }
                case "CreateSpecifiedInformationObjectWithValues":
                    {
                        CreateSpecifiedInformationObjectWithValuesParameters parameters = new CreateSpecifiedInformationObjectWithValuesParameters
                            {
                                Owner = containerOwner,
                                ObjectDomainName = form["ObjectDomainName"],
                                ObjectName = form["ObjectName"],
                                HttpFormData = filterForm(form, filterFields),
                                HttpFileData = fileContent,
                            };
                        var result = CreateSpecifiedInformationObjectWithValues.Execute(parameters);
                        return result.CreatedObjectResult;
                    }
                case "DeleteSpecifiedInformationObject":
                    {
                        DeleteSpecifiedInformationObjectParameters parameters = new DeleteSpecifiedInformationObjectParameters
                            {
                                Owner = containerOwner,
                                ObjectDomainName = form["ObjectDomainName"],
                                ObjectName = form["ObjectName"],
                                ObjectID = form["ObjectID"],
                            };
                        DeleteSpecifiedInformationObject.Execute(parameters);
                        break;
                    }
                default:
                    throw new NotSupportedException("Operation not (yet) supported: " + operationName);
            }
            return null;
        }

        private static NameValueCollection filterForm(NameValueCollection form, params string[] keysToFilter)
        {
            var filteredForm = new NameValueCollection();
            foreach (var key in form.AllKeys)
            {
                if (keysToFilter.Contains(key))
                    continue;
                filteredForm.Add(key, form[key]);
            }
            return filteredForm;
        }

        public static void SetObjectLinks(IInformationObject rootObject, NameValueCollection objectEntries)
        {
            foreach (var objectKey in objectEntries.AllKeys)
            {
                string objectInfo = objectKey.Substring(7); // Substring("Object_".Length);
                int firstIX = objectInfo.IndexOf('_');
                if (firstIX < 0)
                    throw new InvalidDataException("Invalid field data on binary content");
                string containerID = objectInfo.Substring(0, firstIX);
                string containerField = objectInfo.Substring(firstIX + 1);
                string objectIDCommaSeparated = objectEntries[objectKey] ?? "";
                string[] objectIDList = objectIDCommaSeparated.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                rootObject.SetObjectContent(containerID, containerField, objectIDList);
            }
        }

        public static void SetFieldValues(IInformationObject rootObject, NameValueCollection fieldEntries)
        {
            NameValueCollection internalObjectFixedEntries = new NameValueCollection();
            foreach (string key in fieldEntries.AllKeys)
            {
                string fieldValue = fieldEntries[key];
                if (key.Contains("___"))
                {
                    int underscoreIndex = key.IndexOf('_');
                    string containingObjectID = key.Substring(0, underscoreIndex);
                    List<IInformationObject> foundObjects= new List<IInformationObject>();
                    rootObject.FindObjectsFromTree(foundObjects, iObj => iObj.ID == containingObjectID, false);
                    if(foundObjects.Count == 0)
                        throw new InvalidDataException("Containing object with ID not found: " + containingObjectID);
                    var containingRootObject = foundObjects[0];
                    string chainedPropertyName = key.Substring(underscoreIndex + 1);
                    object actualContainingObject;
                    string actualPropertyName;
                    InitializeChainAndReturnPropertyOwningObject(containingRootObject, chainedPropertyName,
                                                                 out actualContainingObject, out actualPropertyName);
                    IInformationObject actualIObj = (IInformationObject) actualContainingObject;
                    string actualKey = actualIObj.ID + "_" + actualPropertyName;
                    internalObjectFixedEntries.Add(actualKey, fieldValue);
                }
                else
                {
                    internalObjectFixedEntries.Add(key, fieldValue);
                }
            }
            rootObject.SetValuesToObjects(internalObjectFixedEntries);
        }

        private static void retrieveDataSourceInfo(string sourceInfo, out string relativeLocation, out string oldETag)
        {
            string[] infoParts = sourceInfo.Split(':');
            relativeLocation = infoParts[0];
            oldETag = infoParts[1];
        }

        public static void SetBinaryContent(IContainerOwner containerOwner, string contentInfo, IInformationObject rootObject,
                                MediaFileData mediaFileData)
        {
            int firstIX = contentInfo.IndexOf('_');
            if (firstIX < 0)
                throw new InvalidDataException("Invalid field data on binary content");
            string containerID = contentInfo.Substring(0, firstIX);
            string containerField = contentInfo.Substring(firstIX + 1);
            rootObject.SetMediaContent(containerOwner, containerID, containerField, mediaFileData);
        }

        public static void InitializeChainAndReturnPropertyOwningObject(IInformationObject createdObject, string objectProp, out object actualContainingObject, out string fieldPropertyName)
        {
            actualContainingObject = null;
            fieldPropertyName = null;
            if (objectProp.Contains("___") == false)
                return;
            string[] objectChain = objectProp.Split(new[] { "___" }, StringSplitOptions.None);
            fieldPropertyName = objectChain[objectChain.Length - 1];
            Stack<string> objectChainStack = new Stack<string>(objectChain.Reverse().Skip(1));
            actualContainingObject = createdObject;
            while (objectChainStack.Count > 0)
            {
                Type currType = actualContainingObject.GetType();
                string currObjectProp = objectChainStack.Pop();
                PropertyInfo prop = currType.GetProperty(currObjectProp);
                if (prop == null)
                    throw new InvalidDataException("Property not found by name: " + currObjectProp);
                var currPropValue = prop.GetValue(actualContainingObject, null);
                if (currPropValue == null)
                {
                    var currPropType = prop.PropertyType;
                    currPropValue = Activator.CreateInstance(currPropType);
                    prop.SetValue(actualContainingObject, currPropValue, null);
                }
                actualContainingObject = currPropValue;
            }
        }

        internal static void ExecuteHttpOperation(string operationName, HttpRequestSerializer.HttpRequestData reqData)
        {
            throw new NotImplementedException();
        }
    }
}