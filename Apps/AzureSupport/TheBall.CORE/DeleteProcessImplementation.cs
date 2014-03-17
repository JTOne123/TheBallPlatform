namespace TheBall.CORE
{
    public class DeleteProcessImplementation
    {
        public static Process GetTarget_Process(string processId)
        {
            return Process.RetrieveFromOwnerContent(InformationContext.CurrentOwner, processId);
        }

        public static ProcessContainer GetTarget_OwnerProcessContainer()
        {
            return ProcessContainer.RetrieveFromOwnerContent(InformationContext.CurrentOwner, "default");
        }

        public static void ExecuteMethod_ObtainLockRemoveFromContainerAndDeleteProcess(string processID, Process process, ProcessContainer ownerProcessContainer)
        {
            if (process == null)
            {
                ownerProcessContainer.ProcessIDs.Remove(processID);
            }
            else
            {
                string lockEtag = process.ObtainLockOnObject();
                if (lockEtag == null)
                    return;
                try
                {
                    ownerProcessContainer.ProcessIDs.Remove(process.ID);
                    ownerProcessContainer.StoreInformation();
                    process.DeleteInformationObject();
                }
                finally
                {
                    process.ReleaseLockOnObject(lockEtag);
                }
            }

        }
    }
}