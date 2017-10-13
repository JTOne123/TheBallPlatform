using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using TheBall.CORE.InstanceSupport;

namespace TheBall.CORE
{
    public class SetOwnerWebRedirectImplementation
    {
        public static async Task ExecuteMethod_SetRedirectionAsync(IContainerOwner owner, string redirectPath)
        {
            CloudBlockBlob redirectBlob = StorageSupport.GetOwnerBlobReference(owner,
                                                                  InfraSharedConfig.Current.RedirectFromFolderFileName);
            if (string.IsNullOrEmpty(redirectPath))
                await redirectBlob.DeleteIfExistsAsync();
            else
                await redirectBlob.UploadTextAsync(redirectPath);
            
        }
    }
}