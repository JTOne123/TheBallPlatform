﻿using System;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.Web;
using TheBall;
using TheBall.CORE;

namespace AaltoGlobalImpact.OIP
{
    partial class TBRLoginRoot
    {
        public static TBRLoginRoot GetOrCreateLoginRootWithAccount(string loginUrl, bool isAccountRequest, string currentDomainName)
        {
            string loginRootID = TBLoginInfo.GetLoginIDFromLoginURL(loginUrl);
            var loginRoot = RetrieveFromDefaultLocation(loginRootID);
            if (loginRoot == null)
            {
                // Storing loginroot
                loginRoot = TBRLoginRoot.CreateDefault();
                loginRoot.ID = loginRootID;
                loginRoot.UpdateRelativeLocationFromID();
                loginRoot.DomainName = currentDomainName;
                StorageSupport.StoreInformation(loginRoot);

                // Creating login info for account and storing the account
                TBLoginInfo loginInfo = TBLoginInfo.CreateDefault();
                loginInfo.OpenIDUrl = loginUrl;

                TBRAccountRoot accountRoot = TBRAccountRoot.CreateAndStoreNewAccount();
                accountRoot.Account.Logins.CollectionContent.Add(loginInfo);
                string accountID = accountRoot.ID;
                accountRoot.StoreInformation();

                GenericPrincipal principal = (GenericPrincipal) HttpContext.Current.User;
                TheBallIdentity identity = (TheBallIdentity) principal.Identity;
                string emailAddress = identity.EmailAddress;
                if (emailAddress != null)
                {
                    try
                    {

                        RegisterEmailAddress.Execute(new RegisterEmailAddressParameters
                        {
                            AccountID = accountID,
                            EmailAddress = emailAddress
                        });
                        if (InstanceConfiguration.PlatformDefaultGroupIDList != null)
                        {
                            foreach (var groupToJoinID in InstanceConfiguration.PlatformDefaultGroupIDList)
                            {
                                try
                                {
                                    JoinUserToGroup.Execute(new JoinUserToGroupParameters
                                    {
                                        GroupID = groupToJoinID,
                                        UserEmailAddress = emailAddress,
                                        MemberRole = TBCollaboratorRole.ViewerRoleValue
                                    });
                                }
                                catch
                                {
                                    
                                }
                                
                            }
                        }
                    }
                    catch (Exception)
                    {

                        UpdateAccountRootToReferences.Execute(new UpdateAccountRootToReferencesParameters
                        {
                            AccountID = accountID
                        });
                        UpdateAccountContainerFromAccountRoot.Execute(new UpdateAccountContainerFromAccountRootParameters
                        {
                            AccountID = accountID
                        });
                    }
                }
                else
                {
                    UpdateAccountRootToReferences.Execute(new UpdateAccountRootToReferencesParameters
                    {
                        AccountID = accountID
                    });
                    UpdateAccountContainerFromAccountRoot.Execute(new UpdateAccountContainerFromAccountRootParameters
                    {
                        AccountID = accountID
                    });
                }

                // If this request is for account, we propagate the pages immediately
                bool useBackgroundWorker = isAccountRequest == false;
                //RenderWebSupport.RefreshAccountTemplates(accountRoot.ID, useBackgroundWorker);
                foreach (var templateName in InstanceConfiguration.DefaultAccountTemplateList)
                {
                    RenderWebSupport.RefreshAccountTemplate(accountID, templateName, useBackgroundWorker: useBackgroundWorker);
                }
                accountRoot.Account.InitializeAndConnectMastersAndCollections();
            }
            loginRoot = RetrieveFromDefaultLocation(loginRootID);
            return loginRoot;
        }

    }
}