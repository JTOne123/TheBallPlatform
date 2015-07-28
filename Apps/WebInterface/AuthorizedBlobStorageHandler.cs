﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Helpers;
using System.Web.Security;
using AaltoGlobalImpact.OIP;
using AzureSupport;
using DotNetOpenAuth.OpenId.RelyingParty;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using SecuritySupport;
using TheBall;
using TheBall.CORE;
using Process = TheBall.CORE.Process;

namespace WebInterface
{
    public class AuthorizedBlobStorageHandler : HttpTaskAsyncHandler
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

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            string user = context.User.Identity.Name;
            var userIdentity = context.User;
            bool isAuthenticated = String.IsNullOrEmpty(user) == false;
            var request = context.Request;
            var response = context.Response;
            if (request.IsAboutRequest())
            {
                await HandleAboutGetRequest(context, request.Path);
                return;
            }

            if (isAuthenticated == false)
            {
                return;
            }
            try
            {
                if (userIdentity.IsInRole("DeviceAES"))
                {
                    await HandleEncryptedDeviceRequest(context);
                }
                else if (request.IsPersonalRequest())
                {
                    await HandlePersonalRequest(context);
                }
                else if (request.IsGroupRequest())
                {
                    await HandleGroupRequest(context);
                }
                else if (request.IsAccountRequest())
                {
                    HandleAccountRequest(context);
                }
            }
            catch (SecurityException securityException)
            {
                response.StatusCode = 403;
                response.Write(securityException.ToString());
            }
        }

        private async Task HandleEncryptedDeviceRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var authorization = request.Headers["Authorization"];
            var authTokens = authorization.Split(':');
            IContainerOwner owner = null;
            if (request.IsGroupRequest())
            {
                string groupID = request.GetGroupID();
                owner = request.GetGroupAsOwner();
            }
            else
                throw new NotSupportedException("Account device requests not yet supported");
            string ivStr = authTokens[1];
            string trustID = authTokens[2];
            string contentName = authTokens[3];
            DeviceMembership deviceMembership = ObjectStorage.RetrieveFromOwnerContent<DeviceMembership>(owner, trustID);
            if(deviceMembership == null)
                throw new InvalidDataException("Device membership not found");
            if(deviceMembership.IsValidatedAndActive == false)
                throw new SecurityException("Device membership not valid and active");
            InformationContext.Current.Owner = owner;
            InformationContext.Current.ExecutingForDevice = deviceMembership;
            if (request.RequestType == "GET")
            {
                string contentPath = request.GetOwnerContentPath();
                if(contentPath.Contains("TheBall.CORE"))
                    throw new SecurityException("Invalid request location");
                var blob = StorageSupport.GetOwnerBlobReference(owner, contentPath);

                AesManaged aes = new AesManaged();
                aes.KeySize = SymmetricSupport.AES_KEYSIZE;
                aes.BlockSize = SymmetricSupport.AES_BLOCKSIZE;
                aes.GenerateIV();
                aes.Key = deviceMembership.ActiveSymmetricAESKey;
                aes.Padding = SymmetricSupport.PADDING_MODE;
                aes.Mode = SymmetricSupport.AES_MODE;
                aes.FeedbackSize = SymmetricSupport.AES_FEEDBACK_SIZE;
                var ivBase64 = Convert.ToBase64String(aes.IV);
                response.Headers.Add("IV", ivBase64);
                var responseStream = response.OutputStream;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                var cryptoStream = new CryptoStream(responseStream, encryptor, CryptoStreamMode.Write);
                blob.DownloadToStream(cryptoStream);
                cryptoStream.Close();
            } else if (request.RequestType == "POST")
            {
                if (contentName.StartsWith(DeviceSupport.OperationPrefixStr))
                {
                    string operationName = contentName.Substring(DeviceSupport.OperationPrefixStr.Length);
                    var reqStream = request.GetBufferedInputStream();
                    AesManaged decAES = new AesManaged
                        {
                            KeySize = SymmetricSupport.AES_KEYSIZE,
                            BlockSize = SymmetricSupport.AES_BLOCKSIZE,
                            IV = Convert.FromBase64String(ivStr),
                            Key = deviceMembership.ActiveSymmetricAESKey,
                            Padding = SymmetricSupport.PADDING_MODE,
                            Mode = SymmetricSupport.AES_MODE,
                            FeedbackSize = SymmetricSupport.AES_FEEDBACK_SIZE
                        };
                    var reqDecryptor = decAES.CreateDecryptor(decAES.Key, decAES.IV);

                    AesManaged encAES = new AesManaged
                        {
                            KeySize = SymmetricSupport.AES_KEYSIZE, 
                            BlockSize = SymmetricSupport.AES_BLOCKSIZE,
                            Key = deviceMembership.ActiveSymmetricAESKey,
                            Padding = SymmetricSupport.PADDING_MODE,
                            Mode = SymmetricSupport.AES_MODE,
                            FeedbackSize = SymmetricSupport.AES_FEEDBACK_SIZE
                        };
                    encAES.GenerateIV();
                    var respivBase64 = Convert.ToBase64String(encAES.IV);
                    response.Headers.Add("IV", respivBase64);
                    var responseStream = response.OutputStream;
                    var respEncryptor = encAES.CreateEncryptor(encAES.Key, encAES.IV);


                    using (CryptoStream reqDecryptStream = new CryptoStream(reqStream, reqDecryptor, CryptoStreamMode.Read),
                            respEncryptedStream = new CryptoStream(responseStream, respEncryptor, CryptoStreamMode.Write))
                    {
                        OperationSupport.ExecuteOperation(operationName, 
                            new Tuple<string, object>("InputStream", reqDecryptStream),
                            new Tuple<string, object>("OutputStream", respEncryptedStream));
                    }
                }
                else
                {
                    string contentRoot = deviceMembership.RelativeLocation + "_Input";
                    string blobName = contentRoot + "/" + contentName;
                    var blob = StorageSupport.GetOwnerBlobReference(owner, blobName);
                    if (blob.Name != blobName)
                        throw new InvalidDataException("Invalid content name");
                    var reqStream = request.GetBufferedInputStream();
                    AesManaged aes = new AesManaged();
                    aes.KeySize = SymmetricSupport.AES_KEYSIZE;
                    aes.BlockSize = SymmetricSupport.AES_BLOCKSIZE;
                    aes.IV = Convert.FromBase64String(ivStr);
                    aes.Key = deviceMembership.ActiveSymmetricAESKey;
                    aes.Padding = SymmetricSupport.PADDING_MODE;
                    aes.Mode = SymmetricSupport.AES_MODE;
                    aes.FeedbackSize = SymmetricSupport.AES_FEEDBACK_SIZE;
                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    CryptoStream cryptoStream = new CryptoStream(reqStream, decryptor, CryptoStreamMode.Read);
                    blob.UploadFromStream(cryptoStream);
                }
                response.StatusCode = 200;
                response.End();
            }
            else
                throw new NotSupportedException("Device request type not supported: " + request.RequestType);

        }

        private static string GetBlobPath(HttpRequest request)
        {
            string contentPath = request.Path;
            if (!request.IsAboutRequest())
                throw new NotSupportedException("Content path for other than about/ is not supported");
            return contentPath.Substring(1);
        }

        private void HandleAccountRequest(HttpContext context)
        {
            //TBRLoginRoot loginRoot = GetOrCreateLoginRoot(context);
        }

        private async Task HandleGroupRequest(HttpContext context)
        {
            var request = context.Request;
            var loginGroupRoot = await request.RequireAndRetrieveGroupAccessRole();
            InformationContext.Current.CurrentGroupRole = loginGroupRoot.Role;
            var contentPath = request.GetOwnerContentPath();
            await HandleOwnerRequest(loginGroupRoot, context, contentPath, loginGroupRoot.Role);
        }

        private async Task HandlePersonalRequest(HttpContext context)
        {
            var domainName = context.Request.Url.Host;
            string loginUrl = WebSupport.GetLoginUrl(context);
            TBRLoginRoot loginRoot = await TBRLoginRoot.GetOrCreateLoginRootWithAccount(loginUrl, true, domainName);
            bool doDelete = false;
            if(doDelete)
            {
                loginRoot.DeleteInformationObject();
                return;
            }
            TBAccount account = loginRoot.Account;
            var request = context.Request;
            var contentPath = request.GetOwnerContentPath();
            await HandleOwnerRequest(account, context, contentPath, TBCollaboratorRole.CollaboratorRoleValue);
        }

        private async Task HandleOwnerRequest(IContainerOwner containerOwner, HttpContext context, string contentPath, string role)
        {
            InformationContext.Current.Owner = containerOwner;
            if (context.Request.RequestType == "POST")
            {
                // Do first post, and then get to the same URL
                if (TBCollaboratorRole.HasCollaboratorRights(role) == false)
                    throw new SecurityException("Role '" + role + "' is not authorized to do changing POST requests to web interface");
                if (contentPath.StartsWith("op/"))
                {
                    SetCurrentAccountFromLogin(context);
                    await HandleOwnerOperationRequestWithUrlPath(containerOwner, context, contentPath.Substring(3));
                }
                else
                {
                    try // Piloting POST account identifying for InformationContext
                    {
                        SetCurrentAccountFromLogin(context);
                    }
                    catch // We don't want any errors for this piloting
                    {

                    }
                    bool redirectAfter = await HandleOwnerPostRequest(containerOwner, context, contentPath);
                    if (redirectAfter)
                        context.Response.Redirect(context.Request.Url.ToString(), true);
                }
                return;
            }
            await HandleOwnerGetRequest(containerOwner, context, contentPath);
        }

        private async Task HandleOwnerOperationRequestWithUrlPath(IContainerOwner containerOwner, HttpContext context, string operationRequestPath)
        {
            string operationName = operationRequestPath.Split('/')[0];
            await HandleOwnerOperationRequest(containerOwner, context, operationName);
        }

        private async Task HandleOwnerOperationRequest(IContainerOwner containerOwner, HttpContext context,
            string operationName)
        {
            var operationAssembly = typeof (OperationSupport).Assembly;
            Type operationType = operationAssembly.GetType(operationName);
            if (operationType == null)
                operationType = OperationSupport.GetLegacyMappedType(operationName);
            if (operationType == null)
            {
                EndResponseWithStatusCode(context, 404);
                return;
            }
            var request = context.Request;
            var operationData = OperationSupport.GetHttpOperationDataFromRequest(request,
                InformationContext.CurrentAccount.AccountID, containerOwner.GetOwnerPrefix(), operationName,
                String.Empty);
            string operationID = OperationSupport.QueueHttpOperation(operationData);
            var response = context.Response;
            response.Write(String.Format("{{ \"OperationID\": {0} }}", operationID));
            EndResponseWithStatusCode(context, 202);
        }

        private static void EndResponseWithStatusCode(HttpContext context, int statusCode)
        {
            var response = context.Response;
            response.StatusCode = statusCode;
            response.Flush();
            response.SuppressContent = true;
            context.ApplicationInstance.CompleteRequest();
        }

        private async Task<bool> HandleOwnerPostRequest(IContainerOwner containerOwner, HttpContext context, string contentPath)
        {
            validateThatOwnerPostComesFromSameReferrer(context);
            HttpRequest request = context.Request;
            var form = request.Unvalidated.Form;

            bool isAjaxDataRequest = request.ContentType.StartsWith("application/json"); // form.Get("AjaxObjectInfo") != null;
            if (isAjaxDataRequest)
            {
                // Various data deserialization tests - options need to be properly set
                // strong type radically faster 151ms over 25sec with flexible type - something ill
                //throw new NotSupportedException("Not supported as-is, implementation for serialization available, not finished");
//                var stream = request.GetBufferedInputStream();
  //              var dataX = JSONSupport.GetObjectFromStream<List<ParentToChildren> >(stream);
//                var streamReader = new StreamReader(request.GetBufferedInputStream());
//                string data = streamReader.ReadToEnd();
//                var jsonData = JSONSupport.GetJsonFromStream(data);
//                HandlerOwnerAjaxDataPOST(containerOwner, request);
                //SetCategoryHierarchyAndOrderInNodeSummary.Execute();
                string operationName = request.Params["operation"];
                
                Type operationType = TypeSupport.GetTypeByName(operationName);
                bool isPaymentsOperation = operationType.Namespace == "TheBall.Payments";
                if (isPaymentsOperation)
                {
                    InformationContext.Current.Owner =
                        VirtualOwner.FigureOwner("grp/" + InstanceConfiguration.PaymentsGroupID);
                }
                var method = operationType.GetMethod("Execute", BindingFlags.Public | BindingFlags.Static);
                method.Invoke(null, null);
                context.Response.End();
                return false;
            }

            bool isClientTemplateRequest = form.Get("ContentSourceInfo") != null ||
                form.Get("ExecuteOperation") != null || form.Get("ExecuteAdminOperation") != null;
            if(isClientTemplateRequest)
            {
                HandleOwnerClientTemplatePOST(containerOwner, request);
                bool isPaymentsGroup = containerOwner.ContainerName == "grp" &&
                                       containerOwner.LocationPrefix == InstanceConfiguration.PaymentsGroupID;
                if (isPaymentsGroup && false)
                {
                    SQLiteSyncOwnerData(containerOwner);
                }

                return false;
            }

            throw new NotSupportedException("Old legacy update no longer supported");
        }

        private static void SetCurrentAccountFromLogin(HttpContext context)
        {
            var loginUrl = context.User.Identity.Name;
            string loginID = TBLoginInfo.GetLoginIDFromLoginURL(loginUrl);
            TBRLoginRoot loginRoot = ObjectStorage.RetrieveFromDefaultLocation<TBRLoginRoot>(loginID);
            if (loginRoot != null)
            {
                var currAccount = loginRoot.Account;
                var accountContainer = ObjectStorage.RetrieveFromOwnerContent<AccountContainer>(currAccount, "default");
                string accountName;
                string accountID = currAccount.ID;
                string accountEmail = currAccount.Emails.CollectionContent.Select(tbEm => tbEm.EmailAddress).FirstOrDefault();
                if (accountEmail == null)
                    accountEmail = "";
                if (accountContainer != null && accountContainer.AccountModule != null &&
                    accountContainer.AccountModule.Profile != null)
                {
                    accountName = string.Format("{0} {1}",
                        accountContainer.AccountModule.Profile.FirstName,
                        accountContainer.AccountModule.Profile.LastName);
                }
                else
                {
                    accountName = accountEmail;
                }
                accountName = accountName.Trim();
                InformationContext.Current.Account = new CoreAccountData(accountID,
                    accountName, accountEmail);
            }
        }

        private static void SQLiteSyncOwnerData(IContainerOwner containerOwner)
        {
            UpdateOwnerDomainObjectsInSQLiteStorage.Execute(new UpdateOwnerDomainObjectsInSQLiteStorageParameters
            {
                Owner = containerOwner,
                SemanticDomain = "TheBall.Payments"
            });
        }

        private void validateThatOwnerPostComesFromSameReferrer(HttpContext context)
        {
            var request = context.Request;
            var requestUrl = request.Url;
            var referrerUrl = request.UrlReferrer;
            if(referrerUrl == null || requestUrl.AbsolutePath != referrerUrl.AbsolutePath)
                throw new SecurityException("UrlReferrer mismatch or missing - potential cause is (un)intentionally malicious web template.");
        }

        private void HandleOwnerClientTemplatePOST(IContainerOwner containerOwner, HttpRequest request)
        {
            var form = request.Form;
            object operationResult = null;
            try
            {
                operationResult = ModifyInformationSupport.ExecuteOwnerWebPOST(containerOwner, form, request.Files);
            }
            catch (Exception ex)
            {
                var response = HttpContext.Current.Response;
                response.ContentEncoding = Encoding.UTF8;
                response.Write(String.Format("{{ \"ErrorType\": \"{0}\", \"ErrorText\": {1} }}", ex.GetType().Name, WebSupport.EncodeJsString(ex.Message)));
                response.ContentType = "application/json";
                response.StatusCode = 500;
                response.TrySkipIisCustomErrors = true;
                return;
            }
            if (operationResult != null)
            {
                if (operationResult is bool)
                    return;
                IInformationObject iObj = operationResult as IInformationObject;
                if (iObj != null)
                {
                    var response = HttpContext.Current.Response;
                    response.Write(String.Format("{{ \"ID\": \"{0}\", \"RelativeLocation\": \"{1}\", \"OwnerRelativeLocation\": \"{2}\", \"MasterETag\": \"{3}\" }}", iObj.ID, 
                        iObj.RelativeLocation + ".json",
                        StorageSupport.RemoveOwnerPrefixIfExists(iObj.RelativeLocation) + ".json", 
                        iObj.ETag.Replace("\"", "\\\"")));
                    response.ContentType = "application/json";
                }
            }
        }

        private void HandlerOwnerAjaxDataPOST(IContainerOwner containerOwner, NameValueCollection form)
        {
            throw new NotImplementedException();
        }

        private async Task HandleAboutGetRequest(HttpContext context, string contentPath)
        {
            var response = context.Response;
            var request = context.Request;
            string blobPath = GetBlobPath(request);
            CloudBlob blob = StorageSupport.CurrActiveContainer.GetBlobReference(blobPath);  //publicClient.GetBlobReference(blobPath);
            response.Clear();
            try
            {
                await HandleBlobRequestWithCacheSupport(context, blob, response);
            }
            catch (StorageClientException scEx)
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
                    response.StatusCode = (int)scEx.StatusCode;
                }
            }
            finally
            {
                //response.End();
                context.ApplicationInstance.CompleteRequest();
            }

        }

        private async Task HandleOwnerGetRequest(IContainerOwner containerOwner, HttpContext context, string contentPath)
        {
            if(String.IsNullOrEmpty(contentPath) == false && contentPath.EndsWith("/") == false)
                validateThatOwnerGetComesFromSameReferer(containerOwner, context.Request, contentPath);
            if ((context.Request.Url.Host == "localhost" || context.Request.Url.Host == "localdev") && 
                (contentPath.Contains("groupmanagement/") || 
                contentPath.Contains("wwwsite/") || 
                contentPath.Contains("webview/") ||
                (contentPath.Contains("webui/") && containerOwner is TBAccount) ||
                contentPath.StartsWith("cpanel/") ||
                (contentPath.Contains("foundation-one/") && containerOwner is TBAccount) ||
                contentPath.Contains("categoriesandcontent/") ||
                contentPath.Contains("controlpanel_comments_v1/")))
            {
                await HandleFileSystemGetRequest(containerOwner, context, contentPath);
                return;
            }
            if (String.IsNullOrEmpty(contentPath) || contentPath.EndsWith("/"))
            {
                CloudBlob redirectBlob = StorageSupport.GetOwnerBlobReference(containerOwner, contentPath +
                                                                      InstanceConfiguration.RedirectFromFolderFileName);
                string redirectToUrl = null;
                try
                {
                    redirectToUrl = redirectBlob.DownloadText();
                }
                catch
                {
                    
                }
                if (redirectToUrl == null)
                {
                    if (containerOwner.IsAccountContainer())
                        redirectToUrl = InstanceConfiguration.AccountDefaultRedirect;
                    else
                        redirectToUrl = InstanceConfiguration.GroupDefaultRedirect;
                }
                context.Response.Redirect(redirectToUrl, true);
                return;
            }
            if (contentPath.Contains("/MediaContent/"))
            {
                int lastIndexOfSlash = contentPath.LastIndexOf('/');
                var strippedPath = contentPath.Substring(0, lastIndexOfSlash);
                int lastIndexOfMediaContent = strippedPath.LastIndexOf("/MediaContent/");
                if (lastIndexOfMediaContent > 0) // Still found MediaContent after stripping the last slash
                    contentPath = strippedPath;
            }
            CloudBlob blob = StorageSupport.GetOwnerBlobReference(containerOwner, contentPath);
            var response = context.Response;
            // Read blob content to response.
            response.Clear();
            try
            {
                await HandleBlobRequestWithCacheSupport(context, blob, response);
            } catch(StorageClientException scEx)
            {
                if(scEx.ErrorCode == StorageErrorCode.BlobNotFound || scEx.ErrorCode == StorageErrorCode.ResourceNotFound || scEx.ErrorCode == StorageErrorCode.BadRequest)
                {
                    response.Write("Blob not found or bad request: " + blob.Name + " (original path: " + context.Request.Path + ")");
                    response.StatusCode = (int)scEx.StatusCode;
                } else
                {
                    response.Write("Error code: " + scEx.ErrorCode.ToString() + Environment.NewLine);
                    response.Write(scEx.ToString());
                    response.StatusCode = (int)scEx.StatusCode;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                string errMsg = ex.ToString();
                response.Write(errMsg);
            }
            context.ApplicationInstance.CompleteRequest();
        }

        private static async Task HandleBlobRequestWithCacheSupport(HttpContext context, CloudBlob blob, HttpResponse response)
        {
            // Set the cache request properties as IIS will include them regardless for now
            // even when we wouldn't want them on 304 response...
            //var request = context.Request;
            //request.
            response.Cache.SetMaxAge(TimeSpan.FromMinutes(0));
            response.Cache.SetCacheability(HttpCacheability.Private);
            var request = context.Request;
            await Task.Factory.FromAsync(blob.BeginFetchAttributes, blob.EndFetchAttributes, null);
            //blob.FetchAttributes();
            string ifNoneMatch = request.Headers["If-None-Match"];
            string ifModifiedSince = request.Headers["If-Modified-Since"];
            if (ifNoneMatch != null)
            {
                if (ifNoneMatch == blob.Properties.ETag)
                {
                    response.ClearContent();
                    response.StatusCode = 304;
                    return;
                }
            }
            else if (ifModifiedSince != null)
            {
                DateTime ifModifiedSinceValue;
                if (DateTime.TryParse(ifModifiedSince, out ifModifiedSinceValue))
                {
                    ifModifiedSinceValue = ifModifiedSinceValue.ToUniversalTime();
                    if (blob.Properties.LastModifiedUtc <= ifModifiedSinceValue)
                    {
                        response.ClearContent();
                        response.StatusCode = 304;
                        return;
                    }
                }
            }
            var fileName = blob.Name.Contains("/MediaContent/") ?
                request.Path : blob.Name;
            response.ContentType = StorageSupport.GetMimeType(fileName);
            //response.Cache.SetETag(blob.Properties.ETag);
            response.Headers.Add("ETag", blob.Properties.ETag);
            response.Cache.SetLastModified(blob.Properties.LastModifiedUtc);
            //blob.DownloadToStream(response.OutputStream);
            await Task.Factory.FromAsync(blob.BeginDownloadToStream, blob.EndDownloadToStream, response.OutputStream,
                    null);
        }

        private void validateThatOwnerGetComesFromSameReferer(IContainerOwner containerOwner, HttpRequest request, string contentPath)
        {
            bool isGroupRequest = containerOwner.IsGroupContainer();
            string requestGroupID = isGroupRequest ? containerOwner.LocationPrefix : null;
            bool isAccountRequest = !isGroupRequest;
            var urlReferrer = request.UrlReferrer;
            string[] groupTemplates = InstanceConfiguration.DefaultGroupTemplateList;
            string[] accountTemplates = InstanceConfiguration.DefaultAccountTemplateList;
            var refererPath = urlReferrer != null && urlReferrer.Host == request.Url.Host ? urlReferrer.AbsolutePath : "";
            bool refererIsAccount = refererPath.StartsWith("/auth/account/");
            bool refererIsGroup = refererPath.StartsWith("/auth/grp/");

            if (String.IsNullOrEmpty(InstanceConfiguration.AllowDirectServingRegexp) == false)
            {
                if (Regex.IsMatch(contentPath, InstanceConfiguration.AllowDirectServingRegexp, RegexOptions.Compiled))
                    return;
            }

            if (isGroupRequest)
            {
                bool defaultMatch = groupTemplates.Any(contentPath.StartsWith);
                if (defaultMatch && (refererIsAccount == false && refererIsGroup == false))
                    return;
            }
            else 
            {
                bool defaultMatch = accountTemplates.Any(contentPath.StartsWith);
                if (defaultMatch && (refererIsAccount == false && refererIsGroup == false))
                    return;
            }
            if (urlReferrer == null)
            {
                if (contentPath.StartsWith("customui_") || contentPath.StartsWith("DEV_") ||
                    contentPath.StartsWith("webview/") || contentPath.StartsWith("wwwsite/") ||
                    contentPath.EndsWith(".html"))
                    return;
                throw new SecurityException(
                    "Url referer required for non-default template requests, that target other than customui_ folder");
            }
            if (refererIsAccount && isAccountRequest)
                return;
            if (refererPath.StartsWith("/about/"))
                return;
            if (refererIsAccount == false && refererIsGroup == false) // referer is neither account nor group from this platform
            {
                if (contentPath.EndsWith("/") || contentPath.EndsWith(".html"))
                    return;
                throw new SecurityException("Url referring outside the platform is not allowed except for .html files");
            }
            // At this point we have referer either account or group, accept any plain html request
            if (contentPath.EndsWith(".html"))
                return;
            string refererOwnerPath = request.GetReferrerOwnerContentPath();
            // Accept account and group referers of default templates
            if (refererIsAccount && accountTemplates.Any(refererOwnerPath.StartsWith))
                return;
            if (refererIsGroup && groupTemplates.Any(refererOwnerPath.StartsWith))
                return;
            // Custom referers
            if (refererIsAccount)
            {
                throw new SecurityException("Non-default account referer accessing non-account content");
            } 
            else // Referer is group
            {
                if(isAccountRequest)
                    throw new SecurityException("Non-default group referer accessing account content");
                string refererGroupID = request.GetGroupID();
                if(refererGroupID != requestGroupID)
                    throw new SecurityException("Non-default group referer accessing other group content");
            }
        }

        private async Task HandleFileSystemGetRequest(IContainerOwner containerOwner, HttpContext context, string contentPath)
        {
            bool isAccount = containerOwner is TBAccount;
            var response = context.Response;
            string contentType = StorageSupport.GetMimeType(Path.GetExtension(contentPath));
            response.ContentType = contentType;
            string prefixStrippedContent = contentPath; //.Substring(AuthGroupPrefixLen + GuidIDLen + 1);
            string LocalWebRootFolder = @"d:\UserData\Kalle\WebstormProjects\OIPTemplates\UI\groupmanagement\";
            string LocalWebCatConFolder = @"d:\UserData\Kalle\WebstormProjects\OIPTemplates\UI\categoriesandcontent\";
            //string LocalWwwSiteFolder = @"d:\UserData\Kalle\WebstormProjects\CustomerWww\EarthhouseWww\UI\earthhouse\";
            //string LocalWwwSiteFolder = @"d:\UserData\Kalle\WebstormProjects\CustomerWww\FOIPWww\UI\foip\";
            string LocalWwwSiteFolder = @"d:\UserData\Kalle\WebstormProjects\OIPTemplates\UI\webpresence_welearnit\";
            string LocalOIPAccountFolder = @"d:\UserData\Kalle\WebstormProjects\OIPTemplates\UI\account\";
            string LocalFoundationOneAccountFolder = @"d:\UserData\Kalle\WebstormProjects\OIPTemplates\UI\foundation-one\";
            string LocalGroupControlPanelFolder = @"D:\UserData\Kalle\WebstormProjects\TheBallMeWeb\WebTemplates\CPanel\";
            string LocalAccountControlPanelFolder = @"D:\UserData\Kalle\WebstormProjects\TheBallMeWeb\WebTemplates\CPanelAccount_iZENZEi\";
            //string LocalAccountControlPanelFolder = @"D:\UserData\Kalle\WebstormProjects\TheBallMeWeb\WebTemplates\CPanelAccount\";
            string LocalOIPControlPanelFolder = @"d:\UserData\Kalle\WebstormProjects\agi-oip-ng\WebTemplates\controlpanel_comments_v1\";
            string fileName;
            if (prefixStrippedContent.Contains("groupmanagement/"))
                fileName = prefixStrippedContent.Replace("groupmanagement/", LocalWebRootFolder);
            else if (prefixStrippedContent.Contains("webui/"))
                fileName = prefixStrippedContent.Replace("webui/", LocalOIPAccountFolder);
            else if (prefixStrippedContent.Contains("cpanel/"))
            {
                if(isAccount)
                    fileName = prefixStrippedContent.Replace("cpanel/", LocalAccountControlPanelFolder);
                else
                    fileName = prefixStrippedContent.Replace("cpanel/", LocalGroupControlPanelFolder);
            }
            else if (prefixStrippedContent.Contains("foundation-one/"))
                fileName = prefixStrippedContent.Replace("foundation-one/", LocalFoundationOneAccountFolder);
            else if (prefixStrippedContent.Contains("categoriesandcontent/"))
                fileName = prefixStrippedContent.Replace("categoriesandcontent/", LocalWebCatConFolder);
            else if (prefixStrippedContent.Contains("wwwsite/"))
                fileName = prefixStrippedContent.Replace("wwwsite/", LocalWwwSiteFolder);
            else if (prefixStrippedContent.Contains("controlpanel_comments_v1/"))
                fileName = prefixStrippedContent.Replace("controlpanel_comments_v1/", LocalOIPControlPanelFolder);
            else
                fileName = prefixStrippedContent.Replace("webview/", LocalWwwSiteFolder);
            if (File.Exists(fileName))
            {
                var lastModified = File.GetLastWriteTimeUtc(fileName);
                lastModified = lastModified.AddTicks(-(lastModified.Ticks % TimeSpan.TicksPerSecond)); ;
                //response.Headers.Add("ETag", blob.Properties.ETag);
                //response.Headers.Add("Last-Modified", blob.Properties.LastModifiedUtc.ToString("R"));
                string ifModifiedSince = context.Request.Headers["If-Modified-Since"];
                if (ifModifiedSince != null)
                {
                    DateTime ifModifiedSinceValue;
                    if (DateTime.TryParse(ifModifiedSince, out ifModifiedSinceValue))
                    {
                        ifModifiedSinceValue = ifModifiedSinceValue.ToUniversalTime();
                        if (lastModified <= ifModifiedSinceValue)
                        {
                            response.ClearContent();
                            response.ClearHeaders();
                            response.StatusCode = 304;
                            return;
                        }
                    }
                }
                response.Cache.SetMaxAge(TimeSpan.FromMinutes(0));
                response.Cache.SetLastModified(lastModified);
                response.Cache.SetCacheability(HttpCacheability.Private);
                using(var fileStream = File.OpenRead(fileName))
                    await fileStream.CopyToAsync(context.Response.OutputStream);
            }
            else
            {
                response.StatusCode = 404;
            }
            context.ApplicationInstance.CompleteRequest();
        }

        #endregion
    }

}
