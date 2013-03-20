﻿using System;
using System.Collections.Generic;
using TheBall;

namespace AaltoGlobalImpact.OIP
{
    public static class PublishGroupContentToWwwImplementation
    {
        public static string GetTarget_CurrentContainerName(string groupID)
        {
            return StorageSupport.CurrActiveContainer.Name;
        }

        public static string GetTarget_WwwContainerName(string groupID)
        {
            return RenderWebSupport.GetCurrentWwwContainerName();
        }

        public static void ExecuteMethod_PublishGroupContentToWww(string groupID, bool useWorker, string currentContainerName, string wwwContainerName)
        {
            if (groupID != RenderWebSupport.DefaultGroupID) // Only controlled groups can have website/do web publishing
                return;
            string groupWwwPublicSiteLocation = "grp/" + groupID + "/" + RenderWebSupport.DefaultPublicWwwSiteLocation;
            PublishWebContentOperation operation = PublishWebContentOperation.CreateDefault();
            operation.SourceContainerName = currentContainerName;
            operation.TargetContainerName = wwwContainerName;
            operation.SourceOwner = "grp/" + groupID;
            operation.SourcePathRoot = "wwwsite";
            if (useWorker)
            {
                //QueueSupport.PutToOperationQueue(localGroupTemplates, renderLocalTemplates);
                OperationRequest operationRequest = new OperationRequest();
                operationRequest.PublishWebContent = operation;
                QueueSupport.PutToOperationQueue(operationRequest);
            }
            else
            {
                WorkerSupport.ProcessPublishWebContent(operation);
            }
        }
    }
}