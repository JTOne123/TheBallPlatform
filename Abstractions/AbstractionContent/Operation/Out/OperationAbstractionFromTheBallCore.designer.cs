 

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

		namespace TheBall.CORE { 
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
				public static CreateProcessReturnValue Execute(CreateProcessParameters parameters)
		{
						PrepareParameters(parameters);
					Process Process = CreateProcessImplementation.GetTarget_Process(parameters.ProcessDescription, parameters.ExecutingOperationName, parameters.InitialArguments);	
				ProcessContainer OwnerProcessContainer = CreateProcessImplementation.GetTarget_OwnerProcessContainer();	
				CreateProcessImplementation.ExecuteMethod_AddProcessObjectToContainerAndStoreBoth(OwnerProcessContainer, Process);		
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
				public static void Execute(DeleteProcessParameters parameters)
		{
						PrepareParameters(parameters);
					Process Process = DeleteProcessImplementation.GetTarget_Process(parameters.ProcessID);	
				ProcessContainer OwnerProcessContainer = DeleteProcessImplementation.GetTarget_OwnerProcessContainer();	
				DeleteProcessImplementation.ExecuteMethod_ObtainLockRemoveFromContainerAndDeleteProcess(parameters.ProcessID, Process, OwnerProcessContainer);		
				}
				}
				public class RequestProcessExecutionParameters 
		{
				public string ProcessID ;
				public TheBall.CORE.IContainerOwner Owner ;
				}
		
		public class RequestProcessExecution 
		{
				private static void PrepareParameters(RequestProcessExecutionParameters parameters)
		{
					}
				public static void Execute(RequestProcessExecutionParameters parameters)
		{
						PrepareParameters(parameters);
					string ActiveContainerName = RequestProcessExecutionImplementation.GetTarget_ActiveContainerName();	
				AaltoGlobalImpact.OIP.QueueEnvelope RequestEnvelope = RequestProcessExecutionImplementation.GetTarget_RequestEnvelope(parameters.ProcessID, parameters.Owner, ActiveContainerName);	
				RequestProcessExecutionImplementation.ExecuteMethod_PutEnvelopeToDefaultQueue(RequestEnvelope);		
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
				public static void Execute(ExecuteProcessParameters parameters)
		{
						PrepareParameters(parameters);
					TheBall.CORE.Process Process = ExecuteProcessImplementation.GetTarget_Process(parameters.ProcessID);	
				string ProcessLockLocation = ExecuteProcessImplementation.GetTarget_ProcessLockLocation(Process);	
				ExecuteProcessImplementation.ExecuteMethod_ExecuteAndStoreProcessWithLock(ProcessLockLocation, Process);		
				}
				}
				public class SetObjectTreeValuesParameters 
		{
				public IInformationObject RootObject ;
				public NameValueCollection HttpFormData ;
				public System.Web.HttpFileCollection HttpFileData ;
				}
		
		public class SetObjectTreeValues 
		{
				private static void PrepareParameters(SetObjectTreeValuesParameters parameters)
		{
					}
				public static void Execute(SetObjectTreeValuesParameters parameters)
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
				SetObjectTreeValuesImplementation.ExecuteMethod_StoreCompleteObject(parameters.RootObject);		
				}
				}
				public class CreateSpecifiedInformationObjectWithValuesParameters 
		{
				public IContainerOwner Owner ;
				public string ObjectDomainName ;
				public string ObjectName ;
				public NameValueCollection HttpFormData ;
				public System.Web.HttpFileCollection HttpFileData ;
				}
		
		public class CreateSpecifiedInformationObjectWithValues 
		{
				private static void PrepareParameters(CreateSpecifiedInformationObjectWithValuesParameters parameters)
		{
					}
				public static CreateSpecifiedInformationObjectWithValuesReturnValue Execute(CreateSpecifiedInformationObjectWithValuesParameters parameters)
		{
						PrepareParameters(parameters);
					CreateSpecifiedInformationObjectWithValuesImplementation.ExecuteMethod_CatchInvalidDomains(parameters.ObjectDomainName);		
				IInformationObject CreatedObject = CreateSpecifiedInformationObjectWithValuesImplementation.GetTarget_CreatedObject(parameters.Owner, parameters.ObjectDomainName, parameters.ObjectName);	
				CreateSpecifiedInformationObjectWithValuesImplementation.ExecuteMethod_StoreInitialObject(CreatedObject);		
				
		{ // Local block to allow local naming
			SetObjectTreeValuesParameters operationParameters = CreateSpecifiedInformationObjectWithValuesImplementation.SetObjectValues_GetParameters(parameters.HttpFormData, parameters.HttpFileData, CreatedObject);
			SetObjectTreeValues.Execute(operationParameters);
									
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
				public static void Execute(DeleteSpecifiedInformationObjectParameters parameters)
		{
						PrepareParameters(parameters);
					DeleteSpecifiedInformationObjectImplementation.ExecuteMethod_CatchInvalidDomains(parameters.ObjectDomainName);		
				IInformationObject ObjectToDelete = DeleteSpecifiedInformationObjectImplementation.GetTarget_ObjectToDelete(parameters.Owner, parameters.ObjectDomainName, parameters.ObjectName, parameters.ObjectID);	
				DeleteSpecifiedInformationObjectImplementation.ExecuteMethod_DeleteObject(ObjectToDelete);		
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
				public static CreateDeviceMembershipReturnValue Execute(CreateDeviceMembershipParameters parameters)
		{
						PrepareParameters(parameters);
					DeviceMembership CreatedDeviceMembership = CreateDeviceMembershipImplementation.GetTarget_CreatedDeviceMembership(parameters.Owner, parameters.DeviceDescription, parameters.ActiveSymmetricAESKey);	
				CreateDeviceMembershipImplementation.ExecuteMethod_StoreObject(CreatedDeviceMembership);		
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
				public static void Execute(SetDeviceMembershipValidationAndActiveStatusParameters parameters)
		{
						PrepareParameters(parameters);
					DeviceMembership DeviceMembership = SetDeviceMembershipValidationAndActiveStatusImplementation.GetTarget_DeviceMembership(parameters.Owner, parameters.DeviceMembershipID);	
				SetDeviceMembershipValidationAndActiveStatusImplementation.ExecuteMethod_SetDeviceValidAndActiveValue(parameters.IsValidAndActive, DeviceMembership);		
				SetDeviceMembershipValidationAndActiveStatusImplementation.ExecuteMethod_StoreObject(DeviceMembership);		
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
				public static void Execute(DeleteDeviceMembershipParameters parameters)
		{
						PrepareParameters(parameters);
					DeviceMembership DeviceMembership = DeleteDeviceMembershipImplementation.GetTarget_DeviceMembership(parameters.Owner, parameters.DeviceMembershipID);	
				DeleteDeviceMembershipImplementation.ExecuteMethod_DeleteDeviceMembership(DeviceMembership);		
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
				public static void Execute(CreateAndSendEmailValidationForDeviceJoinConfirmationParameters parameters)
		{
						PrepareParameters(parameters);
					string[] OwnerEmailAddresses = CreateAndSendEmailValidationForDeviceJoinConfirmationImplementation.GetTarget_OwnerEmailAddresses(parameters.OwningAccount, parameters.OwningGroup);	
				AaltoGlobalImpact.OIP.TBEmailValidation EmailValidation = CreateAndSendEmailValidationForDeviceJoinConfirmationImplementation.GetTarget_EmailValidation(parameters.OwningAccount, parameters.OwningGroup, parameters.DeviceMembership, OwnerEmailAddresses);	
				CreateAndSendEmailValidationForDeviceJoinConfirmationImplementation.ExecuteMethod_StoreObject(EmailValidation);		
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
				public static CreateAuthenticatedAsActiveDeviceReturnValue Execute(CreateAuthenticatedAsActiveDeviceParameters parameters)
		{
						PrepareParameters(parameters);
					string NegotiationURL = CreateAuthenticatedAsActiveDeviceImplementation.GetTarget_NegotiationURL(parameters.TargetBallHostName, parameters.TargetGroupID);	
				string ConnectionURL = CreateAuthenticatedAsActiveDeviceImplementation.GetTarget_ConnectionURL(parameters.TargetBallHostName, parameters.TargetGroupID);	
				AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice = CreateAuthenticatedAsActiveDeviceImplementation.GetTarget_AuthenticatedAsActiveDevice(parameters.Owner, parameters.AuthenticationDeviceDescription, NegotiationURL, ConnectionURL);	
				CreateAuthenticatedAsActiveDeviceImplementation.ExecuteMethod_StoreObject(AuthenticatedAsActiveDevice);		
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
				public static void Execute(PerformNegotiationAndValidateAuthenticationAsActiveDeviceParameters parameters)
		{
						PrepareParameters(parameters);
					AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_AuthenticatedAsActiveDevice(parameters.Owner, parameters.AuthenticatedAsActiveDeviceID);	
				string RemoteBallSecretRequestUrl = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_RemoteBallSecretRequestUrl(AuthenticatedAsActiveDevice);	
				byte[] SharedSecretFullPayload = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_SharedSecretFullPayload(RemoteBallSecretRequestUrl);	
				byte[] SharedSecretData = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_SharedSecretData(SharedSecretFullPayload);	
				byte[] SharedSecretPayload = PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.GetTarget_SharedSecretPayload(SharedSecretFullPayload);	
				PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.ExecuteMethod_NegotiateWithTarget(AuthenticatedAsActiveDevice, SharedSecretData, SharedSecretPayload);		
				PerformNegotiationAndValidateAuthenticationAsActiveDeviceImplementation.ExecuteMethod_StoreObject(AuthenticatedAsActiveDevice);		
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
				public static void Execute(DeleteAuthenticatedAsActiveDeviceParameters parameters)
		{
						PrepareParameters(parameters);
					AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice = DeleteAuthenticatedAsActiveDeviceImplementation.GetTarget_AuthenticatedAsActiveDevice(parameters.Owner, parameters.AuthenticatedAsActiveDeviceID);	
				DeleteAuthenticatedAsActiveDeviceImplementation.ExecuteMethod_CallDeleteDeviceOnRemoteSide(AuthenticatedAsActiveDevice);		
				DeleteAuthenticatedAsActiveDeviceImplementation.ExecuteMethod_DeleteAuthenticatedAsActiveDevice(AuthenticatedAsActiveDevice);		
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
				public static CreateInformationOutputReturnValue Execute(CreateInformationOutputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationOutput CreatedInformationOutput = CreateInformationOutputImplementation.GetTarget_CreatedInformationOutput(parameters.Owner, parameters.OutputDescription, parameters.DestinationURL, parameters.DestinationContentName, parameters.LocalContentURL, parameters.AuthenticatedDeviceID);	
				CreateInformationOutputImplementation.ExecuteMethod_StoreObject(CreatedInformationOutput);		
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
				public static void Execute(SetInformationOutputValidationAndActiveStatusParameters parameters)
		{
						PrepareParameters(parameters);
					InformationOutput InformationOutput = SetInformationOutputValidationAndActiveStatusImplementation.GetTarget_InformationOutput(parameters.Owner, parameters.InformationOutputID);	
				SetInformationOutputValidationAndActiveStatusImplementation.ExecuteMethod_SetInputValidAndActiveValue(parameters.IsValidAndActive, InformationOutput);		
				SetInformationOutputValidationAndActiveStatusImplementation.ExecuteMethod_StoreObject(InformationOutput);		
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
				public static void Execute(DeleteInformationOutputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationOutput InformationOutput = DeleteInformationOutputImplementation.GetTarget_InformationOutput(parameters.Owner, parameters.InformationOutputID);	
				DeleteInformationOutputImplementation.ExecuteMethod_DeleteInformationOutput(InformationOutput);		
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
				public static void Execute(PushToInformationOutputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationOutput InformationOutput = PushToInformationOutputImplementation.GetTarget_InformationOutput(parameters.Owner, parameters.InformationOutputID);	
				PushToInformationOutputImplementation.ExecuteMethod_VerifyValidOutput(InformationOutput);		
				string DestinationURL = PushToInformationOutputImplementation.GetTarget_DestinationURL(InformationOutput);	
				string DestinationContentName = PushToInformationOutputImplementation.GetTarget_DestinationContentName(parameters.SpecificDestinationContentName, InformationOutput);	
				string LocalContentURL = PushToInformationOutputImplementation.GetTarget_LocalContentURL(parameters.LocalContentName, InformationOutput);	
				AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice = PushToInformationOutputImplementation.GetTarget_AuthenticatedAsActiveDevice(InformationOutput);	
				PushToInformationOutputImplementation.ExecuteMethod_PushToInformationOutput(parameters.Owner, InformationOutput, DestinationURL, DestinationContentName, LocalContentURL, AuthenticatedAsActiveDevice);		
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
				public static CreateInformationInputReturnValue Execute(CreateInformationInputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput CreatedInformationInput = CreateInformationInputImplementation.GetTarget_CreatedInformationInput(parameters.Owner, parameters.InputDescription, parameters.LocationURL, parameters.LocalContentName, parameters.AuthenticatedDeviceID);	
				CreateInformationInputImplementation.ExecuteMethod_StoreObject(CreatedInformationInput);		
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
				public static void Execute(SetInformationInputValidationAndActiveStatusParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput InformationInput = SetInformationInputValidationAndActiveStatusImplementation.GetTarget_InformationInput(parameters.Owner, parameters.InformationInputID);	
				SetInformationInputValidationAndActiveStatusImplementation.ExecuteMethod_SetInputValidAndActiveValue(parameters.IsValidAndActive, InformationInput);		
				SetInformationInputValidationAndActiveStatusImplementation.ExecuteMethod_StoreObject(InformationInput);		
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
				public static void Execute(DeleteInformationInputParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput InformationInput = DeleteInformationInputImplementation.GetTarget_InformationInput(parameters.Owner, parameters.InformationInputID);	
				DeleteInformationInputImplementation.ExecuteMethod_DeleteInformationInput(InformationInput);		
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
				public static void Execute(CreateAndSendEmailValidationForInformationInputConfirmationParameters parameters)
		{
						PrepareParameters(parameters);
					string[] OwnerEmailAddresses = CreateAndSendEmailValidationForInformationInputConfirmationImplementation.GetTarget_OwnerEmailAddresses(parameters.OwningAccount, parameters.OwningGroup);	
				AaltoGlobalImpact.OIP.TBEmailValidation EmailValidation = CreateAndSendEmailValidationForInformationInputConfirmationImplementation.GetTarget_EmailValidation(parameters.OwningAccount, parameters.OwningGroup, parameters.InformationInput, OwnerEmailAddresses);	
				CreateAndSendEmailValidationForInformationInputConfirmationImplementation.ExecuteMethod_StoreObject(EmailValidation);		
				CreateAndSendEmailValidationForInformationInputConfirmationImplementation.ExecuteMethod_SendEmailConfirmation(parameters.InformationInput, EmailValidation, OwnerEmailAddresses);		
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
				public static void Execute(FetchInputInformationParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput InformationInput = FetchInputInformationImplementation.GetTarget_InformationInput(parameters.Owner, parameters.InformationInputID);	
				FetchInputInformationImplementation.ExecuteMethod_VerifyValidInput(InformationInput);		
				string InputFetchLocation = FetchInputInformationImplementation.GetTarget_InputFetchLocation(InformationInput);	
				string InputFetchName = FetchInputInformationImplementation.GetTarget_InputFetchName(InformationInput);	
				AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice = FetchInputInformationImplementation.GetTarget_AuthenticatedAsActiveDevice(InformationInput);	
				FetchInputInformationImplementation.ExecuteMethod_FetchInputToStorage(parameters.Owner, parameters.QueryParameters, InformationInput, InputFetchLocation, InputFetchName, AuthenticatedAsActiveDevice);		
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
				public static void Execute(ProcessFetchedInputsParameters parameters)
		{
						PrepareParameters(parameters);
					InformationInput InformationInput = ProcessFetchedInputsImplementation.GetTarget_InformationInput(parameters.Owner, parameters.InformationInputID);	
				ProcessFetchedInputsImplementation.ExecuteMethod_VerifyValidInput(InformationInput);		
				string InputFetchLocation = ProcessFetchedInputsImplementation.GetTarget_InputFetchLocation(InformationInput);	
				ProcessInputFromStorageReturnValue ProcessInputFromStorageOutput = ProcessFetchedInputsImplementation.ExecuteMethod_ProcessInputFromStorage(parameters.ProcessingOperationName, InformationInput, InputFetchLocation);		
				ProcessFetchedInputsImplementation.ExecuteMethod_StoreObjects(ProcessInputFromStorageOutput.ProcessingResultsToStore);		
				ProcessFetchedInputsImplementation.ExecuteMethod_DeleteObjects(ProcessInputFromStorageOutput.ProcessingResultsToDelete);		
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
				public static void Execute(BeginAccountEmailAddressRegistrationParameters parameters)
		{
						PrepareParameters(parameters);
					BeginAccountEmailAddressRegistrationImplementation.ExecuteMethod_ValidateUnexistingEmail(parameters.EmailAddress);		
				AaltoGlobalImpact.OIP.TBEmailValidation EmailValidation = BeginAccountEmailAddressRegistrationImplementation.GetTarget_EmailValidation(parameters.AccountID, parameters.EmailAddress, parameters.RedirectUrlAfterValidation);	
				BeginAccountEmailAddressRegistrationImplementation.ExecuteMethod_StoreObject(EmailValidation);		
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
				public class UnregisterEmailAddressParameters 
		{
				public string AccountID ;
				public string EmailAddress ;
				}
		
		public class UnregisterEmailAddress 
		{
				private static void PrepareParameters(UnregisterEmailAddressParameters parameters)
		{
					}
				public static void Execute(UnregisterEmailAddressParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.AccountContainer AccountContainerBeforeGroupRemoval = UnregisterEmailAddressImplementation.GetTarget_AccountContainerBeforeGroupRemoval(parameters.AccountID);	
				string EmailAddressID = UnregisterEmailAddressImplementation.GetTarget_EmailAddressID(parameters.EmailAddress, AccountContainerBeforeGroupRemoval);	
				UnregisterEmailAddressImplementation.ExecuteMethod_ExecuteUnlinkEmailAddress(parameters.AccountID, AccountContainerBeforeGroupRemoval, EmailAddressID);		
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
				public static void Execute(InitiateImportedGroupWithUnchangedIDParameters parameters)
		{
						PrepareParameters(parameters);
					TheBall.CORE.IContainerOwner GroupAsOwner = InitiateImportedGroupWithUnchangedIDImplementation.GetTarget_GroupAsOwner(parameters.GroupID);	
				AaltoGlobalImpact.OIP.GroupContainer GroupContainer = InitiateImportedGroupWithUnchangedIDImplementation.GetTarget_GroupContainer(GroupAsOwner);	
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_ValidateGroupContainerID(parameters.GroupID, GroupContainer);		
				AaltoGlobalImpact.OIP.TBRGroupRoot GroupRoot = InitiateImportedGroupWithUnchangedIDImplementation.GetTarget_GroupRoot(parameters.GroupID);	
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_StoreObjects(GroupRoot, GroupContainer);		
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_SetGroupInitiatorAccess(GroupRoot, GroupContainer);		
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_FixContentTypesAndMetadataOfBlobs(GroupAsOwner);		
				InitiateImportedGroupWithUnchangedIDImplementation.ExecuteMethod_FixRelativeLocationsOfInformationObjects(GroupAsOwner);		
				}
				}
				public class CreateGroupWithTemplatesParameters 
		{
				public string GroupName ;
				public string AccountID ;
				public string TemplateNameList ;
				public string GroupDefaultRedirect ;
				public string RedirectUrlAfterCreation ;
				}
		
		public class CreateGroupWithTemplates 
		{
				private static void PrepareParameters(CreateGroupWithTemplatesParameters parameters)
		{
					}
				public static void Execute(CreateGroupWithTemplatesParameters parameters)
		{
						PrepareParameters(parameters);
					string ExecuteCreateGroupOutput = CreateGroupWithTemplatesImplementation.ExecuteMethod_ExecuteCreateGroup(parameters.GroupName, parameters.AccountID);		
				CreateGroupWithTemplatesImplementation.ExecuteMethod_CopyGroupTemplates(parameters.TemplateNameList, ExecuteCreateGroupOutput);		
				IContainerOwner GroupAsOwner = CreateGroupWithTemplatesImplementation.GetTarget_GroupAsOwner(ExecuteCreateGroupOutput);	
				
		{ // Local block to allow local naming
			SetOwnerWebRedirectParameters operationParameters = CreateGroupWithTemplatesImplementation.SetDefaultRedirect_GetParameters(parameters.GroupDefaultRedirect, GroupAsOwner);
			SetOwnerWebRedirect.Execute(operationParameters);
									
		} // Local block closing
				CreateGroupWithTemplatesImplementation.ExecuteMethod_InitializeGroupWithDefaultObjects(GroupAsOwner);		
				CreateGroupWithTemplatesImplementation.ExecuteMethod_RedirectToGivenUrl(parameters.RedirectUrlAfterCreation, ExecuteCreateGroupOutput);		
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
				public static void Execute(SetOwnerWebRedirectParameters parameters)
		{
						PrepareParameters(parameters);
					SetOwnerWebRedirectImplementation.ExecuteMethod_SetRedirection(parameters.Owner, parameters.RedirectPath);		
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
				public static void Execute(ProcessAllResourceUsagesToOwnerCollectionsParameters parameters)
		{
						PrepareParameters(parameters);
					ProcessAllResourceUsagesToOwnerCollectionsImplementation.ExecuteMethod_ExecuteBatchProcessor(parameters.ProcessBatchSize);		
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
				public static ProcessBatchOfResourceUsagesToOwnerCollectionsReturnValue Execute(ProcessBatchOfResourceUsagesToOwnerCollectionsParameters parameters)
		{
						PrepareParameters(parameters);
					Microsoft.WindowsAzure.StorageClient.CloudBlockBlob[] BatchToProcess = ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.GetTarget_BatchToProcess(parameters.ProcessBatchSize, parameters.ProcessIfLess);	
				ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.ExecuteMethod_ProcessBatch(BatchToProcess);		
				ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.ExecuteMethod_DeleteProcessedItems(BatchToProcess);		
				ProcessBatchOfResourceUsagesToOwnerCollectionsImplementation.ExecuteMethod_ReleaseLock(BatchToProcess);		
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
				public static void Execute(UpdateUsageMonitoringSummariesParameters parameters)
		{
						PrepareParameters(parameters);
					UsageMonitorItem[] SourceItems = UpdateUsageMonitoringSummariesImplementation.GetTarget_SourceItems(parameters.Owner, parameters.AmountOfDays);	
				UpdateUsageMonitoringSummariesImplementation.ExecuteMethod_CreateUsageMonitoringSummaries(parameters.Owner, parameters.AmountOfDays, SourceItems);		
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
				Microsoft.WindowsAzure.StorageClient.CloudBlockBlob[] CurrentMonitoringItems = UpdateUsageMonitoringItemsImplementation.GetTarget_CurrentMonitoringItems(parameters.Owner);	
				DateTime EndingTimeOfCurrentItems = UpdateUsageMonitoringItemsImplementation.GetTarget_EndingTimeOfCurrentItems(CurrentMonitoringItems);	
				Microsoft.WindowsAzure.StorageClient.CloudBlockBlob[] NewResourceUsageBlobs = UpdateUsageMonitoringItemsImplementation.GetTarget_NewResourceUsageBlobs(parameters.Owner, EndingTimeOfCurrentItems);	
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
				public static void Execute(PublishGroupToWwwParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.GroupContainer GroupContainer = PublishGroupToWwwImplementation.GetTarget_GroupContainer(parameters.Owner);	
				string TargetContainerName = PublishGroupToWwwImplementation.GetTarget_TargetContainerName(GroupContainer);	
				string TargetContainerOwnerString = PublishGroupToWwwImplementation.GetTarget_TargetContainerOwnerString(TargetContainerName);	
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
				public static void Execute(CreateOrUpdateCustomUIParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.GroupContainer GroupContainer = CreateOrUpdateCustomUIImplementation.GetTarget_GroupContainer(parameters.Owner);	
				CreateOrUpdateCustomUIImplementation.ExecuteMethod_ValidateCustomUIName(parameters.CustomUIName);		
				string CustomUIFolder = CreateOrUpdateCustomUIImplementation.GetTarget_CustomUIFolder(parameters.Owner, parameters.CustomUIName);	
				CreateOrUpdateCustomUIImplementation.ExecuteMethod_SetCustomUIName(parameters.CustomUIName, GroupContainer);		
				CreateOrUpdateCustomUIImplementation.ExecuteMethod_CopyUIContentsFromZipArchive(parameters.ZipArchiveStream, CustomUIFolder);		
				CreateOrUpdateCustomUIImplementation.ExecuteMethod_StoreObject(GroupContainer);		
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
				public static void Execute(DeleteCustomUIParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.GroupContainer GroupContainer = DeleteCustomUIImplementation.GetTarget_GroupContainer(parameters.Owner);	
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
				public static ExportOwnerContentToZipReturnValue Execute(ExportOwnerContentToZipParameters parameters)
		{
						PrepareParameters(parameters);
					string[] IncludedFolders = ExportOwnerContentToZipImplementation.GetTarget_IncludedFolders(parameters.Owner, parameters.PackageRootFolder);	
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
				Microsoft.WindowsAzure.StorageClient.CloudBlockBlob ArchiveBlob = PackageOwnerContentImplementation.GetTarget_ArchiveBlob(ContentPackageObject);	
				Microsoft.WindowsAzure.StorageClient.CloudBlockBlob[] ArchiveSourceBlobs = PackageOwnerContentImplementation.GetTarget_ArchiveSourceBlobs(parameters.Owner, parameters.PackageRootFolder, parameters.IncludedFolders);	
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
				public static void Execute(RemoteDeviceCoreOperationParameters parameters)
		{
						PrepareParameters(parameters);
					INT.DeviceOperationData DeviceOperationData = RemoteDeviceCoreOperationImplementation.GetTarget_DeviceOperationData(parameters.InputStream);	
				DeviceMembership CurrentDevice = RemoteDeviceCoreOperationImplementation.GetTarget_CurrentDevice();	
				RemoteDeviceCoreOperationImplementation.ExecuteMethod_PerformOperation(CurrentDevice, DeviceOperationData);		
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
				public static SyncCopyContentToDeviceTargetReturnValue Execute(SyncCopyContentToDeviceTargetParameters parameters)
		{
						PrepareParameters(parameters);
					AuthenticatedAsActiveDevice AuthenticatedAsActiveDevice = SyncCopyContentToDeviceTargetImplementation.GetTarget_AuthenticatedAsActiveDevice(parameters.AuthenticatedAsActiveDeviceID);	
				string ContentRootLocation = SyncCopyContentToDeviceTargetImplementation.GetTarget_ContentRootLocation(AuthenticatedAsActiveDevice);	
				INT.ContentItemLocationWithMD5[] ThisSideContentMD5List = SyncCopyContentToDeviceTargetImplementation.GetTarget_ThisSideContentMD5List(ContentRootLocation);	
				CallPrepareTargetAndListItemsToCopyReturnValue CallPrepareTargetAndListItemsToCopyOutput = SyncCopyContentToDeviceTargetImplementation.ExecuteMethod_CallPrepareTargetAndListItemsToCopy(AuthenticatedAsActiveDevice, ThisSideContentMD5List);		
				SyncCopyContentToDeviceTargetImplementation.ExecuteMethod_CopyItemsToCopyToTargetDevice(AuthenticatedAsActiveDevice, CallPrepareTargetAndListItemsToCopyOutput);		
				SyncCopyContentToDeviceTargetReturnValue returnValue = SyncCopyContentToDeviceTargetImplementation.Get_ReturnValue(CallPrepareTargetAndListItemsToCopyOutput);
		return returnValue;
				}
				}
				public class SyncCopyContentToDeviceTargetReturnValue 
		{
				public INT.ContentItemLocationWithMD5[] CopiedItems ;
				public INT.ContentItemLocationWithMD5[] DeletedItems ;
				}
				public class MergeAccountsDestructivelyParameters 
		{
				public string PrimaryAccountToStayID ;
				public string AccountToBeMergedAndDestroyedID ;
				}
		
		public class MergeAccountsDestructively 
		{
				private static void PrepareParameters(MergeAccountsDestructivelyParameters parameters)
		{
					}
				public static void Execute(MergeAccountsDestructivelyParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.TBRAccountRoot PrimaryAccountToStay = MergeAccountsDestructivelyImplementation.GetTarget_PrimaryAccountToStay(parameters.PrimaryAccountToStayID);	
				AaltoGlobalImpact.OIP.TBRAccountRoot AccountToBeMerged = MergeAccountsDestructivelyImplementation.GetTarget_AccountToBeMerged(parameters.AccountToBeMergedAndDestroyedID);	
				AaltoGlobalImpact.OIP.TBAccountCollaborationGroup[] GroupAccessToBeMerged = MergeAccountsDestructivelyImplementation.GetTarget_GroupAccessToBeMerged(AccountToBeMerged);	
				AaltoGlobalImpact.OIP.TBAccountCollaborationGroup[] GroupInitiatorAccessToBeTransfered = MergeAccountsDestructivelyImplementation.GetTarget_GroupInitiatorAccessToBeTransfered(AccountToBeMerged);	
				AaltoGlobalImpact.OIP.TBEmail[] EmailAddressesToBeMerged = MergeAccountsDestructivelyImplementation.GetTarget_EmailAddressesToBeMerged(AccountToBeMerged);	
				AaltoGlobalImpact.OIP.TBLoginInfo[] LoginAccessToBeMerged = MergeAccountsDestructivelyImplementation.GetTarget_LoginAccessToBeMerged(AccountToBeMerged);	
				MergeAccountsDestructivelyImplementation.ExecuteMethod_ValidateAccountToBeMerged(GroupAccessToBeMerged, GroupInitiatorAccessToBeTransfered, EmailAddressesToBeMerged);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_RemoveAccountToBeMergedFromAllGroups(AccountToBeMerged, GroupAccessToBeMerged);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_RemoveEmailAddressesFromAccountToBeMerged(AccountToBeMerged);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_RemoveLoginsFromAccountToBeMerged(AccountToBeMerged);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_AddLoginsToPrimaryAccount(PrimaryAccountToStay, LoginAccessToBeMerged);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_AddEmailAddressesToPrimaryAccount(PrimaryAccountToStay, EmailAddressesToBeMerged);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_StorePrimaryAccount(PrimaryAccountToStay);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_CallRefreshAccountRootToReferences(parameters.PrimaryAccountToStayID);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_AddPrimaryAccountToAllGroupsWhereItsMissing(PrimaryAccountToStay, GroupAccessToBeMerged);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_TransferGroupInitiatorRights(parameters.PrimaryAccountToStayID, parameters.AccountToBeMergedAndDestroyedID, GroupInitiatorAccessToBeTransfered);		
				MergeAccountsDestructivelyImplementation.ExecuteMethod_UpdateAccountGroupLogins(parameters.PrimaryAccountToStayID);		
				}
				}
				public class TransferGroupInitiatorParameters 
		{
				public string GroupID ;
				public string OldInitiatorAccountID ;
				public string NewInitiatorAccountID ;
				}
		
		public class TransferGroupInitiator 
		{
				private static void PrepareParameters(TransferGroupInitiatorParameters parameters)
		{
					}
				public static void Execute(TransferGroupInitiatorParameters parameters)
		{
						PrepareParameters(parameters);
					TransferGroupInitiatorImplementation.ExecuteMethod_AddNewInitiatorToGroup(parameters.GroupID, parameters.NewInitiatorAccountID);		
				TransferGroupInitiatorImplementation.ExecuteMethod_RemoveOldInitiatorFromGroup(parameters.GroupID, parameters.OldInitiatorAccountID);		
				}
				}
				public class InitiateAccountMergeFromEmailParameters 
		{
				public string EmailAddress ;
				public string CurrentAccountID ;
				public string RedirectUrlAfterValidation ;
				}
		
		public class InitiateAccountMergeFromEmail 
		{
				private static void PrepareParameters(InitiateAccountMergeFromEmailParameters parameters)
		{
					}
				public static void Execute(InitiateAccountMergeFromEmailParameters parameters)
		{
						PrepareParameters(parameters);
					InitiateAccountMergeFromEmailImplementation.ExecuteMethod_ValidateExistingEmail(parameters.EmailAddress);		
				string AccountToMergeToID = InitiateAccountMergeFromEmailImplementation.GetTarget_AccountToMergeToID(parameters.EmailAddress);	
				InitiateAccountMergeFromEmailImplementation.ExecuteMethod_ValidateAccountNotTheSame(parameters.CurrentAccountID, AccountToMergeToID);		
				AaltoGlobalImpact.OIP.TBEmailValidation MergeAccountEmailConfirmation = InitiateAccountMergeFromEmailImplementation.GetTarget_MergeAccountEmailConfirmation(parameters.CurrentAccountID, parameters.EmailAddress, parameters.RedirectUrlAfterValidation, AccountToMergeToID);	
				InitiateAccountMergeFromEmailImplementation.ExecuteMethod_StoreObject(MergeAccountEmailConfirmation);		
				InitiateAccountMergeFromEmailImplementation.ExecuteMethod_SendConfirmationEmail(MergeAccountEmailConfirmation);		
				}
				}
				public class ConfirmAccountMergeFromEmailParameters 
		{
				public string CurrentAccountID ;
				public AaltoGlobalImpact.OIP.TBEmailValidation EmailConfirmation ;
				}
		
		public class ConfirmAccountMergeFromEmail 
		{
				private static void PrepareParameters(ConfirmAccountMergeFromEmailParameters parameters)
		{
					}
				public static void Execute(ConfirmAccountMergeFromEmailParameters parameters)
		{
						PrepareParameters(parameters);
					AaltoGlobalImpact.OIP.TBMergeAccountConfirmation MergeAccountConfirmation = ConfirmAccountMergeFromEmailImplementation.GetTarget_MergeAccountConfirmation(parameters.EmailConfirmation);	
				ConfirmAccountMergeFromEmailImplementation.ExecuteMethod_ValidateCurrentAccountAsMergingActor(parameters.CurrentAccountID, MergeAccountConfirmation);		
				ConfirmAccountMergeFromEmailImplementation.ExecuteMethod_PerformAccountMerge(MergeAccountConfirmation);		
				}
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
				Microsoft.WindowsAzure.StorageClient.CloudBlockBlob[] BlobsToSync = UpdateOwnerDomainObjectsInSQLiteStorageImplementation.GetTarget_BlobsToSync(parameters.Owner, parameters.SemanticDomain);	
				UpdateOwnerDomainObjectsInSQLiteStorageImplementation.ExecuteMethod_PerformSyncing(DataContextType, DatabaseAttachOrCreateMethodName, SQLiteDBLocationFileName, OwnerRootPath, BlobsToSync);		
				}
				}
		 } 