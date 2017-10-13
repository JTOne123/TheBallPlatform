using System.Threading.Tasks;
using TheBall.CORE;
using TheBall.Interface.INT;

namespace TheBall.Interface
{
    public class PublishCollaborationContentOverConnectionImplementation
    {
        public static async Task<Connection> GetTarget_ConnectionAsync(string connectionId)
        {
            return await ObjectStorage.RetrieveFromOwnerContentA<Connection>(InformationContext.CurrentOwner, connectionId);
        }


        public static async Task ExecuteMethod_CallOtherSideProcessingForCopiedContentAsync(Connection connection, bool callDeviceSyncToSendContentOutput)
        {
            if (callDeviceSyncToSendContentOutput == false)
                return;
            ConnectionCommunicationData connectionCommunication = new ConnectionCommunicationData
            {
                ActiveSideConnectionID = connection.ID,
                ProcessRequest = "PROCESSPUSHEDCONTENT",
                ReceivingSideConnectionID = connection.OtherSideConnectionID
            };
            await DeviceSupport.ExecuteRemoteOperationVoid(connection.DeviceID,
                                                     "TheBall.Interface.ExecuteRemoteCalledConnectionOperation", 
                                                     connectionCommunication);
        }

        public static SyncConnectionContentToDeviceToSendParameters CallSyncConnectionContentToDeviceToSend_GetParameters(Connection connection)
        {
            return new SyncConnectionContentToDeviceToSendParameters {Connection = connection};
        }

        public static async Task<bool> ExecuteMethod_CallDeviceSyncToSendContentAsync(Connection connection)
        {
            var result = await SyncCopyContentToDeviceTarget.ExecuteAsync(
                new SyncCopyContentToDeviceTargetParameters { AuthenticatedAsActiveDeviceID = connection.DeviceID});
            return result.CopiedItems.Length > 0 || result.DeletedItems.Length > 0;
        }
    }
}