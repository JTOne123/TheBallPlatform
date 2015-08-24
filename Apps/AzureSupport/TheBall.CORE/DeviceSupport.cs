using System;
using System.Net;
using System.Security.Cryptography;
using AzureSupport;
using SecuritySupport;

namespace TheBall.CORE
{
    public static class DeviceSupport
    {
        public const string OperationPrefixStr = "op/";

        public static TReturnType ExecuteRemoteOperation<TReturnType>(string deviceID, string operationName, object operationParameters)
        {
            return (TReturnType) executeRemoteOperation<TReturnType>(deviceID, operationName, operationParameters);
        }        
        public static void ExecuteRemoteOperationVoid(string deviceID, string operationName, object operationParameters)
        {
            executeRemoteOperation<object>(deviceID, operationName, operationParameters);
        }

        private static object executeRemoteOperation<TReturnType>(string deviceID, string operationName, object operationParameters)
        {
            AuthenticatedAsActiveDevice device = ObjectStorage.RetrieveFromOwnerContent<AuthenticatedAsActiveDevice>(InformationContext.CurrentOwner, deviceID);
            string operationUrl = String.Format("{0}{1}", OperationPrefixStr, operationName);
            string url = device.ConnectionURL.Replace("/DEV", "/" + operationUrl);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            AesManaged aes = new AesManaged();
            aes.KeySize = SymmetricSupport.AES_KEYSIZE;
            aes.BlockSize = SymmetricSupport.AES_BLOCKSIZE;
            aes.GenerateIV();
            aes.Key = device.ActiveSymmetricAESKey;
            aes.Padding = SymmetricSupport.PADDING_MODE;
            aes.Mode = SymmetricSupport.AES_MODE;
            aes.FeedbackSize = SymmetricSupport.AES_FEEDBACK_SIZE;
            var ivBase64 = Convert.ToBase64String(aes.IV);
            request.Headers.Add("Authorization", "DeviceAES:" + ivBase64 
                + ":" + device.EstablishedTrustID 
                + ":");
            var requestStream = request.GetRequestStream();
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var cryptoStream = new CryptoStream(requestStream, encryptor, CryptoStreamMode.Write))
            {
                JSONSupport.SerializeToJSONStream(operationParameters, cryptoStream);
            }
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException("PushToInformationOutput failed with Http status: " + response.StatusCode.ToString());
            if (typeof (TReturnType) == typeof (object))
                return null;
            return getObjectFromResponseStream<TReturnType>(response, device.ActiveSymmetricAESKey);
        }

        private static TReturnType getObjectFromResponseStream<TReturnType>(HttpWebResponse response, byte[] aesKey)
        {
            string ivStr = response.Headers["IV"];
            var respStream = response.GetResponseStream();
            AesManaged aes = new AesManaged();
            aes.KeySize = SymmetricSupport.AES_KEYSIZE;
            aes.BlockSize = SymmetricSupport.AES_BLOCKSIZE;
            aes.IV = Convert.FromBase64String(ivStr);
            aes.Key = aesKey;
            aes.Padding = SymmetricSupport.PADDING_MODE;
            aes.Mode = SymmetricSupport.AES_MODE;
            aes.FeedbackSize = SymmetricSupport.AES_FEEDBACK_SIZE;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (CryptoStream cryptoStream = new CryptoStream(respStream, decryptor, CryptoStreamMode.Read))
            {
                var contentObject = JSONSupport.GetObjectFromStream<TReturnType>(cryptoStream);
                return contentObject;
            }
        }

        public static void PushContentToDevice(AuthenticatedAsActiveDevice authenticatedAsActiveDevice, string localContentUrl, string destinationContentName)
        {
            var owner = InformationContext.CurrentOwner;
            var blob = StorageSupport.GetOwnerBlobReference(owner, localContentUrl);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(authenticatedAsActiveDevice.ConnectionURL);
            request.Method = "POST";
            AesManaged aes = new AesManaged();
            aes.KeySize = SymmetricSupport.AES_KEYSIZE;
            aes.BlockSize = SymmetricSupport.AES_BLOCKSIZE;
            aes.GenerateIV();
            aes.Key = authenticatedAsActiveDevice.ActiveSymmetricAESKey;
            aes.Padding = SymmetricSupport.PADDING_MODE;
            aes.Mode = SymmetricSupport.AES_MODE;
            aes.FeedbackSize = SymmetricSupport.AES_FEEDBACK_SIZE;
            var ivBase64 = Convert.ToBase64String(aes.IV);
            request.Headers.Add("Authorization", "DeviceAES:" + ivBase64 + ":" + authenticatedAsActiveDevice.EstablishedTrustID + ":" + destinationContentName);
            var requestStream = request.GetRequestStream();
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var cryptoStream = new CryptoStream(requestStream, encryptor, CryptoStreamMode.Write);
            blob.DownloadToStream(cryptoStream);
            cryptoStream.Close();
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException("PushToInformationOutput failed with Http status: " + response.StatusCode.ToString());
            
        }
    }
}