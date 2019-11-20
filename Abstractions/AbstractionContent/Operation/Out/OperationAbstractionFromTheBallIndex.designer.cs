 

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

		namespace TheBall.Index { 
				public class ReleaseIndexerResourcesParameters 
		{
				public AttemptToBecomeInfrastructureIndexerReturnValue ResourceInfo ;
				}
		
		public class ReleaseIndexerResources 
		{
				private static void PrepareParameters(ReleaseIndexerResourcesParameters parameters)
		{
					}
				public static void Execute(ReleaseIndexerResourcesParameters parameters)
		{
						PrepareParameters(parameters);
					ReleaseIndexerResourcesImplementation.ExecuteMethod_ReleaseResources(parameters.ResourceInfo);		
				}
				}
				public class AttemptToBecomeInfrastructureIndexerParameters 
		{
				public string IndexName ;
				}
		
		public class AttemptToBecomeInfrastructureIndexer 
		{
				private static void PrepareParameters(AttemptToBecomeInfrastructureIndexerParameters parameters)
		{
					}
				public static AttemptToBecomeInfrastructureIndexerReturnValue Execute(AttemptToBecomeInfrastructureIndexerParameters parameters)
		{
						PrepareParameters(parameters);
					string IndexDriveName = AttemptToBecomeInfrastructureIndexerImplementation.GetTarget_IndexDriveName(parameters.IndexName);	
				AttemptToBecomeInfrastructureIndexerReturnValue MountIndexDriveOutput = AttemptToBecomeInfrastructureIndexerImplementation.ExecuteMethod_MountIndexDrive(IndexDriveName);		
				string QueryQueueName = AttemptToBecomeInfrastructureIndexerImplementation.GetTarget_QueryQueueName(parameters.IndexName);	
				string IndexRequestQueueName = AttemptToBecomeInfrastructureIndexerImplementation.GetTarget_IndexRequestQueueName(parameters.IndexName);	
				AttemptToBecomeInfrastructureIndexerImplementation.ExecuteMethod_EnsureAndRegisterQueues(QueryQueueName, IndexRequestQueueName);		
				AttemptToBecomeInfrastructureIndexerReturnValue returnValue = AttemptToBecomeInfrastructureIndexerImplementation.Get_ReturnValue(MountIndexDriveOutput);
		return returnValue;
				}
				}
				public class AttemptToBecomeInfrastructureIndexerReturnValue 
		{
				public bool Success ;
				public Microsoft.WindowsAzure.StorageClient.CloudDrive CloudDrive ;
				public System.Exception Exception ;
				}
				public class IndexInformationParameters 
		{
				public string IndexingRequestID ;
				public TheBall.Core.IContainerOwner Owner ;
				public string IndexName ;
				public string IndexStorageRootPath ;
				}
		
		public class IndexInformation 
		{
				private static void PrepareParameters(IndexInformationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(IndexInformationParameters parameters)
		{
						PrepareParameters(parameters);
					IndexingRequest IndexingRequest =  await IndexInformationImplementation.GetTarget_IndexingRequestAsync(parameters.Owner, parameters.IndexingRequestID);	
				string LuceneIndexFolder = IndexInformationImplementation.GetTarget_LuceneIndexFolder(parameters.Owner, parameters.IndexName, parameters.IndexStorageRootPath);	
				IndexInformationImplementation.ExecuteMethod_PerformIndexing(parameters.Owner, IndexingRequest, LuceneIndexFolder);		
				 await IndexInformationImplementation.ExecuteMethod_DeleteIndexingRequestAsync(IndexingRequest);		
				}
				}
				public class QueryIndexedInformationParameters 
		{
				public string QueryRequestID ;
				public TheBall.Core.IContainerOwner Owner ;
				public string IndexName ;
				public string IndexStorageRootPath ;
				}
		
		public class QueryIndexedInformation 
		{
				private static void PrepareParameters(QueryIndexedInformationParameters parameters)
		{
					}
				public static async Task ExecuteAsync(QueryIndexedInformationParameters parameters)
		{
						PrepareParameters(parameters);
					QueryRequest QueryRequest =  await QueryIndexedInformationImplementation.GetTarget_QueryRequestAsync(parameters.Owner, parameters.QueryRequestID);	
				string LuceneIndexFolder = QueryIndexedInformationImplementation.GetTarget_LuceneIndexFolder(parameters.Owner, parameters.IndexName, parameters.IndexStorageRootPath);	
				QueryIndexedInformationImplementation.ExecuteMethod_PerformQueryRequest(QueryRequest, LuceneIndexFolder);		
				 await QueryIndexedInformationImplementation.ExecuteMethod_SaveQueryRequestAsync(QueryRequest);		
				}
				}
		
		public class PerformUserQuery 
		{
				public static void Execute()
		{
						
					INT.UserQuery QueryObject = PerformUserQueryImplementation.GetTarget_QueryObject();	
				QueryRequest PerformQueryOutput;
		{ // Local block to allow local naming
			PrepareAndExecuteQueryParameters operationParameters = PerformUserQueryImplementation.PerformQuery_GetParameters(QueryObject);
			var operationReturnValue = PrepareAndExecuteQuery.Execute(operationParameters);
			PerformQueryOutput = PerformUserQueryImplementation.PerformQuery_GetOutput(operationReturnValue, QueryObject);						
		} // Local block closing
				INT.QueryToken ResponseContentObject = PerformUserQueryImplementation.GetTarget_ResponseContentObject(PerformQueryOutput);	
				PerformUserQueryImplementation.ExecuteMethod_WriteContentToHttpResponse(ResponseContentObject);		
				}
				}
				public class PrepareAndExecuteQueryParameters 
		{
				public string QueryString ;
				public string DefaultFieldName ;
				public string IndexName ;
				}
		
		public class PrepareAndExecuteQuery 
		{
				private static void PrepareParameters(PrepareAndExecuteQueryParameters parameters)
		{
					}
				public static PrepareAndExecuteQueryReturnValue Execute(PrepareAndExecuteQueryParameters parameters)
		{
						PrepareParameters(parameters);
					QueryRequest RequestObject = PrepareAndExecuteQueryImplementation.GetTarget_RequestObject(parameters.QueryString, parameters.DefaultFieldName, parameters.IndexName);	
				PrepareAndExecuteQueryImplementation.ExecuteMethod_StoreObject(RequestObject);		
				PrepareAndExecuteQueryImplementation.ExecuteMethod_PutQueryRequestToQueryQueue(parameters.IndexName, RequestObject);		
				PrepareAndExecuteQueryReturnValue returnValue = PrepareAndExecuteQueryImplementation.Get_ReturnValue(RequestObject);
		return returnValue;
				}
				}
				public class PrepareAndExecuteQueryReturnValue 
		{
				public QueryRequest ActiveRequest ;
				}
				public class FilterAndSubmitIndexingRequestsParameters 
		{
				public string[] CandidateObjectLocations ;
				}
		
		public class FilterAndSubmitIndexingRequests 
		{
				private static void PrepareParameters(FilterAndSubmitIndexingRequestsParameters parameters)
		{
					}
				public static void Execute(FilterAndSubmitIndexingRequestsParameters parameters)
		{
						PrepareParameters(parameters);
					string[] ObjectsToIndex = FilterAndSubmitIndexingRequestsImplementation.GetTarget_ObjectsToIndex(parameters.CandidateObjectLocations);	
				IndexingRequest IndexingRequest = FilterAndSubmitIndexingRequestsImplementation.GetTarget_IndexingRequest(ObjectsToIndex);	
				FilterAndSubmitIndexingRequestsImplementation.ExecuteMethod_StoreObject(IndexingRequest);		
				FilterAndSubmitIndexingRequestsImplementation.ExecuteMethod_PutIndexingRequestToQueue(IndexingRequest);		
				}
				}
		 } 