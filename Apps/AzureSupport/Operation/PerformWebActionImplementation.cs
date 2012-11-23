﻿using System;

namespace AaltoGlobalImpact.OIP
{
    public static class PerformWebActionImplementation
    {
        public static bool ExecuteMethod_ExecuteActualOperation(string targetObjectID, string commandName, IContainerOwner owner, InformationSourceCollection informationSources, string[] formSourceNames)
        {
            switch(commandName)
            {
                case "RemoveCollaborator":
                    return CallRemoveGroupMember(targetObjectID, owner);
                case "PublishGroupPublicContent":
                    return CallPublishGroupContentToPublicArea(owner);
                case "PublishGroupWwwContent":
                    return CallPublishGroupContentToWww(owner);
                default:
                    throw new NotImplementedException("Operation mapping for command not implemented: " + commandName);
            }
        }

        private static bool CallPublishGroupContentToWww(IContainerOwner owner)
        {
            string groupID = owner.LocationPrefix;
            PublishGroupContentToWww.Execute(new PublishGroupContentToWwwParameters { GroupID = groupID, UseWorker = true });
            return false;
        }

        private static bool CallPublishGroupContentToPublicArea(IContainerOwner owner)
        {
            string groupID = owner.LocationPrefix;
            PublishGroupContentToPublicArea.Execute(new PublishGroupContentToPublicAreaParameters
                                                        {GroupID = groupID, UseWorker = true});
            return false;
        }

        private static bool CallRemoveGroupMember(string targetObjectID, IContainerOwner owner)
        {
            if(String.IsNullOrEmpty(targetObjectID))
                throw new ArgumentException("AccountID must be given for remove group member", "targetObjectID");
            string accountID = targetObjectID;
            string groupID = owner.LocationPrefix;
            RemoveMemberFromGroup.Execute(new RemoveMemberFromGroupParameters {AccountID = accountID, GroupID = groupID});
            return true;
        }

        public static PerformWebActionReturnValue Get_ReturnValue(bool executeActualOperationOutput)
        {
            return new PerformWebActionReturnValue() {RenderPageAfterOperation = executeActualOperationOutput};
        }
    }
}