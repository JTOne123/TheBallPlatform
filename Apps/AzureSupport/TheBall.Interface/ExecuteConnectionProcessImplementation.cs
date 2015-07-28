using System;
using TheBall.CORE;

namespace TheBall.Interface
{
    public class ExecuteConnectionProcessImplementation
    {
        private static IContainerOwner Owner
        {
            get { return InformationContext.CurrentOwner; }
        }
        public static Connection GetTarget_Connection(string connectionId)
        {
            return ObjectStorage.RetrieveFromOwnerContent<Connection>(Owner, connectionId);
        }

        public static void ExecuteMethod_PerformProcessExecution(string connectionProcessToExecute, Connection connection)
        {
            string processID;
            switch (connectionProcessToExecute)
            {
                case "UpdateConnectionThisSideCategories":
                    processID = connection.ProcessIDToUpdateThisSideCategories;
                    break;
                case "ProcessReceived":
                    processID = connection.ProcessIDToProcessReceived;
                    break;
                default:
                    throw new NotImplementedException("Connection process execution not implemented for: " + connectionProcessToExecute);
            }
            ExecuteProcess.Execute(new ExecuteProcessParameters
            {
                ProcessID = processID
            });
        }
    }
}