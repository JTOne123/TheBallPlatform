 


using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using SQLiteSupport;


namespace SQLite.AaltoGlobalImpact.OIP { 
		
	internal interface ITheBallDataContextStorable
	{
		void PrepareForStoring(bool isInitialInsert);
	}

		public class TheBallDataContext : DataContext, IStorageSyncableDataContext
		{

            public TheBallDataContext(SQLiteConnection connection) : base(connection)
		    {
                if(connection.State != ConnectionState.Open)
                    connection.Open();
		    }

            public override void SubmitChanges(ConflictMode failureMode)
            {
                var changeSet = GetChangeSet();
                var insertsToProcess = changeSet.Inserts.Where(insert => insert is ITheBallDataContextStorable).Cast<ITheBallDataContextStorable>().ToArray();
                foreach (var itemToProcess in insertsToProcess)
                    itemToProcess.PrepareForStoring(true);
                var updatesToProcess = changeSet.Updates.Where(update => update is ITheBallDataContextStorable).Cast<ITheBallDataContextStorable>().ToArray();
                foreach (var itemToProcess in updatesToProcess)
                    itemToProcess.PrepareForStoring(false);
                base.SubmitChanges(failureMode);
            }

			public void CreateDomainDatabaseTablesIfNotExists()
			{
				List<string> tableCreationCommands = new List<string>();
                tableCreationCommands.AddRange(InformationObjectMetaData.GetMetaDataTableCreateSQLs());
				tableCreationCommands.Add(TBSystem.GetCreateTableSQL());
				tableCreationCommands.Add(WebPublishInfo.GetCreateTableSQL());
				tableCreationCommands.Add(PublicationPackage.GetCreateTableSQL());
				tableCreationCommands.Add(TBRLoginRoot.GetCreateTableSQL());
				tableCreationCommands.Add(TBRAccountRoot.GetCreateTableSQL());
				tableCreationCommands.Add(TBRGroupRoot.GetCreateTableSQL());
				tableCreationCommands.Add(TBRLoginGroupRoot.GetCreateTableSQL());
				tableCreationCommands.Add(TBREmailRoot.GetCreateTableSQL());
				tableCreationCommands.Add(TBAccount.GetCreateTableSQL());
				tableCreationCommands.Add(TBAccountCollaborationGroup.GetCreateTableSQL());
				tableCreationCommands.Add(TBLoginInfo.GetCreateTableSQL());
				tableCreationCommands.Add(TBEmail.GetCreateTableSQL());
				tableCreationCommands.Add(TBCollaboratorRole.GetCreateTableSQL());
				tableCreationCommands.Add(TBCollaboratingGroup.GetCreateTableSQL());
				tableCreationCommands.Add(TBEmailValidation.GetCreateTableSQL());
				tableCreationCommands.Add(TBMergeAccountConfirmation.GetCreateTableSQL());
				tableCreationCommands.Add(TBGroupJoinConfirmation.GetCreateTableSQL());
				tableCreationCommands.Add(TBDeviceJoinConfirmation.GetCreateTableSQL());
				tableCreationCommands.Add(TBInformationInputConfirmation.GetCreateTableSQL());
				tableCreationCommands.Add(TBInformationOutputConfirmation.GetCreateTableSQL());
				tableCreationCommands.Add(TBRegisterContainer.GetCreateTableSQL());
				tableCreationCommands.Add(LoginProvider.GetCreateTableSQL());
				tableCreationCommands.Add(ContactOipContainer.GetCreateTableSQL());
				tableCreationCommands.Add(TBPRegisterEmail.GetCreateTableSQL());
				tableCreationCommands.Add(JavaScriptContainer.GetCreateTableSQL());
				tableCreationCommands.Add(JavascriptContainer.GetCreateTableSQL());
				tableCreationCommands.Add(FooterContainer.GetCreateTableSQL());
				tableCreationCommands.Add(NavigationContainer.GetCreateTableSQL());
				tableCreationCommands.Add(AccountSummary.GetCreateTableSQL());
				tableCreationCommands.Add(AccountContainer.GetCreateTableSQL());
				tableCreationCommands.Add(AccountIndex.GetCreateTableSQL());
				tableCreationCommands.Add(AccountModule.GetCreateTableSQL());
				tableCreationCommands.Add(ImageGroupContainer.GetCreateTableSQL());
				tableCreationCommands.Add(LocationContainer.GetCreateTableSQL());
				tableCreationCommands.Add(AddressAndLocation.GetCreateTableSQL());
				tableCreationCommands.Add(StreetAddress.GetCreateTableSQL());
				tableCreationCommands.Add(AccountContent.GetCreateTableSQL());
				tableCreationCommands.Add(AccountProfile.GetCreateTableSQL());
				tableCreationCommands.Add(AccountSecurity.GetCreateTableSQL());
				tableCreationCommands.Add(AccountRoles.GetCreateTableSQL());
				tableCreationCommands.Add(PersonalInfoVisibility.GetCreateTableSQL());
				tableCreationCommands.Add(GroupedInformation.GetCreateTableSQL());
				tableCreationCommands.Add(ReferenceToInformation.GetCreateTableSQL());
				tableCreationCommands.Add(BlogContainer.GetCreateTableSQL());
				tableCreationCommands.Add(RecentBlogSummary.GetCreateTableSQL());
				tableCreationCommands.Add(NodeSummaryContainer.GetCreateTableSQL());
				tableCreationCommands.Add(RenderedNode.GetCreateTableSQL());
				tableCreationCommands.Add(ShortTextObject.GetCreateTableSQL());
				tableCreationCommands.Add(LongTextObject.GetCreateTableSQL());
				tableCreationCommands.Add(MapContainer.GetCreateTableSQL());
				tableCreationCommands.Add(MapMarker.GetCreateTableSQL());
				tableCreationCommands.Add(CalendarContainer.GetCreateTableSQL());
				tableCreationCommands.Add(AboutContainer.GetCreateTableSQL());
				tableCreationCommands.Add(OBSAccountContainer.GetCreateTableSQL());
				tableCreationCommands.Add(ProjectContainer.GetCreateTableSQL());
				tableCreationCommands.Add(CourseContainer.GetCreateTableSQL());
				tableCreationCommands.Add(ContainerHeader.GetCreateTableSQL());
				tableCreationCommands.Add(ActivitySummaryContainer.GetCreateTableSQL());
				tableCreationCommands.Add(ActivityIndex.GetCreateTableSQL());
				tableCreationCommands.Add(ActivityContainer.GetCreateTableSQL());
				tableCreationCommands.Add(Activity.GetCreateTableSQL());
				tableCreationCommands.Add(Moderator.GetCreateTableSQL());
				tableCreationCommands.Add(Collaborator.GetCreateTableSQL());
				tableCreationCommands.Add(GroupSummaryContainer.GetCreateTableSQL());
				tableCreationCommands.Add(GroupContainer.GetCreateTableSQL());
				tableCreationCommands.Add(GroupIndex.GetCreateTableSQL());
				tableCreationCommands.Add(AddAddressAndLocationInfo.GetCreateTableSQL());
				tableCreationCommands.Add(AddImageInfo.GetCreateTableSQL());
				tableCreationCommands.Add(AddImageGroupInfo.GetCreateTableSQL());
				tableCreationCommands.Add(AddEmailAddressInfo.GetCreateTableSQL());
				tableCreationCommands.Add(CreateGroupInfo.GetCreateTableSQL());
				tableCreationCommands.Add(AddActivityInfo.GetCreateTableSQL());
				tableCreationCommands.Add(AddBlogPostInfo.GetCreateTableSQL());
				tableCreationCommands.Add(AddCategoryInfo.GetCreateTableSQL());
				tableCreationCommands.Add(Group.GetCreateTableSQL());
				tableCreationCommands.Add(Introduction.GetCreateTableSQL());
				tableCreationCommands.Add(ContentCategoryRank.GetCreateTableSQL());
				tableCreationCommands.Add(LinkToContent.GetCreateTableSQL());
				tableCreationCommands.Add(EmbeddedContent.GetCreateTableSQL());
				tableCreationCommands.Add(DynamicContentGroup.GetCreateTableSQL());
				tableCreationCommands.Add(DynamicContent.GetCreateTableSQL());
				tableCreationCommands.Add(AttachedToObject.GetCreateTableSQL());
				tableCreationCommands.Add(Comment.GetCreateTableSQL());
				tableCreationCommands.Add(Selection.GetCreateTableSQL());
				tableCreationCommands.Add(TextContent.GetCreateTableSQL());
				tableCreationCommands.Add(Blog.GetCreateTableSQL());
				tableCreationCommands.Add(BlogIndexGroup.GetCreateTableSQL());
				tableCreationCommands.Add(CalendarIndex.GetCreateTableSQL());
				tableCreationCommands.Add(Filter.GetCreateTableSQL());
				tableCreationCommands.Add(Calendar.GetCreateTableSQL());
				tableCreationCommands.Add(Map.GetCreateTableSQL());
				tableCreationCommands.Add(MapIndexCollection.GetCreateTableSQL());
				tableCreationCommands.Add(MapResult.GetCreateTableSQL());
				tableCreationCommands.Add(MapResultsCollection.GetCreateTableSQL());
				tableCreationCommands.Add(Video.GetCreateTableSQL());
				tableCreationCommands.Add(Image.GetCreateTableSQL());
				tableCreationCommands.Add(BinaryFile.GetCreateTableSQL());
				tableCreationCommands.Add(ImageGroup.GetCreateTableSQL());
				tableCreationCommands.Add(VideoGroup.GetCreateTableSQL());
				tableCreationCommands.Add(Tooltip.GetCreateTableSQL());
				tableCreationCommands.Add(SocialPanel.GetCreateTableSQL());
				tableCreationCommands.Add(Longitude.GetCreateTableSQL());
				tableCreationCommands.Add(Latitude.GetCreateTableSQL());
				tableCreationCommands.Add(Location.GetCreateTableSQL());
				tableCreationCommands.Add(Date.GetCreateTableSQL());
				tableCreationCommands.Add(Sex.GetCreateTableSQL());
				tableCreationCommands.Add(OBSAddress.GetCreateTableSQL());
				tableCreationCommands.Add(Identity.GetCreateTableSQL());
				tableCreationCommands.Add(ImageVideoSoundVectorRaw.GetCreateTableSQL());
				tableCreationCommands.Add(CategoryContainer.GetCreateTableSQL());
				tableCreationCommands.Add(Category.GetCreateTableSQL());
				tableCreationCommands.Add(Subscription.GetCreateTableSQL());
				tableCreationCommands.Add(QueueEnvelope.GetCreateTableSQL());
				tableCreationCommands.Add(OperationRequest.GetCreateTableSQL());
				tableCreationCommands.Add(SubscriptionChainRequestMessage.GetCreateTableSQL());
				tableCreationCommands.Add(SubscriptionChainRequestContent.GetCreateTableSQL());
				tableCreationCommands.Add(SubscriptionTarget.GetCreateTableSQL());
				tableCreationCommands.Add(DeleteEntireOwnerOperation.GetCreateTableSQL());
				tableCreationCommands.Add(DeleteOwnerContentOperation.GetCreateTableSQL());
				tableCreationCommands.Add(SystemError.GetCreateTableSQL());
				tableCreationCommands.Add(SystemErrorItem.GetCreateTableSQL());
				tableCreationCommands.Add(InformationSource.GetCreateTableSQL());
				tableCreationCommands.Add(RefreshDefaultViewsOperation.GetCreateTableSQL());
				tableCreationCommands.Add(UpdateWebContentOperation.GetCreateTableSQL());
				tableCreationCommands.Add(UpdateWebContentHandlerItem.GetCreateTableSQL());
				tableCreationCommands.Add(PublishWebContentOperation.GetCreateTableSQL());
				tableCreationCommands.Add(SubscriberInput.GetCreateTableSQL());
				tableCreationCommands.Add(Monitor.GetCreateTableSQL());
				tableCreationCommands.Add(IconTitleDescription.GetCreateTableSQL());
				tableCreationCommands.Add(AboutAGIApplications.GetCreateTableSQL());
			    var connection = this.Connection;
				foreach (string commandText in tableCreationCommands)
			    {
			        var command = connection.CreateCommand();
			        command.CommandText = commandText;
                    command.CommandType = CommandType.Text;
			        command.ExecuteNonQuery();
			    }
			}

			public Table<InformationObjectMetaData> InformationObjectMetaDataTable {
				get {
					return this.GetTable<InformationObjectMetaData>();
				}
			}

