﻿

using System;
using System.IO;
using System.Runtime.Serialization;
using AaltoGlobalImpact.OIP;
using Microsoft.WindowsAzure.StorageClient;

namespace TheBall
{
    public static class SubscribeSupport
    {
        public const string SubscribeType_WebPageToSource = "webpage";

        public static void AddSubscriptionToObject(string targetLocation, string subscriberLocation, string subscriptionType)
        {
            var sub = GetSubscriptionToObject(targetLocation, subscriberLocation, subscriptionType);
            SubscriptionCollection subscriptionCollection = GetSubscriptions(targetLocation);
            if(subscriptionCollection == null)
            {
                subscriptionCollection = new SubscriptionCollection();
                subscriptionCollection.SetRelativeLocationAsMetadataTo(targetLocation);
            }
            if (subscriptionCollection.CollectionContent.Exists(existing => existing.SubscriberRelativeLocation == sub.SubscriberRelativeLocation))
                return;
            subscriptionCollection.CollectionContent.Add(sub);
            StorageSupport.StoreInformation(subscriptionCollection);
        }

        /*
        public static void AddSubscriptionToItem(IInformationObject target, string itemName, IInformationObject subscriber, string operationName)
        {
            var sub = GetSubscriptionToItem(target, itemName, subscriber, operationName);
        }*/

        public static Subscription GetSubscriptionToObject(string targetLocation, string subscriberLocation, string subscriptionType)
        {
            var sub = new Subscription
                          {
                              TargetRelativeLocation = targetLocation,
                              SubscriberRelativeLocation = subscriberLocation,
                              SubscriptionType = subscriptionType
                          };
            return sub;
        }

        /*
        public static Subscription GetSubscriptionToItem(IInformationObject target, string itemName, IInformationObject subscriber, string operationName)
        {
            Subscription sub = GetSubscriptionToObject(target, subscriber, operationName);
            sub.TargetItemName = itemName;
            return sub;
        }*/

        public static SubscriptionCollection GetSubscriptions(string targetLocation)
        {
            string blobPath = SubscriptionCollection.GetRelativeLocationAsMetadataTo(targetLocation);
            var result = StorageSupport.RetrieveInformation(blobPath, typeof(SubscriptionCollection));
            return (SubscriptionCollection) result;
        }

        public static void NotifySubscribers(string targetLocation)
        {
            SubscriptionCollection subscriptionCollection = GetSubscriptions(targetLocation);
            if (subscriptionCollection == null)
                return;
            foreach(var subscription in subscriptionCollection.CollectionContent)
            {
                QueueEnvelope envelope =
                    new QueueEnvelope
                        {
                            SubscriberNotification = subscription
                        };
                QueueSupport.PutToDefaultQueue(envelope);
            }
        }
    }
}