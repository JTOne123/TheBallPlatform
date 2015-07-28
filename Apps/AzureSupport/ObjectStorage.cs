using System;
using System.IO;
using AzureSupport.TheBall.CORE;
using TheBall.CORE;

namespace TheBall
{
    public static class ObjectStorage
    {
        public static T RetrieveFromDefaultLocation<T>(string id,
            IContainerOwner owner = null)
        {
            string relativeLocation = GetRelativeLocationFromID<T>(id);
            return RetrieveObject<T>(relativeLocation, owner);
        }

        public static T RetrieveObject<T>(string relativeLocation, IContainerOwner owner = null)
        {
            var result = (T) StorageSupport.RetrieveInformation(relativeLocation, typeof (T));
            return result;
        }

        public static string GetRelativeLocationFromID<T>(string id)
        {
            string namespaceName = typeof (T).Namespace;
            string className = typeof (T).Name;
            return Path.Combine(namespaceName, className, id).Replace("\\", "/");
        }

        public static T RetrieveFromOwnerContent<T>(IContainerOwner containerOwner, string contentName)
        {
            string namespaceName = typeof (T).Namespace;
            string className = typeof (T).Name;
            string locationPath = String.Format("{0}/{1}/{2}", namespaceName, className, contentName);
            var result = RetrieveObject<T>(locationPath, containerOwner);
            return result;
        }


    }
}