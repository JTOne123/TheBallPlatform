﻿using System;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using AaltoGlobalImpact.OIP;
using DotNetOpenAuth.OpenId.RelyingParty;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using TheBall;
using TheBall.CORE;

namespace WebInterface
{
    public class AnonymousBlobStorageHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            ProcessAnonymousRequest(request, response);
        }

        private void ProcessAnonymousRequest(HttpRequest request, HttpResponse response)
        {
            CloudBlobClient publicClient = new CloudBlobClient("http://caloom.blob.core.windows.net/");
            string blobPath = GetBlobPath(request);
            CloudBlockBlob blob = publicClient.GetBlockBlobReference(blobPath);
            response.Clear();
            try
            {
                blob.FetchAttributes();
                response.ContentType = StorageSupport.GetMimeType(blob.Name);
                blob.DownloadToStream(response.OutputStream);
            } catch(StorageClientException scEx)
            {
                if (scEx.ErrorCode == StorageErrorCode.BlobNotFound || scEx.ErrorCode == StorageErrorCode.ResourceNotFound || scEx.ErrorCode == StorageErrorCode.BadRequest)
                {
                    response.Write("Blob not found or bad request: " + blob.Name + " (original path: " + request.Path + ")");
                    response.StatusCode = (int)scEx.StatusCode;
                }
                else
                {
                    response.Write("Errorcode: " + scEx.ErrorCode.ToString() + Environment.NewLine);
                    response.Write(scEx.ToString());
                    response.StatusCode = (int) scEx.StatusCode;
                }
            } finally
            {
                response.End();
            }
        }

        private static string GetBlobPath(HttpRequest request)
        {
            string hostName = request.Url.DnsSafeHost;
            if (hostName == "localdev")
            {
                hostName = "www.protonit.net";
            }
            string containerName = hostName.Replace('.', '-');
            string currServingFolder = "";
            try
            {
                // "/2013-03-20_08-27-28";
                CloudBlobClient publicClient = new CloudBlobClient("http://caloom.blob.core.windows.net/");
                string currServingPath = containerName + "/" + RenderWebSupport.CurrentToServeFileName;
                var currBlob = publicClient.GetBlockBlobReference(currServingPath);
                string currServingData = currBlob.DownloadText();
                string[] currServeArr = currServingData.Split(':');
                string currActiveFolder = currServeArr[0];
                var currOwner = VirtualOwner.FigureOwner(currServeArr[1]);
                InformationContext.Current.Owner = currOwner;
                currServingFolder = "/" + currActiveFolder;
            }
            catch
            {
                
            }
            return containerName + currServingFolder + request.Path;
        }

        private static string GetAbsoluteLoginUrl(string hostName)
        {
            switch(hostName)
            {
                case "oip.msunit.citrus.fi":
                    return "http://oip.msunit.citrus.fi/TheBallLogin.aspx";
                case "demopublicoip.aaltoglobalimpact.org":
                    return "http://demooip.aaltoglobalimpact.org/TheBallLogin.aspx";
                case "localhost":
                case "localdev":
                    return null;
                default:
                    return null;
            }
        }

        #endregion
    }
}
