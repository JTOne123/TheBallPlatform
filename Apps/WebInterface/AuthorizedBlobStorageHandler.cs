﻿using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using AaltoGlobalImpact.OIP;
using DotNetOpenAuth.OpenId.RelyingParty;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using TheBall;

namespace WebInterface
{
    public class AuthorizedBlobStorageHandler : IHttpHandler
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

        private const string AuthPersonalPrefix = "/auth/personal/";
        private const string AuthGroupPrefix = "/auth/grp/";
        private const string AuthAccountPrefix = "/auth/acc/";
        private const string AuthProcPrefix = "/auth/proc/";
        private const string AuthPrefix = "/auth/";
        private const string AuthEmailValidation = "/auth/emailvalidation/";
        private int AuthGroupPrefixLen;
        private int AuthPersonalPrefixLen;
        private int AuthAccountPrefixLen;
        private int AuthProcPrefixLen;
        private int AuthPrefixLen;
        private int AuthEmailValidationLen;
        private int GuidIDLen;


        public AuthorizedBlobStorageHandler()
        {
            AuthGroupPrefixLen = AuthGroupPrefix.Length;
            AuthPersonalPrefixLen = AuthPersonalPrefix.Length;
            AuthAccountPrefixLen = AuthAccountPrefix.Length;
            AuthProcPrefixLen = AuthProcPrefix.Length;
            AuthPrefixLen = AuthPrefix.Length;
            AuthEmailValidationLen = AuthEmailValidation.Length;
            GuidIDLen = Guid.Empty.ToString().Length;
        }

        public void ProcessRequest(HttpContext context)
        {
            string user = context.User.Identity.Name;
            bool isAuthenticated = String.IsNullOrEmpty(user) == false;
            if (isAuthenticated == false)
            {
                return;
            }
            HttpRequest request = context.Request;
            if(request.Path.StartsWith(AuthPersonalPrefix))
            {
                HandlePersonalRequest(context);
            } else if(request.Path.StartsWith(AuthGroupPrefix))
            {
                HandleGroupRequest(context);
            } else if(request.Path.StartsWith(AuthProcPrefix))
            {
                HandleProcRequest(context);
            } else if(request.Path.StartsWith(AuthAccountPrefix))
            {
                HandleAccountRequest(context);
            } else if(request.Path.StartsWith(AuthEmailValidation))
            {
                HandleEmailValidation(context);
            }
            return;
        }

        private void HandleEmailValidation(HttpContext context)
        {
            TBRLoginRoot loginRoot = GetOrCreateLoginRoot(context);
            string requestPath = context.Request.Path;
            string emailValidationID = requestPath.Substring(AuthEmailValidationLen);
            TBAccount account = loginRoot.Account;
            TBEmailValidation emailValidation = TBEmailValidation.RetrieveFromDefaultLocation(emailValidationID, account);
            if (emailValidation == null)
                return;
            StorageSupport.DeleteInformationObject(emailValidation, account);
            if (emailValidation.ValidUntil < DateTime.Now)
            {
                // TODO: Some invalidation message + UTC time
                StorageSupport.DeleteInformationObject(emailValidation, account);
                throw new TimeoutException("Email validation expired at: " + emailValidation.ToString());
            }
            if(account.Emails.CollectionContent.Find(candidate => candidate.EmailAddress.ToLower() == emailValidation.Email.ToLower()) == null)
            {
                TBEmail email = TBEmail.CreateDefault();
                email.EmailAddress = emailValidation.Email;
                email.ValidatedAt = DateTime.Now;
                account.Emails.CollectionContent.Add(email);
                StorageSupport.StoreInformation(loginRoot);

                TBRAccountRoot accountRoot = TBRAccountRoot.RetrieveFromDefaultLocation(account.ID);
                accountRoot.Account = account;
                StorageSupport.StoreInformation(accountRoot);

                string emailRootID = HttpUtility.UrlEncode(email.EmailAddress);
                TBREmailRoot emailRoot = TBREmailRoot.CreateDefault();
                emailRoot.ID = emailRootID;
                emailRoot.UpdateRelativeLocationFromID();
                emailRoot.Account = account;
                StorageSupport.StoreInformation(emailRoot);

                foreach(var tbEmail in account.Emails.CollectionContent)
                {
                    if (tbEmail == email)
                        continue;
                    TBREmailRoot oldRoot = TBREmailRoot.RetrieveFromDefaultLocation(tbEmail.EmailAddress);
                    oldRoot.Account = account;
                    StorageSupport.StoreInformation(oldRoot);
                }
            }

            context.Response.Redirect("/auth/personal/oip-personal-landing-page.phtml", true);
        }

        private void HandleAccountRequest(HttpContext context)
        {
            TBRLoginRoot loginRoot = GetOrCreateLoginRoot(context);
        }

        private void HandleProcRequest(HttpContext context)
        {
            TBRLoginRoot loginRoot = GetOrCreateLoginRoot(context);
            TBAccount account = loginRoot.Account;
            string requestPath = context.Request.Path;
            string contentPath = requestPath.Substring(AuthPrefixLen);
            HandleOwnerRequest(account, context, contentPath);
        }

        private void HandleGroupRequest(HttpContext context)
        {
            string requestPath = context.Request.Path;
            string groupID = GetGroupID(context.Request.Path);
            string loginRootID = TBLoginInfo.GetLoginIDFromLoginURL(context.User.Identity.Name);
            string loginGroupID = TBRLoginGroupRoot.GetLoginGroupID(groupID, loginRootID);
            TBRLoginGroupRoot loginGroupRoot = TBRLoginGroupRoot.RetrieveFromDefaultLocation(loginGroupID);
            if(loginGroupRoot == null)
            {
                // TODO: Polite invitation request
                return;
            }
            string contentPath = requestPath.Substring(AuthGroupPrefixLen + GuidIDLen + 1);
            HandleOwnerRequest(loginGroupRoot, context, contentPath);
        }

