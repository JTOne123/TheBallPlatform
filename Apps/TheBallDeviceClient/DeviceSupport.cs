using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace TheBall.Support.DeviceClient
{
    public static class DeviceSupport
    {
        public static DeviceOperationData ExecuteDeviceOperation(this Device device, DeviceOperationData operationParameters, string ownerPrefix = null)
        {
            var dod = DeviceSupport.ExecuteRemoteOperation<DeviceOperationData>(device, "TheBall.CORE.RemoteDeviceCoreOperation", operationParameters, ownerPrefix);
            if(!dod.OperationResult)
                throw new OperationCanceledException("Remote device operation failed");
            return dod;
        }

        
        public const string OperationPrefixStr = "op/";

        public static TReturnType ExecuteRemoteOperation<TReturnType>(Device device, string operationName, object operationParameters, string ownerPrefix = null)
        {
            return (TReturnType)executeRemoteOperation<TReturnType>(device, operationName, operationParameters, ownerPrefix);
        }        
        public static void ExecuteRemoteOperationVoid(Device device, string operationName, object operationParameters, string ownerPrefix = null)
        {
            executeRemoteOperation<object>(device, operationName, operationParameters, ownerPrefix);
        }

        public const PaddingMode PADDING_MODE = PaddingMode.PKCS7;
        public const CipherMode AES_MODE = CipherMode.CBC;
        public const int AES_FEEDBACK_SIZE = 8;
        public const int AES_KEYSIZE = 256;
        public const int AES_BLOCKSIZE = 128;

        private static object executeRemoteOperation<TReturnType>(Device device, string operationName, object operationParameters, string ownerPrefix)
        {
            string operationUrl = String.Format("{0}{1}", OperationPrefixStr, operationName);
            string url = device.ConnectionURL.Replace("/DEV", "/" + operationUrl);
            if (ownerPrefix != null)
                url = replaceUrlAccountOwner(url, ownerPrefix);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            AesManaged aes = new AesManaged();
            aes.KeySize = AES_KEYSIZE;
            aes.BlockSize = AES_BLOCKSIZE;
            aes.GenerateIV();
            aes.Key = device.AESKey;
            aes.Padding = PADDING_MODE;
            aes.Mode = AES_MODE;
            aes.FeedbackSize = AES_FEEDBACK_SIZE;
            var ivBase64 = Convert.ToBase64String(aes.IV);
            request.Headers.Add("Authorization", "DeviceAES:" + ivBase64
                                                 + ":" + device.EstablishedTrustID
                                                 + ":" + device.AccountEmail);
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
            return getObjectFromResponseStream<TReturnType>(response, device.AESKey);
        }

        private static string replaceUrlAccountOwner(string url, string ownerPrefix)
        {
            return url.Replace("/auth/account/", "/auth/" + ownerPrefix + "/");
        }

        private static TReturnType getObjectFromResponseStream<TReturnType>(HttpWebResponse response, byte[] aesKey)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                string ivStr = response.Headers["IV"];
                var respStream = response.GetResponseStream();
                respStream.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                AesManaged aes = new AesManaged();
                aes.KeySize = AES_KEYSIZE;
                aes.BlockSize = AES_BLOCKSIZE;
                aes.IV = Convert.FromBase64String(ivStr);
                aes.Key = aesKey;
                aes.Padding = PADDING_MODE;
                aes.Mode = AES_MODE;
                aes.FeedbackSize = AES_FEEDBACK_SIZE;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    var contentObject = JSONSupport.GetObjectFromStream<TReturnType>(cryptoStream);
                    return contentObject;
                }
            }
        }

        public static void PushContentToDevice(Device device, string localContentFileName, string destinationContentName)
        {
            string url = device.ConnectionURL.Replace("/DEV", "/" + destinationContentName);
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            AesManaged aes = new AesManaged();
            aes.KeySize = AES_KEYSIZE;
            aes.BlockSize = AES_BLOCKSIZE;
            aes.GenerateIV();
            aes.Key = device.AESKey;
            aes.Padding = PADDING_MODE;
            aes.Mode = AES_MODE;
            aes.FeedbackSize = AES_FEEDBACK_SIZE;
            var ivBase64 = Convert.ToBase64String(aes.IV);
            request.Headers.Add("Authorization",
                "DeviceAES:" + ivBase64 + ":" + device.EstablishedTrustID + ":" + device.AccountEmail);
            using (var requestStream = request.GetRequestStream())
            {
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                var cryptoStream = new CryptoStream(requestStream, encryptor, CryptoStreamMode.Write);
                var fileStream = File.OpenRead(localContentFileName);
                fileStream.CopyTo(cryptoStream);
                cryptoStream.Close();
            }

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new InvalidOperationException("PushToInformationOutput failed with Http status: " +
                                                        response.StatusCode.ToString());
            }
        }

        public static void FetchContentFromDevice(Device device, string remoteContentFileName, string localContentFileName, string ownerPrefix)
        {
            string url = device.ConnectionURL.Replace("/DEV", "/" + remoteContentFileName);
            if (ownerPrefix != null)
                url = replaceUrlAccountOwner(url, ownerPrefix);
            string establishedTrustID = device.EstablishedTrustID;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "DeviceAES::" + establishedTrustID + ":" + device.AccountEmail);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException("Authroized fetch failed with non-OK status code");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                string ivStr = response.Headers["IV"];
                var respStream = response.GetResponseStream();
                respStream.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                AesManaged aes = new AesManaged();
                aes.KeySize = AES_KEYSIZE;
                aes.BlockSize = AES_BLOCKSIZE;
                aes.IV = Convert.FromBase64String(ivStr);
                aes.Key = device.AESKey;
                aes.Padding = PADDING_MODE;
                aes.Mode = AES_MODE;
                aes.FeedbackSize = AES_FEEDBACK_SIZE;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (FileStream fileStream = File.Create(localContentFileName))
                {
                    cryptoStream.CopyTo(fileStream);
                    fileStream.Close();
                }
            }
        }
    }
}