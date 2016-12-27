using System.Web;
using AzureSupport;
using TheBall.CORE;
using TheBall.CORE.Storage;
using TheBall.Index.INT;

namespace TheBall.Index
{
    public class PerformUserQueryImplementation
    {
        public static UserQuery GetTarget_QueryObject()
        {
            var reqData = LogicalOperationContext.Current.HttpParameters.RequestContent;
            var result = JSONSupport.GetObjectFromData<UserQuery>(reqData);
            return result;
        }

        public static void ExecuteMethod_WriteContentToHttpResponse(QueryToken responseContentObject)
        {
            var httpContext = HttpContext.Current;
            var jsonString = JSONSupport.SerializeToJSONString(responseContentObject);
            httpContext.Response.Write(jsonString);
            httpContext.Response.ContentType = "application/json";
        }

        public static PrepareAndExecuteQueryParameters PerformQuery_GetParameters(UserQuery queryObject)
        {
            return new PrepareAndExecuteQueryParameters
                {
                    QueryString = queryObject.QueryString,
                    IndexName = IndexSupport.DefaultIndexName,
                    DefaultFieldName = queryObject.DefaultFieldName
                };
        }

        public static QueryRequest PerformQuery_GetOutput(PrepareAndExecuteQueryReturnValue operationReturnValue, UserQuery queryObject)
        {
            return operationReturnValue.ActiveRequest;
        }

        public static QueryToken GetTarget_ResponseContentObject(QueryRequest performQueryOutput)
        {
            QueryToken queryToken = new QueryToken
                {
                    QueryRequestObjectDomainName = performQueryOutput.SemanticDomainName, 
                    QueryRequestObjectName = performQueryOutput.Name, 
                    QueryRequestObjectID = performQueryOutput.ID
                };
            return queryToken;
        }
    }
}