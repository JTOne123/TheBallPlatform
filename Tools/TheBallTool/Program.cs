﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AaltoGlobalImpact.OIP;
using Microsoft.WindowsAzure.StorageClient;
using TheBall;
using TheBall.CORE;

namespace TheBallTool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connStr = //String.Format("DefaultEndpointsProtocol=http;AccountName=theball;AccountKey={0}",
                    args[0];
                                 //              );
                //connStr = "UseDevelopmentStorage=true";
                bool debugMode = false;


                StorageSupport.InitializeWithConnectionString(connStr, debugMode);
                InformationContext.InitializeFunctionality(3, true);
                InformationContext.Current.InitializeCloudStorageAccess(Properties.Settings.Default.CurrentActiveContainerName);
                QueueSupport.RegisterQueue("index-defaultindex-index");
                QueueSupport.RegisterQueue("index-defaultindex-query");

                if(DataPatcher.DoPatching())
                    return;

                throw new NotSupportedException("Functionality moved up-to-date to WebTemplateManager");

                //ProcessErrors(false);
                //return;

                string templateLocation = "livetemplate";
                string privateSiteLocation = "livesite";
                string publicSiteLocation = "livepubsite";
                const string accountNamePart = "oip-account\\";
                const string publicGroupNamePart = "oip-public\\";
                const string groupNamePart = "oip-group\\";
                const string wwwNamePart = "www-public\\";
                //DoMapData(webGroup);
                //return;
                string directory = Directory.GetCurrentDirectory();
                if (directory.EndsWith("\\") == false)
                    directory = directory + "\\";
                string[] allFiles =
                    Directory.GetFiles(directory, "*", SearchOption.AllDirectories).Select(
                        str => str.Substring(directory.Length)).Where(str => str.StartsWith("theball-") == false).ToArray();
                string[] groupTemplates =
                    allFiles.Where(file => file.StartsWith(accountNamePart) == false && file.StartsWith(publicGroupNamePart) == false && file.StartsWith(wwwNamePart) == false).
                        ToArray();
                string[] publicGroupTemplates =
                    allFiles.Where(file => file.StartsWith(accountNamePart) == false && file.StartsWith(groupNamePart) == false && file.StartsWith(wwwNamePart) == false).
                        ToArray();
                string[] accountTemplates =
                    allFiles.Where(file => file.StartsWith(groupNamePart) == false && file.StartsWith(publicGroupNamePart) == false && file.StartsWith(wwwNamePart) == false).
                        ToArray();
                string[] wwwTemplates =
                    allFiles.Where(file => file.StartsWith(groupNamePart) == false && file.StartsWith(publicGroupNamePart) == false && file.StartsWith(accountNamePart) == false).
                        ToArray();
                //UploadAndMoveUnused(accountTemplates, groupTemplates, publicGroupTemplates, null);
                //UploadAndMoveUnused(null, null, null, wwwTemplates);
                //UploadAndMoveUnused(null, null, publicGroupTemplates, null);
                //UploadAndMoveUnused(accountTemplates, null, null);

                //DeleteAllAccountAndGroupContents(true);
                //RefreshAllAccounts();

                // TODO: The delete above needs to go through first before the refresh one below

                /*
                RenderWebSupport.RefreshAllAccountAndGroupTemplates(true, "AaltoGlobalImpact.OIP.Blog", "AaltoGlobalImpact.OIP.Activity", "AaltoGlobalImpact.OIP.AddressAndLocation",
                    "AaltoGlobalImpact.OIP.Image", "AaltoGlobalImpact.OIP.ImageGroup", "AaltoGlobalImpact.OIP.Category");
                */
                //RunTaskedQueueWorker();


                //FileSystemSupport.UploadTemplateContent(groupTemplates, webGroup, templateLocation, true);
                Console.WriteLine("Starting to sync...");
                //DoSyncs(templateLocation, privateSiteLocation, publicSiteLocation);
                //"grp/default/pub/", true);
                return;
                //doDataTest(connStr);
                //InitLandingPages();
                //Console.WriteLine("Press enter to continue...");
                //Console.ReadLine();
            } 
                catch(InvalidDataException ex)
            {
                Console.WriteLine("Error exit: " + ex.ToString());
            }
        }

        private static void ProcessErrors(bool useWorker)
        {
            if (useWorker)
            {
                List<QueueEnvelope> envelopes = new List<QueueEnvelope>();
                List<CloudQueueMessage> messages = new List<CloudQueueMessage>();
                CloudQueueMessage message = null;
                QueueEnvelope envelope = ErrorSupport.RetrieveRetryableEnvelope(out message);
                while (envelope != null)
                {
                    //WorkerSupport.ProcessMessage(envelope, false);
                    //QueueSupport.CurrErrorQueue.DeleteMessage(message);
                    messages.Add(message);
                    envelope.CurrentRetryCount++;
                    envelopes.Add(envelope);
                    envelope = ErrorSupport.RetrieveRetryableEnvelope(out message);
                }
                envelopes.ForEach(QueueSupport.PutToDefaultQueue);
                messages.ForEach(msg => QueueSupport.CurrErrorQueue.DeleteMessage(msg));
            }
            else
            {
                CloudQueueMessage message = null;
                QueueEnvelope envelope = ErrorSupport.RetrieveRetryableEnvelope(out message);
                while (envelope != null)
                {
                    WorkerSupport.ProcessMessage(envelope, false);
                    QueueSupport.CurrErrorQueue.DeleteMessage(message);
                    envelope = ErrorSupport.RetrieveRetryableEnvelope(out message);
                }
            }
        }

        private static OperationRequest PushTestQueue()
        {
            OperationRequest operationRequest = new OperationRequest
                                            {
                                                SubscriberNotification = new Subscription
                                                                             {
                                                                                 //SubscriberRelativeLocation =
                                                                                 //    "acc/17e18f1d-c5bd-4955-af5a-a62d1092710a/website/oip-account/oip-layout-account-welcome.phtml",
                                                                                 SubscriberRelativeLocation =
                                                                                 "grp/fd686fbd-deb5-4631-899d-5da4314c27b8/website/oip-group/oip-layout-blog-summary.phtml",
                                                                                 SubscriptionType =
                                                                                     SubscribeSupport.
                                                                                     SubscribeType_WebPageToSource
                                                                             }
                                            };
            return operationRequest;
        }

        private static void RunTaskedQueueWorker()
        {
            Task[] tasks = new Task[]
                               {
                                   Task.Factory.StartNew(() => {}), 
                                   Task.Factory.StartNew(() => {}), 
                                   Task.Factory.StartNew(() => {}), 
                                   //Task.Factory.StartNew(() => {}), 
                                   //Task.Factory.StartNew(() => {}), 
                               };
            bool IsStopped = false;
            int initialCount = 0;
            while (true)
            {
                try
                {
                    Task.WaitAny(tasks);
                    if (IsStopped)
                    {
                        Task.WaitAll(tasks);
                        foreach(var task in tasks)
                        {
                            if(task.Exception != null)
                                ErrorSupport.ReportException(task.Exception);
                        }
                        break;
                    }
                    int availableIx;
                    Task availableTask = WorkerSupport.GetFirstCompleted(tasks, out availableIx);
                    CloudQueueMessage message;
                    QueueEnvelope envelope = QueueSupport.GetFromDefaultQueue(out message);
                    if (envelope != null)
                    {
                        Task executing = Task.Factory.StartNew(() => WorkerSupport.ProcessMessage(envelope));
                        tasks[availableIx] = executing;
                        QueueSupport.CurrDefaultQueue.DeleteMessage(message);
                        if (availableTask.Exception != null)
                            ErrorSupport.ReportException(availableTask.Exception);
                    }
                    else 
                    {
                        if(message != null)
                        {
                            QueueSupport.CurrDefaultQueue.DeleteMessage(message);
                            ErrorSupport.ReportMessageError(message);
                        }
                        Thread.Sleep(1000);
                    }
                }
                catch (AggregateException ae)
                {
                    foreach (var e in ae.Flatten().InnerExceptions)
                    {
                        ErrorSupport.ReportException(e);
                    }
                    Thread.Sleep(10000);
                    // or ...
                    // ae.Flatten().Handle((ex) => ex is MyCustomException);
                }
                    /*
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Trace.WriteLine(e.Message);
                        throw;
                    }
                    Thread.Sleep(10000);
                }*/
                catch (OperationCanceledException e)
                {
                    if (!IsStopped)
                    {
                        Trace.WriteLine(e.Message);
                        throw;
                    }
                } 
                catch(Exception ex)
                {
                    ErrorSupport.ReportException(ex);
                    throw;
                }
            }
        }

        private static void RunQueueWorker(QueueEnvelope givenEnvelope)
        {
            bool loop = true;
            while (loop)
            {
                loop = givenEnvelope == null; 
                QueueEnvelope envelope;
                if (givenEnvelope == null)
                {

                    CloudQueueMessage message = null;
                    envelope = QueueSupport.GetFromDefaultQueue(out message);
                    QueueSupport.CurrDefaultQueue.DeleteMessage(message);
                }
                else
                {
                    envelope = givenEnvelope;
                }
                try
                {
                    WorkerSupport.ProcessMessage(envelope);
                } catch(Exception ex)
                {
                    string msg = ex.Message;
                }
                //Thread.Sleep(5000);
            }
        }

        private static void DeleteAllAccountAndGroupContents(bool useWorker)
        {
            var accountIDs = TBRAccountRoot.GetAllAccountIDs();
            var groupIDs = TBRGroupRoot.GetAllGroupIDs();
            List<string> referenceLocations = new List<string>();
            foreach (var accountID in accountIDs)
            {
                string referenceLocation = "acc/" + accountID + "/";
                referenceLocations.Add(referenceLocation);
            }
            foreach (var groupID in groupIDs)
            {
                string referenceLocation = "grp/" + groupID + "/";
                referenceLocations.Add(referenceLocation);
            }
            if(useWorker)
            {
                referenceLocations.ForEach(refLoc =>
                                               {
                                                   VirtualOwner owner = VirtualOwner.FigureOwner(refLoc);
                                                   QueueSupport.PutToOperationQueue(
                                                       new OperationRequest
                                                           {
                                                               DeleteOwnerContent = new DeleteOwnerContentOperation
                                                                                        {
                                                                                            ContainerName = owner.ContainerName,
                                                                                            LocationPrefix = owner.LocationPrefix
                                                                                        }
                                                           }
                                                       );
                                               });                
            }
            else
            {
                referenceLocations.ForEach(refLoc => StorageSupport.DeleteContentsFromOwner(refLoc));
            }
        }

        private static void TestEmail()
        {
            EmailSupport.SendEmail("no-reply-theball@msunit.citrus.fi", "kalle.launiala@citrus.fi", "The Ball - Says Hello!",
            "Text testing...");
        }

        private static void AddLoginToAccount(string loginUrlID, string accountID)
        {
            TBRAccountRoot accountRoot = ObjectStorage.RetrieveFromDefaultLocation<TBRAccountRoot>(accountID);

            TBLoginInfo loginInfo = TBLoginInfo.CreateDefault();
            loginInfo.OpenIDUrl = loginUrlID;

            accountRoot.Account.Logins.CollectionContent.Add(loginInfo);
            accountRoot.Account.StoreAccountToRoot();
        }

        /*
        private static void TestDriveDynamicCreation()
        {
            object test = RenderWebSupport.GetOrInitiateContentObject(new List<RenderWebSupport.ContentItem>(),
                                                                      "AaltoGlobalImpact.OIP.InformationSource",
                                                                      "vilperi", false);
        }*/

        private const string FixedGroupID = "05DF28FD-58A7-46A7-9830-DA3F51AAF6AF";

        private static TBCollaboratingGroup InitializeDefaultOIPWebGroup()
        {
            TBRGroupRoot groupRoot = ObjectStorage.RetrieveFromDefaultLocation<TBRGroupRoot>(FixedGroupID);
            if(groupRoot == null)
            {
                groupRoot = TBRGroupRoot.CreateDefault();
                groupRoot.ID = FixedGroupID;
                groupRoot.UpdateRelativeLocationFromID();
                groupRoot.Group.JoinToGroup("kalle.launiala@citrus.fi", "moderator");
                groupRoot.Group.JoinToGroup("jeroen@caloom.com", "moderator");
                StorageSupport.StoreInformation(groupRoot);
            }
            return groupRoot.Group;
        }


        private static void ReportInfo(string text)
        {
            Console.WriteLine(text);
        }

    }
}
//AddLoginToAccount("https://www.google.com/accounts/o8/id?id=AItOawkXb-XQERsvhNkZVlEEiCSOuP1y82uHCQc", "fbbaaded-6615-4083-8ea8-92b2aa162861");
//TestDriveQueueWorker();
//TestDriveDynamicCreation();
//return;
//bool result = EmailSupport.SendEmail("kalle.launiala@gmail.com", "kalle.launiala@citrus.fi", "The Ball - Says Hello!",
//                       "Text testing...");