			public void PerformUpdate(string storageRootPath, InformationObjectMetaData updateData)
		    {
                if(updateData.SemanticDomain != "TheBall.Payments")
                    throw new InvalidDataException("Mismatch on domain data");
		        if (updateData.ObjectType == "TBSystem")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBSystem.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBSystemTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.InstanceName = serializedObject.InstanceName;
		            existingObject.AdminGroupID = serializedObject.AdminGroupID;
		            return;
		        } 
		        if (updateData.ObjectType == "WebPublishInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.WebPublishInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = WebPublishInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.PublishType = serializedObject.PublishType;
		            existingObject.PublishContainer = serializedObject.PublishContainer;
		            existingObject.ActivePublication = serializedObject.ActivePublication;
		            existingObject.Publications = serializedObject.Publications;
		            return;
		        } 
		        if (updateData.ObjectType == "PublicationPackage")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.PublicationPackage.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = PublicationPackageTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.PackageName = serializedObject.PackageName;
		            existingObject.PublicationTime = serializedObject.PublicationTime;
		            return;
		        } 
		        if (updateData.ObjectType == "TBRLoginRoot")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBRLoginRoot.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBRLoginRootTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.DomainName = serializedObject.DomainName;
		            existingObject.Account = serializedObject.Account;
		            return;
		        } 
		        if (updateData.ObjectType == "TBRAccountRoot")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBRAccountRoot.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBRAccountRootTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Account = serializedObject.Account;
		            return;
		        } 
		        if (updateData.ObjectType == "TBRGroupRoot")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBRGroupRoot.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBRGroupRootTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Group = serializedObject.Group;
		            return;
		        } 
		        if (updateData.ObjectType == "TBRLoginGroupRoot")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBRLoginGroupRoot.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBRLoginGroupRootTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Role = serializedObject.Role;
		            existingObject.GroupID = serializedObject.GroupID;
		            return;
		        } 
		        if (updateData.ObjectType == "TBREmailRoot")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBREmailRoot.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBREmailRootTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Account = serializedObject.Account;
		            return;
		        } 
		        if (updateData.ObjectType == "TBAccount")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBAccount.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBAccountTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Emails = serializedObject.Emails;
		            existingObject.Logins = serializedObject.Logins;
		            existingObject.GroupRoleCollection = serializedObject.GroupRoleCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "TBAccountCollaborationGroup")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBAccountCollaborationGroup.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBAccountCollaborationGroupTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.GroupID = serializedObject.GroupID;
		            existingObject.GroupRole = serializedObject.GroupRole;
		            existingObject.RoleStatus = serializedObject.RoleStatus;
		            return;
		        } 
		        if (updateData.ObjectType == "TBLoginInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBLoginInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBLoginInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.OpenIDUrl = serializedObject.OpenIDUrl;
		            return;
		        } 
		        if (updateData.ObjectType == "TBEmail")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBEmail.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBEmailTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.EmailAddress = serializedObject.EmailAddress;
		            existingObject.ValidatedAt = serializedObject.ValidatedAt;
		            return;
		        } 
		        if (updateData.ObjectType == "TBCollaboratorRole")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBCollaboratorRole.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBCollaboratorRoleTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Email = serializedObject.Email;
		            existingObject.Role = serializedObject.Role;
		            existingObject.RoleStatus = serializedObject.RoleStatus;
		            return;
		        } 
		        if (updateData.ObjectType == "TBCollaboratingGroup")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBCollaboratingGroup.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBCollaboratingGroupTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            existingObject.Roles = serializedObject.Roles;
		            return;
		        } 
		        if (updateData.ObjectType == "TBEmailValidation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBEmailValidation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBEmailValidationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Email = serializedObject.Email;
		            existingObject.AccountID = serializedObject.AccountID;
		            existingObject.ValidUntil = serializedObject.ValidUntil;
		            existingObject.GroupJoinConfirmation = serializedObject.GroupJoinConfirmation;
		            existingObject.DeviceJoinConfirmation = serializedObject.DeviceJoinConfirmation;
		            existingObject.InformationInputConfirmation = serializedObject.InformationInputConfirmation;
		            existingObject.InformationOutputConfirmation = serializedObject.InformationOutputConfirmation;
		            existingObject.MergeAccountsConfirmation = serializedObject.MergeAccountsConfirmation;
		            existingObject.RedirectUrlAfterValidation = serializedObject.RedirectUrlAfterValidation;
		            return;
		        } 
		        if (updateData.ObjectType == "TBMergeAccountConfirmation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBMergeAccountConfirmation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBMergeAccountConfirmationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.AccountToBeMergedID = serializedObject.AccountToBeMergedID;
		            existingObject.AccountToMergeToID = serializedObject.AccountToMergeToID;
		            return;
		        } 
		        if (updateData.ObjectType == "TBGroupJoinConfirmation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBGroupJoinConfirmation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBGroupJoinConfirmationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.GroupID = serializedObject.GroupID;
		            existingObject.InvitationMode = serializedObject.InvitationMode;
		            return;
		        } 
		        if (updateData.ObjectType == "TBDeviceJoinConfirmation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBDeviceJoinConfirmation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBDeviceJoinConfirmationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.GroupID = serializedObject.GroupID;
		            existingObject.AccountID = serializedObject.AccountID;
		            existingObject.DeviceMembershipID = serializedObject.DeviceMembershipID;
		            return;
		        } 
		        if (updateData.ObjectType == "TBInformationInputConfirmation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBInformationInputConfirmation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBInformationInputConfirmationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.GroupID = serializedObject.GroupID;
		            existingObject.AccountID = serializedObject.AccountID;
		            existingObject.InformationInputID = serializedObject.InformationInputID;
		            return;
		        } 
		        if (updateData.ObjectType == "TBInformationOutputConfirmation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBInformationOutputConfirmation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBInformationOutputConfirmationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.GroupID = serializedObject.GroupID;
		            existingObject.AccountID = serializedObject.AccountID;
		            existingObject.InformationOutputID = serializedObject.InformationOutputID;
		            return;
		        } 
		        if (updateData.ObjectType == "TBRegisterContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBRegisterContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBRegisterContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Header = serializedObject.Header;
		            existingObject.ReturnUrl = serializedObject.ReturnUrl;
		            existingObject.LoginProviderCollection = serializedObject.LoginProviderCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "LoginProvider")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.LoginProvider.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = LoginProviderTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ProviderName = serializedObject.ProviderName;
		            existingObject.ProviderIconClass = serializedObject.ProviderIconClass;
		            existingObject.ProviderType = serializedObject.ProviderType;
		            existingObject.ProviderUrl = serializedObject.ProviderUrl;
		            existingObject.ReturnUrl = serializedObject.ReturnUrl;
		            return;
		        } 
		        if (updateData.ObjectType == "ContactOipContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ContactOipContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ContactOipContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.OIPModeratorGroupID = serializedObject.OIPModeratorGroupID;
		            return;
		        } 
		        if (updateData.ObjectType == "TBPRegisterEmail")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TBPRegisterEmail.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TBPRegisterEmailTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.EmailAddress = serializedObject.EmailAddress;
		            return;
		        } 
		        if (updateData.ObjectType == "JavaScriptContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.JavaScriptContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = JavaScriptContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.HtmlContent = serializedObject.HtmlContent;
		            return;
		        } 
		        if (updateData.ObjectType == "JavascriptContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.JavascriptContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = JavascriptContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.HtmlContent = serializedObject.HtmlContent;
		            return;
		        } 
		        if (updateData.ObjectType == "FooterContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.FooterContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = FooterContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.HtmlContent = serializedObject.HtmlContent;
		            return;
		        } 
		        if (updateData.ObjectType == "NavigationContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.NavigationContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = NavigationContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Dummy = serializedObject.Dummy;
		            return;
		        } 
		        if (updateData.ObjectType == "AccountSummary")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AccountSummary.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AccountSummaryTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.ActivitySummary = serializedObject.ActivitySummary;
		            existingObject.GroupSummary = serializedObject.GroupSummary;
		            return;
		        } 
		        if (updateData.ObjectType == "AccountContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AccountContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AccountContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Header = serializedObject.Header;
		            existingObject.AccountIndex = serializedObject.AccountIndex;
		            existingObject.AccountModule = serializedObject.AccountModule;
		            existingObject.AccountSummary = serializedObject.AccountSummary;
		            return;
		        } 
		        if (updateData.ObjectType == "AccountIndex")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AccountIndex.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AccountIndexTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Icon = serializedObject.Icon;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.Summary = serializedObject.Summary;
		            return;
		        } 
		        if (updateData.ObjectType == "AccountModule")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AccountModule.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AccountModuleTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Profile = serializedObject.Profile;
		            existingObject.Security = serializedObject.Security;
		            existingObject.Roles = serializedObject.Roles;
		            existingObject.LocationCollection = serializedObject.LocationCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "ImageGroupContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ImageGroupContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ImageGroupContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ImageGroups = serializedObject.ImageGroups;
		            return;
		        } 
		        if (updateData.ObjectType == "LocationContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.LocationContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = LocationContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Locations = serializedObject.Locations;
		            return;
		        } 
		        if (updateData.ObjectType == "AddressAndLocation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AddressAndLocation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AddressAndLocationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            existingObject.Address = serializedObject.Address;
		            existingObject.Location = serializedObject.Location;
		            return;
		        } 
		        if (updateData.ObjectType == "StreetAddress")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.StreetAddress.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = StreetAddressTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Street = serializedObject.Street;
		            existingObject.ZipCode = serializedObject.ZipCode;
		            existingObject.Town = serializedObject.Town;
		            existingObject.Country = serializedObject.Country;
		            return;
		        } 
		        if (updateData.ObjectType == "AccountContent")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AccountContent.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AccountContentTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Dummy = serializedObject.Dummy;
		            return;
		        } 
		        if (updateData.ObjectType == "AccountProfile")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AccountProfile.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AccountProfileTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ProfileImage = serializedObject.ProfileImage;
		            existingObject.FirstName = serializedObject.FirstName;
		            existingObject.LastName = serializedObject.LastName;
		            existingObject.Address = serializedObject.Address;
		            existingObject.IsSimplifiedAccount = serializedObject.IsSimplifiedAccount;
		            existingObject.SimplifiedAccountEmail = serializedObject.SimplifiedAccountEmail;
		            existingObject.SimplifiedAccountGroupID = serializedObject.SimplifiedAccountGroupID;
		            return;
		        } 
		        if (updateData.ObjectType == "AccountSecurity")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AccountSecurity.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AccountSecurityTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.LoginInfoCollection = serializedObject.LoginInfoCollection;
		            existingObject.EmailCollection = serializedObject.EmailCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "AccountRoles")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AccountRoles.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AccountRolesTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ModeratorInGroups = serializedObject.ModeratorInGroups;
		            existingObject.MemberInGroups = serializedObject.MemberInGroups;
		            existingObject.OrganizationsImPartOf = serializedObject.OrganizationsImPartOf;
		            return;
		        } 
		        if (updateData.ObjectType == "PersonalInfoVisibility")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.PersonalInfoVisibility.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = PersonalInfoVisibilityTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.NoOne_Network_All = serializedObject.NoOne_Network_All;
		            return;
		        } 
		        if (updateData.ObjectType == "GroupedInformation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.GroupedInformation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = GroupedInformationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.GroupName = serializedObject.GroupName;
		            existingObject.ReferenceCollection = serializedObject.ReferenceCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "ReferenceToInformation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ReferenceToInformation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ReferenceToInformationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            existingObject.URL = serializedObject.URL;
		            return;
		        } 
		        if (updateData.ObjectType == "BlogContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.BlogContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = BlogContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Header = serializedObject.Header;
		            existingObject.FeaturedBlog = serializedObject.FeaturedBlog;
		            existingObject.RecentBlogSummary = serializedObject.RecentBlogSummary;
		            existingObject.BlogIndexGroup = serializedObject.BlogIndexGroup;
		            return;
		        } 
		        if (updateData.ObjectType == "RecentBlogSummary")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.RecentBlogSummary.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = RecentBlogSummaryTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.RecentBlogCollection = serializedObject.RecentBlogCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "NodeSummaryContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.NodeSummaryContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = NodeSummaryContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Nodes = serializedObject.Nodes;
		            existingObject.NodeSourceBlogs = serializedObject.NodeSourceBlogs;
		            existingObject.NodeSourceActivities = serializedObject.NodeSourceActivities;
		            existingObject.NodeSourceTextContent = serializedObject.NodeSourceTextContent;
		            existingObject.NodeSourceLinkToContent = serializedObject.NodeSourceLinkToContent;
		            existingObject.NodeSourceEmbeddedContent = serializedObject.NodeSourceEmbeddedContent;
		            existingObject.NodeSourceImages = serializedObject.NodeSourceImages;
		            existingObject.NodeSourceBinaryFiles = serializedObject.NodeSourceBinaryFiles;
		            existingObject.NodeSourceCategories = serializedObject.NodeSourceCategories;
		            return;
		        } 
		        if (updateData.ObjectType == "RenderedNode")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.RenderedNode.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = RenderedNodeTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.OriginalContentID = serializedObject.OriginalContentID;
		            existingObject.TechnicalSource = serializedObject.TechnicalSource;
		            existingObject.ImageBaseUrl = serializedObject.ImageBaseUrl;
		            existingObject.ImageExt = serializedObject.ImageExt;
		            existingObject.Title = serializedObject.Title;
		            existingObject.ActualContentUrl = serializedObject.ActualContentUrl;
		            existingObject.Excerpt = serializedObject.Excerpt;
		            existingObject.TimestampText = serializedObject.TimestampText;
		            existingObject.MainSortableText = serializedObject.MainSortableText;
		            existingObject.IsCategoryFilteringNode = serializedObject.IsCategoryFilteringNode;
		            existingObject.CategoryFilters = serializedObject.CategoryFilters;
		            existingObject.CategoryNames = serializedObject.CategoryNames;
		            existingObject.Categories = serializedObject.Categories;
		            existingObject.CategoryIDList = serializedObject.CategoryIDList;
		            existingObject.Authors = serializedObject.Authors;
		            existingObject.Locations = serializedObject.Locations;
		            return;
		        } 
		        if (updateData.ObjectType == "ShortTextObject")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ShortTextObject.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ShortTextObjectTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Content = serializedObject.Content;
		            return;
		        } 
		        if (updateData.ObjectType == "LongTextObject")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.LongTextObject.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = LongTextObjectTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Content = serializedObject.Content;
		            return;
		        } 
		        if (updateData.ObjectType == "MapContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.MapContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = MapContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Header = serializedObject.Header;
		            existingObject.MapFeatured = serializedObject.MapFeatured;
		            existingObject.MapCollection = serializedObject.MapCollection;
		            existingObject.MapResultCollection = serializedObject.MapResultCollection;
		            existingObject.MapIndexCollection = serializedObject.MapIndexCollection;
		            existingObject.MarkerSourceLocations = serializedObject.MarkerSourceLocations;
		            existingObject.MarkerSourceBlogs = serializedObject.MarkerSourceBlogs;
		            existingObject.MarkerSourceActivities = serializedObject.MarkerSourceActivities;
		            existingObject.MapMarkers = serializedObject.MapMarkers;
		            return;
		        } 
		        if (updateData.ObjectType == "MapMarker")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.MapMarker.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = MapMarkerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.IconUrl = serializedObject.IconUrl;
		            existingObject.MarkerSource = serializedObject.MarkerSource;
		            existingObject.CategoryName = serializedObject.CategoryName;
		            existingObject.LocationText = serializedObject.LocationText;
		            existingObject.PopupTitle = serializedObject.PopupTitle;
		            existingObject.PopupContent = serializedObject.PopupContent;
		            existingObject.Location = serializedObject.Location;
		            return;
		        } 
		        if (updateData.ObjectType == "CalendarContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.CalendarContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CalendarContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.CalendarContainerHeader = serializedObject.CalendarContainerHeader;
		            existingObject.CalendarFeatured = serializedObject.CalendarFeatured;
		            existingObject.CalendarCollection = serializedObject.CalendarCollection;
		            existingObject.CalendarIndexCollection = serializedObject.CalendarIndexCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "AboutContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AboutContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AboutContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.MainImage = serializedObject.MainImage;
		            existingObject.Header = serializedObject.Header;
		            existingObject.Excerpt = serializedObject.Excerpt;
		            existingObject.Body = serializedObject.Body;
		            existingObject.Published = serializedObject.Published;
		            existingObject.Author = serializedObject.Author;
		            existingObject.ImageGroup = serializedObject.ImageGroup;
		            return;
		        } 
		        if (updateData.ObjectType == "OBSAccountContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.OBSAccountContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = OBSAccountContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.AccountContainerHeader = serializedObject.AccountContainerHeader;
		            existingObject.AccountFeatured = serializedObject.AccountFeatured;
		            existingObject.AccountCollection = serializedObject.AccountCollection;
		            existingObject.AccountIndexCollection = serializedObject.AccountIndexCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "ProjectContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ProjectContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ProjectContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ProjectContainerHeader = serializedObject.ProjectContainerHeader;
		            existingObject.ProjectFeatured = serializedObject.ProjectFeatured;
		            existingObject.ProjectCollection = serializedObject.ProjectCollection;
		            existingObject.ProjectIndexCollection = serializedObject.ProjectIndexCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "CourseContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.CourseContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CourseContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.CourseContainerHeader = serializedObject.CourseContainerHeader;
		            existingObject.CourseFeatured = serializedObject.CourseFeatured;
		            existingObject.CourseCollection = serializedObject.CourseCollection;
		            existingObject.CourseIndexCollection = serializedObject.CourseIndexCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "ContainerHeader")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ContainerHeader.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ContainerHeaderTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            existingObject.SubTitle = serializedObject.SubTitle;
		            return;
		        } 
		        if (updateData.ObjectType == "ActivitySummaryContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ActivitySummaryContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ActivitySummaryContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Header = serializedObject.Header;
		            existingObject.SummaryBody = serializedObject.SummaryBody;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.ActivityIndex = serializedObject.ActivityIndex;
		            existingObject.ActivityCollection = serializedObject.ActivityCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "ActivityIndex")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ActivityIndex.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ActivityIndexTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Icon = serializedObject.Icon;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.Summary = serializedObject.Summary;
		            return;
		        } 
		        if (updateData.ObjectType == "ActivityContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ActivityContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ActivityContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Header = serializedObject.Header;
		            existingObject.ActivityIndex = serializedObject.ActivityIndex;
		            existingObject.ActivityModule = serializedObject.ActivityModule;
		            return;
		        } 
		        if (updateData.ObjectType == "Activity")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Activity.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ActivityTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            existingObject.ProfileImage = serializedObject.ProfileImage;
		            existingObject.IconImage = serializedObject.IconImage;
		            existingObject.ActivityName = serializedObject.ActivityName;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.ContactPerson = serializedObject.ContactPerson;
		            existingObject.StartingTime = serializedObject.StartingTime;
		            existingObject.Excerpt = serializedObject.Excerpt;
		            existingObject.Description = serializedObject.Description;
		            existingObject.IFrameSources = serializedObject.IFrameSources;
		            existingObject.Collaborators = serializedObject.Collaborators;
		            existingObject.ImageGroupCollection = serializedObject.ImageGroupCollection;
		            existingObject.LocationCollection = serializedObject.LocationCollection;
		            existingObject.CategoryCollection = serializedObject.CategoryCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "Moderator")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Moderator.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ModeratorTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ModeratorName = serializedObject.ModeratorName;
		            existingObject.ProfileUrl = serializedObject.ProfileUrl;
		            return;
		        } 
		        if (updateData.ObjectType == "Collaborator")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Collaborator.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CollaboratorTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.AccountID = serializedObject.AccountID;
		            existingObject.EmailAddress = serializedObject.EmailAddress;
		            existingObject.CollaboratorName = serializedObject.CollaboratorName;
		            existingObject.Role = serializedObject.Role;
		            existingObject.ProfileUrl = serializedObject.ProfileUrl;
		            return;
		        } 
		        if (updateData.ObjectType == "GroupSummaryContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.GroupSummaryContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = GroupSummaryContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Header = serializedObject.Header;
		            existingObject.SummaryBody = serializedObject.SummaryBody;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.GroupSummaryIndex = serializedObject.GroupSummaryIndex;
		            existingObject.GroupCollection = serializedObject.GroupCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "GroupContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.GroupContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = GroupContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Header = serializedObject.Header;
		            existingObject.GroupIndex = serializedObject.GroupIndex;
		            existingObject.GroupProfile = serializedObject.GroupProfile;
		            existingObject.Collaborators = serializedObject.Collaborators;
		            existingObject.PendingCollaborators = serializedObject.PendingCollaborators;
		            existingObject.Activities = serializedObject.Activities;
		            existingObject.ImageGroupCollection = serializedObject.ImageGroupCollection;
		            existingObject.LocationCollection = serializedObject.LocationCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "GroupIndex")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.GroupIndex.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = GroupIndexTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Icon = serializedObject.Icon;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.Summary = serializedObject.Summary;
		            return;
		        } 
		        if (updateData.ObjectType == "AddAddressAndLocationInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AddAddressAndLocationInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AddAddressAndLocationInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.LocationName = serializedObject.LocationName;
		            return;
		        } 
		        if (updateData.ObjectType == "AddImageInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AddImageInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AddImageInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ImageTitle = serializedObject.ImageTitle;
		            return;
		        } 
		        if (updateData.ObjectType == "AddImageGroupInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AddImageGroupInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AddImageGroupInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ImageGroupTitle = serializedObject.ImageGroupTitle;
		            return;
		        } 
		        if (updateData.ObjectType == "AddEmailAddressInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AddEmailAddressInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AddEmailAddressInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.EmailAddress = serializedObject.EmailAddress;
		            return;
		        } 
		        if (updateData.ObjectType == "CreateGroupInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.CreateGroupInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CreateGroupInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.GroupName = serializedObject.GroupName;
		            return;
		        } 
		        if (updateData.ObjectType == "AddActivityInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AddActivityInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AddActivityInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ActivityName = serializedObject.ActivityName;
		            return;
		        } 
		        if (updateData.ObjectType == "AddBlogPostInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AddBlogPostInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AddBlogPostInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            return;
		        } 
		        if (updateData.ObjectType == "AddCategoryInfo")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AddCategoryInfo.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AddCategoryInfoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.CategoryName = serializedObject.CategoryName;
		            return;
		        } 
		        if (updateData.ObjectType == "Group")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Group.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = GroupTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            existingObject.ProfileImage = serializedObject.ProfileImage;
		            existingObject.IconImage = serializedObject.IconImage;
		            existingObject.GroupName = serializedObject.GroupName;
		            existingObject.Description = serializedObject.Description;
		            existingObject.OrganizationsAndGroupsLinkedToUs = serializedObject.OrganizationsAndGroupsLinkedToUs;
		            existingObject.WwwSiteToPublishTo = serializedObject.WwwSiteToPublishTo;
		            existingObject.CustomUICollection = serializedObject.CustomUICollection;
		            existingObject.Moderators = serializedObject.Moderators;
		            existingObject.CategoryCollection = serializedObject.CategoryCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "Introduction")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Introduction.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = IntroductionTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            existingObject.Body = serializedObject.Body;
		            return;
		        } 
		        if (updateData.ObjectType == "ContentCategoryRank")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ContentCategoryRank.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ContentCategoryRankTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ContentID = serializedObject.ContentID;
		            existingObject.ContentSemanticType = serializedObject.ContentSemanticType;
		            existingObject.CategoryID = serializedObject.CategoryID;
		            existingObject.RankName = serializedObject.RankName;
		            existingObject.RankValue = serializedObject.RankValue;
		            return;
		        } 
		        if (updateData.ObjectType == "LinkToContent")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.LinkToContent.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = LinkToContentTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.URL = serializedObject.URL;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Description = serializedObject.Description;
		            existingObject.Published = serializedObject.Published;
		            existingObject.Author = serializedObject.Author;
		            existingObject.ImageData = serializedObject.ImageData;
		            existingObject.Locations = serializedObject.Locations;
		            existingObject.Categories = serializedObject.Categories;
		            return;
		        } 
		        if (updateData.ObjectType == "EmbeddedContent")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.EmbeddedContent.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = EmbeddedContentTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.IFrameTagContents = serializedObject.IFrameTagContents;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Published = serializedObject.Published;
		            existingObject.Author = serializedObject.Author;
		            existingObject.Description = serializedObject.Description;
		            existingObject.Locations = serializedObject.Locations;
		            existingObject.Categories = serializedObject.Categories;
		            return;
		        } 
		        if (updateData.ObjectType == "DynamicContentGroup")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.DynamicContentGroup.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = DynamicContentGroupTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.HostName = serializedObject.HostName;
		            existingObject.GroupHeader = serializedObject.GroupHeader;
		            existingObject.SortValue = serializedObject.SortValue;
		            existingObject.PageLocation = serializedObject.PageLocation;
		            existingObject.ContentItemNames = serializedObject.ContentItemNames;
		            return;
		        } 
		        if (updateData.ObjectType == "DynamicContent")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.DynamicContent.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = DynamicContentTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.HostName = serializedObject.HostName;
		            existingObject.ContentName = serializedObject.ContentName;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Description = serializedObject.Description;
		            existingObject.ElementQuery = serializedObject.ElementQuery;
		            existingObject.Content = serializedObject.Content;
		            existingObject.RawContent = serializedObject.RawContent;
		            existingObject.ImageData = serializedObject.ImageData;
		            existingObject.IsEnabled = serializedObject.IsEnabled;
		            existingObject.ApplyActively = serializedObject.ApplyActively;
		            existingObject.EditType = serializedObject.EditType;
		            existingObject.PageLocation = serializedObject.PageLocation;
		            return;
		        } 
		        if (updateData.ObjectType == "AttachedToObject")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AttachedToObject.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AttachedToObjectTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.SourceObjectID = serializedObject.SourceObjectID;
		            existingObject.SourceObjectName = serializedObject.SourceObjectName;
		            existingObject.SourceObjectDomain = serializedObject.SourceObjectDomain;
		            existingObject.TargetObjectID = serializedObject.TargetObjectID;
		            existingObject.TargetObjectName = serializedObject.TargetObjectName;
		            existingObject.TargetObjectDomain = serializedObject.TargetObjectDomain;
		            return;
		        } 
		        if (updateData.ObjectType == "Comment")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Comment.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CommentTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.TargetObjectID = serializedObject.TargetObjectID;
		            existingObject.TargetObjectName = serializedObject.TargetObjectName;
		            existingObject.TargetObjectDomain = serializedObject.TargetObjectDomain;
		            existingObject.CommentText = serializedObject.CommentText;
		            existingObject.Created = serializedObject.Created;
		            existingObject.OriginalAuthorName = serializedObject.OriginalAuthorName;
		            existingObject.OriginalAuthorEmail = serializedObject.OriginalAuthorEmail;
		            existingObject.OriginalAuthorAccountID = serializedObject.OriginalAuthorAccountID;
		            existingObject.LastModified = serializedObject.LastModified;
		            existingObject.LastAuthorName = serializedObject.LastAuthorName;
		            existingObject.LastAuthorEmail = serializedObject.LastAuthorEmail;
		            existingObject.LastAuthorAccountID = serializedObject.LastAuthorAccountID;
		            return;
		        } 
		        if (updateData.ObjectType == "Selection")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Selection.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SelectionTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.TargetObjectID = serializedObject.TargetObjectID;
		            existingObject.TargetObjectName = serializedObject.TargetObjectName;
		            existingObject.TargetObjectDomain = serializedObject.TargetObjectDomain;
		            existingObject.SelectionCategory = serializedObject.SelectionCategory;
		            existingObject.TextValue = serializedObject.TextValue;
		            existingObject.BooleanValue = serializedObject.BooleanValue;
		            existingObject.DoubleValue = serializedObject.DoubleValue;
		            return;
		        } 
		        if (updateData.ObjectType == "TextContent")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.TextContent.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TextContentTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ImageData = serializedObject.ImageData;
		            existingObject.Title = serializedObject.Title;
		            existingObject.SubTitle = serializedObject.SubTitle;
		            existingObject.Published = serializedObject.Published;
		            existingObject.Author = serializedObject.Author;
		            existingObject.Excerpt = serializedObject.Excerpt;
		            existingObject.Body = serializedObject.Body;
		            existingObject.Locations = serializedObject.Locations;
		            existingObject.Categories = serializedObject.Categories;
		            existingObject.SortOrderNumber = serializedObject.SortOrderNumber;
		            existingObject.IFrameSources = serializedObject.IFrameSources;
		            existingObject.RawHtmlContent = serializedObject.RawHtmlContent;
		            return;
		        } 
		        if (updateData.ObjectType == "Blog")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Blog.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = BlogTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            existingObject.ProfileImage = serializedObject.ProfileImage;
		            existingObject.IconImage = serializedObject.IconImage;
		            existingObject.Title = serializedObject.Title;
		            existingObject.SubTitle = serializedObject.SubTitle;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.Published = serializedObject.Published;
		            existingObject.Author = serializedObject.Author;
		            existingObject.FeaturedImage = serializedObject.FeaturedImage;
		            existingObject.ImageGroupCollection = serializedObject.ImageGroupCollection;
		            existingObject.VideoGroup = serializedObject.VideoGroup;
		            existingObject.Body = serializedObject.Body;
		            existingObject.Excerpt = serializedObject.Excerpt;
		            existingObject.IFrameSources = serializedObject.IFrameSources;
		            existingObject.LocationCollection = serializedObject.LocationCollection;
		            existingObject.CategoryCollection = serializedObject.CategoryCollection;
		            existingObject.SocialPanel = serializedObject.SocialPanel;
		            return;
		        } 
		        if (updateData.ObjectType == "BlogIndexGroup")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.BlogIndexGroup.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = BlogIndexGroupTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Icon = serializedObject.Icon;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.GroupedByDate = serializedObject.GroupedByDate;
		            existingObject.GroupedByLocation = serializedObject.GroupedByLocation;
		            existingObject.GroupedByAuthor = serializedObject.GroupedByAuthor;
		            existingObject.GroupedByCategory = serializedObject.GroupedByCategory;
		            existingObject.FullBlogArchive = serializedObject.FullBlogArchive;
		            existingObject.BlogSourceForSummary = serializedObject.BlogSourceForSummary;
		            existingObject.Summary = serializedObject.Summary;
		            return;
		        } 
		        if (updateData.ObjectType == "CalendarIndex")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.CalendarIndex.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CalendarIndexTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Icon = serializedObject.Icon;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Introduction = serializedObject.Introduction;
		            existingObject.Summary = serializedObject.Summary;
		            return;
		        } 
		        if (updateData.ObjectType == "Filter")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Filter.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = FilterTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            return;
		        } 
		        if (updateData.ObjectType == "Calendar")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Calendar.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CalendarTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            return;
		        } 
		        if (updateData.ObjectType == "Map")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Map.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = MapTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            return;
		        } 
		        if (updateData.ObjectType == "MapIndexCollection")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.MapIndexCollection.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = MapIndexCollectionTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.MapByDate = serializedObject.MapByDate;
		            existingObject.MapByLocation = serializedObject.MapByLocation;
		            existingObject.MapByAuthor = serializedObject.MapByAuthor;
		            existingObject.MapByCategory = serializedObject.MapByCategory;
		            return;
		        } 
		        if (updateData.ObjectType == "MapResult")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.MapResult.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = MapResultTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Location = serializedObject.Location;
		            return;
		        } 
		        if (updateData.ObjectType == "MapResultsCollection")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.MapResultsCollection.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = MapResultsCollectionTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ResultByDate = serializedObject.ResultByDate;
		            existingObject.ResultByAuthor = serializedObject.ResultByAuthor;
		            existingObject.ResultByProximity = serializedObject.ResultByProximity;
		            return;
		        } 
		        if (updateData.ObjectType == "Video")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Video.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = VideoTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.VideoData = serializedObject.VideoData;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Caption = serializedObject.Caption;
		            return;
		        } 
		        if (updateData.ObjectType == "Image")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Image.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ImageTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            existingObject.ImageData = serializedObject.ImageData;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Caption = serializedObject.Caption;
		            existingObject.Description = serializedObject.Description;
		            existingObject.Locations = serializedObject.Locations;
		            existingObject.Categories = serializedObject.Categories;
		            return;
		        } 
		        if (updateData.ObjectType == "BinaryFile")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.BinaryFile.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = BinaryFileTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.OriginalFileName = serializedObject.OriginalFileName;
		            existingObject.Data = serializedObject.Data;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Description = serializedObject.Description;
		            existingObject.Categories = serializedObject.Categories;
		            return;
		        } 
		        if (updateData.ObjectType == "ImageGroup")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ImageGroup.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ImageGroupTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Description = serializedObject.Description;
		            existingObject.FeaturedImage = serializedObject.FeaturedImage;
		            existingObject.ImagesCollection = serializedObject.ImagesCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "VideoGroup")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.VideoGroup.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = VideoGroupTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Title = serializedObject.Title;
		            existingObject.Description = serializedObject.Description;
		            existingObject.VideoCollection = serializedObject.VideoCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "Tooltip")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Tooltip.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = TooltipTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.TooltipText = serializedObject.TooltipText;
		            return;
		        } 
		        if (updateData.ObjectType == "SocialPanel")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.SocialPanel.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SocialPanelTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.SocialFilter = serializedObject.SocialFilter;
		            return;
		        } 
		        if (updateData.ObjectType == "Longitude")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Longitude.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = LongitudeTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.TextValue = serializedObject.TextValue;
		            return;
		        } 
		        if (updateData.ObjectType == "Latitude")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Latitude.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = LatitudeTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.TextValue = serializedObject.TextValue;
		            return;
		        } 
		        if (updateData.ObjectType == "Location")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Location.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = LocationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.LocationName = serializedObject.LocationName;
		            existingObject.Longitude = serializedObject.Longitude;
		            existingObject.Latitude = serializedObject.Latitude;
		            return;
		        } 
		        if (updateData.ObjectType == "Date")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Date.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = DateTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Day = serializedObject.Day;
		            existingObject.Week = serializedObject.Week;
		            existingObject.Month = serializedObject.Month;
		            existingObject.Year = serializedObject.Year;
		            return;
		        } 
		        if (updateData.ObjectType == "Sex")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Sex.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SexTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.SexText = serializedObject.SexText;
		            return;
		        } 
		        if (updateData.ObjectType == "OBSAddress")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.OBSAddress.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = OBSAddressTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.StreetName = serializedObject.StreetName;
		            existingObject.BuildingNumber = serializedObject.BuildingNumber;
		            existingObject.PostOfficeBox = serializedObject.PostOfficeBox;
		            existingObject.PostalCode = serializedObject.PostalCode;
		            existingObject.Municipality = serializedObject.Municipality;
		            existingObject.Region = serializedObject.Region;
		            existingObject.Province = serializedObject.Province;
		            existingObject.state = serializedObject.state;
		            existingObject.Country = serializedObject.Country;
		            existingObject.Continent = serializedObject.Continent;
		            return;
		        } 
		        if (updateData.ObjectType == "Identity")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Identity.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = IdentityTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.FirstName = serializedObject.FirstName;
		            existingObject.LastName = serializedObject.LastName;
		            existingObject.Initials = serializedObject.Initials;
		            existingObject.Sex = serializedObject.Sex;
		            existingObject.Birthday = serializedObject.Birthday;
		            return;
		        } 
		        if (updateData.ObjectType == "ImageVideoSoundVectorRaw")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.ImageVideoSoundVectorRaw.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = ImageVideoSoundVectorRawTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Image = serializedObject.Image;
		            existingObject.Video = serializedObject.Video;
		            existingObject.Sound = serializedObject.Sound;
		            existingObject.Vector = serializedObject.Vector;
		            existingObject.Raw = serializedObject.Raw;
		            return;
		        } 
		        if (updateData.ObjectType == "CategoryContainer")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.CategoryContainer.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CategoryContainerTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Categories = serializedObject.Categories;
		            return;
		        } 
		        if (updateData.ObjectType == "Category")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Category.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = CategoryTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            existingObject.CategoryName = serializedObject.CategoryName;
		            existingObject.ImageData = serializedObject.ImageData;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Excerpt = serializedObject.Excerpt;
		            existingObject.ParentCategory = serializedObject.ParentCategory;
		            existingObject.ParentCategoryID = serializedObject.ParentCategoryID;
		            return;
		        } 
		        if (updateData.ObjectType == "Subscription")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Subscription.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SubscriptionTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Priority = serializedObject.Priority;
		            existingObject.TargetRelativeLocation = serializedObject.TargetRelativeLocation;
		            existingObject.TargetInformationObjectType = serializedObject.TargetInformationObjectType;
		            existingObject.SubscriberRelativeLocation = serializedObject.SubscriberRelativeLocation;
		            existingObject.SubscriberInformationObjectType = serializedObject.SubscriberInformationObjectType;
		            existingObject.SubscriptionType = serializedObject.SubscriptionType;
		            return;
		        } 
		        if (updateData.ObjectType == "QueueEnvelope")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.QueueEnvelope.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = QueueEnvelopeTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ActiveContainerName = serializedObject.ActiveContainerName;
		            existingObject.OwnerPrefix = serializedObject.OwnerPrefix;
		            existingObject.CurrentRetryCount = serializedObject.CurrentRetryCount;
		            existingObject.SingleOperation = serializedObject.SingleOperation;
		            existingObject.OrderDependentOperationSequence = serializedObject.OrderDependentOperationSequence;
		            existingObject.ErrorContent = serializedObject.ErrorContent;
		            return;
		        } 
		        if (updateData.ObjectType == "OperationRequest")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.OperationRequest.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = OperationRequestTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.SubscriberNotification = serializedObject.SubscriberNotification;
		            existingObject.SubscriptionChainRequest = serializedObject.SubscriptionChainRequest;
		            existingObject.UpdateWebContentOperation = serializedObject.UpdateWebContentOperation;
		            existingObject.RefreshDefaultViewsOperation = serializedObject.RefreshDefaultViewsOperation;
		            existingObject.DeleteEntireOwner = serializedObject.DeleteEntireOwner;
		            existingObject.DeleteOwnerContent = serializedObject.DeleteOwnerContent;
		            existingObject.PublishWebContent = serializedObject.PublishWebContent;
		            existingObject.ProcessIDToExecute = serializedObject.ProcessIDToExecute;
		            return;
		        } 
		        if (updateData.ObjectType == "SubscriptionChainRequestMessage")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.SubscriptionChainRequestMessage.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SubscriptionChainRequestMessageTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ContentItemID = serializedObject.ContentItemID;
		            return;
		        } 
		        if (updateData.ObjectType == "SubscriptionChainRequestContent")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.SubscriptionChainRequestContent.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SubscriptionChainRequestContentTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.SubmitTime = serializedObject.SubmitTime;
		            existingObject.ProcessingStartTime = serializedObject.ProcessingStartTime;
		            existingObject.ProcessingEndTimeInformationObjects = serializedObject.ProcessingEndTimeInformationObjects;
		            existingObject.ProcessingEndTimeWebTemplatesRendering = serializedObject.ProcessingEndTimeWebTemplatesRendering;
		            existingObject.ProcessingEndTime = serializedObject.ProcessingEndTime;
		            existingObject.SubscriptionTargetCollection = serializedObject.SubscriptionTargetCollection;
		            return;
		        } 
		        if (updateData.ObjectType == "SubscriptionTarget")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.SubscriptionTarget.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SubscriptionTargetTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.BlobLocation = serializedObject.BlobLocation;
		            return;
		        } 
		        if (updateData.ObjectType == "DeleteEntireOwnerOperation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.DeleteEntireOwnerOperation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = DeleteEntireOwnerOperationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ContainerName = serializedObject.ContainerName;
		            existingObject.LocationPrefix = serializedObject.LocationPrefix;
		            return;
		        } 
		        if (updateData.ObjectType == "DeleteOwnerContentOperation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.DeleteOwnerContentOperation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = DeleteOwnerContentOperationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ContainerName = serializedObject.ContainerName;
		            existingObject.LocationPrefix = serializedObject.LocationPrefix;
		            return;
		        } 
		        if (updateData.ObjectType == "SystemError")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.SystemError.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SystemErrorTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ErrorTitle = serializedObject.ErrorTitle;
		            existingObject.OccurredAt = serializedObject.OccurredAt;
		            existingObject.SystemErrorItems = serializedObject.SystemErrorItems;
		            existingObject.MessageContent = serializedObject.MessageContent;
		            return;
		        } 
		        if (updateData.ObjectType == "SystemErrorItem")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.SystemErrorItem.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SystemErrorItemTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ShortDescription = serializedObject.ShortDescription;
		            existingObject.LongDescription = serializedObject.LongDescription;
		            return;
		        } 
		        if (updateData.ObjectType == "InformationSource")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.InformationSource.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = InformationSourceTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.SourceName = serializedObject.SourceName;
		            existingObject.SourceLocation = serializedObject.SourceLocation;
		            existingObject.SourceType = serializedObject.SourceType;
		            existingObject.IsDynamic = serializedObject.IsDynamic;
		            existingObject.SourceInformationObjectType = serializedObject.SourceInformationObjectType;
		            existingObject.SourceETag = serializedObject.SourceETag;
		            existingObject.SourceMD5 = serializedObject.SourceMD5;
		            existingObject.SourceLastModified = serializedObject.SourceLastModified;
		            return;
		        } 
		        if (updateData.ObjectType == "RefreshDefaultViewsOperation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.RefreshDefaultViewsOperation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = RefreshDefaultViewsOperationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.ViewLocation = serializedObject.ViewLocation;
		            existingObject.TypeNameToRefresh = serializedObject.TypeNameToRefresh;
		            return;
		        } 
		        if (updateData.ObjectType == "UpdateWebContentOperation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.UpdateWebContentOperation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = UpdateWebContentOperationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.SourceContainerName = serializedObject.SourceContainerName;
		            existingObject.SourcePathRoot = serializedObject.SourcePathRoot;
		            existingObject.TargetContainerName = serializedObject.TargetContainerName;
		            existingObject.TargetPathRoot = serializedObject.TargetPathRoot;
		            existingObject.RenderWhileSync = serializedObject.RenderWhileSync;
		            existingObject.Handlers = serializedObject.Handlers;
		            return;
		        } 
		        if (updateData.ObjectType == "UpdateWebContentHandlerItem")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.UpdateWebContentHandlerItem.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = UpdateWebContentHandlerItemTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.InformationTypeName = serializedObject.InformationTypeName;
		            existingObject.OptionName = serializedObject.OptionName;
		            return;
		        } 
		        if (updateData.ObjectType == "PublishWebContentOperation")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.PublishWebContentOperation.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = PublishWebContentOperationTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.SourceContainerName = serializedObject.SourceContainerName;
		            existingObject.SourcePathRoot = serializedObject.SourcePathRoot;
		            existingObject.SourceOwner = serializedObject.SourceOwner;
		            existingObject.TargetContainerName = serializedObject.TargetContainerName;
		            return;
		        } 
		        if (updateData.ObjectType == "SubscriberInput")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.SubscriberInput.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = SubscriberInputTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.InputRelativeLocation = serializedObject.InputRelativeLocation;
		            existingObject.InformationObjectName = serializedObject.InformationObjectName;
		            existingObject.InformationItemName = serializedObject.InformationItemName;
		            existingObject.SubscriberRelativeLocation = serializedObject.SubscriberRelativeLocation;
		            return;
		        } 
		        if (updateData.ObjectType == "Monitor")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.Monitor.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = MonitorTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.TargetObjectName = serializedObject.TargetObjectName;
		            existingObject.TargetItemName = serializedObject.TargetItemName;
		            existingObject.MonitoringUtcTimeStampToStart = serializedObject.MonitoringUtcTimeStampToStart;
		            existingObject.MonitoringCycleFrequencyUnit = serializedObject.MonitoringCycleFrequencyUnit;
		            existingObject.MonitoringCycleEveryXthOfUnit = serializedObject.MonitoringCycleEveryXthOfUnit;
		            existingObject.CustomMonitoringCycleOperationName = serializedObject.CustomMonitoringCycleOperationName;
		            existingObject.OperationActionName = serializedObject.OperationActionName;
		            return;
		        } 
		        if (updateData.ObjectType == "IconTitleDescription")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.IconTitleDescription.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = IconTitleDescriptionTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.Icon = serializedObject.Icon;
		            existingObject.Title = serializedObject.Title;
		            existingObject.Description = serializedObject.Description;
		            return;
		        } 
		        if (updateData.ObjectType == "AboutAGIApplications")
		        {
		            string currentFullStoragePath = Path.Combine(storageRootPath, updateData.CurrentStoragePath);
		            var serializedObject =
		                global::TheBall.Payments.AboutAGIApplications.DeserializeFromXml(
		                    ContentStorage.GetContentAsString(currentFullStoragePath));
		            var existingObject = AboutAGIApplicationsTable.Single(item => item.ID == updateData.ObjectID);
		            existingObject.BuiltForAnybody = serializedObject.BuiltForAnybody;
		            existingObject.ForAllPeople = serializedObject.ForAllPeople;
		            return;
		        } 
		    }

		    public void PerformInsert(string storageRootPath, InformationObjectMetaData insertData)
		    {
                if (insertData.SemanticDomain != "TheBall.Payments")
                    throw new InvalidDataException("Mismatch on domain data");
                InformationObjectMetaDataTable.InsertOnSubmit(insertData);
                if (insertData.ObjectType == "TBSystem")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBSystem.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBSystem {ID = insertData.ObjectID};
		            objectToAdd.InstanceName = serializedObject.InstanceName;
		            objectToAdd.AdminGroupID = serializedObject.AdminGroupID;
					TBSystemTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "WebPublishInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.WebPublishInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new WebPublishInfo {ID = insertData.ObjectID};
		            objectToAdd.PublishType = serializedObject.PublishType;
		            objectToAdd.PublishContainer = serializedObject.PublishContainer;
		            objectToAdd.ActivePublication = serializedObject.ActivePublication;
		            objectToAdd.Publications = serializedObject.Publications;
					WebPublishInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "PublicationPackage")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.PublicationPackage.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new PublicationPackage {ID = insertData.ObjectID};
		            objectToAdd.PackageName = serializedObject.PackageName;
		            objectToAdd.PublicationTime = serializedObject.PublicationTime;
					PublicationPackageTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBRLoginRoot")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBRLoginRoot.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBRLoginRoot {ID = insertData.ObjectID};
		            objectToAdd.DomainName = serializedObject.DomainName;
		            objectToAdd.Account = serializedObject.Account;
					TBRLoginRootTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBRAccountRoot")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBRAccountRoot.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBRAccountRoot {ID = insertData.ObjectID};
		            objectToAdd.Account = serializedObject.Account;
					TBRAccountRootTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBRGroupRoot")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBRGroupRoot.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBRGroupRoot {ID = insertData.ObjectID};
		            objectToAdd.Group = serializedObject.Group;
					TBRGroupRootTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBRLoginGroupRoot")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBRLoginGroupRoot.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBRLoginGroupRoot {ID = insertData.ObjectID};
		            objectToAdd.Role = serializedObject.Role;
		            objectToAdd.GroupID = serializedObject.GroupID;
					TBRLoginGroupRootTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBREmailRoot")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBREmailRoot.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBREmailRoot {ID = insertData.ObjectID};
		            objectToAdd.Account = serializedObject.Account;
					TBREmailRootTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBAccount")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBAccount.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBAccount {ID = insertData.ObjectID};
		            objectToAdd.Emails = serializedObject.Emails;
		            objectToAdd.Logins = serializedObject.Logins;
		            objectToAdd.GroupRoleCollection = serializedObject.GroupRoleCollection;
					TBAccountTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBAccountCollaborationGroup")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBAccountCollaborationGroup.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBAccountCollaborationGroup {ID = insertData.ObjectID};
		            objectToAdd.GroupID = serializedObject.GroupID;
		            objectToAdd.GroupRole = serializedObject.GroupRole;
		            objectToAdd.RoleStatus = serializedObject.RoleStatus;
					TBAccountCollaborationGroupTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBLoginInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBLoginInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBLoginInfo {ID = insertData.ObjectID};
		            objectToAdd.OpenIDUrl = serializedObject.OpenIDUrl;
					TBLoginInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBEmail")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBEmail.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBEmail {ID = insertData.ObjectID};
		            objectToAdd.EmailAddress = serializedObject.EmailAddress;
		            objectToAdd.ValidatedAt = serializedObject.ValidatedAt;
					TBEmailTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBCollaboratorRole")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBCollaboratorRole.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBCollaboratorRole {ID = insertData.ObjectID};
		            objectToAdd.Email = serializedObject.Email;
		            objectToAdd.Role = serializedObject.Role;
		            objectToAdd.RoleStatus = serializedObject.RoleStatus;
					TBCollaboratorRoleTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBCollaboratingGroup")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBCollaboratingGroup.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBCollaboratingGroup {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Roles = serializedObject.Roles;
					TBCollaboratingGroupTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBEmailValidation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBEmailValidation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBEmailValidation {ID = insertData.ObjectID};
		            objectToAdd.Email = serializedObject.Email;
		            objectToAdd.AccountID = serializedObject.AccountID;
		            objectToAdd.ValidUntil = serializedObject.ValidUntil;
		            objectToAdd.GroupJoinConfirmation = serializedObject.GroupJoinConfirmation;
		            objectToAdd.DeviceJoinConfirmation = serializedObject.DeviceJoinConfirmation;
		            objectToAdd.InformationInputConfirmation = serializedObject.InformationInputConfirmation;
		            objectToAdd.InformationOutputConfirmation = serializedObject.InformationOutputConfirmation;
		            objectToAdd.MergeAccountsConfirmation = serializedObject.MergeAccountsConfirmation;
		            objectToAdd.RedirectUrlAfterValidation = serializedObject.RedirectUrlAfterValidation;
					TBEmailValidationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBMergeAccountConfirmation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBMergeAccountConfirmation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBMergeAccountConfirmation {ID = insertData.ObjectID};
		            objectToAdd.AccountToBeMergedID = serializedObject.AccountToBeMergedID;
		            objectToAdd.AccountToMergeToID = serializedObject.AccountToMergeToID;
					TBMergeAccountConfirmationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBGroupJoinConfirmation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBGroupJoinConfirmation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBGroupJoinConfirmation {ID = insertData.ObjectID};
		            objectToAdd.GroupID = serializedObject.GroupID;
		            objectToAdd.InvitationMode = serializedObject.InvitationMode;
					TBGroupJoinConfirmationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBDeviceJoinConfirmation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBDeviceJoinConfirmation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBDeviceJoinConfirmation {ID = insertData.ObjectID};
		            objectToAdd.GroupID = serializedObject.GroupID;
		            objectToAdd.AccountID = serializedObject.AccountID;
		            objectToAdd.DeviceMembershipID = serializedObject.DeviceMembershipID;
					TBDeviceJoinConfirmationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBInformationInputConfirmation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBInformationInputConfirmation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBInformationInputConfirmation {ID = insertData.ObjectID};
		            objectToAdd.GroupID = serializedObject.GroupID;
		            objectToAdd.AccountID = serializedObject.AccountID;
		            objectToAdd.InformationInputID = serializedObject.InformationInputID;
					TBInformationInputConfirmationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBInformationOutputConfirmation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBInformationOutputConfirmation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBInformationOutputConfirmation {ID = insertData.ObjectID};
		            objectToAdd.GroupID = serializedObject.GroupID;
		            objectToAdd.AccountID = serializedObject.AccountID;
		            objectToAdd.InformationOutputID = serializedObject.InformationOutputID;
					TBInformationOutputConfirmationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBRegisterContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBRegisterContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBRegisterContainer {ID = insertData.ObjectID};
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.ReturnUrl = serializedObject.ReturnUrl;
		            objectToAdd.LoginProviderCollection = serializedObject.LoginProviderCollection;
					TBRegisterContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "LoginProvider")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.LoginProvider.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new LoginProvider {ID = insertData.ObjectID};
		            objectToAdd.ProviderName = serializedObject.ProviderName;
		            objectToAdd.ProviderIconClass = serializedObject.ProviderIconClass;
		            objectToAdd.ProviderType = serializedObject.ProviderType;
		            objectToAdd.ProviderUrl = serializedObject.ProviderUrl;
		            objectToAdd.ReturnUrl = serializedObject.ReturnUrl;
					LoginProviderTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ContactOipContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ContactOipContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ContactOipContainer {ID = insertData.ObjectID};
		            objectToAdd.OIPModeratorGroupID = serializedObject.OIPModeratorGroupID;
					ContactOipContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TBPRegisterEmail")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TBPRegisterEmail.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TBPRegisterEmail {ID = insertData.ObjectID};
		            objectToAdd.EmailAddress = serializedObject.EmailAddress;
					TBPRegisterEmailTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "JavaScriptContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.JavaScriptContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new JavaScriptContainer {ID = insertData.ObjectID};
		            objectToAdd.HtmlContent = serializedObject.HtmlContent;
					JavaScriptContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "JavascriptContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.JavascriptContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new JavascriptContainer {ID = insertData.ObjectID};
		            objectToAdd.HtmlContent = serializedObject.HtmlContent;
					JavascriptContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "FooterContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.FooterContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new FooterContainer {ID = insertData.ObjectID};
		            objectToAdd.HtmlContent = serializedObject.HtmlContent;
					FooterContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "NavigationContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.NavigationContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new NavigationContainer {ID = insertData.ObjectID};
		            objectToAdd.Dummy = serializedObject.Dummy;
					NavigationContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AccountSummary")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AccountSummary.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AccountSummary {ID = insertData.ObjectID};
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.ActivitySummary = serializedObject.ActivitySummary;
		            objectToAdd.GroupSummary = serializedObject.GroupSummary;
					AccountSummaryTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AccountContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AccountContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AccountContainer {ID = insertData.ObjectID};
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.AccountIndex = serializedObject.AccountIndex;
		            objectToAdd.AccountModule = serializedObject.AccountModule;
		            objectToAdd.AccountSummary = serializedObject.AccountSummary;
					AccountContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AccountIndex")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AccountIndex.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AccountIndex {ID = insertData.ObjectID};
		            objectToAdd.Icon = serializedObject.Icon;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.Summary = serializedObject.Summary;
					AccountIndexTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AccountModule")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AccountModule.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AccountModule {ID = insertData.ObjectID};
		            objectToAdd.Profile = serializedObject.Profile;
		            objectToAdd.Security = serializedObject.Security;
		            objectToAdd.Roles = serializedObject.Roles;
		            objectToAdd.LocationCollection = serializedObject.LocationCollection;
					AccountModuleTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ImageGroupContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ImageGroupContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ImageGroupContainer {ID = insertData.ObjectID};
		            objectToAdd.ImageGroups = serializedObject.ImageGroups;
					ImageGroupContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "LocationContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.LocationContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new LocationContainer {ID = insertData.ObjectID};
		            objectToAdd.Locations = serializedObject.Locations;
					LocationContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AddressAndLocation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AddressAndLocation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AddressAndLocation {ID = insertData.ObjectID};
		            objectToAdd.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            objectToAdd.Address = serializedObject.Address;
		            objectToAdd.Location = serializedObject.Location;
					AddressAndLocationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "StreetAddress")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.StreetAddress.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new StreetAddress {ID = insertData.ObjectID};
		            objectToAdd.Street = serializedObject.Street;
		            objectToAdd.ZipCode = serializedObject.ZipCode;
		            objectToAdd.Town = serializedObject.Town;
		            objectToAdd.Country = serializedObject.Country;
					StreetAddressTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AccountContent")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AccountContent.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AccountContent {ID = insertData.ObjectID};
		            objectToAdd.Dummy = serializedObject.Dummy;
					AccountContentTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AccountProfile")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AccountProfile.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AccountProfile {ID = insertData.ObjectID};
		            objectToAdd.ProfileImage = serializedObject.ProfileImage;
		            objectToAdd.FirstName = serializedObject.FirstName;
		            objectToAdd.LastName = serializedObject.LastName;
		            objectToAdd.Address = serializedObject.Address;
		            objectToAdd.IsSimplifiedAccount = serializedObject.IsSimplifiedAccount;
		            objectToAdd.SimplifiedAccountEmail = serializedObject.SimplifiedAccountEmail;
		            objectToAdd.SimplifiedAccountGroupID = serializedObject.SimplifiedAccountGroupID;
					AccountProfileTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AccountSecurity")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AccountSecurity.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AccountSecurity {ID = insertData.ObjectID};
		            objectToAdd.LoginInfoCollection = serializedObject.LoginInfoCollection;
		            objectToAdd.EmailCollection = serializedObject.EmailCollection;
					AccountSecurityTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AccountRoles")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AccountRoles.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AccountRoles {ID = insertData.ObjectID};
		            objectToAdd.ModeratorInGroups = serializedObject.ModeratorInGroups;
		            objectToAdd.MemberInGroups = serializedObject.MemberInGroups;
		            objectToAdd.OrganizationsImPartOf = serializedObject.OrganizationsImPartOf;
					AccountRolesTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "PersonalInfoVisibility")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.PersonalInfoVisibility.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new PersonalInfoVisibility {ID = insertData.ObjectID};
		            objectToAdd.NoOne_Network_All = serializedObject.NoOne_Network_All;
					PersonalInfoVisibilityTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "GroupedInformation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.GroupedInformation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new GroupedInformation {ID = insertData.ObjectID};
		            objectToAdd.GroupName = serializedObject.GroupName;
		            objectToAdd.ReferenceCollection = serializedObject.ReferenceCollection;
					GroupedInformationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ReferenceToInformation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ReferenceToInformation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ReferenceToInformation {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.URL = serializedObject.URL;
					ReferenceToInformationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "BlogContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.BlogContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new BlogContainer {ID = insertData.ObjectID};
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.FeaturedBlog = serializedObject.FeaturedBlog;
		            objectToAdd.RecentBlogSummary = serializedObject.RecentBlogSummary;
		            objectToAdd.BlogIndexGroup = serializedObject.BlogIndexGroup;
					BlogContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "RecentBlogSummary")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.RecentBlogSummary.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new RecentBlogSummary {ID = insertData.ObjectID};
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.RecentBlogCollection = serializedObject.RecentBlogCollection;
					RecentBlogSummaryTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "NodeSummaryContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.NodeSummaryContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new NodeSummaryContainer {ID = insertData.ObjectID};
		            objectToAdd.Nodes = serializedObject.Nodes;
		            objectToAdd.NodeSourceBlogs = serializedObject.NodeSourceBlogs;
		            objectToAdd.NodeSourceActivities = serializedObject.NodeSourceActivities;
		            objectToAdd.NodeSourceTextContent = serializedObject.NodeSourceTextContent;
		            objectToAdd.NodeSourceLinkToContent = serializedObject.NodeSourceLinkToContent;
		            objectToAdd.NodeSourceEmbeddedContent = serializedObject.NodeSourceEmbeddedContent;
		            objectToAdd.NodeSourceImages = serializedObject.NodeSourceImages;
		            objectToAdd.NodeSourceBinaryFiles = serializedObject.NodeSourceBinaryFiles;
		            objectToAdd.NodeSourceCategories = serializedObject.NodeSourceCategories;
					NodeSummaryContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "RenderedNode")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.RenderedNode.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new RenderedNode {ID = insertData.ObjectID};
		            objectToAdd.OriginalContentID = serializedObject.OriginalContentID;
		            objectToAdd.TechnicalSource = serializedObject.TechnicalSource;
		            objectToAdd.ImageBaseUrl = serializedObject.ImageBaseUrl;
		            objectToAdd.ImageExt = serializedObject.ImageExt;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.ActualContentUrl = serializedObject.ActualContentUrl;
		            objectToAdd.Excerpt = serializedObject.Excerpt;
		            objectToAdd.TimestampText = serializedObject.TimestampText;
		            objectToAdd.MainSortableText = serializedObject.MainSortableText;
		            objectToAdd.IsCategoryFilteringNode = serializedObject.IsCategoryFilteringNode;
		            objectToAdd.CategoryFilters = serializedObject.CategoryFilters;
		            objectToAdd.CategoryNames = serializedObject.CategoryNames;
		            objectToAdd.Categories = serializedObject.Categories;
		            objectToAdd.CategoryIDList = serializedObject.CategoryIDList;
		            objectToAdd.Authors = serializedObject.Authors;
		            objectToAdd.Locations = serializedObject.Locations;
					RenderedNodeTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ShortTextObject")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ShortTextObject.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ShortTextObject {ID = insertData.ObjectID};
		            objectToAdd.Content = serializedObject.Content;
					ShortTextObjectTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "LongTextObject")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.LongTextObject.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new LongTextObject {ID = insertData.ObjectID};
		            objectToAdd.Content = serializedObject.Content;
					LongTextObjectTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "MapContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.MapContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new MapContainer {ID = insertData.ObjectID};
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.MapFeatured = serializedObject.MapFeatured;
		            objectToAdd.MapCollection = serializedObject.MapCollection;
		            objectToAdd.MapResultCollection = serializedObject.MapResultCollection;
		            objectToAdd.MapIndexCollection = serializedObject.MapIndexCollection;
		            objectToAdd.MarkerSourceLocations = serializedObject.MarkerSourceLocations;
		            objectToAdd.MarkerSourceBlogs = serializedObject.MarkerSourceBlogs;
		            objectToAdd.MarkerSourceActivities = serializedObject.MarkerSourceActivities;
		            objectToAdd.MapMarkers = serializedObject.MapMarkers;
					MapContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "MapMarker")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.MapMarker.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new MapMarker {ID = insertData.ObjectID};
		            objectToAdd.IconUrl = serializedObject.IconUrl;
		            objectToAdd.MarkerSource = serializedObject.MarkerSource;
		            objectToAdd.CategoryName = serializedObject.CategoryName;
		            objectToAdd.LocationText = serializedObject.LocationText;
		            objectToAdd.PopupTitle = serializedObject.PopupTitle;
		            objectToAdd.PopupContent = serializedObject.PopupContent;
		            objectToAdd.Location = serializedObject.Location;
					MapMarkerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "CalendarContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.CalendarContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new CalendarContainer {ID = insertData.ObjectID};
		            objectToAdd.CalendarContainerHeader = serializedObject.CalendarContainerHeader;
		            objectToAdd.CalendarFeatured = serializedObject.CalendarFeatured;
		            objectToAdd.CalendarCollection = serializedObject.CalendarCollection;
		            objectToAdd.CalendarIndexCollection = serializedObject.CalendarIndexCollection;
					CalendarContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AboutContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AboutContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AboutContainer {ID = insertData.ObjectID};
		            objectToAdd.MainImage = serializedObject.MainImage;
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.Excerpt = serializedObject.Excerpt;
		            objectToAdd.Body = serializedObject.Body;
		            objectToAdd.Published = serializedObject.Published;
		            objectToAdd.Author = serializedObject.Author;
		            objectToAdd.ImageGroup = serializedObject.ImageGroup;
					AboutContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "OBSAccountContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.OBSAccountContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new OBSAccountContainer {ID = insertData.ObjectID};
		            objectToAdd.AccountContainerHeader = serializedObject.AccountContainerHeader;
		            objectToAdd.AccountFeatured = serializedObject.AccountFeatured;
		            objectToAdd.AccountCollection = serializedObject.AccountCollection;
		            objectToAdd.AccountIndexCollection = serializedObject.AccountIndexCollection;
					OBSAccountContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ProjectContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ProjectContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ProjectContainer {ID = insertData.ObjectID};
		            objectToAdd.ProjectContainerHeader = serializedObject.ProjectContainerHeader;
		            objectToAdd.ProjectFeatured = serializedObject.ProjectFeatured;
		            objectToAdd.ProjectCollection = serializedObject.ProjectCollection;
		            objectToAdd.ProjectIndexCollection = serializedObject.ProjectIndexCollection;
					ProjectContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "CourseContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.CourseContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new CourseContainer {ID = insertData.ObjectID};
		            objectToAdd.CourseContainerHeader = serializedObject.CourseContainerHeader;
		            objectToAdd.CourseFeatured = serializedObject.CourseFeatured;
		            objectToAdd.CourseCollection = serializedObject.CourseCollection;
		            objectToAdd.CourseIndexCollection = serializedObject.CourseIndexCollection;
					CourseContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ContainerHeader")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ContainerHeader.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ContainerHeader {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.SubTitle = serializedObject.SubTitle;
					ContainerHeaderTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ActivitySummaryContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ActivitySummaryContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ActivitySummaryContainer {ID = insertData.ObjectID};
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.SummaryBody = serializedObject.SummaryBody;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.ActivityIndex = serializedObject.ActivityIndex;
		            objectToAdd.ActivityCollection = serializedObject.ActivityCollection;
					ActivitySummaryContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ActivityIndex")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ActivityIndex.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ActivityIndex {ID = insertData.ObjectID};
		            objectToAdd.Icon = serializedObject.Icon;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.Summary = serializedObject.Summary;
					ActivityIndexTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ActivityContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ActivityContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ActivityContainer {ID = insertData.ObjectID};
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.ActivityIndex = serializedObject.ActivityIndex;
		            objectToAdd.ActivityModule = serializedObject.ActivityModule;
					ActivityContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Activity")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Activity.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Activity {ID = insertData.ObjectID};
		            objectToAdd.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            objectToAdd.ProfileImage = serializedObject.ProfileImage;
		            objectToAdd.IconImage = serializedObject.IconImage;
		            objectToAdd.ActivityName = serializedObject.ActivityName;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.ContactPerson = serializedObject.ContactPerson;
		            objectToAdd.StartingTime = serializedObject.StartingTime;
		            objectToAdd.Excerpt = serializedObject.Excerpt;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.IFrameSources = serializedObject.IFrameSources;
		            objectToAdd.Collaborators = serializedObject.Collaborators;
		            objectToAdd.ImageGroupCollection = serializedObject.ImageGroupCollection;
		            objectToAdd.LocationCollection = serializedObject.LocationCollection;
		            objectToAdd.CategoryCollection = serializedObject.CategoryCollection;
					ActivityTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Moderator")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Moderator.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Moderator {ID = insertData.ObjectID};
		            objectToAdd.ModeratorName = serializedObject.ModeratorName;
		            objectToAdd.ProfileUrl = serializedObject.ProfileUrl;
					ModeratorTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Collaborator")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Collaborator.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Collaborator {ID = insertData.ObjectID};
		            objectToAdd.AccountID = serializedObject.AccountID;
		            objectToAdd.EmailAddress = serializedObject.EmailAddress;
		            objectToAdd.CollaboratorName = serializedObject.CollaboratorName;
		            objectToAdd.Role = serializedObject.Role;
		            objectToAdd.ProfileUrl = serializedObject.ProfileUrl;
					CollaboratorTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "GroupSummaryContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.GroupSummaryContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new GroupSummaryContainer {ID = insertData.ObjectID};
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.SummaryBody = serializedObject.SummaryBody;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.GroupSummaryIndex = serializedObject.GroupSummaryIndex;
		            objectToAdd.GroupCollection = serializedObject.GroupCollection;
					GroupSummaryContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "GroupContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.GroupContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new GroupContainer {ID = insertData.ObjectID};
		            objectToAdd.Header = serializedObject.Header;
		            objectToAdd.GroupIndex = serializedObject.GroupIndex;
		            objectToAdd.GroupProfile = serializedObject.GroupProfile;
		            objectToAdd.Collaborators = serializedObject.Collaborators;
		            objectToAdd.PendingCollaborators = serializedObject.PendingCollaborators;
		            objectToAdd.Activities = serializedObject.Activities;
		            objectToAdd.ImageGroupCollection = serializedObject.ImageGroupCollection;
		            objectToAdd.LocationCollection = serializedObject.LocationCollection;
					GroupContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "GroupIndex")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.GroupIndex.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new GroupIndex {ID = insertData.ObjectID};
		            objectToAdd.Icon = serializedObject.Icon;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.Summary = serializedObject.Summary;
					GroupIndexTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AddAddressAndLocationInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AddAddressAndLocationInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AddAddressAndLocationInfo {ID = insertData.ObjectID};
		            objectToAdd.LocationName = serializedObject.LocationName;
					AddAddressAndLocationInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AddImageInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AddImageInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AddImageInfo {ID = insertData.ObjectID};
		            objectToAdd.ImageTitle = serializedObject.ImageTitle;
					AddImageInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AddImageGroupInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AddImageGroupInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AddImageGroupInfo {ID = insertData.ObjectID};
		            objectToAdd.ImageGroupTitle = serializedObject.ImageGroupTitle;
					AddImageGroupInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AddEmailAddressInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AddEmailAddressInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AddEmailAddressInfo {ID = insertData.ObjectID};
		            objectToAdd.EmailAddress = serializedObject.EmailAddress;
					AddEmailAddressInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "CreateGroupInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.CreateGroupInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new CreateGroupInfo {ID = insertData.ObjectID};
		            objectToAdd.GroupName = serializedObject.GroupName;
					CreateGroupInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AddActivityInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AddActivityInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AddActivityInfo {ID = insertData.ObjectID};
		            objectToAdd.ActivityName = serializedObject.ActivityName;
					AddActivityInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AddBlogPostInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AddBlogPostInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AddBlogPostInfo {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
					AddBlogPostInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AddCategoryInfo")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AddCategoryInfo.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AddCategoryInfo {ID = insertData.ObjectID};
		            objectToAdd.CategoryName = serializedObject.CategoryName;
					AddCategoryInfoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Group")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Group.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Group {ID = insertData.ObjectID};
		            objectToAdd.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            objectToAdd.ProfileImage = serializedObject.ProfileImage;
		            objectToAdd.IconImage = serializedObject.IconImage;
		            objectToAdd.GroupName = serializedObject.GroupName;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.OrganizationsAndGroupsLinkedToUs = serializedObject.OrganizationsAndGroupsLinkedToUs;
		            objectToAdd.WwwSiteToPublishTo = serializedObject.WwwSiteToPublishTo;
		            objectToAdd.CustomUICollection = serializedObject.CustomUICollection;
		            objectToAdd.Moderators = serializedObject.Moderators;
		            objectToAdd.CategoryCollection = serializedObject.CategoryCollection;
					GroupTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Introduction")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Introduction.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Introduction {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Body = serializedObject.Body;
					IntroductionTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ContentCategoryRank")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ContentCategoryRank.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ContentCategoryRank {ID = insertData.ObjectID};
		            objectToAdd.ContentID = serializedObject.ContentID;
		            objectToAdd.ContentSemanticType = serializedObject.ContentSemanticType;
		            objectToAdd.CategoryID = serializedObject.CategoryID;
		            objectToAdd.RankName = serializedObject.RankName;
		            objectToAdd.RankValue = serializedObject.RankValue;
					ContentCategoryRankTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "LinkToContent")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.LinkToContent.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new LinkToContent {ID = insertData.ObjectID};
		            objectToAdd.URL = serializedObject.URL;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.Published = serializedObject.Published;
		            objectToAdd.Author = serializedObject.Author;
		            objectToAdd.ImageData = serializedObject.ImageData;
		            objectToAdd.Locations = serializedObject.Locations;
		            objectToAdd.Categories = serializedObject.Categories;
					LinkToContentTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "EmbeddedContent")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.EmbeddedContent.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new EmbeddedContent {ID = insertData.ObjectID};
		            objectToAdd.IFrameTagContents = serializedObject.IFrameTagContents;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Published = serializedObject.Published;
		            objectToAdd.Author = serializedObject.Author;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.Locations = serializedObject.Locations;
		            objectToAdd.Categories = serializedObject.Categories;
					EmbeddedContentTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "DynamicContentGroup")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.DynamicContentGroup.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new DynamicContentGroup {ID = insertData.ObjectID};
		            objectToAdd.HostName = serializedObject.HostName;
		            objectToAdd.GroupHeader = serializedObject.GroupHeader;
		            objectToAdd.SortValue = serializedObject.SortValue;
		            objectToAdd.PageLocation = serializedObject.PageLocation;
		            objectToAdd.ContentItemNames = serializedObject.ContentItemNames;
					DynamicContentGroupTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "DynamicContent")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.DynamicContent.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new DynamicContent {ID = insertData.ObjectID};
		            objectToAdd.HostName = serializedObject.HostName;
		            objectToAdd.ContentName = serializedObject.ContentName;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.ElementQuery = serializedObject.ElementQuery;
		            objectToAdd.Content = serializedObject.Content;
		            objectToAdd.RawContent = serializedObject.RawContent;
		            objectToAdd.ImageData = serializedObject.ImageData;
		            objectToAdd.IsEnabled = serializedObject.IsEnabled;
		            objectToAdd.ApplyActively = serializedObject.ApplyActively;
		            objectToAdd.EditType = serializedObject.EditType;
		            objectToAdd.PageLocation = serializedObject.PageLocation;
					DynamicContentTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AttachedToObject")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AttachedToObject.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AttachedToObject {ID = insertData.ObjectID};
		            objectToAdd.SourceObjectID = serializedObject.SourceObjectID;
		            objectToAdd.SourceObjectName = serializedObject.SourceObjectName;
		            objectToAdd.SourceObjectDomain = serializedObject.SourceObjectDomain;
		            objectToAdd.TargetObjectID = serializedObject.TargetObjectID;
		            objectToAdd.TargetObjectName = serializedObject.TargetObjectName;
		            objectToAdd.TargetObjectDomain = serializedObject.TargetObjectDomain;
					AttachedToObjectTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Comment")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Comment.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Comment {ID = insertData.ObjectID};
		            objectToAdd.TargetObjectID = serializedObject.TargetObjectID;
		            objectToAdd.TargetObjectName = serializedObject.TargetObjectName;
		            objectToAdd.TargetObjectDomain = serializedObject.TargetObjectDomain;
		            objectToAdd.CommentText = serializedObject.CommentText;
		            objectToAdd.Created = serializedObject.Created;
		            objectToAdd.OriginalAuthorName = serializedObject.OriginalAuthorName;
		            objectToAdd.OriginalAuthorEmail = serializedObject.OriginalAuthorEmail;
		            objectToAdd.OriginalAuthorAccountID = serializedObject.OriginalAuthorAccountID;
		            objectToAdd.LastModified = serializedObject.LastModified;
		            objectToAdd.LastAuthorName = serializedObject.LastAuthorName;
		            objectToAdd.LastAuthorEmail = serializedObject.LastAuthorEmail;
		            objectToAdd.LastAuthorAccountID = serializedObject.LastAuthorAccountID;
					CommentTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Selection")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Selection.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Selection {ID = insertData.ObjectID};
		            objectToAdd.TargetObjectID = serializedObject.TargetObjectID;
		            objectToAdd.TargetObjectName = serializedObject.TargetObjectName;
		            objectToAdd.TargetObjectDomain = serializedObject.TargetObjectDomain;
		            objectToAdd.SelectionCategory = serializedObject.SelectionCategory;
		            objectToAdd.TextValue = serializedObject.TextValue;
		            objectToAdd.BooleanValue = serializedObject.BooleanValue;
		            objectToAdd.DoubleValue = serializedObject.DoubleValue;
					SelectionTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "TextContent")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.TextContent.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new TextContent {ID = insertData.ObjectID};
		            objectToAdd.ImageData = serializedObject.ImageData;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.SubTitle = serializedObject.SubTitle;
		            objectToAdd.Published = serializedObject.Published;
		            objectToAdd.Author = serializedObject.Author;
		            objectToAdd.Excerpt = serializedObject.Excerpt;
		            objectToAdd.Body = serializedObject.Body;
		            objectToAdd.Locations = serializedObject.Locations;
		            objectToAdd.Categories = serializedObject.Categories;
		            objectToAdd.SortOrderNumber = serializedObject.SortOrderNumber;
		            objectToAdd.IFrameSources = serializedObject.IFrameSources;
		            objectToAdd.RawHtmlContent = serializedObject.RawHtmlContent;
					TextContentTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Blog")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Blog.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Blog {ID = insertData.ObjectID};
		            objectToAdd.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            objectToAdd.ProfileImage = serializedObject.ProfileImage;
		            objectToAdd.IconImage = serializedObject.IconImage;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.SubTitle = serializedObject.SubTitle;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.Published = serializedObject.Published;
		            objectToAdd.Author = serializedObject.Author;
		            objectToAdd.FeaturedImage = serializedObject.FeaturedImage;
		            objectToAdd.ImageGroupCollection = serializedObject.ImageGroupCollection;
		            objectToAdd.VideoGroup = serializedObject.VideoGroup;
		            objectToAdd.Body = serializedObject.Body;
		            objectToAdd.Excerpt = serializedObject.Excerpt;
		            objectToAdd.IFrameSources = serializedObject.IFrameSources;
		            objectToAdd.LocationCollection = serializedObject.LocationCollection;
		            objectToAdd.CategoryCollection = serializedObject.CategoryCollection;
		            objectToAdd.SocialPanel = serializedObject.SocialPanel;
					BlogTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "BlogIndexGroup")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.BlogIndexGroup.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new BlogIndexGroup {ID = insertData.ObjectID};
		            objectToAdd.Icon = serializedObject.Icon;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.GroupedByDate = serializedObject.GroupedByDate;
		            objectToAdd.GroupedByLocation = serializedObject.GroupedByLocation;
		            objectToAdd.GroupedByAuthor = serializedObject.GroupedByAuthor;
		            objectToAdd.GroupedByCategory = serializedObject.GroupedByCategory;
		            objectToAdd.FullBlogArchive = serializedObject.FullBlogArchive;
		            objectToAdd.BlogSourceForSummary = serializedObject.BlogSourceForSummary;
		            objectToAdd.Summary = serializedObject.Summary;
					BlogIndexGroupTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "CalendarIndex")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.CalendarIndex.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new CalendarIndex {ID = insertData.ObjectID};
		            objectToAdd.Icon = serializedObject.Icon;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Introduction = serializedObject.Introduction;
		            objectToAdd.Summary = serializedObject.Summary;
					CalendarIndexTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Filter")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Filter.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Filter {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
					FilterTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Calendar")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Calendar.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Calendar {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
					CalendarTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Map")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Map.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Map {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
					MapTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "MapIndexCollection")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.MapIndexCollection.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new MapIndexCollection {ID = insertData.ObjectID};
		            objectToAdd.MapByDate = serializedObject.MapByDate;
		            objectToAdd.MapByLocation = serializedObject.MapByLocation;
		            objectToAdd.MapByAuthor = serializedObject.MapByAuthor;
		            objectToAdd.MapByCategory = serializedObject.MapByCategory;
					MapIndexCollectionTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "MapResult")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.MapResult.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new MapResult {ID = insertData.ObjectID};
		            objectToAdd.Location = serializedObject.Location;
					MapResultTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "MapResultsCollection")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.MapResultsCollection.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new MapResultsCollection {ID = insertData.ObjectID};
		            objectToAdd.ResultByDate = serializedObject.ResultByDate;
		            objectToAdd.ResultByAuthor = serializedObject.ResultByAuthor;
		            objectToAdd.ResultByProximity = serializedObject.ResultByProximity;
					MapResultsCollectionTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Video")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Video.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Video {ID = insertData.ObjectID};
		            objectToAdd.VideoData = serializedObject.VideoData;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Caption = serializedObject.Caption;
					VideoTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Image")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Image.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Image {ID = insertData.ObjectID};
		            objectToAdd.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            objectToAdd.ImageData = serializedObject.ImageData;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Caption = serializedObject.Caption;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.Locations = serializedObject.Locations;
		            objectToAdd.Categories = serializedObject.Categories;
					ImageTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "BinaryFile")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.BinaryFile.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new BinaryFile {ID = insertData.ObjectID};
		            objectToAdd.OriginalFileName = serializedObject.OriginalFileName;
		            objectToAdd.Data = serializedObject.Data;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.Categories = serializedObject.Categories;
					BinaryFileTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ImageGroup")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ImageGroup.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ImageGroup {ID = insertData.ObjectID};
		            objectToAdd.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.FeaturedImage = serializedObject.FeaturedImage;
		            objectToAdd.ImagesCollection = serializedObject.ImagesCollection;
					ImageGroupTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "VideoGroup")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.VideoGroup.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new VideoGroup {ID = insertData.ObjectID};
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Description = serializedObject.Description;
		            objectToAdd.VideoCollection = serializedObject.VideoCollection;
					VideoGroupTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Tooltip")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Tooltip.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Tooltip {ID = insertData.ObjectID};
		            objectToAdd.TooltipText = serializedObject.TooltipText;
					TooltipTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "SocialPanel")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.SocialPanel.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new SocialPanel {ID = insertData.ObjectID};
		            objectToAdd.SocialFilter = serializedObject.SocialFilter;
					SocialPanelTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Longitude")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Longitude.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Longitude {ID = insertData.ObjectID};
		            objectToAdd.TextValue = serializedObject.TextValue;
					LongitudeTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Latitude")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Latitude.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Latitude {ID = insertData.ObjectID};
		            objectToAdd.TextValue = serializedObject.TextValue;
					LatitudeTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Location")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Location.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Location {ID = insertData.ObjectID};
		            objectToAdd.LocationName = serializedObject.LocationName;
		            objectToAdd.Longitude = serializedObject.Longitude;
		            objectToAdd.Latitude = serializedObject.Latitude;
					LocationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Date")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Date.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Date {ID = insertData.ObjectID};
		            objectToAdd.Day = serializedObject.Day;
		            objectToAdd.Week = serializedObject.Week;
		            objectToAdd.Month = serializedObject.Month;
		            objectToAdd.Year = serializedObject.Year;
					DateTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Sex")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Sex.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Sex {ID = insertData.ObjectID};
		            objectToAdd.SexText = serializedObject.SexText;
					SexTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "OBSAddress")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.OBSAddress.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new OBSAddress {ID = insertData.ObjectID};
		            objectToAdd.StreetName = serializedObject.StreetName;
		            objectToAdd.BuildingNumber = serializedObject.BuildingNumber;
		            objectToAdd.PostOfficeBox = serializedObject.PostOfficeBox;
		            objectToAdd.PostalCode = serializedObject.PostalCode;
		            objectToAdd.Municipality = serializedObject.Municipality;
		            objectToAdd.Region = serializedObject.Region;
		            objectToAdd.Province = serializedObject.Province;
		            objectToAdd.state = serializedObject.state;
		            objectToAdd.Country = serializedObject.Country;
		            objectToAdd.Continent = serializedObject.Continent;
					OBSAddressTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Identity")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Identity.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Identity {ID = insertData.ObjectID};
		            objectToAdd.FirstName = serializedObject.FirstName;
		            objectToAdd.LastName = serializedObject.LastName;
		            objectToAdd.Initials = serializedObject.Initials;
		            objectToAdd.Sex = serializedObject.Sex;
		            objectToAdd.Birthday = serializedObject.Birthday;
					IdentityTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "ImageVideoSoundVectorRaw")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.ImageVideoSoundVectorRaw.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new ImageVideoSoundVectorRaw {ID = insertData.ObjectID};
		            objectToAdd.Image = serializedObject.Image;
		            objectToAdd.Video = serializedObject.Video;
		            objectToAdd.Sound = serializedObject.Sound;
		            objectToAdd.Vector = serializedObject.Vector;
		            objectToAdd.Raw = serializedObject.Raw;
					ImageVideoSoundVectorRawTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "CategoryContainer")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.CategoryContainer.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new CategoryContainer {ID = insertData.ObjectID};
		            objectToAdd.Categories = serializedObject.Categories;
					CategoryContainerTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Category")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Category.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Category {ID = insertData.ObjectID};
		            objectToAdd.ReferenceToInformation = serializedObject.ReferenceToInformation;
		            objectToAdd.CategoryName = serializedObject.CategoryName;
		            objectToAdd.ImageData = serializedObject.ImageData;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Excerpt = serializedObject.Excerpt;
		            objectToAdd.ParentCategory = serializedObject.ParentCategory;
		            objectToAdd.ParentCategoryID = serializedObject.ParentCategoryID;
					CategoryTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Subscription")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Subscription.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Subscription {ID = insertData.ObjectID};
		            objectToAdd.Priority = serializedObject.Priority;
		            objectToAdd.TargetRelativeLocation = serializedObject.TargetRelativeLocation;
		            objectToAdd.TargetInformationObjectType = serializedObject.TargetInformationObjectType;
		            objectToAdd.SubscriberRelativeLocation = serializedObject.SubscriberRelativeLocation;
		            objectToAdd.SubscriberInformationObjectType = serializedObject.SubscriberInformationObjectType;
		            objectToAdd.SubscriptionType = serializedObject.SubscriptionType;
					SubscriptionTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "QueueEnvelope")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.QueueEnvelope.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new QueueEnvelope {ID = insertData.ObjectID};
		            objectToAdd.ActiveContainerName = serializedObject.ActiveContainerName;
		            objectToAdd.OwnerPrefix = serializedObject.OwnerPrefix;
		            objectToAdd.CurrentRetryCount = serializedObject.CurrentRetryCount;
		            objectToAdd.SingleOperation = serializedObject.SingleOperation;
		            objectToAdd.OrderDependentOperationSequence = serializedObject.OrderDependentOperationSequence;
		            objectToAdd.ErrorContent = serializedObject.ErrorContent;
					QueueEnvelopeTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "OperationRequest")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.OperationRequest.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new OperationRequest {ID = insertData.ObjectID};
		            objectToAdd.SubscriberNotification = serializedObject.SubscriberNotification;
		            objectToAdd.SubscriptionChainRequest = serializedObject.SubscriptionChainRequest;
		            objectToAdd.UpdateWebContentOperation = serializedObject.UpdateWebContentOperation;
		            objectToAdd.RefreshDefaultViewsOperation = serializedObject.RefreshDefaultViewsOperation;
		            objectToAdd.DeleteEntireOwner = serializedObject.DeleteEntireOwner;
		            objectToAdd.DeleteOwnerContent = serializedObject.DeleteOwnerContent;
		            objectToAdd.PublishWebContent = serializedObject.PublishWebContent;
		            objectToAdd.ProcessIDToExecute = serializedObject.ProcessIDToExecute;
					OperationRequestTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "SubscriptionChainRequestMessage")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.SubscriptionChainRequestMessage.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new SubscriptionChainRequestMessage {ID = insertData.ObjectID};
		            objectToAdd.ContentItemID = serializedObject.ContentItemID;
					SubscriptionChainRequestMessageTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "SubscriptionChainRequestContent")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.SubscriptionChainRequestContent.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new SubscriptionChainRequestContent {ID = insertData.ObjectID};
		            objectToAdd.SubmitTime = serializedObject.SubmitTime;
		            objectToAdd.ProcessingStartTime = serializedObject.ProcessingStartTime;
		            objectToAdd.ProcessingEndTimeInformationObjects = serializedObject.ProcessingEndTimeInformationObjects;
		            objectToAdd.ProcessingEndTimeWebTemplatesRendering = serializedObject.ProcessingEndTimeWebTemplatesRendering;
		            objectToAdd.ProcessingEndTime = serializedObject.ProcessingEndTime;
		            objectToAdd.SubscriptionTargetCollection = serializedObject.SubscriptionTargetCollection;
					SubscriptionChainRequestContentTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "SubscriptionTarget")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.SubscriptionTarget.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new SubscriptionTarget {ID = insertData.ObjectID};
		            objectToAdd.BlobLocation = serializedObject.BlobLocation;
					SubscriptionTargetTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "DeleteEntireOwnerOperation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.DeleteEntireOwnerOperation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new DeleteEntireOwnerOperation {ID = insertData.ObjectID};
		            objectToAdd.ContainerName = serializedObject.ContainerName;
		            objectToAdd.LocationPrefix = serializedObject.LocationPrefix;
					DeleteEntireOwnerOperationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "DeleteOwnerContentOperation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.DeleteOwnerContentOperation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new DeleteOwnerContentOperation {ID = insertData.ObjectID};
		            objectToAdd.ContainerName = serializedObject.ContainerName;
		            objectToAdd.LocationPrefix = serializedObject.LocationPrefix;
					DeleteOwnerContentOperationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "SystemError")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.SystemError.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new SystemError {ID = insertData.ObjectID};
		            objectToAdd.ErrorTitle = serializedObject.ErrorTitle;
		            objectToAdd.OccurredAt = serializedObject.OccurredAt;
		            objectToAdd.SystemErrorItems = serializedObject.SystemErrorItems;
		            objectToAdd.MessageContent = serializedObject.MessageContent;
					SystemErrorTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "SystemErrorItem")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.SystemErrorItem.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new SystemErrorItem {ID = insertData.ObjectID};
		            objectToAdd.ShortDescription = serializedObject.ShortDescription;
		            objectToAdd.LongDescription = serializedObject.LongDescription;
					SystemErrorItemTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "InformationSource")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.InformationSource.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new InformationSource {ID = insertData.ObjectID};
		            objectToAdd.SourceName = serializedObject.SourceName;
		            objectToAdd.SourceLocation = serializedObject.SourceLocation;
		            objectToAdd.SourceType = serializedObject.SourceType;
		            objectToAdd.IsDynamic = serializedObject.IsDynamic;
		            objectToAdd.SourceInformationObjectType = serializedObject.SourceInformationObjectType;
		            objectToAdd.SourceETag = serializedObject.SourceETag;
		            objectToAdd.SourceMD5 = serializedObject.SourceMD5;
		            objectToAdd.SourceLastModified = serializedObject.SourceLastModified;
					InformationSourceTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "RefreshDefaultViewsOperation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.RefreshDefaultViewsOperation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new RefreshDefaultViewsOperation {ID = insertData.ObjectID};
		            objectToAdd.ViewLocation = serializedObject.ViewLocation;
		            objectToAdd.TypeNameToRefresh = serializedObject.TypeNameToRefresh;
					RefreshDefaultViewsOperationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "UpdateWebContentOperation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.UpdateWebContentOperation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new UpdateWebContentOperation {ID = insertData.ObjectID};
		            objectToAdd.SourceContainerName = serializedObject.SourceContainerName;
		            objectToAdd.SourcePathRoot = serializedObject.SourcePathRoot;
		            objectToAdd.TargetContainerName = serializedObject.TargetContainerName;
		            objectToAdd.TargetPathRoot = serializedObject.TargetPathRoot;
		            objectToAdd.RenderWhileSync = serializedObject.RenderWhileSync;
		            objectToAdd.Handlers = serializedObject.Handlers;
					UpdateWebContentOperationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "UpdateWebContentHandlerItem")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.UpdateWebContentHandlerItem.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new UpdateWebContentHandlerItem {ID = insertData.ObjectID};
		            objectToAdd.InformationTypeName = serializedObject.InformationTypeName;
		            objectToAdd.OptionName = serializedObject.OptionName;
					UpdateWebContentHandlerItemTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "PublishWebContentOperation")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.PublishWebContentOperation.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new PublishWebContentOperation {ID = insertData.ObjectID};
		            objectToAdd.SourceContainerName = serializedObject.SourceContainerName;
		            objectToAdd.SourcePathRoot = serializedObject.SourcePathRoot;
		            objectToAdd.SourceOwner = serializedObject.SourceOwner;
		            objectToAdd.TargetContainerName = serializedObject.TargetContainerName;
					PublishWebContentOperationTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "SubscriberInput")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.SubscriberInput.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new SubscriberInput {ID = insertData.ObjectID};
		            objectToAdd.InputRelativeLocation = serializedObject.InputRelativeLocation;
		            objectToAdd.InformationObjectName = serializedObject.InformationObjectName;
		            objectToAdd.InformationItemName = serializedObject.InformationItemName;
		            objectToAdd.SubscriberRelativeLocation = serializedObject.SubscriberRelativeLocation;
					SubscriberInputTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "Monitor")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.Monitor.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new Monitor {ID = insertData.ObjectID};
		            objectToAdd.TargetObjectName = serializedObject.TargetObjectName;
		            objectToAdd.TargetItemName = serializedObject.TargetItemName;
		            objectToAdd.MonitoringUtcTimeStampToStart = serializedObject.MonitoringUtcTimeStampToStart;
		            objectToAdd.MonitoringCycleFrequencyUnit = serializedObject.MonitoringCycleFrequencyUnit;
		            objectToAdd.MonitoringCycleEveryXthOfUnit = serializedObject.MonitoringCycleEveryXthOfUnit;
		            objectToAdd.CustomMonitoringCycleOperationName = serializedObject.CustomMonitoringCycleOperationName;
		            objectToAdd.OperationActionName = serializedObject.OperationActionName;
					MonitorTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "IconTitleDescription")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.IconTitleDescription.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new IconTitleDescription {ID = insertData.ObjectID};
		            objectToAdd.Icon = serializedObject.Icon;
		            objectToAdd.Title = serializedObject.Title;
		            objectToAdd.Description = serializedObject.Description;
					IconTitleDescriptionTable.InsertOnSubmit(objectToAdd);
                    return;
                }
                if (insertData.ObjectType == "AboutAGIApplications")
                {
                    string currentFullStoragePath = Path.Combine(storageRootPath, insertData.CurrentStoragePath);
                    var serializedObject =
                        global::TheBall.Payments.AboutAGIApplications.DeserializeFromXml(
                            ContentStorage.GetContentAsString(currentFullStoragePath));
                    var objectToAdd = new AboutAGIApplications {ID = insertData.ObjectID};
		            objectToAdd.BuiltForAnybody = serializedObject.BuiltForAnybody;
		            objectToAdd.ForAllPeople = serializedObject.ForAllPeople;
					AboutAGIApplicationsTable.InsertOnSubmit(objectToAdd);
                    return;
                }
            }

		    public void PerformDelete(string storageRootPath, InformationObjectMetaData deleteData)
		    {
                if (deleteData.SemanticDomain != "TheBall.Payments")
                    throw new InvalidDataException("Mismatch on domain data");
				InformationObjectMetaDataTable.DeleteOnSubmit(deleteData);
		        if (deleteData.ObjectType == "TBSystem")
		        {
                    TBSystemTable.DeleteOnSubmit(new TBSystem { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "WebPublishInfo")
		        {
                    WebPublishInfoTable.DeleteOnSubmit(new WebPublishInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "PublicationPackage")
		        {
                    PublicationPackageTable.DeleteOnSubmit(new PublicationPackage { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBRLoginRoot")
		        {
                    TBRLoginRootTable.DeleteOnSubmit(new TBRLoginRoot { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBRAccountRoot")
		        {
                    TBRAccountRootTable.DeleteOnSubmit(new TBRAccountRoot { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBRGroupRoot")
		        {
                    TBRGroupRootTable.DeleteOnSubmit(new TBRGroupRoot { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBRLoginGroupRoot")
		        {
                    TBRLoginGroupRootTable.DeleteOnSubmit(new TBRLoginGroupRoot { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBREmailRoot")
		        {
                    TBREmailRootTable.DeleteOnSubmit(new TBREmailRoot { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBAccount")
		        {
                    TBAccountTable.DeleteOnSubmit(new TBAccount { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBAccountCollaborationGroup")
		        {
                    TBAccountCollaborationGroupTable.DeleteOnSubmit(new TBAccountCollaborationGroup { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBLoginInfo")
		        {
                    TBLoginInfoTable.DeleteOnSubmit(new TBLoginInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBEmail")
		        {
                    TBEmailTable.DeleteOnSubmit(new TBEmail { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBCollaboratorRole")
		        {
                    TBCollaboratorRoleTable.DeleteOnSubmit(new TBCollaboratorRole { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBCollaboratingGroup")
		        {
                    TBCollaboratingGroupTable.DeleteOnSubmit(new TBCollaboratingGroup { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBEmailValidation")
		        {
                    TBEmailValidationTable.DeleteOnSubmit(new TBEmailValidation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBMergeAccountConfirmation")
		        {
                    TBMergeAccountConfirmationTable.DeleteOnSubmit(new TBMergeAccountConfirmation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBGroupJoinConfirmation")
		        {
                    TBGroupJoinConfirmationTable.DeleteOnSubmit(new TBGroupJoinConfirmation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBDeviceJoinConfirmation")
		        {
                    TBDeviceJoinConfirmationTable.DeleteOnSubmit(new TBDeviceJoinConfirmation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBInformationInputConfirmation")
		        {
                    TBInformationInputConfirmationTable.DeleteOnSubmit(new TBInformationInputConfirmation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBInformationOutputConfirmation")
		        {
                    TBInformationOutputConfirmationTable.DeleteOnSubmit(new TBInformationOutputConfirmation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBRegisterContainer")
		        {
                    TBRegisterContainerTable.DeleteOnSubmit(new TBRegisterContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "LoginProvider")
		        {
                    LoginProviderTable.DeleteOnSubmit(new LoginProvider { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ContactOipContainer")
		        {
                    ContactOipContainerTable.DeleteOnSubmit(new ContactOipContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TBPRegisterEmail")
		        {
                    TBPRegisterEmailTable.DeleteOnSubmit(new TBPRegisterEmail { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "JavaScriptContainer")
		        {
                    JavaScriptContainerTable.DeleteOnSubmit(new JavaScriptContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "JavascriptContainer")
		        {
                    JavascriptContainerTable.DeleteOnSubmit(new JavascriptContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "FooterContainer")
		        {
                    FooterContainerTable.DeleteOnSubmit(new FooterContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "NavigationContainer")
		        {
                    NavigationContainerTable.DeleteOnSubmit(new NavigationContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AccountSummary")
		        {
                    AccountSummaryTable.DeleteOnSubmit(new AccountSummary { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AccountContainer")
		        {
                    AccountContainerTable.DeleteOnSubmit(new AccountContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AccountIndex")
		        {
                    AccountIndexTable.DeleteOnSubmit(new AccountIndex { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AccountModule")
		        {
                    AccountModuleTable.DeleteOnSubmit(new AccountModule { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ImageGroupContainer")
		        {
                    ImageGroupContainerTable.DeleteOnSubmit(new ImageGroupContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "LocationContainer")
		        {
                    LocationContainerTable.DeleteOnSubmit(new LocationContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AddressAndLocation")
		        {
                    AddressAndLocationTable.DeleteOnSubmit(new AddressAndLocation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "StreetAddress")
		        {
                    StreetAddressTable.DeleteOnSubmit(new StreetAddress { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AccountContent")
		        {
                    AccountContentTable.DeleteOnSubmit(new AccountContent { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AccountProfile")
		        {
                    AccountProfileTable.DeleteOnSubmit(new AccountProfile { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AccountSecurity")
		        {
                    AccountSecurityTable.DeleteOnSubmit(new AccountSecurity { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AccountRoles")
		        {
                    AccountRolesTable.DeleteOnSubmit(new AccountRoles { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "PersonalInfoVisibility")
		        {
                    PersonalInfoVisibilityTable.DeleteOnSubmit(new PersonalInfoVisibility { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "GroupedInformation")
		        {
                    GroupedInformationTable.DeleteOnSubmit(new GroupedInformation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ReferenceToInformation")
		        {
                    ReferenceToInformationTable.DeleteOnSubmit(new ReferenceToInformation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "BlogContainer")
		        {
                    BlogContainerTable.DeleteOnSubmit(new BlogContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "RecentBlogSummary")
		        {
                    RecentBlogSummaryTable.DeleteOnSubmit(new RecentBlogSummary { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "NodeSummaryContainer")
		        {
                    NodeSummaryContainerTable.DeleteOnSubmit(new NodeSummaryContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "RenderedNode")
		        {
                    RenderedNodeTable.DeleteOnSubmit(new RenderedNode { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ShortTextObject")
		        {
                    ShortTextObjectTable.DeleteOnSubmit(new ShortTextObject { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "LongTextObject")
		        {
                    LongTextObjectTable.DeleteOnSubmit(new LongTextObject { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "MapContainer")
		        {
                    MapContainerTable.DeleteOnSubmit(new MapContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "MapMarker")
		        {
                    MapMarkerTable.DeleteOnSubmit(new MapMarker { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "CalendarContainer")
		        {
                    CalendarContainerTable.DeleteOnSubmit(new CalendarContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AboutContainer")
		        {
                    AboutContainerTable.DeleteOnSubmit(new AboutContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "OBSAccountContainer")
		        {
                    OBSAccountContainerTable.DeleteOnSubmit(new OBSAccountContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ProjectContainer")
		        {
                    ProjectContainerTable.DeleteOnSubmit(new ProjectContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "CourseContainer")
		        {
                    CourseContainerTable.DeleteOnSubmit(new CourseContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ContainerHeader")
		        {
                    ContainerHeaderTable.DeleteOnSubmit(new ContainerHeader { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ActivitySummaryContainer")
		        {
                    ActivitySummaryContainerTable.DeleteOnSubmit(new ActivitySummaryContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ActivityIndex")
		        {
                    ActivityIndexTable.DeleteOnSubmit(new ActivityIndex { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ActivityContainer")
		        {
                    ActivityContainerTable.DeleteOnSubmit(new ActivityContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Activity")
		        {
                    ActivityTable.DeleteOnSubmit(new Activity { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Moderator")
		        {
                    ModeratorTable.DeleteOnSubmit(new Moderator { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Collaborator")
		        {
                    CollaboratorTable.DeleteOnSubmit(new Collaborator { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "GroupSummaryContainer")
		        {
                    GroupSummaryContainerTable.DeleteOnSubmit(new GroupSummaryContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "GroupContainer")
		        {
                    GroupContainerTable.DeleteOnSubmit(new GroupContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "GroupIndex")
		        {
                    GroupIndexTable.DeleteOnSubmit(new GroupIndex { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AddAddressAndLocationInfo")
		        {
                    AddAddressAndLocationInfoTable.DeleteOnSubmit(new AddAddressAndLocationInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AddImageInfo")
		        {
                    AddImageInfoTable.DeleteOnSubmit(new AddImageInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AddImageGroupInfo")
		        {
                    AddImageGroupInfoTable.DeleteOnSubmit(new AddImageGroupInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AddEmailAddressInfo")
		        {
                    AddEmailAddressInfoTable.DeleteOnSubmit(new AddEmailAddressInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "CreateGroupInfo")
		        {
                    CreateGroupInfoTable.DeleteOnSubmit(new CreateGroupInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AddActivityInfo")
		        {
                    AddActivityInfoTable.DeleteOnSubmit(new AddActivityInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AddBlogPostInfo")
		        {
                    AddBlogPostInfoTable.DeleteOnSubmit(new AddBlogPostInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AddCategoryInfo")
		        {
                    AddCategoryInfoTable.DeleteOnSubmit(new AddCategoryInfo { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Group")
		        {
                    GroupTable.DeleteOnSubmit(new Group { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Introduction")
		        {
                    IntroductionTable.DeleteOnSubmit(new Introduction { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ContentCategoryRank")
		        {
                    ContentCategoryRankTable.DeleteOnSubmit(new ContentCategoryRank { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "LinkToContent")
		        {
                    LinkToContentTable.DeleteOnSubmit(new LinkToContent { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "EmbeddedContent")
		        {
                    EmbeddedContentTable.DeleteOnSubmit(new EmbeddedContent { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "DynamicContentGroup")
		        {
                    DynamicContentGroupTable.DeleteOnSubmit(new DynamicContentGroup { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "DynamicContent")
		        {
                    DynamicContentTable.DeleteOnSubmit(new DynamicContent { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AttachedToObject")
		        {
                    AttachedToObjectTable.DeleteOnSubmit(new AttachedToObject { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Comment")
		        {
                    CommentTable.DeleteOnSubmit(new Comment { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Selection")
		        {
                    SelectionTable.DeleteOnSubmit(new Selection { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "TextContent")
		        {
                    TextContentTable.DeleteOnSubmit(new TextContent { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Blog")
		        {
                    BlogTable.DeleteOnSubmit(new Blog { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "BlogIndexGroup")
		        {
                    BlogIndexGroupTable.DeleteOnSubmit(new BlogIndexGroup { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "CalendarIndex")
		        {
                    CalendarIndexTable.DeleteOnSubmit(new CalendarIndex { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Filter")
		        {
                    FilterTable.DeleteOnSubmit(new Filter { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Calendar")
		        {
                    CalendarTable.DeleteOnSubmit(new Calendar { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Map")
		        {
                    MapTable.DeleteOnSubmit(new Map { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "MapIndexCollection")
		        {
                    MapIndexCollectionTable.DeleteOnSubmit(new MapIndexCollection { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "MapResult")
		        {
                    MapResultTable.DeleteOnSubmit(new MapResult { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "MapResultsCollection")
		        {
                    MapResultsCollectionTable.DeleteOnSubmit(new MapResultsCollection { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Video")
		        {
                    VideoTable.DeleteOnSubmit(new Video { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Image")
		        {
                    ImageTable.DeleteOnSubmit(new Image { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "BinaryFile")
		        {
                    BinaryFileTable.DeleteOnSubmit(new BinaryFile { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ImageGroup")
		        {
                    ImageGroupTable.DeleteOnSubmit(new ImageGroup { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "VideoGroup")
		        {
                    VideoGroupTable.DeleteOnSubmit(new VideoGroup { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Tooltip")
		        {
                    TooltipTable.DeleteOnSubmit(new Tooltip { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "SocialPanel")
		        {
                    SocialPanelTable.DeleteOnSubmit(new SocialPanel { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Longitude")
		        {
                    LongitudeTable.DeleteOnSubmit(new Longitude { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Latitude")
		        {
                    LatitudeTable.DeleteOnSubmit(new Latitude { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Location")
		        {
                    LocationTable.DeleteOnSubmit(new Location { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Date")
		        {
                    DateTable.DeleteOnSubmit(new Date { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Sex")
		        {
                    SexTable.DeleteOnSubmit(new Sex { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "OBSAddress")
		        {
                    OBSAddressTable.DeleteOnSubmit(new OBSAddress { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Identity")
		        {
                    IdentityTable.DeleteOnSubmit(new Identity { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "ImageVideoSoundVectorRaw")
		        {
                    ImageVideoSoundVectorRawTable.DeleteOnSubmit(new ImageVideoSoundVectorRaw { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "CategoryContainer")
		        {
                    CategoryContainerTable.DeleteOnSubmit(new CategoryContainer { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Category")
		        {
                    CategoryTable.DeleteOnSubmit(new Category { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Subscription")
		        {
                    SubscriptionTable.DeleteOnSubmit(new Subscription { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "QueueEnvelope")
		        {
                    QueueEnvelopeTable.DeleteOnSubmit(new QueueEnvelope { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "OperationRequest")
		        {
                    OperationRequestTable.DeleteOnSubmit(new OperationRequest { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "SubscriptionChainRequestMessage")
		        {
                    SubscriptionChainRequestMessageTable.DeleteOnSubmit(new SubscriptionChainRequestMessage { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "SubscriptionChainRequestContent")
		        {
                    SubscriptionChainRequestContentTable.DeleteOnSubmit(new SubscriptionChainRequestContent { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "SubscriptionTarget")
		        {
                    SubscriptionTargetTable.DeleteOnSubmit(new SubscriptionTarget { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "DeleteEntireOwnerOperation")
		        {
                    DeleteEntireOwnerOperationTable.DeleteOnSubmit(new DeleteEntireOwnerOperation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "DeleteOwnerContentOperation")
		        {
                    DeleteOwnerContentOperationTable.DeleteOnSubmit(new DeleteOwnerContentOperation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "SystemError")
		        {
                    SystemErrorTable.DeleteOnSubmit(new SystemError { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "SystemErrorItem")
		        {
                    SystemErrorItemTable.DeleteOnSubmit(new SystemErrorItem { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "InformationSource")
		        {
                    InformationSourceTable.DeleteOnSubmit(new InformationSource { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "RefreshDefaultViewsOperation")
		        {
                    RefreshDefaultViewsOperationTable.DeleteOnSubmit(new RefreshDefaultViewsOperation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "UpdateWebContentOperation")
		        {
                    UpdateWebContentOperationTable.DeleteOnSubmit(new UpdateWebContentOperation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "UpdateWebContentHandlerItem")
		        {
                    UpdateWebContentHandlerItemTable.DeleteOnSubmit(new UpdateWebContentHandlerItem { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "PublishWebContentOperation")
		        {
                    PublishWebContentOperationTable.DeleteOnSubmit(new PublishWebContentOperation { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "SubscriberInput")
		        {
                    SubscriberInputTable.DeleteOnSubmit(new SubscriberInput { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "Monitor")
		        {
                    MonitorTable.DeleteOnSubmit(new Monitor { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "IconTitleDescription")
		        {
                    IconTitleDescriptionTable.DeleteOnSubmit(new IconTitleDescription { ID = deleteData.ObjectID });
		            return;
		        }
		        if (deleteData.ObjectType == "AboutAGIApplications")
		        {
                    AboutAGIApplicationsTable.DeleteOnSubmit(new AboutAGIApplications { ID = deleteData.ObjectID });
		            return;
		        }
		    }


			public Table<TBSystem> TBSystemTable {
				get {
					return this.GetTable<TBSystem>();
				}
			}
			public Table<WebPublishInfo> WebPublishInfoTable {
				get {
					return this.GetTable<WebPublishInfo>();
				}
			}
			public Table<PublicationPackage> PublicationPackageTable {
				get {
					return this.GetTable<PublicationPackage>();
				}
			}
			public Table<TBRLoginRoot> TBRLoginRootTable {
				get {
					return this.GetTable<TBRLoginRoot>();
				}
			}
			public Table<TBRAccountRoot> TBRAccountRootTable {
				get {
					return this.GetTable<TBRAccountRoot>();
				}
			}
			public Table<TBRGroupRoot> TBRGroupRootTable {
				get {
					return this.GetTable<TBRGroupRoot>();
				}
			}
			public Table<TBRLoginGroupRoot> TBRLoginGroupRootTable {
				get {
					return this.GetTable<TBRLoginGroupRoot>();
				}
			}
			public Table<TBREmailRoot> TBREmailRootTable {
				get {
					return this.GetTable<TBREmailRoot>();
				}
			}
			public Table<TBAccount> TBAccountTable {
				get {
					return this.GetTable<TBAccount>();
				}
			}
			public Table<TBAccountCollaborationGroup> TBAccountCollaborationGroupTable {
				get {
					return this.GetTable<TBAccountCollaborationGroup>();
				}
			}
			public Table<TBLoginInfo> TBLoginInfoTable {
				get {
					return this.GetTable<TBLoginInfo>();
				}
			}
			public Table<TBEmail> TBEmailTable {
				get {
					return this.GetTable<TBEmail>();
				}
			}
			public Table<TBCollaboratorRole> TBCollaboratorRoleTable {
				get {
					return this.GetTable<TBCollaboratorRole>();
				}
			}
			public Table<TBCollaboratingGroup> TBCollaboratingGroupTable {
				get {
					return this.GetTable<TBCollaboratingGroup>();
				}
			}
			public Table<TBEmailValidation> TBEmailValidationTable {
				get {
					return this.GetTable<TBEmailValidation>();
				}
			}
			public Table<TBMergeAccountConfirmation> TBMergeAccountConfirmationTable {
				get {
					return this.GetTable<TBMergeAccountConfirmation>();
				}
			}
			public Table<TBGroupJoinConfirmation> TBGroupJoinConfirmationTable {
				get {
					return this.GetTable<TBGroupJoinConfirmation>();
				}
			}
			public Table<TBDeviceJoinConfirmation> TBDeviceJoinConfirmationTable {
				get {
					return this.GetTable<TBDeviceJoinConfirmation>();
				}
			}
			public Table<TBInformationInputConfirmation> TBInformationInputConfirmationTable {
				get {
					return this.GetTable<TBInformationInputConfirmation>();
				}
			}
			public Table<TBInformationOutputConfirmation> TBInformationOutputConfirmationTable {
				get {
					return this.GetTable<TBInformationOutputConfirmation>();
				}
			}
			public Table<TBRegisterContainer> TBRegisterContainerTable {
				get {
					return this.GetTable<TBRegisterContainer>();
				}
			}
			public Table<LoginProvider> LoginProviderTable {
				get {
					return this.GetTable<LoginProvider>();
				}
			}
			public Table<ContactOipContainer> ContactOipContainerTable {
				get {
					return this.GetTable<ContactOipContainer>();
				}
			}
			public Table<TBPRegisterEmail> TBPRegisterEmailTable {
				get {
					return this.GetTable<TBPRegisterEmail>();
				}
			}
			public Table<JavaScriptContainer> JavaScriptContainerTable {
				get {
					return this.GetTable<JavaScriptContainer>();
				}
			}
			public Table<JavascriptContainer> JavascriptContainerTable {
				get {
					return this.GetTable<JavascriptContainer>();
				}
			}
			public Table<FooterContainer> FooterContainerTable {
				get {
					return this.GetTable<FooterContainer>();
				}
			}
			public Table<NavigationContainer> NavigationContainerTable {
				get {
					return this.GetTable<NavigationContainer>();
				}
			}
			public Table<AccountSummary> AccountSummaryTable {
				get {
					return this.GetTable<AccountSummary>();
				}
			}
			public Table<AccountContainer> AccountContainerTable {
				get {
					return this.GetTable<AccountContainer>();
				}
			}
			public Table<AccountIndex> AccountIndexTable {
				get {
					return this.GetTable<AccountIndex>();
				}
			}
			public Table<AccountModule> AccountModuleTable {
				get {
					return this.GetTable<AccountModule>();
				}
			}
			public Table<ImageGroupContainer> ImageGroupContainerTable {
				get {
					return this.GetTable<ImageGroupContainer>();
				}
			}
			public Table<LocationContainer> LocationContainerTable {
				get {
					return this.GetTable<LocationContainer>();
				}
			}
			public Table<AddressAndLocation> AddressAndLocationTable {
				get {
					return this.GetTable<AddressAndLocation>();
				}
			}
			public Table<StreetAddress> StreetAddressTable {
				get {
					return this.GetTable<StreetAddress>();
				}
			}
			public Table<AccountContent> AccountContentTable {
				get {
					return this.GetTable<AccountContent>();
				}
			}
			public Table<AccountProfile> AccountProfileTable {
				get {
					return this.GetTable<AccountProfile>();
				}
			}
			public Table<AccountSecurity> AccountSecurityTable {
				get {
					return this.GetTable<AccountSecurity>();
				}
			}
			public Table<AccountRoles> AccountRolesTable {
				get {
					return this.GetTable<AccountRoles>();
				}
			}
			public Table<PersonalInfoVisibility> PersonalInfoVisibilityTable {
				get {
					return this.GetTable<PersonalInfoVisibility>();
				}
			}
			public Table<GroupedInformation> GroupedInformationTable {
				get {
					return this.GetTable<GroupedInformation>();
				}
			}
			public Table<ReferenceToInformation> ReferenceToInformationTable {
				get {
					return this.GetTable<ReferenceToInformation>();
				}
			}
			public Table<BlogContainer> BlogContainerTable {
				get {
					return this.GetTable<BlogContainer>();
				}
			}
			public Table<RecentBlogSummary> RecentBlogSummaryTable {
				get {
					return this.GetTable<RecentBlogSummary>();
				}
			}
			public Table<NodeSummaryContainer> NodeSummaryContainerTable {
				get {
					return this.GetTable<NodeSummaryContainer>();
				}
			}
			public Table<RenderedNode> RenderedNodeTable {
				get {
					return this.GetTable<RenderedNode>();
				}
			}
			public Table<ShortTextObject> ShortTextObjectTable {
				get {
					return this.GetTable<ShortTextObject>();
				}
			}
			public Table<LongTextObject> LongTextObjectTable {
				get {
					return this.GetTable<LongTextObject>();
				}
			}
			public Table<MapContainer> MapContainerTable {
				get {
					return this.GetTable<MapContainer>();
				}
			}
			public Table<MapMarker> MapMarkerTable {
				get {
					return this.GetTable<MapMarker>();
				}
			}
			public Table<CalendarContainer> CalendarContainerTable {
				get {
					return this.GetTable<CalendarContainer>();
				}
			}
			public Table<AboutContainer> AboutContainerTable {
				get {
					return this.GetTable<AboutContainer>();
				}
			}
			public Table<OBSAccountContainer> OBSAccountContainerTable {
				get {
					return this.GetTable<OBSAccountContainer>();
				}
			}
			public Table<ProjectContainer> ProjectContainerTable {
				get {
					return this.GetTable<ProjectContainer>();
				}
			}
			public Table<CourseContainer> CourseContainerTable {
				get {
					return this.GetTable<CourseContainer>();
				}
			}
			public Table<ContainerHeader> ContainerHeaderTable {
				get {
					return this.GetTable<ContainerHeader>();
				}
			}
			public Table<ActivitySummaryContainer> ActivitySummaryContainerTable {
				get {
					return this.GetTable<ActivitySummaryContainer>();
				}
			}
			public Table<ActivityIndex> ActivityIndexTable {
				get {
					return this.GetTable<ActivityIndex>();
				}
			}
			public Table<ActivityContainer> ActivityContainerTable {
				get {
					return this.GetTable<ActivityContainer>();
				}
			}
			public Table<Activity> ActivityTable {
				get {
					return this.GetTable<Activity>();
				}
			}
			public Table<Moderator> ModeratorTable {
				get {
					return this.GetTable<Moderator>();
				}
			}
			public Table<Collaborator> CollaboratorTable {
				get {
					return this.GetTable<Collaborator>();
				}
			}
			public Table<GroupSummaryContainer> GroupSummaryContainerTable {
				get {
					return this.GetTable<GroupSummaryContainer>();
				}
			}
			public Table<GroupContainer> GroupContainerTable {
				get {
					return this.GetTable<GroupContainer>();
				}
			}
			public Table<GroupIndex> GroupIndexTable {
				get {
					return this.GetTable<GroupIndex>();
				}
			}
			public Table<AddAddressAndLocationInfo> AddAddressAndLocationInfoTable {
				get {
					return this.GetTable<AddAddressAndLocationInfo>();
				}
			}
			public Table<AddImageInfo> AddImageInfoTable {
				get {
					return this.GetTable<AddImageInfo>();
				}
			}
			public Table<AddImageGroupInfo> AddImageGroupInfoTable {
				get {
					return this.GetTable<AddImageGroupInfo>();
				}
			}
			public Table<AddEmailAddressInfo> AddEmailAddressInfoTable {
				get {
					return this.GetTable<AddEmailAddressInfo>();
				}
			}
			public Table<CreateGroupInfo> CreateGroupInfoTable {
				get {
					return this.GetTable<CreateGroupInfo>();
				}
			}
			public Table<AddActivityInfo> AddActivityInfoTable {
				get {
					return this.GetTable<AddActivityInfo>();
				}
			}
			public Table<AddBlogPostInfo> AddBlogPostInfoTable {
				get {
					return this.GetTable<AddBlogPostInfo>();
				}
			}
			public Table<AddCategoryInfo> AddCategoryInfoTable {
				get {
					return this.GetTable<AddCategoryInfo>();
				}
			}
			public Table<Group> GroupTable {
				get {
					return this.GetTable<Group>();
				}
			}
			public Table<Introduction> IntroductionTable {
				get {
					return this.GetTable<Introduction>();
				}
			}
			public Table<ContentCategoryRank> ContentCategoryRankTable {
				get {
					return this.GetTable<ContentCategoryRank>();
				}
			}
			public Table<LinkToContent> LinkToContentTable {
				get {
					return this.GetTable<LinkToContent>();
				}
			}
			public Table<EmbeddedContent> EmbeddedContentTable {
				get {
					return this.GetTable<EmbeddedContent>();
				}
			}
			public Table<DynamicContentGroup> DynamicContentGroupTable {
				get {
					return this.GetTable<DynamicContentGroup>();
				}
			}
			public Table<DynamicContent> DynamicContentTable {
				get {
					return this.GetTable<DynamicContent>();
				}
			}
			public Table<AttachedToObject> AttachedToObjectTable {
				get {
					return this.GetTable<AttachedToObject>();
				}
			}
			public Table<Comment> CommentTable {
				get {
					return this.GetTable<Comment>();
				}
			}
			public Table<Selection> SelectionTable {
				get {
					return this.GetTable<Selection>();
				}
			}
			public Table<TextContent> TextContentTable {
				get {
					return this.GetTable<TextContent>();
				}
			}
			public Table<Blog> BlogTable {
				get {
					return this.GetTable<Blog>();
				}
			}
			public Table<BlogIndexGroup> BlogIndexGroupTable {
				get {
					return this.GetTable<BlogIndexGroup>();
				}
			}
			public Table<CalendarIndex> CalendarIndexTable {
				get {
					return this.GetTable<CalendarIndex>();
				}
			}
			public Table<Filter> FilterTable {
				get {
					return this.GetTable<Filter>();
				}
			}
			public Table<Calendar> CalendarTable {
				get {
					return this.GetTable<Calendar>();
				}
			}
			public Table<Map> MapTable {
				get {
					return this.GetTable<Map>();
				}
			}
			public Table<MapIndexCollection> MapIndexCollectionTable {
				get {
					return this.GetTable<MapIndexCollection>();
				}
			}
			public Table<MapResult> MapResultTable {
				get {
					return this.GetTable<MapResult>();
				}
			}
			public Table<MapResultsCollection> MapResultsCollectionTable {
				get {
					return this.GetTable<MapResultsCollection>();
				}
			}
			public Table<Video> VideoTable {
				get {
					return this.GetTable<Video>();
				}
			}
			public Table<Image> ImageTable {
				get {
					return this.GetTable<Image>();
				}
			}
			public Table<BinaryFile> BinaryFileTable {
				get {
					return this.GetTable<BinaryFile>();
				}
			}
			public Table<ImageGroup> ImageGroupTable {
				get {
					return this.GetTable<ImageGroup>();
				}
			}
			public Table<VideoGroup> VideoGroupTable {
				get {
					return this.GetTable<VideoGroup>();
				}
			}
			public Table<Tooltip> TooltipTable {
				get {
					return this.GetTable<Tooltip>();
				}
			}
			public Table<SocialPanel> SocialPanelTable {
				get {
					return this.GetTable<SocialPanel>();
				}
			}
			public Table<Longitude> LongitudeTable {
				get {
					return this.GetTable<Longitude>();
				}
			}
			public Table<Latitude> LatitudeTable {
				get {
					return this.GetTable<Latitude>();
				}
			}
			public Table<Location> LocationTable {
				get {
					return this.GetTable<Location>();
				}
			}
			public Table<Date> DateTable {
				get {
					return this.GetTable<Date>();
				}
			}
			public Table<Sex> SexTable {
				get {
					return this.GetTable<Sex>();
				}
			}
			public Table<OBSAddress> OBSAddressTable {
				get {
					return this.GetTable<OBSAddress>();
				}
			}
			public Table<Identity> IdentityTable {
				get {
					return this.GetTable<Identity>();
				}
			}
			public Table<ImageVideoSoundVectorRaw> ImageVideoSoundVectorRawTable {
				get {
					return this.GetTable<ImageVideoSoundVectorRaw>();
				}
			}
			public Table<CategoryContainer> CategoryContainerTable {
				get {
					return this.GetTable<CategoryContainer>();
				}
			}
			public Table<Category> CategoryTable {
				get {
					return this.GetTable<Category>();
				}
			}
			public Table<Subscription> SubscriptionTable {
				get {
					return this.GetTable<Subscription>();
				}
			}
			public Table<QueueEnvelope> QueueEnvelopeTable {
				get {
					return this.GetTable<QueueEnvelope>();
				}
			}
			public Table<OperationRequest> OperationRequestTable {
				get {
					return this.GetTable<OperationRequest>();
				}
			}
			public Table<SubscriptionChainRequestMessage> SubscriptionChainRequestMessageTable {
				get {
					return this.GetTable<SubscriptionChainRequestMessage>();
				}
			}
			public Table<SubscriptionChainRequestContent> SubscriptionChainRequestContentTable {
				get {
					return this.GetTable<SubscriptionChainRequestContent>();
				}
			}
			public Table<SubscriptionTarget> SubscriptionTargetTable {
				get {
					return this.GetTable<SubscriptionTarget>();
				}
			}
			public Table<DeleteEntireOwnerOperation> DeleteEntireOwnerOperationTable {
				get {
					return this.GetTable<DeleteEntireOwnerOperation>();
				}
			}
			public Table<DeleteOwnerContentOperation> DeleteOwnerContentOperationTable {
				get {
					return this.GetTable<DeleteOwnerContentOperation>();
				}
			}
			public Table<SystemError> SystemErrorTable {
				get {
					return this.GetTable<SystemError>();
				}
			}
			public Table<SystemErrorItem> SystemErrorItemTable {
				get {
					return this.GetTable<SystemErrorItem>();
				}
			}
			public Table<InformationSource> InformationSourceTable {
				get {
					return this.GetTable<InformationSource>();
				}
			}
			public Table<RefreshDefaultViewsOperation> RefreshDefaultViewsOperationTable {
				get {
					return this.GetTable<RefreshDefaultViewsOperation>();
				}
			}
			public Table<UpdateWebContentOperation> UpdateWebContentOperationTable {
				get {
					return this.GetTable<UpdateWebContentOperation>();
				}
			}
			public Table<UpdateWebContentHandlerItem> UpdateWebContentHandlerItemTable {
				get {
					return this.GetTable<UpdateWebContentHandlerItem>();
				}
			}
			public Table<PublishWebContentOperation> PublishWebContentOperationTable {
				get {
					return this.GetTable<PublishWebContentOperation>();
				}
			}
			public Table<SubscriberInput> SubscriberInputTable {
				get {
					return this.GetTable<SubscriberInput>();
				}
			}
			public Table<Monitor> MonitorTable {
				get {
					return this.GetTable<Monitor>();
				}
			}
			public Table<IconTitleDescription> IconTitleDescriptionTable {
				get {
					return this.GetTable<IconTitleDescription>();
				}
			}
			public Table<AboutAGIApplications> AboutAGIApplicationsTable {
				get {
					return this.GetTable<AboutAGIApplications>();
				}
			}
        }

    [Table(Name = "TBSystem")]
	public class TBSystem : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBSystem(
[ID] TEXT NOT NULL PRIMARY KEY, 
[InstanceName] TEXT NOT NULL, 
[AdminGroupID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string InstanceName { get; set; }
		// private string _unmodified_InstanceName;

		[Column]
		public string AdminGroupID { get; set; }
		// private string _unmodified_AdminGroupID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(InstanceName == null)
				InstanceName = string.Empty;
			if(AdminGroupID == null)
				AdminGroupID = string.Empty;
		}
	}
    [Table(Name = "WebPublishInfo")]
	public class WebPublishInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS WebPublishInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[PublishType] TEXT NOT NULL, 
[PublishContainer] TEXT NOT NULL, 
[ActivePublication] TEXT NOT NULL, 
[Publications] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string PublishType { get; set; }
		// private string _unmodified_PublishType;

		[Column]
		public string PublishContainer { get; set; }
		// private string _unmodified_PublishContainer;

		[Column]
		public PublicationPackage ActivePublication { get; set; }
		// private PublicationPackage _unmodified_ActivePublication;

		[Column]
		public PublicationPackageCollection Publications { get; set; }
		// private PublicationPackageCollection _unmodified_Publications;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(PublishType == null)
				PublishType = string.Empty;
			if(PublishContainer == null)
				PublishContainer = string.Empty;
		}
	}
    [Table(Name = "PublicationPackage")]
	public class PublicationPackage : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS PublicationPackage(
[ID] TEXT NOT NULL PRIMARY KEY, 
[PackageName] TEXT NOT NULL, 
[PublicationTime] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string PackageName { get; set; }
		// private string _unmodified_PackageName;

		[Column]
		public DateTime PublicationTime { get; set; }
		// private DateTime _unmodified_PublicationTime;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(PackageName == null)
				PackageName = string.Empty;
		}
	}
    [Table(Name = "TBRLoginRoot")]
	public class TBRLoginRoot : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBRLoginRoot(
[ID] TEXT NOT NULL PRIMARY KEY, 
[DomainName] TEXT NOT NULL, 
[Account] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string DomainName { get; set; }
		// private string _unmodified_DomainName;

		[Column]
		public TBAccount Account { get; set; }
		// private TBAccount _unmodified_Account;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(DomainName == null)
				DomainName = string.Empty;
		}
	}
    [Table(Name = "TBRAccountRoot")]
	public class TBRAccountRoot : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBRAccountRoot(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Account] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public TBAccount Account { get; set; }
		// private TBAccount _unmodified_Account;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "TBRGroupRoot")]
	public class TBRGroupRoot : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBRGroupRoot(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Group] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public TBCollaboratingGroup Group { get; set; }
		// private TBCollaboratingGroup _unmodified_Group;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "TBRLoginGroupRoot")]
	public class TBRLoginGroupRoot : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBRLoginGroupRoot(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Role] TEXT NOT NULL, 
[GroupID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Role { get; set; }
		// private string _unmodified_Role;

		[Column]
		public string GroupID { get; set; }
		// private string _unmodified_GroupID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Role == null)
				Role = string.Empty;
			if(GroupID == null)
				GroupID = string.Empty;
		}
	}
    [Table(Name = "TBREmailRoot")]
	public class TBREmailRoot : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBREmailRoot(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Account] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public TBAccount Account { get; set; }
		// private TBAccount _unmodified_Account;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "TBAccount")]
	public class TBAccount : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBAccount(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Emails] TEXT NOT NULL, 
[Logins] TEXT NOT NULL, 
[GroupRoleCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public TBEmailCollection Emails { get; set; }
		// private TBEmailCollection _unmodified_Emails;

		[Column]
		public TBLoginInfoCollection Logins { get; set; }
		// private TBLoginInfoCollection _unmodified_Logins;

		[Column]
		public TBAccountCollaborationGroupCollection GroupRoleCollection { get; set; }
		// private TBAccountCollaborationGroupCollection _unmodified_GroupRoleCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "TBAccountCollaborationGroup")]
	public class TBAccountCollaborationGroup : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBAccountCollaborationGroup(
[ID] TEXT NOT NULL PRIMARY KEY, 
[GroupID] TEXT NOT NULL, 
[GroupRole] TEXT NOT NULL, 
[RoleStatus] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string GroupID { get; set; }
		// private string _unmodified_GroupID;

		[Column]
		public string GroupRole { get; set; }
		// private string _unmodified_GroupRole;

		[Column]
		public string RoleStatus { get; set; }
		// private string _unmodified_RoleStatus;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(GroupID == null)
				GroupID = string.Empty;
			if(GroupRole == null)
				GroupRole = string.Empty;
			if(RoleStatus == null)
				RoleStatus = string.Empty;
		}
	}
    [Table(Name = "TBLoginInfo")]
	public class TBLoginInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBLoginInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[OpenIDUrl] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string OpenIDUrl { get; set; }
		// private string _unmodified_OpenIDUrl;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(OpenIDUrl == null)
				OpenIDUrl = string.Empty;
		}
	}
    [Table(Name = "TBEmail")]
	public class TBEmail : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBEmail(
[ID] TEXT NOT NULL PRIMARY KEY, 
[EmailAddress] TEXT NOT NULL, 
[ValidatedAt] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string EmailAddress { get; set; }
		// private string _unmodified_EmailAddress;

		[Column]
		public DateTime ValidatedAt { get; set; }
		// private DateTime _unmodified_ValidatedAt;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(EmailAddress == null)
				EmailAddress = string.Empty;
		}
	}
    [Table(Name = "TBCollaboratorRole")]
	public class TBCollaboratorRole : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBCollaboratorRole(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Email] TEXT NOT NULL, 
[Role] TEXT NOT NULL, 
[RoleStatus] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public TBEmail Email { get; set; }
		// private TBEmail _unmodified_Email;

		[Column]
		public string Role { get; set; }
		// private string _unmodified_Role;

		[Column]
		public string RoleStatus { get; set; }
		// private string _unmodified_RoleStatus;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Role == null)
				Role = string.Empty;
			if(RoleStatus == null)
				RoleStatus = string.Empty;
		}
	}
    [Table(Name = "TBCollaboratingGroup")]
	public class TBCollaboratingGroup : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBCollaboratingGroup(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL, 
[Roles] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public TBCollaboratorRoleCollection Roles { get; set; }
		// private TBCollaboratorRoleCollection _unmodified_Roles;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
		}
	}
    [Table(Name = "TBEmailValidation")]
	public class TBEmailValidation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBEmailValidation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Email] TEXT NOT NULL, 
[AccountID] TEXT NOT NULL, 
[ValidUntil] TEXT NOT NULL, 
[GroupJoinConfirmation] TEXT NOT NULL, 
[DeviceJoinConfirmation] TEXT NOT NULL, 
[InformationInputConfirmation] TEXT NOT NULL, 
[InformationOutputConfirmation] TEXT NOT NULL, 
[MergeAccountsConfirmation] TEXT NOT NULL, 
[RedirectUrlAfterValidation] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Email { get; set; }
		// private string _unmodified_Email;

		[Column]
		public string AccountID { get; set; }
		// private string _unmodified_AccountID;

		[Column]
		public DateTime ValidUntil { get; set; }
		// private DateTime _unmodified_ValidUntil;

		[Column]
		public TBGroupJoinConfirmation GroupJoinConfirmation { get; set; }
		// private TBGroupJoinConfirmation _unmodified_GroupJoinConfirmation;

		[Column]
		public TBDeviceJoinConfirmation DeviceJoinConfirmation { get; set; }
		// private TBDeviceJoinConfirmation _unmodified_DeviceJoinConfirmation;

		[Column]
		public TBInformationInputConfirmation InformationInputConfirmation { get; set; }
		// private TBInformationInputConfirmation _unmodified_InformationInputConfirmation;

		[Column]
		public TBInformationOutputConfirmation InformationOutputConfirmation { get; set; }
		// private TBInformationOutputConfirmation _unmodified_InformationOutputConfirmation;

		[Column]
		public TBMergeAccountConfirmation MergeAccountsConfirmation { get; set; }
		// private TBMergeAccountConfirmation _unmodified_MergeAccountsConfirmation;

		[Column]
		public string RedirectUrlAfterValidation { get; set; }
		// private string _unmodified_RedirectUrlAfterValidation;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Email == null)
				Email = string.Empty;
			if(AccountID == null)
				AccountID = string.Empty;
			if(RedirectUrlAfterValidation == null)
				RedirectUrlAfterValidation = string.Empty;
		}
	}
    [Table(Name = "TBMergeAccountConfirmation")]
	public class TBMergeAccountConfirmation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBMergeAccountConfirmation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[AccountToBeMergedID] TEXT NOT NULL, 
[AccountToMergeToID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string AccountToBeMergedID { get; set; }
		// private string _unmodified_AccountToBeMergedID;

		[Column]
		public string AccountToMergeToID { get; set; }
		// private string _unmodified_AccountToMergeToID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(AccountToBeMergedID == null)
				AccountToBeMergedID = string.Empty;
			if(AccountToMergeToID == null)
				AccountToMergeToID = string.Empty;
		}
	}
    [Table(Name = "TBGroupJoinConfirmation")]
	public class TBGroupJoinConfirmation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBGroupJoinConfirmation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[GroupID] TEXT NOT NULL, 
[InvitationMode] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string GroupID { get; set; }
		// private string _unmodified_GroupID;

		[Column]
		public string InvitationMode { get; set; }
		// private string _unmodified_InvitationMode;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(GroupID == null)
				GroupID = string.Empty;
			if(InvitationMode == null)
				InvitationMode = string.Empty;
		}
	}
    [Table(Name = "TBDeviceJoinConfirmation")]
	public class TBDeviceJoinConfirmation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBDeviceJoinConfirmation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[GroupID] TEXT NOT NULL, 
[AccountID] TEXT NOT NULL, 
[DeviceMembershipID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string GroupID { get; set; }
		// private string _unmodified_GroupID;

		[Column]
		public string AccountID { get; set; }
		// private string _unmodified_AccountID;

		[Column]
		public string DeviceMembershipID { get; set; }
		// private string _unmodified_DeviceMembershipID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(GroupID == null)
				GroupID = string.Empty;
			if(AccountID == null)
				AccountID = string.Empty;
			if(DeviceMembershipID == null)
				DeviceMembershipID = string.Empty;
		}
	}
    [Table(Name = "TBInformationInputConfirmation")]
	public class TBInformationInputConfirmation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBInformationInputConfirmation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[GroupID] TEXT NOT NULL, 
[AccountID] TEXT NOT NULL, 
[InformationInputID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string GroupID { get; set; }
		// private string _unmodified_GroupID;

		[Column]
		public string AccountID { get; set; }
		// private string _unmodified_AccountID;

		[Column]
		public string InformationInputID { get; set; }
		// private string _unmodified_InformationInputID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(GroupID == null)
				GroupID = string.Empty;
			if(AccountID == null)
				AccountID = string.Empty;
			if(InformationInputID == null)
				InformationInputID = string.Empty;
		}
	}
    [Table(Name = "TBInformationOutputConfirmation")]
	public class TBInformationOutputConfirmation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBInformationOutputConfirmation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[GroupID] TEXT NOT NULL, 
[AccountID] TEXT NOT NULL, 
[InformationOutputID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string GroupID { get; set; }
		// private string _unmodified_GroupID;

		[Column]
		public string AccountID { get; set; }
		// private string _unmodified_AccountID;

		[Column]
		public string InformationOutputID { get; set; }
		// private string _unmodified_InformationOutputID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(GroupID == null)
				GroupID = string.Empty;
			if(AccountID == null)
				AccountID = string.Empty;
			if(InformationOutputID == null)
				InformationOutputID = string.Empty;
		}
	}
    [Table(Name = "TBRegisterContainer")]
	public class TBRegisterContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBRegisterContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Header] TEXT NOT NULL, 
[ReturnUrl] TEXT NOT NULL, 
[LoginProviderCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public string ReturnUrl { get; set; }
		// private string _unmodified_ReturnUrl;

		[Column]
		public LoginProviderCollection LoginProviderCollection { get; set; }
		// private LoginProviderCollection _unmodified_LoginProviderCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ReturnUrl == null)
				ReturnUrl = string.Empty;
		}
	}
    [Table(Name = "LoginProvider")]
	public class LoginProvider : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS LoginProvider(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ProviderName] TEXT NOT NULL, 
[ProviderIconClass] TEXT NOT NULL, 
[ProviderType] TEXT NOT NULL, 
[ProviderUrl] TEXT NOT NULL, 
[ReturnUrl] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ProviderName { get; set; }
		// private string _unmodified_ProviderName;

		[Column]
		public string ProviderIconClass { get; set; }
		// private string _unmodified_ProviderIconClass;

		[Column]
		public string ProviderType { get; set; }
		// private string _unmodified_ProviderType;

		[Column]
		public string ProviderUrl { get; set; }
		// private string _unmodified_ProviderUrl;

		[Column]
		public string ReturnUrl { get; set; }
		// private string _unmodified_ReturnUrl;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ProviderName == null)
				ProviderName = string.Empty;
			if(ProviderIconClass == null)
				ProviderIconClass = string.Empty;
			if(ProviderType == null)
				ProviderType = string.Empty;
			if(ProviderUrl == null)
				ProviderUrl = string.Empty;
			if(ReturnUrl == null)
				ReturnUrl = string.Empty;
		}
	}
    [Table(Name = "ContactOipContainer")]
	public class ContactOipContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ContactOipContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[OIPModeratorGroupID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string OIPModeratorGroupID { get; set; }
		// private string _unmodified_OIPModeratorGroupID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(OIPModeratorGroupID == null)
				OIPModeratorGroupID = string.Empty;
		}
	}
    [Table(Name = "TBPRegisterEmail")]
	public class TBPRegisterEmail : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TBPRegisterEmail(
[ID] TEXT NOT NULL PRIMARY KEY, 
[EmailAddress] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string EmailAddress { get; set; }
		// private string _unmodified_EmailAddress;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(EmailAddress == null)
				EmailAddress = string.Empty;
		}
	}
    [Table(Name = "JavaScriptContainer")]
	public class JavaScriptContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS JavaScriptContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[HtmlContent] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string HtmlContent { get; set; }
		// private string _unmodified_HtmlContent;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(HtmlContent == null)
				HtmlContent = string.Empty;
		}
	}
    [Table(Name = "JavascriptContainer")]
	public class JavascriptContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS JavascriptContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[HtmlContent] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string HtmlContent { get; set; }
		// private string _unmodified_HtmlContent;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(HtmlContent == null)
				HtmlContent = string.Empty;
		}
	}
    [Table(Name = "FooterContainer")]
	public class FooterContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS FooterContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[HtmlContent] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string HtmlContent { get; set; }
		// private string _unmodified_HtmlContent;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(HtmlContent == null)
				HtmlContent = string.Empty;
		}
	}
    [Table(Name = "NavigationContainer")]
	public class NavigationContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS NavigationContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Dummy] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Dummy { get; set; }
		// private string _unmodified_Dummy;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Dummy == null)
				Dummy = string.Empty;
		}
	}
    [Table(Name = "AccountSummary")]
	public class AccountSummary : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AccountSummary(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Introduction] TEXT NOT NULL, 
[ActivitySummary] TEXT NOT NULL, 
[GroupSummary] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Introduction Introduction { get; set; }
		// private Introduction _unmodified_Introduction;

		[Column]
		public ActivitySummaryContainer ActivitySummary { get; set; }
		// private ActivitySummaryContainer _unmodified_ActivitySummary;

		[Column]
		public GroupSummaryContainer GroupSummary { get; set; }
		// private GroupSummaryContainer _unmodified_GroupSummary;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "AccountContainer")]
	public class AccountContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AccountContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Header] TEXT NOT NULL, 
[AccountIndex] TEXT NOT NULL, 
[AccountModule] TEXT NOT NULL, 
[AccountSummary] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public AccountIndex AccountIndex { get; set; }
		// private AccountIndex _unmodified_AccountIndex;

		[Column]
		public AccountModule AccountModule { get; set; }
		// private AccountModule _unmodified_AccountModule;

		[Column]
		public AccountSummary AccountSummary { get; set; }
		// private AccountSummary _unmodified_AccountSummary;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "AccountIndex")]
	public class AccountIndex : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AccountIndex(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Icon] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[Summary] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Image Icon { get; set; }
		// private Image _unmodified_Icon;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Introduction { get; set; }
		// private string _unmodified_Introduction;

		[Column]
		public string Summary { get; set; }
		// private string _unmodified_Summary;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Introduction == null)
				Introduction = string.Empty;
			if(Summary == null)
				Summary = string.Empty;
		}
	}
    [Table(Name = "AccountModule")]
	public class AccountModule : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AccountModule(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Profile] TEXT NOT NULL, 
[Security] TEXT NOT NULL, 
[Roles] TEXT NOT NULL, 
[LocationCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public AccountProfile Profile { get; set; }
		// private AccountProfile _unmodified_Profile;

		[Column]
		public AccountSecurity Security { get; set; }
		// private AccountSecurity _unmodified_Security;

		[Column]
		public AccountRoles Roles { get; set; }
		// private AccountRoles _unmodified_Roles;

		[Column]
		public AddressAndLocationCollection LocationCollection { get; set; }
		// private AddressAndLocationCollection _unmodified_LocationCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "ImageGroupContainer")]
	public class ImageGroupContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ImageGroupContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ImageGroups] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ImageGroupCollection ImageGroups { get; set; }
		// private ImageGroupCollection _unmodified_ImageGroups;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "LocationContainer")]
	public class LocationContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS LocationContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Locations] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public AddressAndLocationCollection Locations { get; set; }
		// private AddressAndLocationCollection _unmodified_Locations;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "AddressAndLocation")]
	public class AddressAndLocation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AddressAndLocation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ReferenceToInformation] TEXT NOT NULL, 
[Address] TEXT NOT NULL, 
[Location] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ReferenceToInformation ReferenceToInformation { get; set; }
		// private ReferenceToInformation _unmodified_ReferenceToInformation;

		[Column]
		public StreetAddress Address { get; set; }
		// private StreetAddress _unmodified_Address;

		[Column]
		public Location Location { get; set; }
		// private Location _unmodified_Location;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "StreetAddress")]
	public class StreetAddress : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS StreetAddress(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Street] TEXT NOT NULL, 
[ZipCode] TEXT NOT NULL, 
[Town] TEXT NOT NULL, 
[Country] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Street { get; set; }
		// private string _unmodified_Street;

		[Column]
		public string ZipCode { get; set; }
		// private string _unmodified_ZipCode;

		[Column]
		public string Town { get; set; }
		// private string _unmodified_Town;

		[Column]
		public string Country { get; set; }
		// private string _unmodified_Country;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Street == null)
				Street = string.Empty;
			if(ZipCode == null)
				ZipCode = string.Empty;
			if(Town == null)
				Town = string.Empty;
			if(Country == null)
				Country = string.Empty;
		}
	}
    [Table(Name = "AccountContent")]
	public class AccountContent : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AccountContent(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Dummy] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Dummy { get; set; }
		// private string _unmodified_Dummy;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Dummy == null)
				Dummy = string.Empty;
		}
	}
    [Table(Name = "AccountProfile")]
	public class AccountProfile : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AccountProfile(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ProfileImage] TEXT NOT NULL, 
[FirstName] TEXT NOT NULL, 
[LastName] TEXT NOT NULL, 
[Address] TEXT NOT NULL, 
[IsSimplifiedAccount] INTEGER NOT NULL, 
[SimplifiedAccountEmail] TEXT NOT NULL, 
[SimplifiedAccountGroupID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Image ProfileImage { get; set; }
		// private Image _unmodified_ProfileImage;

		[Column]
		public string FirstName { get; set; }
		// private string _unmodified_FirstName;

		[Column]
		public string LastName { get; set; }
		// private string _unmodified_LastName;

		[Column]
		public StreetAddress Address { get; set; }
		// private StreetAddress _unmodified_Address;

		[Column]
		public bool IsSimplifiedAccount { get; set; }
		// private bool _unmodified_IsSimplifiedAccount;

		[Column]
		public string SimplifiedAccountEmail { get; set; }
		// private string _unmodified_SimplifiedAccountEmail;

		[Column]
		public string SimplifiedAccountGroupID { get; set; }
		// private string _unmodified_SimplifiedAccountGroupID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(FirstName == null)
				FirstName = string.Empty;
			if(LastName == null)
				LastName = string.Empty;
			if(SimplifiedAccountEmail == null)
				SimplifiedAccountEmail = string.Empty;
			if(SimplifiedAccountGroupID == null)
				SimplifiedAccountGroupID = string.Empty;
		}
	}
    [Table(Name = "AccountSecurity")]
	public class AccountSecurity : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AccountSecurity(
[ID] TEXT NOT NULL PRIMARY KEY, 
[LoginInfoCollection] TEXT NOT NULL, 
[EmailCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public TBLoginInfoCollection LoginInfoCollection { get; set; }
		// private TBLoginInfoCollection _unmodified_LoginInfoCollection;

		[Column]
		public TBEmailCollection EmailCollection { get; set; }
		// private TBEmailCollection _unmodified_EmailCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "AccountRoles")]
	public class AccountRoles : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AccountRoles(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ModeratorInGroups] TEXT NOT NULL, 
[MemberInGroups] TEXT NOT NULL, 
[OrganizationsImPartOf] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ReferenceCollection ModeratorInGroups { get; set; }
		// private ReferenceCollection _unmodified_ModeratorInGroups;

		[Column]
		public ReferenceCollection MemberInGroups { get; set; }
		// private ReferenceCollection _unmodified_MemberInGroups;

		[Column]
		public string OrganizationsImPartOf { get; set; }
		// private string _unmodified_OrganizationsImPartOf;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(OrganizationsImPartOf == null)
				OrganizationsImPartOf = string.Empty;
		}
	}
    [Table(Name = "PersonalInfoVisibility")]
	public class PersonalInfoVisibility : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS PersonalInfoVisibility(
[ID] TEXT NOT NULL PRIMARY KEY, 
[NoOne_Network_All] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string NoOne_Network_All { get; set; }
		// private string _unmodified_NoOne_Network_All;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(NoOne_Network_All == null)
				NoOne_Network_All = string.Empty;
		}
	}
    [Table(Name = "GroupedInformation")]
	public class GroupedInformation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS GroupedInformation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[GroupName] TEXT NOT NULL, 
[ReferenceCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string GroupName { get; set; }
		// private string _unmodified_GroupName;

		[Column]
		public ReferenceCollection ReferenceCollection { get; set; }
		// private ReferenceCollection _unmodified_ReferenceCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(GroupName == null)
				GroupName = string.Empty;
		}
	}
    [Table(Name = "ReferenceToInformation")]
	public class ReferenceToInformation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ReferenceToInformation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL, 
[URL] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string URL { get; set; }
		// private string _unmodified_URL;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(URL == null)
				URL = string.Empty;
		}
	}
    [Table(Name = "BlogContainer")]
	public class BlogContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS BlogContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Header] TEXT NOT NULL, 
[FeaturedBlog] TEXT NOT NULL, 
[RecentBlogSummary] TEXT NOT NULL, 
[BlogIndexGroup] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public Blog FeaturedBlog { get; set; }
		// private Blog _unmodified_FeaturedBlog;

		[Column]
		public RecentBlogSummary RecentBlogSummary { get; set; }
		// private RecentBlogSummary _unmodified_RecentBlogSummary;

		[Column]
		public BlogIndexGroup BlogIndexGroup { get; set; }
		// private BlogIndexGroup _unmodified_BlogIndexGroup;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "RecentBlogSummary")]
	public class RecentBlogSummary : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS RecentBlogSummary(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Introduction] TEXT NOT NULL, 
[RecentBlogCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Introduction Introduction { get; set; }
		// private Introduction _unmodified_Introduction;

		[Column]
		public BlogCollection RecentBlogCollection { get; set; }
		// private BlogCollection _unmodified_RecentBlogCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "NodeSummaryContainer")]
	public class NodeSummaryContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS NodeSummaryContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Nodes] TEXT NOT NULL, 
[NodeSourceBlogs] TEXT NOT NULL, 
[NodeSourceActivities] TEXT NOT NULL, 
[NodeSourceTextContent] TEXT NOT NULL, 
[NodeSourceLinkToContent] TEXT NOT NULL, 
[NodeSourceEmbeddedContent] TEXT NOT NULL, 
[NodeSourceImages] TEXT NOT NULL, 
[NodeSourceBinaryFiles] TEXT NOT NULL, 
[NodeSourceCategories] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public RenderedNodeCollection Nodes { get; set; }
		// private RenderedNodeCollection _unmodified_Nodes;

		[Column]
		public BlogCollection NodeSourceBlogs { get; set; }
		// private BlogCollection _unmodified_NodeSourceBlogs;

		[Column]
		public ActivityCollection NodeSourceActivities { get; set; }
		// private ActivityCollection _unmodified_NodeSourceActivities;

		[Column]
		public TextContentCollection NodeSourceTextContent { get; set; }
		// private TextContentCollection _unmodified_NodeSourceTextContent;

		[Column]
		public LinkToContentCollection NodeSourceLinkToContent { get; set; }
		// private LinkToContentCollection _unmodified_NodeSourceLinkToContent;

		[Column]
		public EmbeddedContentCollection NodeSourceEmbeddedContent { get; set; }
		// private EmbeddedContentCollection _unmodified_NodeSourceEmbeddedContent;

		[Column]
		public ImageCollection NodeSourceImages { get; set; }
		// private ImageCollection _unmodified_NodeSourceImages;

		[Column]
		public BinaryFileCollection NodeSourceBinaryFiles { get; set; }
		// private BinaryFileCollection _unmodified_NodeSourceBinaryFiles;

		[Column]
		public CategoryCollection NodeSourceCategories { get; set; }
		// private CategoryCollection _unmodified_NodeSourceCategories;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "RenderedNode")]
	public class RenderedNode : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS RenderedNode(
[ID] TEXT NOT NULL PRIMARY KEY, 
[OriginalContentID] TEXT NOT NULL, 
[TechnicalSource] TEXT NOT NULL, 
[ImageBaseUrl] TEXT NOT NULL, 
[ImageExt] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[ActualContentUrl] TEXT NOT NULL, 
[Excerpt] TEXT NOT NULL, 
[TimestampText] TEXT NOT NULL, 
[MainSortableText] TEXT NOT NULL, 
[IsCategoryFilteringNode] INTEGER NOT NULL, 
[CategoryFilters] TEXT NOT NULL, 
[CategoryNames] TEXT NOT NULL, 
[Categories] TEXT NOT NULL, 
[CategoryIDList] TEXT NOT NULL, 
[Authors] TEXT NOT NULL, 
[Locations] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string OriginalContentID { get; set; }
		// private string _unmodified_OriginalContentID;

		[Column]
		public string TechnicalSource { get; set; }
		// private string _unmodified_TechnicalSource;

		[Column]
		public string ImageBaseUrl { get; set; }
		// private string _unmodified_ImageBaseUrl;

		[Column]
		public string ImageExt { get; set; }
		// private string _unmodified_ImageExt;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string ActualContentUrl { get; set; }
		// private string _unmodified_ActualContentUrl;

		[Column]
		public string Excerpt { get; set; }
		// private string _unmodified_Excerpt;

		[Column]
		public string TimestampText { get; set; }
		// private string _unmodified_TimestampText;

		[Column]
		public string MainSortableText { get; set; }
		// private string _unmodified_MainSortableText;

		[Column]
		public bool IsCategoryFilteringNode { get; set; }
		// private bool _unmodified_IsCategoryFilteringNode;

		[Column]
		public ShortTextCollection CategoryFilters { get; set; }
		// private ShortTextCollection _unmodified_CategoryFilters;

		[Column]
		public ShortTextCollection CategoryNames { get; set; }
		// private ShortTextCollection _unmodified_CategoryNames;

		[Column]
		public ShortTextCollection Categories { get; set; }
		// private ShortTextCollection _unmodified_Categories;

		[Column]
		public string CategoryIDList { get; set; }
		// private string _unmodified_CategoryIDList;

		[Column]
		public ShortTextCollection Authors { get; set; }
		// private ShortTextCollection _unmodified_Authors;

		[Column]
		public ShortTextCollection Locations { get; set; }
		// private ShortTextCollection _unmodified_Locations;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(OriginalContentID == null)
				OriginalContentID = string.Empty;
			if(TechnicalSource == null)
				TechnicalSource = string.Empty;
			if(ImageBaseUrl == null)
				ImageBaseUrl = string.Empty;
			if(ImageExt == null)
				ImageExt = string.Empty;
			if(Title == null)
				Title = string.Empty;
			if(ActualContentUrl == null)
				ActualContentUrl = string.Empty;
			if(Excerpt == null)
				Excerpt = string.Empty;
			if(TimestampText == null)
				TimestampText = string.Empty;
			if(MainSortableText == null)
				MainSortableText = string.Empty;
			if(CategoryIDList == null)
				CategoryIDList = string.Empty;
		}
	}
    [Table(Name = "ShortTextObject")]
	public class ShortTextObject : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ShortTextObject(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Content] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Content { get; set; }
		// private string _unmodified_Content;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Content == null)
				Content = string.Empty;
		}
	}
    [Table(Name = "LongTextObject")]
	public class LongTextObject : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS LongTextObject(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Content] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Content { get; set; }
		// private string _unmodified_Content;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Content == null)
				Content = string.Empty;
		}
	}
    [Table(Name = "MapContainer")]
	public class MapContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS MapContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Header] TEXT NOT NULL, 
[MapFeatured] TEXT NOT NULL, 
[MapCollection] TEXT NOT NULL, 
[MapResultCollection] TEXT NOT NULL, 
[MapIndexCollection] TEXT NOT NULL, 
[MarkerSourceLocations] TEXT NOT NULL, 
[MarkerSourceBlogs] TEXT NOT NULL, 
[MarkerSourceActivities] TEXT NOT NULL, 
[MapMarkers] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public Map MapFeatured { get; set; }
		// private Map _unmodified_MapFeatured;

		[Column]
		public MapCollection MapCollection { get; set; }
		// private MapCollection _unmodified_MapCollection;

		[Column]
		public MapResultCollection MapResultCollection { get; set; }
		// private MapResultCollection _unmodified_MapResultCollection;

		[Column]
		public MapIndexCollection MapIndexCollection { get; set; }
		// private MapIndexCollection _unmodified_MapIndexCollection;

		[Column]
		public AddressAndLocationCollection MarkerSourceLocations { get; set; }
		// private AddressAndLocationCollection _unmodified_MarkerSourceLocations;

		[Column]
		public BlogCollection MarkerSourceBlogs { get; set; }
		// private BlogCollection _unmodified_MarkerSourceBlogs;

		[Column]
		public ActivityCollection MarkerSourceActivities { get; set; }
		// private ActivityCollection _unmodified_MarkerSourceActivities;

		[Column]
		public MapMarkerCollection MapMarkers { get; set; }
		// private MapMarkerCollection _unmodified_MapMarkers;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "MapMarker")]
	public class MapMarker : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS MapMarker(
[ID] TEXT NOT NULL PRIMARY KEY, 
[IconUrl] TEXT NOT NULL, 
[MarkerSource] TEXT NOT NULL, 
[CategoryName] TEXT NOT NULL, 
[LocationText] TEXT NOT NULL, 
[PopupTitle] TEXT NOT NULL, 
[PopupContent] TEXT NOT NULL, 
[Location] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string IconUrl { get; set; }
		// private string _unmodified_IconUrl;

		[Column]
		public string MarkerSource { get; set; }
		// private string _unmodified_MarkerSource;

		[Column]
		public string CategoryName { get; set; }
		// private string _unmodified_CategoryName;

		[Column]
		public string LocationText { get; set; }
		// private string _unmodified_LocationText;

		[Column]
		public string PopupTitle { get; set; }
		// private string _unmodified_PopupTitle;

		[Column]
		public string PopupContent { get; set; }
		// private string _unmodified_PopupContent;

		[Column]
		public Location Location { get; set; }
		// private Location _unmodified_Location;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(IconUrl == null)
				IconUrl = string.Empty;
			if(MarkerSource == null)
				MarkerSource = string.Empty;
			if(CategoryName == null)
				CategoryName = string.Empty;
			if(LocationText == null)
				LocationText = string.Empty;
			if(PopupTitle == null)
				PopupTitle = string.Empty;
			if(PopupContent == null)
				PopupContent = string.Empty;
		}
	}
    [Table(Name = "CalendarContainer")]
	public class CalendarContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS CalendarContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[CalendarContainerHeader] TEXT NOT NULL, 
[CalendarFeatured] TEXT NOT NULL, 
[CalendarCollection] TEXT NOT NULL, 
[CalendarIndexCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader CalendarContainerHeader { get; set; }
		// private ContainerHeader _unmodified_CalendarContainerHeader;

		[Column]
		public Calendar CalendarFeatured { get; set; }
		// private Calendar _unmodified_CalendarFeatured;

		[Column]
		public CalendarCollection CalendarCollection { get; set; }
		// private CalendarCollection _unmodified_CalendarCollection;

		[Column]
		public CalendarIndex CalendarIndexCollection { get; set; }
		// private CalendarIndex _unmodified_CalendarIndexCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "AboutContainer")]
	public class AboutContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AboutContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[MainImage] TEXT NOT NULL, 
[Header] TEXT NOT NULL, 
[Excerpt] TEXT NOT NULL, 
[Body] TEXT NOT NULL, 
[Published] TEXT NOT NULL, 
[Author] TEXT NOT NULL, 
[ImageGroup] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Image MainImage { get; set; }
		// private Image _unmodified_MainImage;

		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public string Excerpt { get; set; }
		// private string _unmodified_Excerpt;

		[Column]
		public string Body { get; set; }
		// private string _unmodified_Body;

		[Column]
		public DateTime Published { get; set; }
		// private DateTime _unmodified_Published;

		[Column]
		public string Author { get; set; }
		// private string _unmodified_Author;

		[Column]
		public ImageGroup ImageGroup { get; set; }
		// private ImageGroup _unmodified_ImageGroup;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Excerpt == null)
				Excerpt = string.Empty;
			if(Body == null)
				Body = string.Empty;
			if(Author == null)
				Author = string.Empty;
		}
	}
    [Table(Name = "OBSAccountContainer")]
	public class OBSAccountContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS OBSAccountContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[AccountContainerHeader] TEXT NOT NULL, 
[AccountFeatured] TEXT NOT NULL, 
[AccountCollection] TEXT NOT NULL, 
[AccountIndexCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader AccountContainerHeader { get; set; }
		// private ContainerHeader _unmodified_AccountContainerHeader;

		[Column]
		public Calendar AccountFeatured { get; set; }
		// private Calendar _unmodified_AccountFeatured;

		[Column]
		public CalendarCollection AccountCollection { get; set; }
		// private CalendarCollection _unmodified_AccountCollection;

		[Column]
		public CalendarIndex AccountIndexCollection { get; set; }
		// private CalendarIndex _unmodified_AccountIndexCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "ProjectContainer")]
	public class ProjectContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ProjectContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ProjectContainerHeader] TEXT NOT NULL, 
[ProjectFeatured] TEXT NOT NULL, 
[ProjectCollection] TEXT NOT NULL, 
[ProjectIndexCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader ProjectContainerHeader { get; set; }
		// private ContainerHeader _unmodified_ProjectContainerHeader;

		[Column]
		public Calendar ProjectFeatured { get; set; }
		// private Calendar _unmodified_ProjectFeatured;

		[Column]
		public CalendarCollection ProjectCollection { get; set; }
		// private CalendarCollection _unmodified_ProjectCollection;

		[Column]
		public CalendarIndex ProjectIndexCollection { get; set; }
		// private CalendarIndex _unmodified_ProjectIndexCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "CourseContainer")]
	public class CourseContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS CourseContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[CourseContainerHeader] TEXT NOT NULL, 
[CourseFeatured] TEXT NOT NULL, 
[CourseCollection] TEXT NOT NULL, 
[CourseIndexCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader CourseContainerHeader { get; set; }
		// private ContainerHeader _unmodified_CourseContainerHeader;

		[Column]
		public Calendar CourseFeatured { get; set; }
		// private Calendar _unmodified_CourseFeatured;

		[Column]
		public CalendarCollection CourseCollection { get; set; }
		// private CalendarCollection _unmodified_CourseCollection;

		[Column]
		public CalendarIndex CourseIndexCollection { get; set; }
		// private CalendarIndex _unmodified_CourseIndexCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "ContainerHeader")]
	public class ContainerHeader : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ContainerHeader(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL, 
[SubTitle] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string SubTitle { get; set; }
		// private string _unmodified_SubTitle;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(SubTitle == null)
				SubTitle = string.Empty;
		}
	}
    [Table(Name = "ActivitySummaryContainer")]
	public class ActivitySummaryContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ActivitySummaryContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Header] TEXT NOT NULL, 
[SummaryBody] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[ActivityIndex] TEXT NOT NULL, 
[ActivityCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public string SummaryBody { get; set; }
		// private string _unmodified_SummaryBody;

		[Column]
		public Introduction Introduction { get; set; }
		// private Introduction _unmodified_Introduction;

		[Column]
		public ActivityIndex ActivityIndex { get; set; }
		// private ActivityIndex _unmodified_ActivityIndex;

		[Column]
		public ActivityCollection ActivityCollection { get; set; }
		// private ActivityCollection _unmodified_ActivityCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(SummaryBody == null)
				SummaryBody = string.Empty;
		}
	}
    [Table(Name = "ActivityIndex")]
	public class ActivityIndex : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ActivityIndex(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Icon] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[Summary] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Image Icon { get; set; }
		// private Image _unmodified_Icon;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Introduction { get; set; }
		// private string _unmodified_Introduction;

		[Column]
		public string Summary { get; set; }
		// private string _unmodified_Summary;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Introduction == null)
				Introduction = string.Empty;
			if(Summary == null)
				Summary = string.Empty;
		}
	}
    [Table(Name = "ActivityContainer")]
	public class ActivityContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ActivityContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Header] TEXT NOT NULL, 
[ActivityIndex] TEXT NOT NULL, 
[ActivityModule] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public ActivityIndex ActivityIndex { get; set; }
		// private ActivityIndex _unmodified_ActivityIndex;

		[Column]
		public Activity ActivityModule { get; set; }
		// private Activity _unmodified_ActivityModule;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "Activity")]
	public class Activity : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Activity(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ReferenceToInformation] TEXT NOT NULL, 
[ProfileImage] TEXT NOT NULL, 
[IconImage] TEXT NOT NULL, 
[ActivityName] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[ContactPerson] TEXT NOT NULL, 
[StartingTime] TEXT NOT NULL, 
[Excerpt] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[IFrameSources] TEXT NOT NULL, 
[Collaborators] TEXT NOT NULL, 
[ImageGroupCollection] TEXT NOT NULL, 
[LocationCollection] TEXT NOT NULL, 
[CategoryCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ReferenceToInformation ReferenceToInformation { get; set; }
		// private ReferenceToInformation _unmodified_ReferenceToInformation;

		[Column]
		public Image ProfileImage { get; set; }
		// private Image _unmodified_ProfileImage;

		[Column]
		public Image IconImage { get; set; }
		// private Image _unmodified_IconImage;

		[Column]
		public string ActivityName { get; set; }
		// private string _unmodified_ActivityName;

		[Column]
		public Introduction Introduction { get; set; }
		// private Introduction _unmodified_Introduction;

		[Column]
		public string ContactPerson { get; set; }
		// private string _unmodified_ContactPerson;

		[Column]
		public DateTime StartingTime { get; set; }
		// private DateTime _unmodified_StartingTime;

		[Column]
		public string Excerpt { get; set; }
		// private string _unmodified_Excerpt;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public string IFrameSources { get; set; }
		// private string _unmodified_IFrameSources;

		[Column]
		public CollaboratorCollection Collaborators { get; set; }
		// private CollaboratorCollection _unmodified_Collaborators;

		[Column]
		public ImageGroupCollection ImageGroupCollection { get; set; }
		// private ImageGroupCollection _unmodified_ImageGroupCollection;

		[Column]
		public AddressAndLocationCollection LocationCollection { get; set; }
		// private AddressAndLocationCollection _unmodified_LocationCollection;

		[Column]
		public CategoryCollection CategoryCollection { get; set; }
		// private CategoryCollection _unmodified_CategoryCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ActivityName == null)
				ActivityName = string.Empty;
			if(ContactPerson == null)
				ContactPerson = string.Empty;
			if(Excerpt == null)
				Excerpt = string.Empty;
			if(Description == null)
				Description = string.Empty;
			if(IFrameSources == null)
				IFrameSources = string.Empty;
		}
	}
    [Table(Name = "Moderator")]
	public class Moderator : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Moderator(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ModeratorName] TEXT NOT NULL, 
[ProfileUrl] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ModeratorName { get; set; }
		// private string _unmodified_ModeratorName;

		[Column]
		public string ProfileUrl { get; set; }
		// private string _unmodified_ProfileUrl;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ModeratorName == null)
				ModeratorName = string.Empty;
			if(ProfileUrl == null)
				ProfileUrl = string.Empty;
		}
	}
    [Table(Name = "Collaborator")]
	public class Collaborator : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Collaborator(
[ID] TEXT NOT NULL PRIMARY KEY, 
[AccountID] TEXT NOT NULL, 
[EmailAddress] TEXT NOT NULL, 
[CollaboratorName] TEXT NOT NULL, 
[Role] TEXT NOT NULL, 
[ProfileUrl] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string AccountID { get; set; }
		// private string _unmodified_AccountID;

		[Column]
		public string EmailAddress { get; set; }
		// private string _unmodified_EmailAddress;

		[Column]
		public string CollaboratorName { get; set; }
		// private string _unmodified_CollaboratorName;

		[Column]
		public string Role { get; set; }
		// private string _unmodified_Role;

		[Column]
		public string ProfileUrl { get; set; }
		// private string _unmodified_ProfileUrl;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(AccountID == null)
				AccountID = string.Empty;
			if(EmailAddress == null)
				EmailAddress = string.Empty;
			if(CollaboratorName == null)
				CollaboratorName = string.Empty;
			if(Role == null)
				Role = string.Empty;
			if(ProfileUrl == null)
				ProfileUrl = string.Empty;
		}
	}
    [Table(Name = "GroupSummaryContainer")]
	public class GroupSummaryContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS GroupSummaryContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Header] TEXT NOT NULL, 
[SummaryBody] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[GroupSummaryIndex] TEXT NOT NULL, 
[GroupCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public string SummaryBody { get; set; }
		// private string _unmodified_SummaryBody;

		[Column]
		public Introduction Introduction { get; set; }
		// private Introduction _unmodified_Introduction;

		[Column]
		public GroupIndex GroupSummaryIndex { get; set; }
		// private GroupIndex _unmodified_GroupSummaryIndex;

		[Column]
		public GroupCollection GroupCollection { get; set; }
		// private GroupCollection _unmodified_GroupCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(SummaryBody == null)
				SummaryBody = string.Empty;
		}
	}
    [Table(Name = "GroupContainer")]
	public class GroupContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS GroupContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Header] TEXT NOT NULL, 
[GroupIndex] TEXT NOT NULL, 
[GroupProfile] TEXT NOT NULL, 
[Collaborators] TEXT NOT NULL, 
[PendingCollaborators] TEXT NOT NULL, 
[Activities] TEXT NOT NULL, 
[ImageGroupCollection] TEXT NOT NULL, 
[LocationCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ContainerHeader Header { get; set; }
		// private ContainerHeader _unmodified_Header;

		[Column]
		public GroupIndex GroupIndex { get; set; }
		// private GroupIndex _unmodified_GroupIndex;

		[Column]
		public Group GroupProfile { get; set; }
		// private Group _unmodified_GroupProfile;

		[Column]
		public CollaboratorCollection Collaborators { get; set; }
		// private CollaboratorCollection _unmodified_Collaborators;

		[Column]
		public CollaboratorCollection PendingCollaborators { get; set; }
		// private CollaboratorCollection _unmodified_PendingCollaborators;

		[Column]
		public ActivityCollection Activities { get; set; }
		// private ActivityCollection _unmodified_Activities;

		[Column]
		public ImageGroupCollection ImageGroupCollection { get; set; }
		// private ImageGroupCollection _unmodified_ImageGroupCollection;

		[Column]
		public AddressAndLocationCollection LocationCollection { get; set; }
		// private AddressAndLocationCollection _unmodified_LocationCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "GroupIndex")]
	public class GroupIndex : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS GroupIndex(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Icon] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[Summary] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Image Icon { get; set; }
		// private Image _unmodified_Icon;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Introduction { get; set; }
		// private string _unmodified_Introduction;

		[Column]
		public string Summary { get; set; }
		// private string _unmodified_Summary;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Introduction == null)
				Introduction = string.Empty;
			if(Summary == null)
				Summary = string.Empty;
		}
	}
    [Table(Name = "AddAddressAndLocationInfo")]
	public class AddAddressAndLocationInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AddAddressAndLocationInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[LocationName] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string LocationName { get; set; }
		// private string _unmodified_LocationName;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(LocationName == null)
				LocationName = string.Empty;
		}
	}
    [Table(Name = "AddImageInfo")]
	public class AddImageInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AddImageInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ImageTitle] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ImageTitle { get; set; }
		// private string _unmodified_ImageTitle;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ImageTitle == null)
				ImageTitle = string.Empty;
		}
	}
    [Table(Name = "AddImageGroupInfo")]
	public class AddImageGroupInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AddImageGroupInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ImageGroupTitle] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ImageGroupTitle { get; set; }
		// private string _unmodified_ImageGroupTitle;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ImageGroupTitle == null)
				ImageGroupTitle = string.Empty;
		}
	}
    [Table(Name = "AddEmailAddressInfo")]
	public class AddEmailAddressInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AddEmailAddressInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[EmailAddress] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string EmailAddress { get; set; }
		// private string _unmodified_EmailAddress;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(EmailAddress == null)
				EmailAddress = string.Empty;
		}
	}
    [Table(Name = "CreateGroupInfo")]
	public class CreateGroupInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS CreateGroupInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[GroupName] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string GroupName { get; set; }
		// private string _unmodified_GroupName;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(GroupName == null)
				GroupName = string.Empty;
		}
	}
    [Table(Name = "AddActivityInfo")]
	public class AddActivityInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AddActivityInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ActivityName] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ActivityName { get; set; }
		// private string _unmodified_ActivityName;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ActivityName == null)
				ActivityName = string.Empty;
		}
	}
    [Table(Name = "AddBlogPostInfo")]
	public class AddBlogPostInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AddBlogPostInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
		}
	}
    [Table(Name = "AddCategoryInfo")]
	public class AddCategoryInfo : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AddCategoryInfo(
[ID] TEXT NOT NULL PRIMARY KEY, 
[CategoryName] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string CategoryName { get; set; }
		// private string _unmodified_CategoryName;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(CategoryName == null)
				CategoryName = string.Empty;
		}
	}
    [Table(Name = "Group")]
	public class Group : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Group(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ReferenceToInformation] TEXT NOT NULL, 
[ProfileImage] TEXT NOT NULL, 
[IconImage] TEXT NOT NULL, 
[GroupName] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[OrganizationsAndGroupsLinkedToUs] TEXT NOT NULL, 
[WwwSiteToPublishTo] TEXT NOT NULL, 
[CustomUICollection] TEXT NOT NULL, 
[Moderators] TEXT NOT NULL, 
[CategoryCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ReferenceToInformation ReferenceToInformation { get; set; }
		// private ReferenceToInformation _unmodified_ReferenceToInformation;

		[Column]
		public Image ProfileImage { get; set; }
		// private Image _unmodified_ProfileImage;

		[Column]
		public Image IconImage { get; set; }
		// private Image _unmodified_IconImage;

		[Column]
		public string GroupName { get; set; }
		// private string _unmodified_GroupName;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public string OrganizationsAndGroupsLinkedToUs { get; set; }
		// private string _unmodified_OrganizationsAndGroupsLinkedToUs;

		[Column]
		public string WwwSiteToPublishTo { get; set; }
		// private string _unmodified_WwwSiteToPublishTo;

		[Column]
		public ShortTextCollection CustomUICollection { get; set; }
		// private ShortTextCollection _unmodified_CustomUICollection;

		[Column]
		public ModeratorCollection Moderators { get; set; }
		// private ModeratorCollection _unmodified_Moderators;

		[Column]
		public CategoryCollection CategoryCollection { get; set; }
		// private CategoryCollection _unmodified_CategoryCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(GroupName == null)
				GroupName = string.Empty;
			if(Description == null)
				Description = string.Empty;
			if(OrganizationsAndGroupsLinkedToUs == null)
				OrganizationsAndGroupsLinkedToUs = string.Empty;
			if(WwwSiteToPublishTo == null)
				WwwSiteToPublishTo = string.Empty;
		}
	}
    [Table(Name = "Introduction")]
	public class Introduction : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Introduction(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL, 
[Body] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Body { get; set; }
		// private string _unmodified_Body;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Body == null)
				Body = string.Empty;
		}
	}
    [Table(Name = "ContentCategoryRank")]
	public class ContentCategoryRank : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ContentCategoryRank(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ContentID] TEXT NOT NULL, 
[ContentSemanticType] TEXT NOT NULL, 
[CategoryID] TEXT NOT NULL, 
[RankName] TEXT NOT NULL, 
[RankValue] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ContentID { get; set; }
		// private string _unmodified_ContentID;

		[Column]
		public string ContentSemanticType { get; set; }
		// private string _unmodified_ContentSemanticType;

		[Column]
		public string CategoryID { get; set; }
		// private string _unmodified_CategoryID;

		[Column]
		public string RankName { get; set; }
		// private string _unmodified_RankName;

		[Column]
		public string RankValue { get; set; }
		// private string _unmodified_RankValue;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ContentID == null)
				ContentID = string.Empty;
			if(ContentSemanticType == null)
				ContentSemanticType = string.Empty;
			if(CategoryID == null)
				CategoryID = string.Empty;
			if(RankName == null)
				RankName = string.Empty;
			if(RankValue == null)
				RankValue = string.Empty;
		}
	}
    [Table(Name = "LinkToContent")]
	public class LinkToContent : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS LinkToContent(
[ID] TEXT NOT NULL PRIMARY KEY, 
[URL] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[Published] TEXT NOT NULL, 
[Author] TEXT NOT NULL, 
[ImageData] TEXT NOT NULL, 
[Locations] TEXT NOT NULL, 
[Categories] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string URL { get; set; }
		// private string _unmodified_URL;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public DateTime Published { get; set; }
		// private DateTime _unmodified_Published;

		[Column]
		public string Author { get; set; }
		// private string _unmodified_Author;

		[Column]
		public MediaContent ImageData { get; set; }
		// private MediaContent _unmodified_ImageData;

		[Column]
		public AddressAndLocationCollection Locations { get; set; }
		// private AddressAndLocationCollection _unmodified_Locations;

		[Column]
		public CategoryCollection Categories { get; set; }
		// private CategoryCollection _unmodified_Categories;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(URL == null)
				URL = string.Empty;
			if(Title == null)
				Title = string.Empty;
			if(Description == null)
				Description = string.Empty;
			if(Author == null)
				Author = string.Empty;
		}
	}
    [Table(Name = "EmbeddedContent")]
	public class EmbeddedContent : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS EmbeddedContent(
[ID] TEXT NOT NULL PRIMARY KEY, 
[IFrameTagContents] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Published] TEXT NOT NULL, 
[Author] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[Locations] TEXT NOT NULL, 
[Categories] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string IFrameTagContents { get; set; }
		// private string _unmodified_IFrameTagContents;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public DateTime Published { get; set; }
		// private DateTime _unmodified_Published;

		[Column]
		public string Author { get; set; }
		// private string _unmodified_Author;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public AddressAndLocationCollection Locations { get; set; }
		// private AddressAndLocationCollection _unmodified_Locations;

		[Column]
		public CategoryCollection Categories { get; set; }
		// private CategoryCollection _unmodified_Categories;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(IFrameTagContents == null)
				IFrameTagContents = string.Empty;
			if(Title == null)
				Title = string.Empty;
			if(Author == null)
				Author = string.Empty;
			if(Description == null)
				Description = string.Empty;
		}
	}
    [Table(Name = "DynamicContentGroup")]
	public class DynamicContentGroup : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS DynamicContentGroup(
[ID] TEXT NOT NULL PRIMARY KEY, 
[HostName] TEXT NOT NULL, 
[GroupHeader] TEXT NOT NULL, 
[SortValue] TEXT NOT NULL, 
[PageLocation] TEXT NOT NULL, 
[ContentItemNames] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string HostName { get; set; }
		// private string _unmodified_HostName;

		[Column]
		public string GroupHeader { get; set; }
		// private string _unmodified_GroupHeader;

		[Column]
		public string SortValue { get; set; }
		// private string _unmodified_SortValue;

		[Column]
		public string PageLocation { get; set; }
		// private string _unmodified_PageLocation;

		[Column]
		public string ContentItemNames { get; set; }
		// private string _unmodified_ContentItemNames;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(HostName == null)
				HostName = string.Empty;
			if(GroupHeader == null)
				GroupHeader = string.Empty;
			if(SortValue == null)
				SortValue = string.Empty;
			if(PageLocation == null)
				PageLocation = string.Empty;
			if(ContentItemNames == null)
				ContentItemNames = string.Empty;
		}
	}
    [Table(Name = "DynamicContent")]
	public class DynamicContent : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS DynamicContent(
[ID] TEXT NOT NULL PRIMARY KEY, 
[HostName] TEXT NOT NULL, 
[ContentName] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[ElementQuery] TEXT NOT NULL, 
[Content] TEXT NOT NULL, 
[RawContent] TEXT NOT NULL, 
[ImageData] TEXT NOT NULL, 
[IsEnabled] INTEGER NOT NULL, 
[ApplyActively] INTEGER NOT NULL, 
[EditType] TEXT NOT NULL, 
[PageLocation] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string HostName { get; set; }
		// private string _unmodified_HostName;

		[Column]
		public string ContentName { get; set; }
		// private string _unmodified_ContentName;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public string ElementQuery { get; set; }
		// private string _unmodified_ElementQuery;

		[Column]
		public string Content { get; set; }
		// private string _unmodified_Content;

		[Column]
		public string RawContent { get; set; }
		// private string _unmodified_RawContent;

		[Column]
		public MediaContent ImageData { get; set; }
		// private MediaContent _unmodified_ImageData;

		[Column]
		public bool IsEnabled { get; set; }
		// private bool _unmodified_IsEnabled;

		[Column]
		public bool ApplyActively { get; set; }
		// private bool _unmodified_ApplyActively;

		[Column]
		public string EditType { get; set; }
		// private string _unmodified_EditType;

		[Column]
		public string PageLocation { get; set; }
		// private string _unmodified_PageLocation;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(HostName == null)
				HostName = string.Empty;
			if(ContentName == null)
				ContentName = string.Empty;
			if(Title == null)
				Title = string.Empty;
			if(Description == null)
				Description = string.Empty;
			if(ElementQuery == null)
				ElementQuery = string.Empty;
			if(Content == null)
				Content = string.Empty;
			if(RawContent == null)
				RawContent = string.Empty;
			if(EditType == null)
				EditType = string.Empty;
			if(PageLocation == null)
				PageLocation = string.Empty;
		}
	}
    [Table(Name = "AttachedToObject")]
	public class AttachedToObject : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AttachedToObject(
[ID] TEXT NOT NULL PRIMARY KEY, 
[SourceObjectID] TEXT NOT NULL, 
[SourceObjectName] TEXT NOT NULL, 
[SourceObjectDomain] TEXT NOT NULL, 
[TargetObjectID] TEXT NOT NULL, 
[TargetObjectName] TEXT NOT NULL, 
[TargetObjectDomain] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string SourceObjectID { get; set; }
		// private string _unmodified_SourceObjectID;

		[Column]
		public string SourceObjectName { get; set; }
		// private string _unmodified_SourceObjectName;

		[Column]
		public string SourceObjectDomain { get; set; }
		// private string _unmodified_SourceObjectDomain;

		[Column]
		public string TargetObjectID { get; set; }
		// private string _unmodified_TargetObjectID;

		[Column]
		public string TargetObjectName { get; set; }
		// private string _unmodified_TargetObjectName;

		[Column]
		public string TargetObjectDomain { get; set; }
		// private string _unmodified_TargetObjectDomain;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(SourceObjectID == null)
				SourceObjectID = string.Empty;
			if(SourceObjectName == null)
				SourceObjectName = string.Empty;
			if(SourceObjectDomain == null)
				SourceObjectDomain = string.Empty;
			if(TargetObjectID == null)
				TargetObjectID = string.Empty;
			if(TargetObjectName == null)
				TargetObjectName = string.Empty;
			if(TargetObjectDomain == null)
				TargetObjectDomain = string.Empty;
		}
	}
    [Table(Name = "Comment")]
	public class Comment : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Comment(
[ID] TEXT NOT NULL PRIMARY KEY, 
[TargetObjectID] TEXT NOT NULL, 
[TargetObjectName] TEXT NOT NULL, 
[TargetObjectDomain] TEXT NOT NULL, 
[CommentText] TEXT NOT NULL, 
[Created] TEXT NOT NULL, 
[OriginalAuthorName] TEXT NOT NULL, 
[OriginalAuthorEmail] TEXT NOT NULL, 
[OriginalAuthorAccountID] TEXT NOT NULL, 
[LastModified] TEXT NOT NULL, 
[LastAuthorName] TEXT NOT NULL, 
[LastAuthorEmail] TEXT NOT NULL, 
[LastAuthorAccountID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string TargetObjectID { get; set; }
		// private string _unmodified_TargetObjectID;

		[Column]
		public string TargetObjectName { get; set; }
		// private string _unmodified_TargetObjectName;

		[Column]
		public string TargetObjectDomain { get; set; }
		// private string _unmodified_TargetObjectDomain;

		[Column]
		public string CommentText { get; set; }
		// private string _unmodified_CommentText;

		[Column]
		public DateTime Created { get; set; }
		// private DateTime _unmodified_Created;

		[Column]
		public string OriginalAuthorName { get; set; }
		// private string _unmodified_OriginalAuthorName;

		[Column]
		public string OriginalAuthorEmail { get; set; }
		// private string _unmodified_OriginalAuthorEmail;

		[Column]
		public string OriginalAuthorAccountID { get; set; }
		// private string _unmodified_OriginalAuthorAccountID;

		[Column]
		public DateTime LastModified { get; set; }
		// private DateTime _unmodified_LastModified;

		[Column]
		public string LastAuthorName { get; set; }
		// private string _unmodified_LastAuthorName;

		[Column]
		public string LastAuthorEmail { get; set; }
		// private string _unmodified_LastAuthorEmail;

		[Column]
		public string LastAuthorAccountID { get; set; }
		// private string _unmodified_LastAuthorAccountID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(TargetObjectID == null)
				TargetObjectID = string.Empty;
			if(TargetObjectName == null)
				TargetObjectName = string.Empty;
			if(TargetObjectDomain == null)
				TargetObjectDomain = string.Empty;
			if(CommentText == null)
				CommentText = string.Empty;
			if(OriginalAuthorName == null)
				OriginalAuthorName = string.Empty;
			if(OriginalAuthorEmail == null)
				OriginalAuthorEmail = string.Empty;
			if(OriginalAuthorAccountID == null)
				OriginalAuthorAccountID = string.Empty;
			if(LastAuthorName == null)
				LastAuthorName = string.Empty;
			if(LastAuthorEmail == null)
				LastAuthorEmail = string.Empty;
			if(LastAuthorAccountID == null)
				LastAuthorAccountID = string.Empty;
		}
	}
    [Table(Name = "Selection")]
	public class Selection : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Selection(
[ID] TEXT NOT NULL PRIMARY KEY, 
[TargetObjectID] TEXT NOT NULL, 
[TargetObjectName] TEXT NOT NULL, 
[TargetObjectDomain] TEXT NOT NULL, 
[SelectionCategory] TEXT NOT NULL, 
[TextValue] TEXT NOT NULL, 
[BooleanValue] INTEGER NOT NULL, 
[DoubleValue] REAL NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string TargetObjectID { get; set; }
		// private string _unmodified_TargetObjectID;

		[Column]
		public string TargetObjectName { get; set; }
		// private string _unmodified_TargetObjectName;

		[Column]
		public string TargetObjectDomain { get; set; }
		// private string _unmodified_TargetObjectDomain;

		[Column]
		public string SelectionCategory { get; set; }
		// private string _unmodified_SelectionCategory;

		[Column]
		public string TextValue { get; set; }
		// private string _unmodified_TextValue;

		[Column]
		public bool BooleanValue { get; set; }
		// private bool _unmodified_BooleanValue;

		[Column]
		public double DoubleValue { get; set; }
		// private double _unmodified_DoubleValue;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(TargetObjectID == null)
				TargetObjectID = string.Empty;
			if(TargetObjectName == null)
				TargetObjectName = string.Empty;
			if(TargetObjectDomain == null)
				TargetObjectDomain = string.Empty;
			if(SelectionCategory == null)
				SelectionCategory = string.Empty;
			if(TextValue == null)
				TextValue = string.Empty;
		}
	}
    [Table(Name = "TextContent")]
	public class TextContent : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS TextContent(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ImageData] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[SubTitle] TEXT NOT NULL, 
[Published] TEXT NOT NULL, 
[Author] TEXT NOT NULL, 
[Excerpt] TEXT NOT NULL, 
[Body] TEXT NOT NULL, 
[Locations] TEXT NOT NULL, 
[Categories] TEXT NOT NULL, 
[SortOrderNumber] REAL NOT NULL, 
[IFrameSources] TEXT NOT NULL, 
[RawHtmlContent] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public MediaContent ImageData { get; set; }
		// private MediaContent _unmodified_ImageData;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string SubTitle { get; set; }
		// private string _unmodified_SubTitle;

		[Column]
		public DateTime Published { get; set; }
		// private DateTime _unmodified_Published;

		[Column]
		public string Author { get; set; }
		// private string _unmodified_Author;

		[Column]
		public string Excerpt { get; set; }
		// private string _unmodified_Excerpt;

		[Column]
		public string Body { get; set; }
		// private string _unmodified_Body;

		[Column]
		public AddressAndLocationCollection Locations { get; set; }
		// private AddressAndLocationCollection _unmodified_Locations;

		[Column]
		public CategoryCollection Categories { get; set; }
		// private CategoryCollection _unmodified_Categories;

		[Column]
		public double SortOrderNumber { get; set; }
		// private double _unmodified_SortOrderNumber;

		[Column]
		public string IFrameSources { get; set; }
		// private string _unmodified_IFrameSources;

		[Column]
		public string RawHtmlContent { get; set; }
		// private string _unmodified_RawHtmlContent;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(SubTitle == null)
				SubTitle = string.Empty;
			if(Author == null)
				Author = string.Empty;
			if(Excerpt == null)
				Excerpt = string.Empty;
			if(Body == null)
				Body = string.Empty;
			if(IFrameSources == null)
				IFrameSources = string.Empty;
			if(RawHtmlContent == null)
				RawHtmlContent = string.Empty;
		}
	}
    [Table(Name = "Blog")]
	public class Blog : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Blog(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ReferenceToInformation] TEXT NOT NULL, 
[ProfileImage] TEXT NOT NULL, 
[IconImage] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[SubTitle] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[Published] TEXT NOT NULL, 
[Author] TEXT NOT NULL, 
[FeaturedImage] TEXT NOT NULL, 
[ImageGroupCollection] TEXT NOT NULL, 
[VideoGroup] TEXT NOT NULL, 
[Body] TEXT NOT NULL, 
[Excerpt] TEXT NOT NULL, 
[IFrameSources] TEXT NOT NULL, 
[LocationCollection] TEXT NOT NULL, 
[CategoryCollection] TEXT NOT NULL, 
[SocialPanel] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ReferenceToInformation ReferenceToInformation { get; set; }
		// private ReferenceToInformation _unmodified_ReferenceToInformation;

		[Column]
		public Image ProfileImage { get; set; }
		// private Image _unmodified_ProfileImage;

		[Column]
		public Image IconImage { get; set; }
		// private Image _unmodified_IconImage;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string SubTitle { get; set; }
		// private string _unmodified_SubTitle;

		[Column]
		public Introduction Introduction { get; set; }
		// private Introduction _unmodified_Introduction;

		[Column]
		public DateTime Published { get; set; }
		// private DateTime _unmodified_Published;

		[Column]
		public string Author { get; set; }
		// private string _unmodified_Author;

		[Column]
		public Image FeaturedImage { get; set; }
		// private Image _unmodified_FeaturedImage;

		[Column]
		public ImageGroupCollection ImageGroupCollection { get; set; }
		// private ImageGroupCollection _unmodified_ImageGroupCollection;

		[Column]
		public VideoGroup VideoGroup { get; set; }
		// private VideoGroup _unmodified_VideoGroup;

		[Column]
		public string Body { get; set; }
		// private string _unmodified_Body;

		[Column]
		public string Excerpt { get; set; }
		// private string _unmodified_Excerpt;

		[Column]
		public string IFrameSources { get; set; }
		// private string _unmodified_IFrameSources;

		[Column]
		public AddressAndLocationCollection LocationCollection { get; set; }
		// private AddressAndLocationCollection _unmodified_LocationCollection;

		[Column]
		public CategoryCollection CategoryCollection { get; set; }
		// private CategoryCollection _unmodified_CategoryCollection;

		[Column]
		public SocialPanelCollection SocialPanel { get; set; }
		// private SocialPanelCollection _unmodified_SocialPanel;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(SubTitle == null)
				SubTitle = string.Empty;
			if(Author == null)
				Author = string.Empty;
			if(Body == null)
				Body = string.Empty;
			if(Excerpt == null)
				Excerpt = string.Empty;
			if(IFrameSources == null)
				IFrameSources = string.Empty;
		}
	}
    [Table(Name = "BlogIndexGroup")]
	public class BlogIndexGroup : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS BlogIndexGroup(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Icon] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[GroupedByDate] TEXT NOT NULL, 
[GroupedByLocation] TEXT NOT NULL, 
[GroupedByAuthor] TEXT NOT NULL, 
[GroupedByCategory] TEXT NOT NULL, 
[FullBlogArchive] TEXT NOT NULL, 
[BlogSourceForSummary] TEXT NOT NULL, 
[Summary] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Image Icon { get; set; }
		// private Image _unmodified_Icon;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Introduction { get; set; }
		// private string _unmodified_Introduction;

		[Column]
		public GroupedInformationCollection GroupedByDate { get; set; }
		// private GroupedInformationCollection _unmodified_GroupedByDate;

		[Column]
		public GroupedInformationCollection GroupedByLocation { get; set; }
		// private GroupedInformationCollection _unmodified_GroupedByLocation;

		[Column]
		public GroupedInformationCollection GroupedByAuthor { get; set; }
		// private GroupedInformationCollection _unmodified_GroupedByAuthor;

		[Column]
		public GroupedInformationCollection GroupedByCategory { get; set; }
		// private GroupedInformationCollection _unmodified_GroupedByCategory;

		[Column]
		public ReferenceCollection FullBlogArchive { get; set; }
		// private ReferenceCollection _unmodified_FullBlogArchive;

		[Column]
		public BlogCollection BlogSourceForSummary { get; set; }
		// private BlogCollection _unmodified_BlogSourceForSummary;

		[Column]
		public string Summary { get; set; }
		// private string _unmodified_Summary;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Introduction == null)
				Introduction = string.Empty;
			if(Summary == null)
				Summary = string.Empty;
		}
	}
    [Table(Name = "CalendarIndex")]
	public class CalendarIndex : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS CalendarIndex(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Icon] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Introduction] TEXT NOT NULL, 
[Summary] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Image Icon { get; set; }
		// private Image _unmodified_Icon;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Introduction { get; set; }
		// private string _unmodified_Introduction;

		[Column]
		public string Summary { get; set; }
		// private string _unmodified_Summary;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Introduction == null)
				Introduction = string.Empty;
			if(Summary == null)
				Summary = string.Empty;
		}
	}
    [Table(Name = "Filter")]
	public class Filter : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Filter(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
		}
	}
    [Table(Name = "Calendar")]
	public class Calendar : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Calendar(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
		}
	}
    [Table(Name = "Map")]
	public class Map : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Map(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
		}
	}
    [Table(Name = "MapIndexCollection")]
	public class MapIndexCollection : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS MapIndexCollection(
[ID] TEXT NOT NULL PRIMARY KEY, 
[MapByDate] TEXT NOT NULL, 
[MapByLocation] TEXT NOT NULL, 
[MapByAuthor] TEXT NOT NULL, 
[MapByCategory] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public MapCollection MapByDate { get; set; }
		// private MapCollection _unmodified_MapByDate;

		[Column]
		public MapCollection MapByLocation { get; set; }
		// private MapCollection _unmodified_MapByLocation;

		[Column]
		public MapCollection MapByAuthor { get; set; }
		// private MapCollection _unmodified_MapByAuthor;

		[Column]
		public MapCollection MapByCategory { get; set; }
		// private MapCollection _unmodified_MapByCategory;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "MapResult")]
	public class MapResult : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS MapResult(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Location] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Location Location { get; set; }
		// private Location _unmodified_Location;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "MapResultsCollection")]
	public class MapResultsCollection : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS MapResultsCollection(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ResultByDate] TEXT NOT NULL, 
[ResultByAuthor] TEXT NOT NULL, 
[ResultByProximity] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public MapResultCollection ResultByDate { get; set; }
		// private MapResultCollection _unmodified_ResultByDate;

		[Column]
		public MapResultCollection ResultByAuthor { get; set; }
		// private MapResultCollection _unmodified_ResultByAuthor;

		[Column]
		public MapResultCollection ResultByProximity { get; set; }
		// private MapResultCollection _unmodified_ResultByProximity;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "Video")]
	public class Video : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Video(
[ID] TEXT NOT NULL PRIMARY KEY, 
[VideoData] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Caption] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public MediaContent VideoData { get; set; }
		// private MediaContent _unmodified_VideoData;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Caption { get; set; }
		// private string _unmodified_Caption;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Caption == null)
				Caption = string.Empty;
		}
	}
    [Table(Name = "Image")]
	public class Image : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Image(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ReferenceToInformation] TEXT NOT NULL, 
[ImageData] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Caption] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[Locations] TEXT NOT NULL, 
[Categories] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ReferenceToInformation ReferenceToInformation { get; set; }
		// private ReferenceToInformation _unmodified_ReferenceToInformation;

		[Column]
		public MediaContent ImageData { get; set; }
		// private MediaContent _unmodified_ImageData;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Caption { get; set; }
		// private string _unmodified_Caption;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public AddressAndLocationCollection Locations { get; set; }
		// private AddressAndLocationCollection _unmodified_Locations;

		[Column]
		public CategoryCollection Categories { get; set; }
		// private CategoryCollection _unmodified_Categories;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Caption == null)
				Caption = string.Empty;
			if(Description == null)
				Description = string.Empty;
		}
	}
    [Table(Name = "BinaryFile")]
	public class BinaryFile : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS BinaryFile(
[ID] TEXT NOT NULL PRIMARY KEY, 
[OriginalFileName] TEXT NOT NULL, 
[Data] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[Categories] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string OriginalFileName { get; set; }
		// private string _unmodified_OriginalFileName;

		[Column]
		public MediaContent Data { get; set; }
		// private MediaContent _unmodified_Data;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public CategoryCollection Categories { get; set; }
		// private CategoryCollection _unmodified_Categories;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(OriginalFileName == null)
				OriginalFileName = string.Empty;
			if(Title == null)
				Title = string.Empty;
			if(Description == null)
				Description = string.Empty;
		}
	}
    [Table(Name = "ImageGroup")]
	public class ImageGroup : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ImageGroup(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ReferenceToInformation] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[FeaturedImage] TEXT NOT NULL, 
[ImagesCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ReferenceToInformation ReferenceToInformation { get; set; }
		// private ReferenceToInformation _unmodified_ReferenceToInformation;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public Image FeaturedImage { get; set; }
		// private Image _unmodified_FeaturedImage;

		[Column]
		public ImageCollection ImagesCollection { get; set; }
		// private ImageCollection _unmodified_ImagesCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Description == null)
				Description = string.Empty;
		}
	}
    [Table(Name = "VideoGroup")]
	public class VideoGroup : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS VideoGroup(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Title] TEXT NOT NULL, 
[Description] TEXT NOT NULL, 
[VideoCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;

		[Column]
		public VideoCollection VideoCollection { get; set; }
		// private VideoCollection _unmodified_VideoCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Description == null)
				Description = string.Empty;
		}
	}
    [Table(Name = "Tooltip")]
	public class Tooltip : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Tooltip(
[ID] TEXT NOT NULL PRIMARY KEY, 
[TooltipText] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string TooltipText { get; set; }
		// private string _unmodified_TooltipText;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(TooltipText == null)
				TooltipText = string.Empty;
		}
	}
    [Table(Name = "SocialPanel")]
	public class SocialPanel : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS SocialPanel(
[ID] TEXT NOT NULL PRIMARY KEY, 
[SocialFilter] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Filter SocialFilter { get; set; }
		// private Filter _unmodified_SocialFilter;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "Longitude")]
	public class Longitude : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Longitude(
[ID] TEXT NOT NULL PRIMARY KEY, 
[TextValue] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string TextValue { get; set; }
		// private string _unmodified_TextValue;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(TextValue == null)
				TextValue = string.Empty;
		}
	}
    [Table(Name = "Latitude")]
	public class Latitude : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Latitude(
[ID] TEXT NOT NULL PRIMARY KEY, 
[TextValue] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string TextValue { get; set; }
		// private string _unmodified_TextValue;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(TextValue == null)
				TextValue = string.Empty;
		}
	}
    [Table(Name = "Location")]
	public class Location : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Location(
[ID] TEXT NOT NULL PRIMARY KEY, 
[LocationName] TEXT NOT NULL, 
[Longitude] TEXT NOT NULL, 
[Latitude] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string LocationName { get; set; }
		// private string _unmodified_LocationName;

		[Column]
		public Longitude Longitude { get; set; }
		// private Longitude _unmodified_Longitude;

		[Column]
		public Latitude Latitude { get; set; }
		// private Latitude _unmodified_Latitude;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(LocationName == null)
				LocationName = string.Empty;
		}
	}
    [Table(Name = "Date")]
	public class Date : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Date(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Day] TEXT NOT NULL, 
[Week] TEXT NOT NULL, 
[Month] TEXT NOT NULL, 
[Year] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public DateTime Day { get; set; }
		// private DateTime _unmodified_Day;

		[Column]
		public DateTime Week { get; set; }
		// private DateTime _unmodified_Week;

		[Column]
		public DateTime Month { get; set; }
		// private DateTime _unmodified_Month;

		[Column]
		public DateTime Year { get; set; }
		// private DateTime _unmodified_Year;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "Sex")]
	public class Sex : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Sex(
[ID] TEXT NOT NULL PRIMARY KEY, 
[SexText] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string SexText { get; set; }
		// private string _unmodified_SexText;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(SexText == null)
				SexText = string.Empty;
		}
	}
    [Table(Name = "OBSAddress")]
	public class OBSAddress : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS OBSAddress(
[ID] TEXT NOT NULL PRIMARY KEY, 
[StreetName] TEXT NOT NULL, 
[BuildingNumber] TEXT NOT NULL, 
[PostOfficeBox] TEXT NOT NULL, 
[PostalCode] TEXT NOT NULL, 
[Municipality] TEXT NOT NULL, 
[Region] TEXT NOT NULL, 
[Province] TEXT NOT NULL, 
[state] TEXT NOT NULL, 
[Country] TEXT NOT NULL, 
[Continent] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string StreetName { get; set; }
		// private string _unmodified_StreetName;

		[Column]
		public string BuildingNumber { get; set; }
		// private string _unmodified_BuildingNumber;

		[Column]
		public string PostOfficeBox { get; set; }
		// private string _unmodified_PostOfficeBox;

		[Column]
		public string PostalCode { get; set; }
		// private string _unmodified_PostalCode;

		[Column]
		public string Municipality { get; set; }
		// private string _unmodified_Municipality;

		[Column]
		public string Region { get; set; }
		// private string _unmodified_Region;

		[Column]
		public string Province { get; set; }
		// private string _unmodified_Province;

		[Column]
		public string state { get; set; }
		// private string _unmodified_state;

		[Column]
		public string Country { get; set; }
		// private string _unmodified_Country;

		[Column]
		public string Continent { get; set; }
		// private string _unmodified_Continent;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(StreetName == null)
				StreetName = string.Empty;
			if(BuildingNumber == null)
				BuildingNumber = string.Empty;
			if(PostOfficeBox == null)
				PostOfficeBox = string.Empty;
			if(PostalCode == null)
				PostalCode = string.Empty;
			if(Municipality == null)
				Municipality = string.Empty;
			if(Region == null)
				Region = string.Empty;
			if(Province == null)
				Province = string.Empty;
			if(state == null)
				state = string.Empty;
			if(Country == null)
				Country = string.Empty;
			if(Continent == null)
				Continent = string.Empty;
		}
	}
    [Table(Name = "Identity")]
	public class Identity : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Identity(
[ID] TEXT NOT NULL PRIMARY KEY, 
[FirstName] TEXT NOT NULL, 
[LastName] TEXT NOT NULL, 
[Initials] TEXT NOT NULL, 
[Sex] TEXT NOT NULL, 
[Birthday] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string FirstName { get; set; }
		// private string _unmodified_FirstName;

		[Column]
		public string LastName { get; set; }
		// private string _unmodified_LastName;

		[Column]
		public string Initials { get; set; }
		// private string _unmodified_Initials;

		[Column]
		public Sex Sex { get; set; }
		// private Sex _unmodified_Sex;

		[Column]
		public Date Birthday { get; set; }
		// private Date _unmodified_Birthday;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(FirstName == null)
				FirstName = string.Empty;
			if(LastName == null)
				LastName = string.Empty;
			if(Initials == null)
				Initials = string.Empty;
		}
	}
    [Table(Name = "ImageVideoSoundVectorRaw")]
	public class ImageVideoSoundVectorRaw : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS ImageVideoSoundVectorRaw(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Image] BLOB NOT NULL, 
[Video] BLOB NOT NULL, 
[Sound] BLOB NOT NULL, 
[Vector] TEXT NOT NULL, 
[Raw] BLOB NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public byte[] Image { get; set; }
		// private byte[] _unmodified_Image;

		[Column]
		public byte[] Video { get; set; }
		// private byte[] _unmodified_Video;

		[Column]
		public byte[] Sound { get; set; }
		// private byte[] _unmodified_Sound;

		[Column]
		public string Vector { get; set; }
		// private string _unmodified_Vector;

		[Column]
		public byte[] Raw { get; set; }
		// private byte[] _unmodified_Raw;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Vector == null)
				Vector = string.Empty;
		}
	}
    [Table(Name = "CategoryContainer")]
	public class CategoryContainer : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS CategoryContainer(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Categories] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public CategoryCollection Categories { get; set; }
		// private CategoryCollection _unmodified_Categories;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "Category")]
	public class Category : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Category(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ReferenceToInformation] TEXT NOT NULL, 
[CategoryName] TEXT NOT NULL, 
[ImageData] TEXT NOT NULL, 
[Title] TEXT NOT NULL, 
[Excerpt] TEXT NOT NULL, 
[ParentCategory] TEXT NOT NULL, 
[ParentCategoryID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public ReferenceToInformation ReferenceToInformation { get; set; }
		// private ReferenceToInformation _unmodified_ReferenceToInformation;

		[Column]
		public string CategoryName { get; set; }
		// private string _unmodified_CategoryName;

		[Column]
		public MediaContent ImageData { get; set; }
		// private MediaContent _unmodified_ImageData;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Excerpt { get; set; }
		// private string _unmodified_Excerpt;

		[Column]
		public Category ParentCategory { get; set; }
		// private Category _unmodified_ParentCategory;

		[Column]
		public string ParentCategoryID { get; set; }
		// private string _unmodified_ParentCategoryID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(CategoryName == null)
				CategoryName = string.Empty;
			if(Title == null)
				Title = string.Empty;
			if(Excerpt == null)
				Excerpt = string.Empty;
			if(ParentCategoryID == null)
				ParentCategoryID = string.Empty;
		}
	}
    [Table(Name = "Subscription")]
	public class Subscription : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Subscription(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Priority] INTEGER NOT NULL, 
[TargetRelativeLocation] TEXT NOT NULL, 
[TargetInformationObjectType] TEXT NOT NULL, 
[SubscriberRelativeLocation] TEXT NOT NULL, 
[SubscriberInformationObjectType] TEXT NOT NULL, 
[SubscriptionType] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public long Priority { get; set; }
		// private long _unmodified_Priority;

		[Column]
		public string TargetRelativeLocation { get; set; }
		// private string _unmodified_TargetRelativeLocation;

		[Column]
		public string TargetInformationObjectType { get; set; }
		// private string _unmodified_TargetInformationObjectType;

		[Column]
		public string SubscriberRelativeLocation { get; set; }
		// private string _unmodified_SubscriberRelativeLocation;

		[Column]
		public string SubscriberInformationObjectType { get; set; }
		// private string _unmodified_SubscriberInformationObjectType;

		[Column]
		public string SubscriptionType { get; set; }
		// private string _unmodified_SubscriptionType;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(TargetRelativeLocation == null)
				TargetRelativeLocation = string.Empty;
			if(TargetInformationObjectType == null)
				TargetInformationObjectType = string.Empty;
			if(SubscriberRelativeLocation == null)
				SubscriberRelativeLocation = string.Empty;
			if(SubscriberInformationObjectType == null)
				SubscriberInformationObjectType = string.Empty;
			if(SubscriptionType == null)
				SubscriptionType = string.Empty;
		}
	}
    [Table(Name = "QueueEnvelope")]
	public class QueueEnvelope : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS QueueEnvelope(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ActiveContainerName] TEXT NOT NULL, 
[OwnerPrefix] TEXT NOT NULL, 
[CurrentRetryCount] INTEGER NOT NULL, 
[SingleOperation] TEXT NOT NULL, 
[OrderDependentOperationSequence] TEXT NOT NULL, 
[ErrorContent] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ActiveContainerName { get; set; }
		// private string _unmodified_ActiveContainerName;

		[Column]
		public string OwnerPrefix { get; set; }
		// private string _unmodified_OwnerPrefix;

		[Column]
		public long CurrentRetryCount { get; set; }
		// private long _unmodified_CurrentRetryCount;

		[Column]
		public OperationRequest SingleOperation { get; set; }
		// private OperationRequest _unmodified_SingleOperation;

		[Column]
		public OperationRequestCollection OrderDependentOperationSequence { get; set; }
		// private OperationRequestCollection _unmodified_OrderDependentOperationSequence;

		[Column]
		public SystemError ErrorContent { get; set; }
		// private SystemError _unmodified_ErrorContent;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ActiveContainerName == null)
				ActiveContainerName = string.Empty;
			if(OwnerPrefix == null)
				OwnerPrefix = string.Empty;
		}
	}
    [Table(Name = "OperationRequest")]
	public class OperationRequest : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS OperationRequest(
[ID] TEXT NOT NULL PRIMARY KEY, 
[SubscriberNotification] TEXT NOT NULL, 
[SubscriptionChainRequest] TEXT NOT NULL, 
[UpdateWebContentOperation] TEXT NOT NULL, 
[RefreshDefaultViewsOperation] TEXT NOT NULL, 
[DeleteEntireOwner] TEXT NOT NULL, 
[DeleteOwnerContent] TEXT NOT NULL, 
[PublishWebContent] TEXT NOT NULL, 
[ProcessIDToExecute] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public Subscription SubscriberNotification { get; set; }
		// private Subscription _unmodified_SubscriberNotification;

		[Column]
		public SubscriptionChainRequestMessage SubscriptionChainRequest { get; set; }
		// private SubscriptionChainRequestMessage _unmodified_SubscriptionChainRequest;

		[Column]
		public UpdateWebContentOperation UpdateWebContentOperation { get; set; }
		// private UpdateWebContentOperation _unmodified_UpdateWebContentOperation;

		[Column]
		public RefreshDefaultViewsOperation RefreshDefaultViewsOperation { get; set; }
		// private RefreshDefaultViewsOperation _unmodified_RefreshDefaultViewsOperation;

		[Column]
		public DeleteEntireOwnerOperation DeleteEntireOwner { get; set; }
		// private DeleteEntireOwnerOperation _unmodified_DeleteEntireOwner;

		[Column]
		public DeleteOwnerContentOperation DeleteOwnerContent { get; set; }
		// private DeleteOwnerContentOperation _unmodified_DeleteOwnerContent;

		[Column]
		public PublishWebContentOperation PublishWebContent { get; set; }
		// private PublishWebContentOperation _unmodified_PublishWebContent;

		[Column]
		public string ProcessIDToExecute { get; set; }
		// private string _unmodified_ProcessIDToExecute;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ProcessIDToExecute == null)
				ProcessIDToExecute = string.Empty;
		}
	}
    [Table(Name = "SubscriptionChainRequestMessage")]
	public class SubscriptionChainRequestMessage : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS SubscriptionChainRequestMessage(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ContentItemID] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ContentItemID { get; set; }
		// private string _unmodified_ContentItemID;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ContentItemID == null)
				ContentItemID = string.Empty;
		}
	}
    [Table(Name = "SubscriptionChainRequestContent")]
	public class SubscriptionChainRequestContent : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS SubscriptionChainRequestContent(
[ID] TEXT NOT NULL PRIMARY KEY, 
[SubmitTime] TEXT NOT NULL, 
[ProcessingStartTime] TEXT NOT NULL, 
[ProcessingEndTimeInformationObjects] TEXT NOT NULL, 
[ProcessingEndTimeWebTemplatesRendering] TEXT NOT NULL, 
[ProcessingEndTime] TEXT NOT NULL, 
[SubscriptionTargetCollection] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public DateTime SubmitTime { get; set; }
		// private DateTime _unmodified_SubmitTime;

		[Column]
		public DateTime ProcessingStartTime { get; set; }
		// private DateTime _unmodified_ProcessingStartTime;

		[Column]
		public DateTime ProcessingEndTimeInformationObjects { get; set; }
		// private DateTime _unmodified_ProcessingEndTimeInformationObjects;

		[Column]
		public DateTime ProcessingEndTimeWebTemplatesRendering { get; set; }
		// private DateTime _unmodified_ProcessingEndTimeWebTemplatesRendering;

		[Column]
		public DateTime ProcessingEndTime { get; set; }
		// private DateTime _unmodified_ProcessingEndTime;

		[Column]
		public SubscriptionTargetCollection SubscriptionTargetCollection { get; set; }
		// private SubscriptionTargetCollection _unmodified_SubscriptionTargetCollection;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
    [Table(Name = "SubscriptionTarget")]
	public class SubscriptionTarget : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS SubscriptionTarget(
[ID] TEXT NOT NULL PRIMARY KEY, 
[BlobLocation] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string BlobLocation { get; set; }
		// private string _unmodified_BlobLocation;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(BlobLocation == null)
				BlobLocation = string.Empty;
		}
	}
    [Table(Name = "DeleteEntireOwnerOperation")]
	public class DeleteEntireOwnerOperation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS DeleteEntireOwnerOperation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ContainerName] TEXT NOT NULL, 
[LocationPrefix] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ContainerName { get; set; }
		// private string _unmodified_ContainerName;

		[Column]
		public string LocationPrefix { get; set; }
		// private string _unmodified_LocationPrefix;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ContainerName == null)
				ContainerName = string.Empty;
			if(LocationPrefix == null)
				LocationPrefix = string.Empty;
		}
	}
    [Table(Name = "DeleteOwnerContentOperation")]
	public class DeleteOwnerContentOperation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS DeleteOwnerContentOperation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ContainerName] TEXT NOT NULL, 
[LocationPrefix] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ContainerName { get; set; }
		// private string _unmodified_ContainerName;

		[Column]
		public string LocationPrefix { get; set; }
		// private string _unmodified_LocationPrefix;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ContainerName == null)
				ContainerName = string.Empty;
			if(LocationPrefix == null)
				LocationPrefix = string.Empty;
		}
	}
    [Table(Name = "SystemError")]
	public class SystemError : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS SystemError(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ErrorTitle] TEXT NOT NULL, 
[OccurredAt] TEXT NOT NULL, 
[SystemErrorItems] TEXT NOT NULL, 
[MessageContent] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ErrorTitle { get; set; }
		// private string _unmodified_ErrorTitle;

		[Column]
		public DateTime OccurredAt { get; set; }
		// private DateTime _unmodified_OccurredAt;

		[Column]
		public SystemErrorItemCollection SystemErrorItems { get; set; }
		// private SystemErrorItemCollection _unmodified_SystemErrorItems;

		[Column]
		public QueueEnvelope MessageContent { get; set; }
		// private QueueEnvelope _unmodified_MessageContent;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ErrorTitle == null)
				ErrorTitle = string.Empty;
		}
	}
    [Table(Name = "SystemErrorItem")]
	public class SystemErrorItem : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS SystemErrorItem(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ShortDescription] TEXT NOT NULL, 
[LongDescription] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ShortDescription { get; set; }
		// private string _unmodified_ShortDescription;

		[Column]
		public string LongDescription { get; set; }
		// private string _unmodified_LongDescription;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ShortDescription == null)
				ShortDescription = string.Empty;
			if(LongDescription == null)
				LongDescription = string.Empty;
		}
	}
    [Table(Name = "InformationSource")]
	public class InformationSource : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS InformationSource(
[ID] TEXT NOT NULL PRIMARY KEY, 
[SourceName] TEXT NOT NULL, 
[SourceLocation] TEXT NOT NULL, 
[SourceType] TEXT NOT NULL, 
[IsDynamic] INTEGER NOT NULL, 
[SourceInformationObjectType] TEXT NOT NULL, 
[SourceETag] TEXT NOT NULL, 
[SourceMD5] TEXT NOT NULL, 
[SourceLastModified] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string SourceName { get; set; }
		// private string _unmodified_SourceName;

		[Column]
		public string SourceLocation { get; set; }
		// private string _unmodified_SourceLocation;

		[Column]
		public string SourceType { get; set; }
		// private string _unmodified_SourceType;

		[Column]
		public bool IsDynamic { get; set; }
		// private bool _unmodified_IsDynamic;

		[Column]
		public string SourceInformationObjectType { get; set; }
		// private string _unmodified_SourceInformationObjectType;

		[Column]
		public string SourceETag { get; set; }
		// private string _unmodified_SourceETag;

		[Column]
		public string SourceMD5 { get; set; }
		// private string _unmodified_SourceMD5;

		[Column]
		public DateTime SourceLastModified { get; set; }
		// private DateTime _unmodified_SourceLastModified;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(SourceName == null)
				SourceName = string.Empty;
			if(SourceLocation == null)
				SourceLocation = string.Empty;
			if(SourceType == null)
				SourceType = string.Empty;
			if(SourceInformationObjectType == null)
				SourceInformationObjectType = string.Empty;
			if(SourceETag == null)
				SourceETag = string.Empty;
			if(SourceMD5 == null)
				SourceMD5 = string.Empty;
		}
	}
    [Table(Name = "RefreshDefaultViewsOperation")]
	public class RefreshDefaultViewsOperation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS RefreshDefaultViewsOperation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[ViewLocation] TEXT NOT NULL, 
[TypeNameToRefresh] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string ViewLocation { get; set; }
		// private string _unmodified_ViewLocation;

		[Column]
		public string TypeNameToRefresh { get; set; }
		// private string _unmodified_TypeNameToRefresh;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(ViewLocation == null)
				ViewLocation = string.Empty;
			if(TypeNameToRefresh == null)
				TypeNameToRefresh = string.Empty;
		}
	}
    [Table(Name = "UpdateWebContentOperation")]
	public class UpdateWebContentOperation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS UpdateWebContentOperation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[SourceContainerName] TEXT NOT NULL, 
[SourcePathRoot] TEXT NOT NULL, 
[TargetContainerName] TEXT NOT NULL, 
[TargetPathRoot] TEXT NOT NULL, 
[RenderWhileSync] INTEGER NOT NULL, 
[Handlers] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string SourceContainerName { get; set; }
		// private string _unmodified_SourceContainerName;

		[Column]
		public string SourcePathRoot { get; set; }
		// private string _unmodified_SourcePathRoot;

		[Column]
		public string TargetContainerName { get; set; }
		// private string _unmodified_TargetContainerName;

		[Column]
		public string TargetPathRoot { get; set; }
		// private string _unmodified_TargetPathRoot;

		[Column]
		public bool RenderWhileSync { get; set; }
		// private bool _unmodified_RenderWhileSync;

		[Column]
		public UpdateWebContentHandlerCollection Handlers { get; set; }
		// private UpdateWebContentHandlerCollection _unmodified_Handlers;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(SourceContainerName == null)
				SourceContainerName = string.Empty;
			if(SourcePathRoot == null)
				SourcePathRoot = string.Empty;
			if(TargetContainerName == null)
				TargetContainerName = string.Empty;
			if(TargetPathRoot == null)
				TargetPathRoot = string.Empty;
		}
	}
    [Table(Name = "UpdateWebContentHandlerItem")]
	public class UpdateWebContentHandlerItem : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS UpdateWebContentHandlerItem(
[ID] TEXT NOT NULL PRIMARY KEY, 
[InformationTypeName] TEXT NOT NULL, 
[OptionName] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string InformationTypeName { get; set; }
		// private string _unmodified_InformationTypeName;

		[Column]
		public string OptionName { get; set; }
		// private string _unmodified_OptionName;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(InformationTypeName == null)
				InformationTypeName = string.Empty;
			if(OptionName == null)
				OptionName = string.Empty;
		}
	}
    [Table(Name = "PublishWebContentOperation")]
	public class PublishWebContentOperation : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS PublishWebContentOperation(
[ID] TEXT NOT NULL PRIMARY KEY, 
[SourceContainerName] TEXT NOT NULL, 
[SourcePathRoot] TEXT NOT NULL, 
[SourceOwner] TEXT NOT NULL, 
[TargetContainerName] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string SourceContainerName { get; set; }
		// private string _unmodified_SourceContainerName;

		[Column]
		public string SourcePathRoot { get; set; }
		// private string _unmodified_SourcePathRoot;

		[Column]
		public string SourceOwner { get; set; }
		// private string _unmodified_SourceOwner;

		[Column]
		public string TargetContainerName { get; set; }
		// private string _unmodified_TargetContainerName;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(SourceContainerName == null)
				SourceContainerName = string.Empty;
			if(SourcePathRoot == null)
				SourcePathRoot = string.Empty;
			if(SourceOwner == null)
				SourceOwner = string.Empty;
			if(TargetContainerName == null)
				TargetContainerName = string.Empty;
		}
	}
    [Table(Name = "SubscriberInput")]
	public class SubscriberInput : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS SubscriberInput(
[ID] TEXT NOT NULL PRIMARY KEY, 
[InputRelativeLocation] TEXT NOT NULL, 
[InformationObjectName] TEXT NOT NULL, 
[InformationItemName] TEXT NOT NULL, 
[SubscriberRelativeLocation] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string InputRelativeLocation { get; set; }
		// private string _unmodified_InputRelativeLocation;

		[Column]
		public string InformationObjectName { get; set; }
		// private string _unmodified_InformationObjectName;

		[Column]
		public string InformationItemName { get; set; }
		// private string _unmodified_InformationItemName;

		[Column]
		public string SubscriberRelativeLocation { get; set; }
		// private string _unmodified_SubscriberRelativeLocation;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(InputRelativeLocation == null)
				InputRelativeLocation = string.Empty;
			if(InformationObjectName == null)
				InformationObjectName = string.Empty;
			if(InformationItemName == null)
				InformationItemName = string.Empty;
			if(SubscriberRelativeLocation == null)
				SubscriberRelativeLocation = string.Empty;
		}
	}
    [Table(Name = "Monitor")]
	public class Monitor : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS Monitor(
[ID] TEXT NOT NULL PRIMARY KEY, 
[TargetObjectName] TEXT NOT NULL, 
[TargetItemName] TEXT NOT NULL, 
[MonitoringUtcTimeStampToStart] TEXT NOT NULL, 
[MonitoringCycleFrequencyUnit] TEXT NOT NULL, 
[MonitoringCycleEveryXthOfUnit] INTEGER NOT NULL, 
[CustomMonitoringCycleOperationName] TEXT NOT NULL, 
[OperationActionName] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public string TargetObjectName { get; set; }
		// private string _unmodified_TargetObjectName;

		[Column]
		public string TargetItemName { get; set; }
		// private string _unmodified_TargetItemName;

		[Column]
		public DateTime MonitoringUtcTimeStampToStart { get; set; }
		// private DateTime _unmodified_MonitoringUtcTimeStampToStart;

		[Column]
		public string MonitoringCycleFrequencyUnit { get; set; }
		// private string _unmodified_MonitoringCycleFrequencyUnit;

		[Column]
		public long MonitoringCycleEveryXthOfUnit { get; set; }
		// private long _unmodified_MonitoringCycleEveryXthOfUnit;

		[Column]
		public string CustomMonitoringCycleOperationName { get; set; }
		// private string _unmodified_CustomMonitoringCycleOperationName;

		[Column]
		public string OperationActionName { get; set; }
		// private string _unmodified_OperationActionName;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(TargetObjectName == null)
				TargetObjectName = string.Empty;
			if(TargetItemName == null)
				TargetItemName = string.Empty;
			if(MonitoringCycleFrequencyUnit == null)
				MonitoringCycleFrequencyUnit = string.Empty;
			if(CustomMonitoringCycleOperationName == null)
				CustomMonitoringCycleOperationName = string.Empty;
			if(OperationActionName == null)
				OperationActionName = string.Empty;
		}
	}
    [Table(Name = "IconTitleDescription")]
	public class IconTitleDescription : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS IconTitleDescription(
[ID] TEXT NOT NULL PRIMARY KEY, 
[Icon] BLOB NOT NULL, 
[Title] TEXT NOT NULL, 
[Description] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public byte[] Icon { get; set; }
		// private byte[] _unmodified_Icon;

		[Column]
		public string Title { get; set; }
		// private string _unmodified_Title;

		[Column]
		public string Description { get; set; }
		// private string _unmodified_Description;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
			if(Title == null)
				Title = string.Empty;
			if(Description == null)
				Description = string.Empty;
		}
	}
    [Table(Name = "AboutAGIApplications")]
	public class AboutAGIApplications : ITheBallDataContextStorable
	{
        public static string GetCreateTableSQL()
        {
            return
                @"
CREATE TABLE IF NOT EXISTS AboutAGIApplications(
[ID] TEXT NOT NULL PRIMARY KEY, 
[BuiltForAnybody] TEXT NOT NULL, 
[ForAllPeople] TEXT NOT NULL
)";
        }


		[Column(IsPrimaryKey = true)]
		public string ID { get; set; }


		[Column]
		public IconTitleDescription BuiltForAnybody { get; set; }
		// private IconTitleDescription _unmodified_BuiltForAnybody;

		[Column]
		public IconTitleDescription ForAllPeople { get; set; }
		// private IconTitleDescription _unmodified_ForAllPeople;
        public void PrepareForStoring(bool isInitialInsert)
        {
		
		}
	}
 } 