        private string GetGroupID(string path)
        {
            return path.Substring(AuthGroupPrefixLen, GuidIDLen);
        }

        private void HandlePersonalRequest(HttpContext context)
        {
            TBRLoginRoot loginRoot = GetOrCreateLoginRoot(context);

            TBAccount account = loginRoot.Account;
            bool hasRegisteredEmail = account.Emails.CollectionContent.Count > 0;
            if(hasRegisteredEmail == false)
            {
                PrepareEmailRegistrationPage(account, context, true);
                context.Response.Redirect("/auth/proc/tbp-layout-registeremail.phtml", true);
                return;
            }
            string requestPath = context.Request.Path;
            string contentPath = requestPath.Substring(AuthPersonalPrefixLen);
            HandleOwnerRequest(account, context, contentPath);
        }

        private void PrepareEmailRegistrationPage(TBAccount account, HttpContext context, bool forceRecreate)
        {
            const string procRegisterEmailPage = "proc/tbp-layout-registeremail.phtml";
            string currPage = StorageSupport.DownloadOwnerBlobText(account, procRegisterEmailPage, true);
            if(currPage == null || forceRecreate)
            {
                string singletonID = Guid.Empty.ToString();
                TBPRegisterEmail registerEmail = TBPRegisterEmail.RetrieveFromDefaultLocation(singletonID, account);
                if(registerEmail == null)
                {
                    registerEmail = TBPRegisterEmail.CreateDefault();
                    registerEmail.ID = singletonID;
                    registerEmail.UpdateRelativeLocationFromID();
                }
                registerEmail.EmailAddress = "";
                StorageSupport.StoreInformation(registerEmail, account);
                string template = StorageSupport.CurrTemplateContainer.DownloadBlobText("theball-proc/tbp-layout-registeremail.phtml");
                string result = RenderWebSupport.RenderTemplateWithContent(template, registerEmail);
                StorageSupport.UploadOwnerBlobText(account, procRegisterEmailPage, result, StorageSupport.InformationType_GenericContentValue);
            }
        }

        private void HandleOwnerRequest(IContainerOwner containerOwner, HttpContext context, string contentPath)
        {
            if (context.Request.RequestType == "POST")
                HandleOwnerPostRequest(containerOwner, context, contentPath);
            else
                HandleOwnerGetRequest(containerOwner, context, contentPath);
        }

        private void HandleOwnerPostRequest(IContainerOwner containerOwner, HttpContext context, string contentPath)
        {
            HttpRequest request = context.Request;
            var form = request.Form;
            string objectTypeName = form["RootObjectType"];
            string objectRelativeLocation = form["RootObjectRelativeLocation"];
            string sourceName = form["RootSourceName"];
            //if (eTag == null)
            //{
            //    throw new InvalidDataException("ETag must be present in submit request for root container object");
            //}
            CloudBlob webPageBlob = StorageSupport.CurrActiveContainer.GetBlob(contentPath, containerOwner);
            InformationSourceCollection sources = webPageBlob.GetBlobInformationSources();
            //var informationObjects = sources.FetchAllInformationObjects();
            if (sourceName == null)
                sourceName = "";
            InformationSource source =
                sources.CollectionContent.First(
                    src => src.IsInformationObjectSource && src.SourceName == sourceName);
            string oldETag = source.SourceETag;
            IInformationObject rootObject = source.RetrieveInformationObject();
            if (oldETag != rootObject.ETag)
                throw new InvalidDataException("Information under editing was modified during display and save");
            rootObject.SetValuesToObjects(form);
            StorageSupport.StoreInformation(rootObject, containerOwner);
            RenderWebSupport.RefreshContent(webPageBlob);
            HandleOwnerGetRequest(containerOwner, context, contentPath);
        }

        private void HandleOwnerGetRequest(IContainerOwner containerOwner, HttpContext context, string contentPath)
        {
            CloudBlob blob = StorageSupport.GetOwnerBlobReference(containerOwner, contentPath);

            // Read blob content to response.
            context.Response.Clear();
            try
            {
                blob.FetchAttributes();
                context.Response.ContentType = blob.Properties.ContentType;
                blob.DownloadToStream(context.Response.OutputStream);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
            context.Response.End();
        }

        private TBRLoginRoot GetOrCreateLoginRoot(HttpContext context)
        {
            string user = context.User.Identity.Name;
            string loginRootID = TBLoginInfo.GetLoginIDFromLoginURL(user);
            TBRLoginRoot loginRoot = TBRLoginRoot.RetrieveFromDefaultLocation(loginRootID);
            if(loginRoot == null)
            {
                TBLoginInfo loginInfo = TBLoginInfo.CreateDefault();
                //loginInfo.ID = loginRootID;
                loginInfo.OpenIDUrl = user;

                TBRAccountRoot accountRoot = TBRAccountRoot.CreateDefault();
                accountRoot.Account.Logins.CollectionContent.Add(loginInfo);
                accountRoot.ID = accountRoot.Account.ID;
                accountRoot.UpdateRelativeLocationFromID();
                StorageSupport.StoreInformation(accountRoot);

                loginRoot = TBRLoginRoot.CreateDefault();
                loginRoot.ID = loginRootID;
                loginRoot.UpdateRelativeLocationFromID();
                loginRoot.Account = accountRoot.Account;
                StorageSupport.StoreInformation(loginRoot);
            }
            HttpContext.Current.Items.Add("Account", loginRoot.Account);
            return loginRoot;
        }

        #endregion
    }
}
