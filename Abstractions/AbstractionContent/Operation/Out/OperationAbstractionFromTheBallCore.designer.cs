 

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

		namespace TheBall.CORE { 
				public class ImportAccountFromOIPLegacyParameters 
		{
				public AaltoGlobalImpact.OIP.TBRLoginRoot LegacyLogin ;
				}
		
		public class ImportAccountFromOIPLegacy 
		{
				private static void PrepareParameters(ImportAccountFromOIPLegacyParameters parameters)
		{
					}
				public static async Task<ImportAccountFromOIPLegacyReturnValue> ExecuteAsync(ImportAccountFromOIPLegacyParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.TBRAccountRoot LegacyAccountRoot =  await ImportAccountFromOIPLegacyImplementation.GetTarget_LegacyAccountRootAsync(parameters.LegacyLogin);	
				AaltoGlobalImpact.OIP.TBAccount LegacyAccount = ImportAccountFromOIPLegacyImplementation.GetTarget_LegacyAccount(LegacyAccountRoot);	
				Account Account =  await ImportAccountFromOIPLegacyImplementation.GetTarget_AccountAsync(LegacyAccount);	
				 await ImportAccountFromOIPLegacyImplementation.ExecuteMethod_AddMissingLoginsAsync(Account, LegacyAccount);		
				 await ImportAccountFromOIPLegacyImplementation.ExecuteMethod_AddMissingEmailsAsync(Account, LegacyAccount);		
				 await ImportAccountFromOIPLegacyImplementation.ExecuteMethod_StoreObjectAsync(Account);		
				ImportAccountFromOIPLegacyReturnValue returnValue = ImportAccountFromOIPLegacyImplementation.Get_ReturnValue(Account);
		return returnValue;
				}
				}
				public class ImportAccountFromOIPLegacyReturnValue 
		{
				public Account ImportedAccount ;
				}
				public class EnsureLoginParameters 
		{
				public string LoginURL ;
				public string AccountID ;
				}
		
		public class EnsureLogin 
		{
				private static void PrepareParameters(EnsureLoginParameters parameters)
		{
					}
				public static async Task<EnsureLoginReturnValue> ExecuteAsync(EnsureLoginParameters parameters)
		{
						PrepareParameters(parameters);
					Login Login =  await EnsureLoginImplementation.GetTarget_LoginAsync(parameters.LoginURL);	
				EnsureLoginImplementation.ExecuteMethod_ValidateExistingAccountIDToMatch(parameters.AccountID, Login);		
				EnsureLoginReturnValue returnValue = EnsureLoginImplementation.Get_ReturnValue(Login);
		return returnValue;
				}
				}
				public class EnsureLoginReturnValue 
		{
				public Login EnsuredLogin ;
				}
				public class ActivateEmailValidationParameters 
		{
				public Email Email ;
				public bool SendValidationCodeIfReissued ;
				public bool ResendValidationCode ;
				}
		
		public class ActivateEmailValidation 
		{
				private static void PrepareParameters(ActivateEmailValidationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(ActivateEmailValidationParameters parameters)
		{
						PrepareParameters(parameters);
					bool ProcessEmailValidationActivationOutput =  await ActivateEmailValidationImplementation.ExecuteMethod_ProcessEmailValidationActivationAsync(parameters.Email, parameters.SendValidationCodeIfReissued, parameters.ResendValidationCode);		
				 await ActivateEmailValidationImplementation.ExecuteMethod_StoreEmailIfChangedAsync(parameters.Email, ProcessEmailValidationActivationOutput);		
				}
				}
				public class EnsureEmailParameters 
		{
				public string EmailAddress ;
				public string AccountID ;
				}
		
		public class EnsureEmail 
		{
				private static void PrepareParameters(EnsureEmailParameters parameters)
		{
					}
				public static async Task<EnsureEmailReturnValue> ExecuteAsync(EnsureEmailParameters parameters)
		{
						PrepareParameters(parameters);
					EnsureEmailImplementation.ExecuteMethod_ValidateEmailAddress(parameters.EmailAddress);		
				Email Email =  await EnsureEmailImplementation.GetTarget_EmailAsync(parameters.EmailAddress);	
				EnsureEmailImplementation.ExecuteMethod_ValidateExistingAccountIDToMatch(parameters.AccountID, Email);		
				EnsureEmailReturnValue returnValue = EnsureEmailImplementation.Get_ReturnValue(Email);
		return returnValue;
				}
				}
				public class EnsureEmailReturnValue 
		{
				public Email EnsuredEmail ;
				}
				public class EnsureAccountParameters 
		{
				public string LoginUrl ;
				public string EmailAddress ;
				}
		
		public class EnsureAccount 
		{
				private static void PrepareParameters(EnsureAccountParameters parameters)
		{
					}
				public static async Task<EnsureAccountReturnValue> ExecuteAsync(EnsureAccountParameters parameters)
		{
						PrepareParameters(parameters);
					Email EnsureEmailOutput;
		{ // Local block to allow local naming
			EnsureEmailParameters operationParameters = EnsureAccountImplementation.EnsureEmail_GetParameters(parameters.EmailAddress);
			var operationReturnValue =  await EnsureEmail.ExecuteAsync(operationParameters);
			EnsureEmailOutput = EnsureAccountImplementation.EnsureEmail_GetOutput(operationReturnValue, parameters.EmailAddress);						
		} // Local block closing
				Account ExistingAccount =  await EnsureAccountImplementation.GetTarget_ExistingAccountAsync(EnsureEmailOutput);	
				Account ResultingAccount =  await EnsureAccountImplementation.GetTarget_ResultingAccountAsync(parameters.LoginUrl, EnsureEmailOutput, ExistingAccount);	
				EnsureAccountReturnValue returnValue = EnsureAccountImplementation.Get_ReturnValue(ResultingAccount);
		return returnValue;
				}
				}
				public class EnsureAccountReturnValue 
		{
				public Account EnsuredAccount ;
				}
				public class SetAccountClientMetadataParameters 
		{
				public INT.AccountMetadata MetadataInfo ;
				}
		
		public class SetAccountClientMetadata 
		{
				private static void PrepareParameters(SetAccountClientMetadataParameters parameters)
		{
					}
				public static async Task ExecuteAsync(SetAccountClientMetadataParameters parameters)
		{
						PrepareParameters(parameters);
					Account Account =  await SetAccountClientMetadataImplementation.GetTarget_AccountAsync(parameters.MetadataInfo);	
				string MetadataAsJSONString = SetAccountClientMetadataImplementation.GetTarget_MetadataAsJSONString(parameters.MetadataInfo);	
				SetAccountClientMetadataImplementation.ExecuteMethod_SetAccountClientMetadata(Account, MetadataAsJSONString);		
				 await SetAccountClientMetadataImplementation.ExecuteMethod_StoreObjectAsync(Account);		
				}
				}
				public class SetAccountServerMetadataParameters 
		{
				public INT.AccountMetadata MetadataInfo ;
				}
		
		public class SetAccountServerMetadata 
		{
				private static void PrepareParameters(SetAccountServerMetadataParameters parameters)
		{
					}
				public static async Task ExecuteAsync(SetAccountServerMetadataParameters parameters)
		{
						PrepareParameters(parameters);
					Account Account =  await SetAccountServerMetadataImplementation.GetTarget_AccountAsync(parameters.MetadataInfo);	
				string MetadataAsJSONString = SetAccountServerMetadataImplementation.GetTarget_MetadataAsJSONString(parameters.MetadataInfo);	
				SetAccountServerMetadataImplementation.ExecuteMethod_SetAccountServerMetadata(Account, MetadataAsJSONString);		
				 await SetAccountServerMetadataImplementation.ExecuteMethod_StoreObjectAsync(Account);		
				}
				}
				public class CreateAccountParameters 
		{
				public string AccountID ;
				public string LoginUrl ;
				public string EmailAddress ;
				}
		
		public class CreateAccount 
		{
				private static void PrepareParameters(CreateAccountParameters parameters)
		{
					}
				public static async Task<CreateAccountReturnValue> ExecuteAsync(CreateAccountParameters parameters)
		{
						PrepareParameters(parameters);
					Account AccountToBeCreated = CreateAccountImplementation.GetTarget_AccountToBeCreated(parameters.AccountID);	
				Login LoginOutput;
		{ // Local block to allow local naming
			EnsureLoginParameters operationParameters = CreateAccountImplementation.Login_GetParameters(parameters.LoginUrl, AccountToBeCreated);
			var operationReturnValue =  await EnsureLogin.ExecuteAsync(operationParameters);
			LoginOutput = CreateAccountImplementation.Login_GetOutput(operationReturnValue, parameters.LoginUrl, AccountToBeCreated);						
		} // Local block closing
				Email EmailOutput;
		{ // Local block to allow local naming
			EnsureEmailParameters operationParameters = CreateAccountImplementation.Email_GetParameters(parameters.EmailAddress, AccountToBeCreated);
			var operationReturnValue =  await EnsureEmail.ExecuteAsync(operationParameters);
			EmailOutput = CreateAccountImplementation.Email_GetOutput(operationReturnValue, parameters.EmailAddress, AccountToBeCreated);						
		} // Local block closing
				CreateAccountImplementation.ExecuteMethod_ConnectLoginAndAccount(AccountToBeCreated, LoginOutput);		
				CreateAccountImplementation.ExecuteMethod_ConnectEmailAndAccount(AccountToBeCreated, EmailOutput);		
				 await CreateAccountImplementation.ExecuteMethod_StoreObjectsAsync(AccountToBeCreated, LoginOutput, EmailOutput);		
				 await CreateAccountImplementation.ExecuteMethod_CopyAccountTemplatesAsync(AccountToBeCreated);		
				CreateAccountReturnValue returnValue = CreateAccountImplementation.Get_ReturnValue(AccountToBeCreated);
		return returnValue;
				}
				}
				public class CreateAccountReturnValue 
		{
				public Account CreatedAccount ;
				}
				public class CreateGroupParameters 
		{
				public string GroupID ;
				}
		
		public class CreateGroup 
		{
				private static void PrepareParameters(CreateGroupParameters parameters)
		{
					}
				public static async Task ExecuteAsync(CreateGroupParameters parameters)
		{
						PrepareParameters(parameters);
					Group GroupToBeCreated = CreateGroupImplementation.GetTarget_GroupToBeCreated(parameters.GroupID);	
				 await CreateGroupImplementation.ExecuteMethod_StoreObjectAsync(GroupToBeCreated);		
				}
				}
				public class SetGroupMembershipParameters 
		{
				public string GroupID ;
				public string AccountID ;
				public string Role ;
				}
		
		public class SetGroupMembership 
		{
				private static void PrepareParameters(SetGroupMembershipParameters parameters)
		{
					}
				public static async Task ExecuteAsync(SetGroupMembershipParameters parameters)
		{
						PrepareParameters(parameters);
					Account Account =  await SetGroupMembershipImplementation.GetTarget_AccountAsync(parameters.AccountID);	
				Group Group =  await SetGroupMembershipImplementation.GetTarget_GroupAsync(parameters.GroupID);	
				GroupMembership GroupMembership =  await SetGroupMembershipImplementation.GetTarget_GroupMembershipAsync(parameters.AccountID, parameters.GroupID);	
				SetGroupMembershipImplementation.ExecuteMethod_SetRoleToMembership(parameters.Role, GroupMembership);		
				SetGroupMembershipImplementation.ExecuteMethod_SetMembershipToGroup(GroupMembership, Group);		
				SetGroupMembershipImplementation.ExecuteMethod_SetMembershipToAccount(GroupMembership, Account);		
				 await SetGroupMembershipImplementation.ExecuteMethod_StoreObjectsAsync(GroupMembership, Group, Account);		
				
		{ // Local block to allow local naming
			UpdateAccountMembershipStatusesParameters operationParameters = SetGroupMembershipImplementation.UpdateAccountStatuses_GetParameters(parameters.AccountID, parameters.GroupID);
			 await UpdateAccountMembershipStatuses.ExecuteAsync(operationParameters);
									
		} // Local block closing
				
		{ // Local block to allow local naming
			UpdateGroupMembershipStatusesParameters operationParameters = SetGroupMembershipImplementation.UpdateGroupStatuses_GetParameters(parameters.GroupID, parameters.AccountID);
			 await UpdateGroupMembershipStatuses.ExecuteAsync(operationParameters);
									
		} // Local block closing
				}
				}
				public class UpdateAccountMembershipStatusesParameters 
		{
				public string AccountID ;
				public string GroupID ;
				}
		
		public class UpdateAccountMembershipStatuses 
		{
				private static void PrepareParameters(UpdateAccountMembershipStatusesParameters parameters)
		{
					}
				public static async Task ExecuteAsync(UpdateAccountMembershipStatusesParameters parameters)
		{
						PrepareParameters(parameters);
					Account Account =  await UpdateAccountMembershipStatusesImplementation.GetTarget_AccountAsync(parameters.AccountID);	
				GroupMembership[] Memberships =  await UpdateAccountMembershipStatusesImplementation.GetTarget_MembershipsAsync(Account);	
				TheBall.Interface.INT.AccountMembershipData AccountMembershipData =  await UpdateAccountMembershipStatusesImplementation.GetTarget_AccountMembershipDataAsync(Account);	
				 await UpdateAccountMembershipStatusesImplementation.ExecuteMethod_UpdateMembershipDataAsync(parameters.GroupID, AccountMembershipData, Memberships);		
				 await UpdateAccountMembershipStatusesImplementation.ExecuteMethod_StoreObjectAsync(Account, AccountMembershipData);		
				}
				}
				public class UpdateGroupMembershipStatusesParameters 
		{
				public string GroupID ;
				public string AccountID ;
				}
		
		public class UpdateGroupMembershipStatuses 
		{
				private static void PrepareParameters(UpdateGroupMembershipStatusesParameters parameters)
		{
					}
				public static async Task ExecuteAsync(UpdateGroupMembershipStatusesParameters parameters)
		{
						PrepareParameters(parameters);
					Group Group =  await UpdateGroupMembershipStatusesImplementation.GetTarget_GroupAsync(parameters.GroupID);	
				GroupMembership[] Memberships =  await UpdateGroupMembershipStatusesImplementation.GetTarget_MembershipsAsync(Group);	
				TheBall.Interface.INT.GroupMembershipData GroupMembershipData =  await UpdateGroupMembershipStatusesImplementation.GetTarget_GroupMembershipDataAsync(Group);	
				 await UpdateGroupMembershipStatusesImplementation.ExecuteMethod_UpdateMembershipDataAsync(parameters.AccountID, GroupMembershipData, Memberships);		
				 await UpdateGroupMembershipStatusesImplementation.ExecuteMethod_StoreObjectAsync(Group, GroupMembershipData);		
				}
				}
				public class RemoveGroupMembershipParameters 
		{
				public string GroupID ;
				public string AccountID ;
				}
		
		public class RemoveGroupMembership 
		{
				private static void PrepareParameters(RemoveGroupMembershipParameters parameters)
		{
					}
				public static async Task ExecuteAsync(RemoveGroupMembershipParameters parameters)
		{
						PrepareParameters(parameters);
					Account Account =  await RemoveGroupMembershipImplementation.GetTarget_AccountAsync(parameters.AccountID);	
				Group Group =  await RemoveGroupMembershipImplementation.GetTarget_GroupAsync(parameters.GroupID);	
				GroupMembership GroupMembership =  await RemoveGroupMembershipImplementation.GetTarget_GroupMembershipAsync(parameters.AccountID, parameters.GroupID);	
				RemoveGroupMembershipImplementation.ExecuteMethod_RemoveMembershipFromGroup(GroupMembership, Group);		
				RemoveGroupMembershipImplementation.ExecuteMethod_RemoveMembershipFromAccount(GroupMembership, Account);		
				 await RemoveGroupMembershipImplementation.ExecuteMethod_StoreObjectsAsync(Group, Account);		
				 await RemoveGroupMembershipImplementation.ExecuteMethod_DeleteObjectAsync(GroupMembership);		
				}
				}
				public class AddLoginToAccountParameters 
		{
				public string AccountID ;
				public string LoginUrl ;
				}
		
		public class AddLoginToAccount 
		{
				private static void PrepareParameters(AddLoginToAccountParameters parameters)
		{
					}
				public static async Task ExecuteAsync(AddLoginToAccountParameters parameters)
		{
						PrepareParameters(parameters);
					Account Account =  await AddLoginToAccountImplementation.GetTarget_AccountAsync(parameters.AccountID);	
				Login Login =  await AddLoginToAccountImplementation.GetTarget_LoginAsync(parameters.LoginUrl);	
				AddLoginToAccountImplementation.ExecuteMethod_AddLoginToAccount(Account, Login);		
				AddLoginToAccountImplementation.ExecuteMethod_AddAccountToLogin(Login, Account);		
				 await AddLoginToAccountImplementation.ExecuteMethod_StoreObjectsAsync(Account, Login);		
				}
				}
				public class RemoveLoginParameters 
		{
				public string LoginUrl ;
				}
		
		public class RemoveLogin 
		{
				private static void PrepareParameters(RemoveLoginParameters parameters)
		{
					}
				public static async Task ExecuteAsync(RemoveLoginParameters parameters)
		{
						PrepareParameters(parameters);
					Login Login =  await RemoveLoginImplementation.GetTarget_LoginAsync(parameters.LoginUrl);	
				Account Account =  await RemoveLoginImplementation.GetTarget_AccountAsync(Login);	
				RemoveLoginImplementation.ExecuteMethod_RemoveLoginFromAccount(Account, Login);		
				 await RemoveLoginImplementation.ExecuteMethod_DeleteObjectAsync(Login);		
				 await RemoveLoginImplementation.ExecuteMethod_StoreObjectAsync(Account);		
				}
				}
				public class AddEmailToAccountParameters 
		{
				public string AccountID ;
				public string EmailAddress ;
				}
		
		public class AddEmailToAccount 
		{
				private static void PrepareParameters(AddEmailToAccountParameters parameters)
		{
					}
				public static async Task ExecuteAsync(AddEmailToAccountParameters parameters)
		{
						PrepareParameters(parameters);
					Account Account =  await AddEmailToAccountImplementation.GetTarget_AccountAsync(parameters.AccountID);	
				Email Email =  await AddEmailToAccountImplementation.GetTarget_EmailAsync(parameters.EmailAddress);	
				AddEmailToAccountImplementation.ExecuteMethod_AddEmailToAccount(Account, Email);		
				AddEmailToAccountImplementation.ExecuteMethod_AddAccountToEmail(Email, Account);		
				 await AddEmailToAccountImplementation.ExecuteMethod_StoreObjectsAsync(Account, Email);		
				}
				}
				public class RemoveEmailParameters 
		{
				public string EmailAddress ;
				}
		
		public class RemoveEmail 
		{
				private static void PrepareParameters(RemoveEmailParameters parameters)
		{
					}
				public static async Task ExecuteAsync(RemoveEmailParameters parameters)
		{
						PrepareParameters(parameters);
					Email Email =  await RemoveEmailImplementation.GetTarget_EmailAsync(parameters.EmailAddress);	
				Account Account =  await RemoveEmailImplementation.GetTarget_AccountAsync(Email);	
				RemoveEmailImplementation.ExecuteMethod_RemoveEmailFromAccount(Account, Email);		
				 await RemoveEmailImplementation.ExecuteMethod_DeleteObjectAsync(Email);		
				 await RemoveEmailImplementation.ExecuteMethod_StoreObjectAsync(Account);		
				}
				}
				public class ObtainSystemProcessLockParameters 
		{
				public IContainerOwner Owner ;
				public DateTime LatestEntryTime ;
				public int AmountOfEntries ;
				}
		
		public class ObtainSystemProcessLock 
		{
				private static void PrepareParameters(ObtainSystemProcessLockParameters parameters)
		{
					}
				public static async Task<ObtainSystemProcessLockReturnValue> ExecuteAsync(ObtainSystemProcessLockParameters parameters)
		{
						PrepareParameters(parameters);
					string LockFileContent = ObtainSystemProcessLockImplementation.GetTarget_LockFileContent(parameters.LatestEntryTime, parameters.AmountOfEntries);	
				string OwnerLockFileName = ObtainSystemProcessLockImplementation.GetTarget_OwnerLockFileName();	
				string SystemOwnerLockFileName = ObtainSystemProcessLockImplementation.GetTarget_SystemOwnerLockFileName(parameters.Owner, parameters.LatestEntryTime);	
				string ObtainOwnerLevelLockOutput =  await ObtainSystemProcessLockImplementation.ExecuteMethod_ObtainOwnerLevelLockAsync(parameters.Owner, OwnerLockFileName, LockFileContent);		
				 await ObtainSystemProcessLockImplementation.ExecuteMethod_ReportSystemLockToMatchOwnerLockAsync(ObtainOwnerLevelLockOutput, SystemOwnerLockFileName, LockFileContent);		
				ObtainSystemProcessLockReturnValue returnValue = ObtainSystemProcessLockImplementation.Get_ReturnValue(ObtainOwnerLevelLockOutput);
		return returnValue;
				}
				}
				public class ObtainSystemProcessLockReturnValue 
		{
				public string ObtainedLockID ;
				}
				public class ReleaseSystemProcessLockParameters 
		{
				public IContainerOwner Owner ;
				public DateTime LatestEntryTime ;
				public string LockID ;
				}
		
		public class ReleaseSystemProcessLock 
		{
				private static void PrepareParameters(ReleaseSystemProcessLockParameters parameters)
		{
					}
				public static void Execute(ReleaseSystemProcessLockParameters parameters)
		{
						PrepareParameters(parameters);
					string OwnerLockFileName = ReleaseSystemProcessLockImplementation.GetTarget_OwnerLockFileName();	
				string SystemOwnerLockFileName = ReleaseSystemProcessLockImplementation.GetTarget_SystemOwnerLockFileName(parameters.Owner, parameters.LatestEntryTime);	
				ReleaseSystemProcessLockImplementation.ExecuteMethod_ReleaseOwnedOwnerLevelLock(parameters.Owner, parameters.LockID, OwnerLockFileName);		
				ReleaseSystemProcessLockImplementation.ExecuteMethod_ReleaseReportingSystemLock(SystemOwnerLockFileName);		
				}
				}
				public class CreateProcessParameters 
		{
				public string ProcessDescription ;
				public string ExecutingOperationName ;
				public SemanticInformationItem[] InitialArguments ;
				}
		
		public class CreateProcess 
		{
				private static void PrepareParameters(CreateProcessParameters parameters)
		{
					}
				public static async Task<CreateProcessReturnValue> ExecuteAsync(CreateProcessParameters parameters)
		{
						PrepareParameters(parameters);
					Process Process = CreateProcessImplementation.GetTarget_Process(parameters.ProcessDescription, parameters.ExecutingOperationName, parameters.InitialArguments);	
				ProcessContainer OwnerProcessContainer =  await CreateProcessImplementation.GetTarget_OwnerProcessContainerAsync();	
				 await CreateProcessImplementation.ExecuteMethod_AddProcessObjectToContainerAndStoreBothAsync(OwnerProcessContainer, Process);		
				CreateProcessReturnValue returnValue = CreateProcessImplementation.Get_ReturnValue(Process);
		return returnValue;
				}
				}
				public class CreateProcessReturnValue 
		{
				public Process CreatedProcess ;
				}
				public class DeleteProcessParameters 
		{
				public string ProcessID ;
				}
		
		public class DeleteProcess 
		{
				private static void PrepareParameters(DeleteProcessParameters parameters)
		{
					}
				public static async Task ExecuteAsync(DeleteProcessParameters parameters)
		{
						PrepareParameters(parameters);
					Process Process =  await DeleteProcessImplementation.GetTarget_ProcessAsync(parameters.ProcessID);	
				ProcessContainer OwnerProcessContainer =  await DeleteProcessImplementation.GetTarget_OwnerProcessContainerAsync();	
				 await DeleteProcessImplementation.ExecuteMethod_ObtainLockRemoveFromContainerAndDeleteProcessAsync(parameters.ProcessID, Process, OwnerProcessContainer);		
				}
				}
				public class ExecuteProcessParameters 
		{
				public string ProcessID ;
				}
		
		public class ExecuteProcess 
		{
				private static void PrepareParameters(ExecuteProcessParameters parameters)
		{
					}
				public static async Task ExecuteAsync(ExecuteProcessParameters parameters)
		{
						PrepareParameters(parameters);
					TheBall.CORE.Process Process =  await ExecuteProcessImplementation.GetTarget_ProcessAsync(parameters.ProcessID);	
				string ProcessLockLocation = ExecuteProcessImplementation.GetTarget_ProcessLockLocation(Process);	
				 await ExecuteProcessImplementation.ExecuteMethod_ExecuteAndStoreProcessWithLockAsync(ProcessLockLocation, Process);		
				}
				}
				public class SetObjectTreeValuesParameters 
		{
				public IInformationObject RootObject ;
				public NameValueCollection HttpFormData ;
				public Microsoft.AspNetCore.Http.IFormFileCollection HttpFileData ;
				}
		
		public class SetObjectTreeValues 
		{
				private static void PrepareParameters(SetObjectTreeValuesParameters parameters)
		{
					}
				public static async Task ExecuteAsync(SetObjectTreeValuesParameters parameters)
		{
						PrepareParameters(parameters);
					SetObjectTreeValuesImplementation.ExecuteMethod_CreateInternalObjects(parameters.RootObject, parameters.HttpFormData);		
				NameValueCollection FieldValues = SetObjectTreeValuesImplementation.GetTarget_FieldValues(parameters.RootObject, parameters.HttpFormData);	
				SetObjectTreeValuesImplementation.ExecuteMethod_DecodeEncodedRawHTMLValues(FieldValues);		
				NameValueCollection ObjectLinkValues = SetObjectTreeValuesImplementation.GetTarget_ObjectLinkValues(parameters.RootObject, parameters.HttpFormData);	
				Dictionary<string, MediaFileData> BinaryContentFiles = SetObjectTreeValuesImplementation.GetTarget_BinaryContentFiles(parameters.RootObject, parameters.HttpFormData, parameters.HttpFileData);	
				SetObjectTreeValuesImplementation.ExecuteMethod_AddEncodedFormDataToBinaryFiles(parameters.RootObject, parameters.HttpFormData, BinaryContentFiles);		
				SetObjectTreeValuesImplementation.ExecuteMethod_SetFieldValues(parameters.RootObject, FieldValues);		
				SetObjectTreeValuesImplementation.ExecuteMethod_SetObjectLinks(parameters.RootObject, ObjectLinkValues);		
				SetObjectTreeValuesImplementation.ExecuteMethod_SetBinaryContent(parameters.RootObject, BinaryContentFiles);		
				 await SetObjectTreeValuesImplementation.ExecuteMethod_StoreCompleteObjectAsync(parameters.RootObject);		
				}
				}
				public class CreateSpecifiedInformationObjectWithValuesParameters 
		{
				public IContainerOwner Owner ;
				public string ObjectDomainName ;
				public string ObjectName ;
				public NameValueCollection HttpFormData ;
				public Microsoft.AspNetCore.Http.IFormFileCollection HttpFileData ;
				}
		
		public class CreateSpecifiedInformationObjectWithValues 
		{
				private static void PrepareParameters(CreateSpecifiedInformationObjectWithValuesParameters parameters)
		{
					}
				public static async Task<CreateSpecifiedInformationObjectWithValuesReturnValue> ExecuteAsync(CreateSpecifiedInformationObjectWithValuesParameters parameters)
		{
						PrepareParameters(parameters);
					CreateSpecifiedInformationObjectWithValuesImplementation.ExecuteMethod_CatchInvalidDomains(parameters.ObjectDomainName);		
				IInformationObject CreatedObject = CreateSpecifiedInformationObjectWithValuesImplementation.GetTarget_CreatedObject(parameters.Owner, parameters.ObjectDomainName, parameters.ObjectName);	
				CreateSpecifiedInformationObjectWithValuesImplementation.ExecuteMethod_StoreInitialObject(CreatedObject);		
				
		{ // Local block to allow local naming
			SetObjectTreeValuesParameters operationParameters = CreateSpecifiedInformationObjectWithValuesImplementation.SetObjectValues_GetParameters(parameters.HttpFormData, parameters.HttpFileData, CreatedObject);
			 await SetObjectTreeValues.ExecuteAsync(operationParameters);
									
		} // Local block closing
				CreateSpecifiedInformationObjectWithValuesReturnValue returnValue = CreateSpecifiedInformationObjectWithValuesImplementation.Get_ReturnValue(CreatedObject);
		return returnValue;
				}
				}
				public class CreateSpecifiedInformationObjectWithValuesReturnValue 
		{
				public IInformationObject CreatedObjectResult ;
				}
				public class DeleteSpecifiedInformationObjectParameters 
		{
				public IContainerOwner Owner ;
				public string ObjectDomainName ;
				public string ObjectName ;
				public string ObjectID ;
				}
		
		public class DeleteSpecifiedInformationObject 
		{
				private static void PrepareParameters(DeleteSpecifiedInformationObjectParameters parameters)
		{
					}
				public static async Task ExecuteAsync(DeleteSpecifiedInformationObjectParameters parameters)
		{
						PrepareParameters(parameters);
					DeleteSpecifiedInformationObjectImplementation.ExecuteMethod_CatchInvalidDomains(parameters.ObjectDomainName);		
				IInformationObject ObjectToDelete =  await DeleteSpecifiedInformationObjectImplementation.GetTarget_ObjectToDeleteAsync(parameters.Owner, parameters.ObjectDomainName, parameters.ObjectName, parameters.ObjectID);	
				 await DeleteSpecifiedInformationObjectImplementation.ExecuteMethod_DeleteObjectAsync(ObjectToDelete);		
				}
				}
				public class CreateDeviceMembershipParameters 
		{
				public IContainerOwner Owner ;
				public string DeviceDescription ;
				public byte[] ActiveSymmetricAESKey ;
				}
		
		public class CreateDeviceMembership 
		{
				private static void PrepareParameters(CreateDeviceMembershipParameters parameters)
		{
					}
				public static async Task<CreateDeviceMembershipReturnValue> ExecuteAsync(CreateDeviceMembershipParameters parameters)
		{
						PrepareParameters(parameters);
					DeviceMembership CreatedDeviceMembership = CreateDeviceMembershipImplementation.GetTarget_CreatedDeviceMembership(parameters.Owner, parameters.DeviceDescription, parameters.ActiveSymmetricAESKey);	
				 await CreateDeviceMembershipImplementation.ExecuteMethod_StoreObjectAsync(CreatedDeviceMembership);		
				CreateDeviceMembershipReturnValue returnValue = CreateDeviceMembershipImplementation.Get_ReturnValue(CreatedDeviceMembership);
		return returnValue;
				}
				}
				public class CreateDeviceMembershipReturnValue 
		{
				public DeviceMembership DeviceMembership ;
				}
				public class SetDeviceMembershipValidationAndActiveStatusParameters 
		{
				public IContainerOwner Owner ;
				public string DeviceMembershipID ;
				public bool IsValidAndActive ;
				}
		
		public class SetDeviceMembershipValidationAndActiveStatus 
		{
				private static void PrepareParameters(SetDeviceMembershipValidationAndActiveStatusParameters parameters)
		{
					}
				public static async Task ExecuteAsync(SetDeviceMembershipValidationAndActiveStatusParameters parameters)
		{
						PrepareParameters(parameters);
					DeviceMembership DeviceMembership =  await SetDeviceMembershipValidationAndActiveStatusImplementation.GetTarget_DeviceMembershipAsync(parameters.Owner, parameters.DeviceMembershipID);	
				SetDeviceMembershipValidationAndActiveStatusImplementation.ExecuteMethod_SetDeviceValidAndActiveValue(parameters.IsValidAndActive, DeviceMembership);		
				 await SetDeviceMembershipValidationAndActiveStatusImplementation.ExecuteMethod_StoreObjectAsync(DeviceMembership);		
				}
				}
				public class DeleteDeviceMembershipParameters 
		{
				public IContainerOwner Owner ;
				public string DeviceMembershipID ;
				}
		
		public class DeleteDeviceMembership 
		{
				private static void PrepareParameters(DeleteDeviceMembershipParameters parameters)
		{
					}
				public static async Task ExecuteAsync(DeleteDeviceMembershipParameters parameters)
		{
						PrepareParameters(parameters);
					DeviceMembership DeviceMembership =  await DeleteDeviceMembershipImplementation.GetTarget_DeviceMembershipAsync(parameters.Owner, parameters.DeviceMembershipID);	
				 await DeleteDeviceMembershipImplementation.ExecuteMethod_DeleteDeviceMembershipAsync(DeviceMembership);		
				}
				}
				public class CreateAndSendEmailValidationForDeviceJoinConfirmationParameters 
		{
				public AaltoGlobalImpact.OIP.TBAccount OwningAccount ;
				public AaltoGlobalImpact.OIP.TBCollaboratingGroup OwningGroup ;
				public DeviceMembership DeviceMembership ;
				}
		
		public class CreateAndSendEmailValidationForDeviceJoinConfirmation 
		{
				private static void PrepareParameters(CreateAndSendEmailValidationForDeviceJoinConfirmationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(CreateAndSendEmailValidationForDeviceJoinConfirmationParameters parameters)
		{
						PrepareParameters(parameters);
					string[] OwnerEmailAddresses = CreateAndSendEmailValidationForDeviceJoinConfirmationImplementation.GetTarget_OwnerEmailAddresses(parameters.OwningAccount, parameters.OwningGroup);	
				AaltoGlobalImpact.OIP.TBEmailValidation EmailValidation = CreateAndSendEmailValidationForDeviceJoinConfirmationImplementation.GetTarget_EmailValidation(parameters.OwningAccount, parameters.OwningGroup, parameters.DeviceMembership, OwnerEmailAddresses);	
				 await CreateAndSendEmailValidationForDeviceJoinConfirmationImplementation.ExecuteMethod_StoreObjectAsync(EmailValidation);		
				CreateAndSendEmailValidationForDeviceJoinConfirmationImplementation.ExecuteMethod_SendEmailConfirmation(parameters.DeviceMembership, EmailValidation, OwnerEmailAddresses);		
				}
				}
				public class CreateAuthenticatedAsActiveDeviceParameters 
		{
				public IContainerOwner Owner ;
				public string AuthenticationDeviceDescription ;
				public string TargetBallHostName ;
				public string TargetGroupID ;
				}
		
		public class CreateAuthenticatedAsActiveDevice 
		{
				private static void PrepareParameters(CreateAuthenticatedAsActiveDeviceParameters parameters)
		{
					}
				public static async Task<CreateAuthenticatedAsActiveDeviceReturnValue> ExecuteAsync(CreateAuthenticatedAsActiveDeviceParameters parameters)
		{
						PrepareParameters(parameters);
					string NegotiationURL = CreateAuthenticatedAsActiveDeviceImplementation.GetTarget_NegotiationURL(parameters.TargetBallHostName, parameters.TargetGroupID);	
				string ConnectionURL = CreateAuthenticatedAsActiveDeviceImplementation.GetTarget_ConnectionURL(parameters.TargetBallHostName, parameters.TargetGroupID);	
				AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice = CreateAuthenticatedAsActiveDeviceImplementation.GetTarget_AuthenticatedAsActiveDevice(parameters.Owner, parameters.AuthenticationDeviceDescription, NegotiationURL, ConnectionURL);	
				 await CreateAuthenticatedAsActiveDeviceImplementation.ExecuteMethod_StoreObjectAsync(AuthenticatedAsActiveDevice);		
				CreateAuthenticatedAsActiveDeviceReturnValue returnValue = CreateAuthenticatedAsActiveDeviceImplementation.Get_ReturnValue(AuthenticatedAsActiveDevice);
		return returnValue;
				}
				}
				public class CreateAuthenticatedAsActiveDeviceReturnValue 
		{
				public AuthenticatedAsActiveDevice CreatedAuthenticatedAsActiveDevice ;
				}
				public class PerformNegotiationAndValidateAuthenticationAsActiveDeviceParameters 
		{
				public IContainerOwner Owner ;
				public string AuthenticatedAsActiveDeviceID ;
				}
		
		public class PerformNegotiationAndValidateAuthenticationAsActiveDevice 
		{
				private static void PrepareParameters(PerformNegotiationAndValidateAuthenticationAsActiveDeviceParameters parameters)
		{
					}
				public static async Task ExecuteAsync(PerformNegotiationAndValidateAuthenticationAsActiveDeviceParameters parameters)
		{
						PrepareParameters(parameters);
					AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice =  await PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_AuthenticatedAsActiveDeviceAsync(parameters.Owner, parameters.AuthenticatedAsActiveDeviceID);	
				string RemoteBallSecretRequestUrl = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_RemoteBallSecretRequestUrl(AuthenticatedAsActiveDevice);	
				byte[] SharedSecretFullPayload = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_SharedSecretFullPayload(RemoteBallSecretRequestUrl);	
				byte[] SharedSecretData = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_SharedSecretData(SharedSecretFullPayload);	
				byte[] SharedSecretPayload = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_SharedSecretPayload(SharedSecretFullPayload);	
				PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.ExecuteMethod_NegotiateWithTarget(AuthenticatedAsActiveDevice, SharedSecretData, SharedSecretPayload);		
				 await PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.ExecuteMethod_StoreObjectAsync(AuthenticatedAsActiveDevice);		
				}
				}
				public class DeleteAuthenticatedAsActiveDeviceParameters 
		{
				public IContainerOwner Owner ;
				public string AuthenticatedAsActiveDeviceID ;
				}
		
		public class DeleteAuthenticatedAsActiveDevice 
		{
				private static void PrepareParameters(DeleteAuthenticatedAsActiveDeviceParameters parameters)
		{
					}
				public static async Task ExecuteAsync(DeleteAuthenticatedAsActiveDeviceParameters parameters)
		{
						PrepareParameters(parameters);
					AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice =  await DeleteAuthenticatedAsActiveDeviceImplementation.GetTarget_AuthenticatedAsActiveDeviceAsync(parameters.Owner, parameters.AuthenticatedAsActiveDeviceID);	
				DeleteAuthenticatedAsActiveDeviceImplementation.ExecuteMethod_CallDeleteDeviceOnRemoteSide(AuthenticatedAsActiveDevice);		
				 await DeleteAuthenticatedAsActiveDeviceImplementation.ExecuteMethod_DeleteAuthenticatedAsActiveDeviceAsync(AuthenticatedAsActiveDevice);		
				}
				}
				public class CreateInformationOutputParameters 
		{
				public IContainerOwner Owner ;
				public string OutputDescription ;
				public string DestinationURL ;
				public string DestinationContentName ;
				public string LocalContentURL ;
				public string AuthenticatedDeviceID ;
				}
		
		public class CreateInformationOutput 
		{
				private static void PrepareParameters(CreateInformationOutputParameters parameters)
		{
					}
				public static async Task<CreateInformationOutputReturnValue> ExecuteAsync(CreateInformationOutputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationOutput CreatedInformationOutput = CreateInformationOutputImplementation.GetTarget_CreatedInformationOutput(parameters.Owner, parameters.OutputDescription, parameters.DestinationURL, parameters.DestinationContentName, parameters.LocalContentURL, parameters.AuthenticatedDeviceID);	
				 await CreateInformationOutputImplementation.ExecuteMethod_StoreObjectAsync(CreatedInformationOutput);		
				CreateInformationOutputReturnValue returnValue = CreateInformationOutputImplementation.Get_ReturnValue(CreatedInformationOutput);
		return returnValue;
				}
				}
				public class CreateInformationOutputReturnValue 
		{
				public InformationOutput InformationOutput ;
				}
				public class SetInformationOutputValidationAndActiveStatusParameters 
		{
				public IContainerOwner Owner ;
				public string InformationOutputID ;
				public bool IsValidAndActive ;
				}
		
		public class SetInformationOutputValidationAndActiveStatus 
		{
				private static void PrepareParameters(SetInformationOutputValidationAndActiveStatusParameters parameters)
		{
					}
				public static async Task ExecuteAsync(SetInformationOutputValidationAndActiveStatusParameters parameters)
		{
						PrepareParameters(parameters);
					InformationOutput InformationOutput =  await SetInformationOutputValidationAndActiveStatusImplementation.GetTarget_InformationOutputAsync(parameters.Owner, parameters.InformationOutputID);	
				SetInformationOutputValidationAndActiveStatusImplementation.ExecuteMethod_SetInputValidAndActiveValue(parameters.IsValidAndActive, InformationOutput);		
				 await SetInformationOutputValidationAndActiveStatusImplementation.ExecuteMethod_StoreObjectAsync(InformationOutput);		
				}
				}
				public class DeleteInformationOutputParameters 
		{
				public IContainerOwner Owner ;
				public string InformationOutputID ;
				}
		
		public class DeleteInformationOutput 
		{
				private static void PrepareParameters(DeleteInformationOutputParameters parameters)
		{
					}
				public static async Task ExecuteAsync(DeleteInformationOutputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationOutput InformationOutput =  await DeleteInformationOutputImplementation.GetTarget_InformationOutputAsync(parameters.Owner, parameters.InformationOutputID);	
				 await DeleteInformationOutputImplementation.ExecuteMethod_DeleteInformationOutputAsync(InformationOutput);		
				}
				}
				public class CreateAndSendEmailValidationForInformationOutputConfirmationParameters 
		{
				public AaltoGlobalImpact.OIP.TBAccount OwningAccount ;
				public AaltoGlobalImpact.OIP.TBCollaboratingGroup OwningGroup ;
				public InformationOutput InformationOutput ;
				}
		
		public class CreateAndSendEmailValidationForInformationOutputConfirmation 
		{
				private static void PrepareParameters(CreateAndSendEmailValidationForInformationOutputConfirmationParameters parameters)
		{
					}
				public static void Execute(CreateAndSendEmailValidationForInformationOutputConfirmationParameters parameters)
		{
						PrepareParameters(parameters);
					string[] OwnerEmailAddresses = CreateAndSendEmailValidationForInformationOutputConfirmationImplementation.GetTarget_OwnerEmailAddresses(parameters.OwningAccount, parameters.OwningGroup);	
				AaltoGlobalImpact.OIP.TBEmailValidation EmailValidation = CreateAndSendEmailValidationForInformationOutputConfirmationImplementation.GetTarget_EmailValidation(parameters.OwningAccount, parameters.OwningGroup, parameters.InformationOutput, OwnerEmailAddresses);	
				CreateAndSendEmailValidationForInformationOutputConfirmationImplementation.ExecuteMethod_StoreObject(EmailValidation);		
				CreateAndSendEmailValidationForInformationOutputConfirmationImplementation.ExecuteMethod_SendEmailConfirmation(parameters.InformationOutput, EmailValidation, OwnerEmailAddresses);		
				}
				}
				public class PushToInformationOutputParameters 
		{
				public IContainerOwner Owner ;
				public string InformationOutputID ;
				public string LocalContentName ;
				public string SpecificDestinationContentName ;
				}
		
		public class PushToInformationOutput 
		{
				private static void PrepareParameters(PushToInformationOutputParameters parameters)
		{
					}
				public static async Task ExecuteAsync(PushToInformationOutputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationOutput InformationOutput =  await PushToInformationOutputImplementation.GetTarget_InformationOutputAsync(parameters.Owner, parameters.InformationOutputID);	
				PushToInformationOutputImplementation.ExecuteMethod_VerifyValidOutput(InformationOutput);		
				string DestinationURL = PushToInformationOutputImplementation.GetTarget_DestinationURL(InformationOutput);	
				string DestinationContentName = PushToInformationOutputImplementation.GetTarget_DestinationContentName(parameters.SpecificDestinationContentName, InformationOutput);	
				string LocalContentURL = PushToInformationOutputImplementation.GetTarget_LocalContentURL(parameters.LocalContentName, InformationOutput);	
				AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice =  await PushToInformationOutputImplementation.GetTarget_AuthenticatedAsActiveDeviceAsync(InformationOutput);	
				 await PushToInformationOutputImplementation.ExecuteMethod_PushToInformationOutputAsync(parameters.Owner, InformationOutput, DestinationURL, DestinationContentName, LocalContentURL, AuthenticatedAsActiveDevice);		
				}
				}
				public class CreateInformationInputParameters 
		{
				public IContainerOwner Owner ;
				public string InputDescription ;
				public string LocationURL ;
				public string LocalContentName ;
				public string AuthenticatedDeviceID ;
				}
		
		public class CreateInformationInput 
		{
				private static void PrepareParameters(CreateInformationInputParameters parameters)
		{
					}
				public static async Task<CreateInformationInputReturnValue> ExecuteAsync(CreateInformationInputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput CreatedInformationInput = CreateInformationInputImplementation.GetTarget_CreatedInformationInput(parameters.Owner, parameters.InputDescription, parameters.LocationURL, parameters.LocalContentName, parameters.AuthenticatedDeviceID);	
				 await CreateInformationInputImplementation.ExecuteMethod_StoreObjectAsync(CreatedInformationInput);		
				CreateInformationInputReturnValue returnValue = CreateInformationInputImplementation.Get_ReturnValue(CreatedInformationInput);
		return returnValue;
				}
				}
				public class CreateInformationInputReturnValue 
		{
				public InformationInput InformationInput ;
				}
				public class SetInformationInputValidationAndActiveStatusParameters 
		{
				public IContainerOwner Owner ;
				public string InformationInputID ;
				public bool IsValidAndActive ;
				}
		
		public class SetInformationInputValidationAndActiveStatus 
		{
				private static void PrepareParameters(SetInformationInputValidationAndActiveStatusParameters parameters)
		{
					}
				public static async Task ExecuteAsync(SetInformationInputValidationAndActiveStatusParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput InformationInput =  await SetInformationInputValidationAndActiveStatusImplementation.GetTarget_InformationInputAsync(parameters.Owner, parameters.InformationInputID);	
				SetInformationInputValidationAndActiveStatusImplementation.ExecuteMethod_SetInputValidAndActiveValue(parameters.IsValidAndActive, InformationInput);		
				 await SetInformationInputValidationAndActiveStatusImplementation.ExecuteMethod_StoreObjectAsync(InformationInput);		
				}
				}
				public class DeleteInformationInputParameters 
		{
				public IContainerOwner Owner ;
				public string InformationInputID ;
				}
		
		public class DeleteInformationInput 
		{
				private static void PrepareParameters(DeleteInformationInputParameters parameters)
		{
					}
				public static async Task ExecuteAsync(DeleteInformationInputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput InformationInput =  await DeleteInformationInputImplementation.GetTarget_InformationInputAsync(parameters.Owner, parameters.InformationInputID);	
				 await DeleteInformationInputImplementation.ExecuteMethod_DeleteInformationInputAsync(InformationInput);		
				}
				}
				public class CreateAndSendEmailValidationForInformationInputConfirmationParameters 
		{
				public AaltoGlobalImpact.OIP.TBAccount OwningAccount ;
				public AaltoGlobalImpact.OIP.TBCollaboratingGroup OwningGroup ;
				public InformationInput InformationInput ;
				}
		
		public class CreateAndSendEmailValidationForInformationInputConfirmation 
		{
				private static void PrepareParameters(CreateAndSendEmailValidationForInformationInputConfirmationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(CreateAndSendEmailValidationForInformationInputConfirmationParameters parameters)
		{
						PrepareParameters(parameters);
					string[] OwnerEmailAddresses = CreateAndSendEmailValidationForInformationInputConfirmationImplementation.GetTarget_OwnerEmailAddresses(parameters.OwningAccount, parameters.OwningGroup);	
				AaltoGlobalImpact.OIP.TBEmailValidation EmailValidation = CreateAndSendEmailValidationForInformationInputConfirmationImplementation.GetTarget_EmailValidation(parameters.OwningAccount, parameters.OwningGroup, parameters.InformationInput, OwnerEmailAddresses);	
				 await CreateAndSendEmailValidationForInformationInputConfirmationImplementation.ExecuteMethod_StoreObjectAsync(EmailValidation);		
				 await CreateAndSendEmailValidationForInformationInputConfirmationImplementation.ExecuteMethod_SendEmailConfirmationAsync(parameters.InformationInput, EmailValidation, OwnerEmailAddresses);		
				}
				}
				public class JoinAccountToGroupParameters 
		{
				public string AccountEmailAddress ;
				public string GroupID ;
				public string MemberRole ;
				}
		
		public class JoinAccountToGroup 
		{
				private static void PrepareParameters(JoinAccountToGroupParameters parameters)
		{
					}
				public static void Execute(JoinAccountToGroupParameters parameters)
		{
						PrepareParameters(parameters);
					JoinAccountToGroupImplementation.ExecuteMethod_JoinAccountToGroup(parameters.AccountEmailAddress, parameters.GroupID, parameters.MemberRole);		
				}
				}
				public class RemoveAccountFromGroupParameters 
		{
				public string AccountEmailAddress ;
				public string AccountID ;
				public string GroupID ;
				}
		
		public class RemoveAccountFromGroup 
		{
				private static void PrepareParameters(RemoveAccountFromGroupParameters parameters)
		{
					}
				public static void Execute(RemoveAccountFromGroupParameters parameters)
		{
						PrepareParameters(parameters);
					RemoveAccountFromGroupImplementation.ExecuteMethod_RemoveAccountFromGroup(parameters.AccountEmailAddress, parameters.AccountID, parameters.GroupID);		
				}
				}
				public class FetchInputInformationParameters 
		{
				public IContainerOwner Owner ;
				public string InformationInputID ;
				public string QueryParameters ;
				}
		
		public class FetchInputInformation 
		{
				private static void PrepareParameters(FetchInputInformationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(FetchInputInformationParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput InformationInput =  await FetchInputInformationImplementation.GetTarget_InformationInputAsync(parameters.Owner, parameters.InformationInputID);	
				FetchInputInformationImplementation.ExecuteMethod_VerifyValidInput(InformationInput);		
				string InputFetchLocation = FetchInputInformationImplementation.GetTarget_InputFetchLocation(InformationInput);	
				string InputFetchName = FetchInputInformationImplementation.GetTarget_InputFetchName(InformationInput);	
				AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice =  await FetchInputInformationImplementation.GetTarget_AuthenticatedAsActiveDeviceAsync(InformationInput);	
				 await FetchInputInformationImplementation.ExecuteMethod_FetchInputToStorageAsync(parameters.Owner, parameters.QueryParameters, InformationInput, InputFetchLocation, InputFetchName, AuthenticatedAsActiveDevice);		
				}
				}
				public class ProcessFetchedInputsParameters 
		{
				public IContainerOwner Owner ;
				public string InformationInputID ;
				public string ProcessingOperationName ;
				}
		
		public class ProcessFetchedInputs 
		{
				private static void PrepareParameters(ProcessFetchedInputsParameters parameters)
		{
					}
				public class ProcessInputFromStorageReturnValue 
		{
				public IInformationObject[] ProcessingResultsToStore ;
				public IInformationObject[] ProcessingResultsToDelete ;
				}
				public static async Task ExecuteAsync(ProcessFetchedInputsParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput InformationInput =  await ProcessFetchedInputsImplementation.GetTarget_InformationInputAsync(parameters.Owner, parameters.InformationInputID);	
				ProcessFetchedInputsImplementation.ExecuteMethod_VerifyValidInput(InformationInput);		
				string InputFetchLocation = ProcessFetchedInputsImplementation.GetTarget_InputFetchLocation(InformationInput);	
				ProcessInputFromStorageReturnValue ProcessInputFromStorageOutput = ProcessFetchedInputsImplementation.ExecuteMethod_ProcessInputFromStorage(parameters.ProcessingOperationName, InformationInput, InputFetchLocation);		
				 await ProcessFetchedInputsImplementation.ExecuteMethod_StoreObjectsAsync(ProcessInputFromStorageOutput.ProcessingResultsToStore);		
				 await ProcessFetchedInputsImplementation.ExecuteMethod_DeleteObjectsAsync(ProcessInputFromStorageOutput.ProcessingResultsToDelete);		
				}
				}
				public class BeginAccountEmailAddressRegistrationParameters 
		{
				public string AccountID ;
				public string EmailAddress ;
				public string RedirectUrlAfterValidation ;
				}
		
		public class BeginAccountEmailAddressRegistration 
		{
				private static void PrepareParameters(BeginAccountEmailAddressRegistrationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(BeginAccountEmailAddressRegistrationParameters parameters)
		{
						PrepareParameters(parameters);
					 await BeginAccountEmailAddressRegistrationImplementation.ExecuteMethod_ValidateUnexistingEmailAsync(parameters.EmailAddress);		
				AaltoGlobalImpact.OIP.TBEmailValidation EmailValidation = BeginAccountEmailAddressRegistrationImplementation.GetTarget_EmailValidation(parameters.AccountID, parameters.EmailAddress, parameters.RedirectUrlAfterValidation);	
				 await BeginAccountEmailAddressRegistrationImplementation.ExecuteMethod_StoreObjectAsync(EmailValidation);		
				BeginAccountEmailAddressRegistrationImplementation.ExecuteMethod_SendEmailConfirmation(EmailValidation);		
				}
				}
				public class RegisterEmailAddressParameters 
		{
				public string AccountID ;
				public string EmailAddress ;
				}
		
		public class RegisterEmailAddress 
		{
				private static void PrepareParameters(RegisterEmailAddressParameters parameters)
		{
					}
				public static void Execute(RegisterEmailAddressParameters parameters)
		{
						PrepareParameters(parameters);
					RegisterEmailAddressImplementation.ExecuteMethod_ValidateUnexistingEmail(parameters.EmailAddress);		
				AaltoGlobalImpact.OIP.TBRAccountRoot AccountRoot = RegisterEmailAddressImplementation.GetTarget_AccountRoot(parameters.AccountID);	
				AaltoGlobalImpact.OIP.TBREmailRoot EmailRoot = RegisterEmailAddressImplementation.GetTarget_EmailRoot(parameters.EmailAddress);	
				RegisterEmailAddressImplementation.ExecuteMethod_AddEmailToAccount(parameters.EmailAddress, AccountRoot);		
				RegisterEmailAddressImplementation.ExecuteMethod_AddAccountToEmailRoot(AccountRoot, EmailRoot);		
				RegisterEmailAddressImplementation.ExecuteMethod_StoreEmailRoot(EmailRoot);		
				RegisterEmailAddressImplementation.ExecuteMethod_StoreAccountRoot(AccountRoot);		
				RegisterEmailAddressImplementation.ExecuteMethod_UpdateAccountRootAndContainerWithChanges(parameters.AccountID);		
				}
				}
				public class InitiateImportedGroupWithUnchangedIDParameters 
		{
				public string GroupID ;
				public string InitiatorAccountID ;
				public string TemplateNameList ;
				}
		
		public class InitiateImportedGroupWithUnchangedID 
		{
				private static void PrepareParameters(InitiateImportedGroupWithUnchangedIDParameters parameters)
		{
					}
				public static async Task ExecuteAsync(InitiateImportedGroupWithUnchangedIDParameters parameters)
		{
						PrepareParameters(parameters);
					TheBall.CORE.IContainerOwner GroupAsOwner = InitiateImportedGroupWithUnchangedIDImplementation.GetTarget_GroupAsOwner(parameters.GroupID);	
				AaltoGlobalImpact.OIP.GroupContainer GroupContainer =  await InitiateImportedGroupWithUnchangedIDImplementation.GetTarget_GroupContainerAsync(GroupAsOwner);	
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_ValidateGroupContainerID(parameters.GroupID, GroupContainer);		
				AaltoGlobalImpact.OIP.TBRGroupRoot GroupRoot = InitiateImportedGroupWithUnchangedIDImplementation.GetTarget_GroupRoot(parameters.GroupID);	
				 await InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_StoreObjectsAsync(GroupRoot, GroupContainer);		
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_SetGroupInitiatorAccess(GroupRoot, GroupContainer);		
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_FixContentTypesAndMetadataOfBlobs(GroupAsOwner);		
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_FixRelativeLocationsOfInformationObjects(GroupAsOwner);		
				}
				}
				public class UpdateTemplateForAllGroupsParameters 
		{
				public string TemplateName ;
				}
		
		public class UpdateTemplateForAllGroups 
		{
				private static void PrepareParameters(UpdateTemplateForAllGroupsParameters parameters)
		{
					}
				public static async Task ExecuteAsync(UpdateTemplateForAllGroupsParameters parameters)
		{
						PrepareParameters(parameters);
					string[] GroupLocations =  await UpdateTemplateForAllGroupsImplementation.GetTarget_GroupLocationsAsync();	
				 await UpdateTemplateForAllGroupsImplementation.ExecuteMethod_CallUpdateOwnerTemplatesAsync(parameters.TemplateName, GroupLocations);		
				}
				}
				public class UpdateTemplateForAllAccountsParameters 
		{
				public string TemplateName ;
				}
		
		public class UpdateTemplateForAllAccounts 
		{
				private static void PrepareParameters(UpdateTemplateForAllAccountsParameters parameters)
		{
					}
				public static async Task ExecuteAsync(UpdateTemplateForAllAccountsParameters parameters)
		{
						PrepareParameters(parameters);
					string[] AccountLocations =  await UpdateTemplateForAllAccountsImplementation.GetTarget_AccountLocationsAsync();	
				 await UpdateTemplateForAllAccountsImplementation.ExecuteMethod_CallUpdateOwnerTemplatesAsync(parameters.TemplateName, AccountLocations);		
				}
				}
				public class UpdateContainerOwnerTemplatesParameters 
		{
				public string OwnerRootLocation ;
				public string TemplateName ;
				}
		
		public class UpdateContainerOwnerTemplates 
		{
				private static void PrepareParameters(UpdateContainerOwnerTemplatesParameters parameters)
		{
					}
				public static async Task ExecuteAsync(UpdateContainerOwnerTemplatesParameters parameters)
		{
						PrepareParameters(parameters);
					IContainerOwner TargetOwner = UpdateContainerOwnerTemplatesImplementation.GetTarget_TargetOwner(parameters.OwnerRootLocation);	
				 await UpdateContainerOwnerTemplatesImplementation.ExecuteMethod_ValidateTargetIsAccountOrGroupAsync(TargetOwner);		
				UpdateContainerOwnerTemplatesImplementation.ExecuteMethod_ValidateTemplateName(parameters.TemplateName);		
				string SystemTemplateSource = UpdateContainerOwnerTemplatesImplementation.GetTarget_SystemTemplateSource(TargetOwner);	
				string TemplateSourceLocation = UpdateContainerOwnerTemplatesImplementation.GetTarget_TemplateSourceLocation(parameters.TemplateName, SystemTemplateSource);	
				string TemplateTargetLocation = UpdateContainerOwnerTemplatesImplementation.GetTarget_TemplateTargetLocation(parameters.TemplateName, TargetOwner);	
				 await UpdateContainerOwnerTemplatesImplementation.ExecuteMethod_SyncTemplateContentAsync(TemplateSourceLocation, TemplateTargetLocation);		
				}
				}
				public class SetOwnerWebRedirectParameters 
		{
				public IContainerOwner Owner ;
				public string RedirectPath ;
				}
		
		public class SetOwnerWebRedirect 
		{
				private static void PrepareParameters(SetOwnerWebRedirectParameters parameters)
		{
					}
				public static async Task ExecuteAsync(SetOwnerWebRedirectParameters parameters)
		{
						PrepareParameters(parameters);
					 await SetOwnerWebRedirectImplementation.ExecuteMethod_SetRedirectionAsync(parameters.Owner, parameters.RedirectPath);		
				}
				}
				public class ProcessAllResourceUsagesToOwnerCollectionsParameters 
		{
				public int ProcessBatchSize ;
				}
		
		public class ProcessAllResourceUsagesToOwnerCollections 
		{
				private static void PrepareParameters(ProcessAllResourceUsagesToOwnerCollectionsParameters parameters)
		{
					}
				public static async Task ExecuteAsync(ProcessAllResourceUsagesToOwnerCollectionsParameters parameters)
		{
						PrepareParameters(parameters);
					 await ProcessAllResourceUsagesToOwnerCollectionsImplementation.ExecuteMethod_ExecuteBatchProcessorAsync(parameters.ProcessBatchSize);		
				}
				}
				public class ProcessBatchOfResourceUsagesToOwnerCollectionsParameters 
		{
				public int ProcessBatchSize ;
				public bool ProcessIfLess ;
				}
		
		public class ProcessBatchOfResourceUsagesToOwnerCollections 
		{
				private static void PrepareParameters(ProcessBatchOfResourceUsagesToOwnerCollectionsParameters parameters)
		{
					}
				public static async Task<ProcessBatchOfResourceUsagesToOwnerCollectionsReturnValue> ExecuteAsync(ProcessBatchOfResourceUsagesToOwnerCollectionsParameters parameters)
		{
						PrepareParameters(parameters);
					Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob[] BatchToProcess =  await ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.GetTarget_BatchToProcessAsync(parameters.ProcessBatchSize, parameters.ProcessIfLess);	
				 await ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.ExecuteMethod_ProcessBatchAsync(BatchToProcess);		
				 await ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.ExecuteMethod_DeleteProcessedItemsAsync(BatchToProcess);		
				 await ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.ExecuteMethod_ReleaseLockAsync(BatchToProcess);		
				ProcessBatchOfResourceUsagesToOwnerCollectionsReturnValue returnValue = ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.Get_ReturnValue(parameters.ProcessBatchSize, BatchToProcess);
		return returnValue;
				}
				}
				public class ProcessBatchOfResourceUsagesToOwnerCollectionsReturnValue 
		{
				public bool ProcessedAnything ;
				public bool ProcessedFullCount ;
				}
				public class UpdateUsageMonitoringSummariesParameters 
		{
				public IContainerOwner Owner ;
				public int AmountOfDays ;
				}
		
		public class UpdateUsageMonitoringSummaries 
		{
				private static void PrepareParameters(UpdateUsageMonitoringSummariesParameters parameters)
		{
					}
				public static async Task ExecuteAsync(UpdateUsageMonitoringSummariesParameters parameters)
		{
						PrepareParameters(parameters);
					UsageMonitorItem[] SourceItems =  await UpdateUsageMonitoringSummariesImplementation.GetTarget_SourceItemsAsync(parameters.Owner, parameters.AmountOfDays);	
				 await UpdateUsageMonitoringSummariesImplementation.ExecuteMethod_CreateUsageMonitoringSummariesAsync(parameters.Owner, parameters.AmountOfDays, SourceItems);		
				}
				}
				public class UpdateUsageMonitoringItemsParameters 
		{
				public IContainerOwner Owner ;
				public int MonitoringItemTimeSpanInMinutes ;
				public int MonitoringIntervalInMinutes ;
				}
		
		public class UpdateUsageMonitoringItems 
		{
				private static void PrepareParameters(UpdateUsageMonitoringItemsParameters parameters)
		{
					}
				public static void Execute(UpdateUsageMonitoringItemsParameters parameters)
		{
						PrepareParameters(parameters);
					UpdateUsageMonitoringItemsImplementation.ExecuteMethod_ValidateEqualSplitOfIntervalsInTimeSpan(parameters.MonitoringItemTimeSpanInMinutes, parameters.MonitoringIntervalInMinutes);		
				Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob[] CurrentMonitoringItems = UpdateUsageMonitoringItemsImplementation.GetTarget_CurrentMonitoringItems(parameters.Owner);	
				DateTime EndingTimeOfCurrentItems = UpdateUsageMonitoringItemsImplementation.GetTarget_EndingTimeOfCurrentItems(CurrentMonitoringItems);	
				Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob[] NewResourceUsageBlobs = UpdateUsageMonitoringItemsImplementation.GetTarget_NewResourceUsageBlobs(parameters.Owner, EndingTimeOfCurrentItems);	
				DateTime StartingTimeOfNewItems = UpdateUsageMonitoringItemsImplementation.GetTarget_StartingTimeOfNewItems(parameters.MonitoringItemTimeSpanInMinutes, EndingTimeOfCurrentItems, NewResourceUsageBlobs);	
				DateTime EndingTimeOfNewItems = UpdateUsageMonitoringItemsImplementation.GetTarget_EndingTimeOfNewItems(parameters.MonitoringItemTimeSpanInMinutes, StartingTimeOfNewItems, NewResourceUsageBlobs);	
				RequestResourceUsageCollection[] ResourcesToIncludeInMonitoring = UpdateUsageMonitoringItemsImplementation.GetTarget_ResourcesToIncludeInMonitoring(NewResourceUsageBlobs, EndingTimeOfNewItems);	
				UsageMonitorItem[] NewMonitoringItems = UpdateUsageMonitoringItemsImplementation.GetTarget_NewMonitoringItems(parameters.Owner, parameters.MonitoringItemTimeSpanInMinutes, parameters.MonitoringIntervalInMinutes, StartingTimeOfNewItems, EndingTimeOfNewItems);	
				UpdateUsageMonitoringItemsImplementation.ExecuteMethod_PopulateMonitoringItems(ResourcesToIncludeInMonitoring, NewMonitoringItems);		
				UpdateUsageMonitoringItemsImplementation.ExecuteMethod_StoreObjects(NewMonitoringItems);		
				}
				}
				public class PublishGroupToWwwParameters 
		{
				public IContainerOwner Owner ;
				}
		
		public class PublishGroupToWww 
		{
				private static void PrepareParameters(PublishGroupToWwwParameters parameters)
		{
					}
				public static async Task ExecuteAsync(PublishGroupToWwwParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.GroupContainer GroupContainer =  await PublishGroupToWwwImplementation.GetTarget_GroupContainerAsync(parameters.Owner);	
				string TargetContainerName = PublishGroupToWwwImplementation.GetTarget_TargetContainerName(GroupContainer);	
				string TargetContainerOwnerString =  await PublishGroupToWwwImplementation.GetTarget_TargetContainerOwnerStringAsync(TargetContainerName);	
				PublishGroupToWwwImplementation.ExecuteMethod_ValidatePublishParameters(parameters.Owner, TargetContainerOwnerString);		
				PublishGroupToWwwImplementation.ExecuteMethod_PublishWithWorker(parameters.Owner, TargetContainerName, TargetContainerOwnerString);		
				}
				}
				public class CreateOrUpdateCustomUIParameters 
		{
				public IContainerOwner Owner ;
				public string CustomUIName ;
				public Stream ZipArchiveStream ;
				}
		
		public class CreateOrUpdateCustomUI 
		{
				private static void PrepareParameters(CreateOrUpdateCustomUIParameters parameters)
		{
					}
				public static async Task ExecuteAsync(CreateOrUpdateCustomUIParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.GroupContainer GroupContainer =  await CreateOrUpdateCustomUIImplementation.GetTarget_GroupContainerAsync(parameters.Owner);	
				CreateOrUpdateCustomUIImplementation.ExecuteMethod_ValidateCustomUIName(parameters.CustomUIName);		
				string CustomUIFolder = CreateOrUpdateCustomUIImplementation.GetTarget_CustomUIFolder(parameters.Owner, parameters.CustomUIName);	
				CreateOrUpdateCustomUIImplementation.ExecuteMethod_SetCustomUIName(parameters.CustomUIName, GroupContainer);		
				 await CreateOrUpdateCustomUIImplementation.ExecuteMethod_CopyUIContentsFromZipArchiveAsync(parameters.ZipArchiveStream, CustomUIFolder);		
				 await CreateOrUpdateCustomUIImplementation.ExecuteMethod_StoreObjectAsync(GroupContainer);		
				}
				}
				public class DeleteCustomUIParameters 
		{
				public IContainerOwner Owner ;
				public string CustomUIName ;
				}
		
		public class DeleteCustomUI 
		{
				private static void PrepareParameters(DeleteCustomUIParameters parameters)
		{
					}
				public static async Task ExecuteAsync(DeleteCustomUIParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.GroupContainer GroupContainer =  await DeleteCustomUIImplementation.GetTarget_GroupContainerAsync(parameters.Owner);	
				string CustomUIFolder = DeleteCustomUIImplementation.GetTarget_CustomUIFolder(parameters.Owner, parameters.CustomUIName);	
				DeleteCustomUIImplementation.ExecuteMethod_RemoveCustomUIName(parameters.CustomUIName, GroupContainer);		
				DeleteCustomUIImplementation.ExecuteMethod_RemoveCustomUIContents(CustomUIFolder);		
				DeleteCustomUIImplementation.ExecuteMethod_StoreObject(GroupContainer);		
				}
				}
				public class ExportOwnerContentToZipParameters 
		{
				public IContainerOwner Owner ;
				public string PackageRootFolder ;
				}
		
		public class ExportOwnerContentToZip 
		{
				private static void PrepareParameters(ExportOwnerContentToZipParameters parameters)
		{
					}
				public static async Task<ExportOwnerContentToZipReturnValue> ExecuteAsync(ExportOwnerContentToZipParameters parameters)
		{
						PrepareParameters(parameters);
					string[] IncludedFolders =  await ExportOwnerContentToZipImplementation.GetTarget_IncludedFoldersAsync(parameters.Owner, parameters.PackageRootFolder);	
				ContentPackage PackageOwnerContentToZipOutput;
		{ // Local block to allow local naming
			PackageOwnerContentParameters operationParameters = ExportOwnerContentToZipImplementation.PackageOwnerContentToZip_GetParameters(parameters.Owner, parameters.PackageRootFolder, IncludedFolders);
			var operationReturnValue = PackageOwnerContent.Execute(operationParameters);
			PackageOwnerContentToZipOutput = ExportOwnerContentToZipImplementation.PackageOwnerContentToZip_GetOutput(operationReturnValue, parameters.Owner, parameters.PackageRootFolder, IncludedFolders);						
		} // Local block closing
				ExportOwnerContentToZipReturnValue returnValue = ExportOwnerContentToZipImplementation.Get_ReturnValue(PackageOwnerContentToZipOutput);
		return returnValue;
				}
				}
				public class ExportOwnerContentToZipReturnValue 
		{
				public string ContentPackageID ;
				}
				public class PackageOwnerContentParameters 
		{
				public IContainerOwner Owner ;
				public string PackageType ;
				public string PackageName ;
				public string Description ;
				public string PackageRootFolder ;
				public string[] IncludedFolders ;
				}
		
		public class PackageOwnerContent 
		{
				private static void PrepareParameters(PackageOwnerContentParameters parameters)
		{
					}
				public static PackageOwnerContentReturnValue Execute(PackageOwnerContentParameters parameters)
		{
						PrepareParameters(parameters);
					ContentPackage ContentPackageObject = PackageOwnerContentImplementation.GetTarget_ContentPackageObject(parameters.Owner, parameters.PackageType, parameters.PackageName, parameters.Description, parameters.PackageRootFolder);	
				PackageOwnerContentImplementation.ExecuteMethod_StoreObject(ContentPackageObject);		
				Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob ArchiveBlob = PackageOwnerContentImplementation.GetTarget_ArchiveBlob(ContentPackageObject);	
				Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob[] ArchiveSourceBlobs = PackageOwnerContentImplementation.GetTarget_ArchiveSourceBlobs(parameters.Owner, parameters.PackageRootFolder, parameters.IncludedFolders);	
				string[] CreateZipPackageContentOutput = PackageOwnerContentImplementation.ExecuteMethod_CreateZipPackageContent(parameters.IncludedFolders, ContentPackageObject, ArchiveSourceBlobs, ArchiveBlob);		
				PackageOwnerContentImplementation.ExecuteMethod_CommitArchiveBlob(ArchiveBlob, CreateZipPackageContentOutput);		
				PackageOwnerContentReturnValue returnValue = PackageOwnerContentImplementation.Get_ReturnValue(ContentPackageObject);
		return returnValue;
				}
				}
				public class PackageOwnerContentReturnValue 
		{
				public ContentPackage ContentPackage ;
				}
				public class DeviceSyncFullAccountOperationParameters 
		{
				public System.IO.Stream InputStream ;
				public System.IO.Stream OutputStream ;
				}
		
		public class DeviceSyncFullAccountOperation 
		{
				private static void PrepareParameters(DeviceSyncFullAccountOperationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(DeviceSyncFullAccountOperationParameters parameters)
		{
						PrepareParameters(parameters);
					TheBall.Support.VirtualStorage.ContentSyncRequest SyncRequest = DeviceSyncFullAccountOperationImplementation.GetTarget_SyncRequest(parameters.InputStream);	
				AaltoGlobalImpact.OIP.TBAccount AccountOwner = DeviceSyncFullAccountOperationImplementation.GetTarget_AccountOwner();	
				IContainerOwner[] GroupOwners = DeviceSyncFullAccountOperationImplementation.GetTarget_GroupOwners(AccountOwner);	
				TheBall.Support.VirtualStorage.ContentSyncResponse SyncResponse =  await DeviceSyncFullAccountOperationImplementation.GetTarget_SyncResponseAsync(SyncRequest, AccountOwner, GroupOwners);	
				 await DeviceSyncFullAccountOperationImplementation.ExecuteMethod_WriteResponseToStreamAsync(parameters.OutputStream, SyncResponse);		
				}
				}
				public class RemoteDeviceCoreOperationParameters 
		{
				public System.IO.Stream InputStream ;
				public System.IO.Stream OutputStream ;
				}
		
		public class RemoteDeviceCoreOperation 
		{
				private static void PrepareParameters(RemoteDeviceCoreOperationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(RemoteDeviceCoreOperationParameters parameters)
		{
						PrepareParameters(parameters);
					INT.DeviceOperationData DeviceOperationData = RemoteDeviceCoreOperationImplementation.GetTarget_DeviceOperationData(parameters.InputStream);	
				DeviceMembership CurrentDevice = RemoteDeviceCoreOperationImplementation.GetTarget_CurrentDevice();	
				 await RemoteDeviceCoreOperationImplementation.ExecuteMethod_PerformOperationAsync(CurrentDevice, DeviceOperationData);		
				RemoteDeviceCoreOperationImplementation.ExecuteMethod_SerializeDeviceOperationDataToOutput(parameters.OutputStream, DeviceOperationData);		
				}
				}
				public class SyncCopyContentToDeviceTargetParameters 
		{
				public string AuthenticatedAsActiveDeviceID ;
				}
		
		public class SyncCopyContentToDeviceTarget 
		{
				private static void PrepareParameters(SyncCopyContentToDeviceTargetParameters parameters)
		{
					}
				public class CallPrepareTargetAndListItemsToCopyReturnValue 
		{
				public INT.ContentItemLocationWithMD5[] ItemsToCopy ;
				public INT.ContentItemLocationWithMD5[] ItemsDeleted ;
				}
				public static async Task<SyncCopyContentToDeviceTargetReturnValue> ExecuteAsync(SyncCopyContentToDeviceTargetParameters parameters)
		{
						PrepareParameters(parameters);
					AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice =  await SyncCopyContentToDeviceTargetImplementation.GetTarget_AuthenticatedAsActiveDeviceAsync(parameters.AuthenticatedAsActiveDeviceID);	
				string ContentRootLocation = SyncCopyContentToDeviceTargetImplementation.GetTarget_ContentRootLocation(AuthenticatedAsActiveDevice);	
				INT.ContentItemLocationWithMD5[] ThisSideContentMD5List =  await SyncCopyContentToDeviceTargetImplementation.GetTarget_ThisSideContentMD5ListAsync(ContentRootLocation);	
				CallPrepareTargetAndListItemsToCopyReturnValue CallPrepareTargetAndListItemsToCopyOutput =  await SyncCopyContentToDeviceTargetImplementation.ExecuteMethod_CallPrepareTargetAndListItemsToCopyAsync(AuthenticatedAsActiveDevice, ThisSideContentMD5List);		
				 await SyncCopyContentToDeviceTargetImplementation.ExecuteMethod_CopyItemsToCopyToTargetDeviceAsync(AuthenticatedAsActiveDevice, CallPrepareTargetAndListItemsToCopyOutput);		
				SyncCopyContentToDeviceTargetReturnValue returnValue = SyncCopyContentToDeviceTargetImplementation.Get_ReturnValue(CallPrepareTargetAndListItemsToCopyOutput);
		return returnValue;
				}
				}
				public class SyncCopyContentToDeviceTargetReturnValue 
		{
				public INT.ContentItemLocationWithMD5[] CopiedItems ;
				public INT.ContentItemLocationWithMD5[] DeletedItems ;
				}
				public class GetOwnerSemanticDomainsParameters 
		{
				public bool SkipSystemDomains ;
				}
		
		public class GetOwnerSemanticDomains 
		{
				private static void PrepareParameters(GetOwnerSemanticDomainsParameters parameters)
		{
					}
				public static async Task<GetOwnerSemanticDomainsReturnValue> ExecuteAsync(GetOwnerSemanticDomainsParameters parameters)
		{
						PrepareParameters(parameters);
					string[] OwnerDomains =  await GetOwnerSemanticDomainsImplementation.GetTarget_OwnerDomainsAsync(parameters.SkipSystemDomains);	
				GetOwnerSemanticDomainsReturnValue returnValue = GetOwnerSemanticDomainsImplementation.Get_ReturnValue(OwnerDomains);
		return returnValue;
				}
				}
				public class GetOwnerSemanticDomainsReturnValue 
		{
				public string[] OwnerSemanticDomains ;
				}
				public class UpdateOwnerDomainObjectsInSQLiteStorageParameters 
		{
				public IContainerOwner Owner ;
				public string SemanticDomain ;
				}
		
		public class UpdateOwnerDomainObjectsInSQLiteStorage 
		{
				private static void PrepareParameters(UpdateOwnerDomainObjectsInSQLiteStorageParameters parameters)
		{
					}
				public static void Execute(UpdateOwnerDomainObjectsInSQLiteStorageParameters parameters)
		{
						PrepareParameters(parameters);
					string SQLiteDBLocationDirectory = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_SQLiteDBLocationDirectory(parameters.Owner);	
				UpdateOwnerDomainObjectsInSQLiteStorageImplementation.ExecuteMethod_CreateDBLocationDirectoryIfMissing(SQLiteDBLocationDirectory);		
				string DataContextFullTypeName = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_DataContextFullTypeName(parameters.SemanticDomain);	
				System.Type DataContextType = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_DataContextType(DataContextFullTypeName);	
				string DatabaseAttachOrCreateMethodName = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_DatabaseAttachOrCreateMethodName();	
				string SQLiteDBLocationFileName = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_SQLiteDBLocationFileName(parameters.SemanticDomain, SQLiteDBLocationDirectory);	
				string OwnerRootPath = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_OwnerRootPath(parameters.Owner);	
				Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob[] BlobsToSync = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_BlobsToSync(parameters.Owner, parameters.SemanticDomain);	
				UpdateOwnerDomainObjectsInSQLiteStorageImplementation.ExecuteMethod_PerformSyncing(DataContextType, DatabaseAttachOrCreateMethodName, SQLiteDBLocationFileName, OwnerRootPath, BlobsToSync);		
				}
				public static async Task ExecuteAsync(UpdateOwnerDomainObjectsInSQLiteStorageParameters parameters)
		{
						PrepareParameters(parameters);
					string SQLiteDBLocationDirectory = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_SQLiteDBLocationDirectory(parameters.Owner);	
				UpdateOwnerDomainObjectsInSQLiteStorageImplementation.ExecuteMethod_CreateDBLocationDirectoryIfMissing(SQLiteDBLocationDirectory);		
				string DataContextFullTypeName = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_DataContextFullTypeName(parameters.SemanticDomain);	
				System.Type DataContextType = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_DataContextType(DataContextFullTypeName);	
				string DatabaseAttachOrCreateMethodName = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_DatabaseAttachOrCreateMethodName();	
				string SQLiteDBLocationFileName = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_SQLiteDBLocationFileName(parameters.SemanticDomain, SQLiteDBLocationDirectory);	
				string OwnerRootPath = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_OwnerRootPath(parameters.Owner);	
				Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob[] BlobsToSync =  await UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_BlobsToSyncAsync(parameters.Owner, parameters.SemanticDomain);	
				 await UpdateOwnerDomainObjectsInSQLiteStorageImplementation.ExecuteMethod_PerformSyncingAsync(DataContextType, DatabaseAttachOrCreateMethodName, SQLiteDBLocationFileName, OwnerRootPath, BlobsToSync);		
				}
				}
		 } 