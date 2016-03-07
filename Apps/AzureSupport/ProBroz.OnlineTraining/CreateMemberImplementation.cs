using System.Threading.Tasks;
using TheBall;

namespace ProBroz.OnlineTraining
{
    public class CreateMemberImplementation
    {
        public static Member GetTarget_MemberToCreate(INT.Member memberData)
        {
            return new Member
            {
                FirstName = memberData.FirstName,
                LastName = memberData.LastName,
                MiddleName = memberData.MiddleName,
                BirthDay = memberData.BirthDay,
                Email = memberData.Email,
                PhoneNumber = memberData.PhoneNumber,
                Address = memberData.Address,
                Address2 = memberData.Address2,
                ZipCode = memberData.ZipCode,
                PostOffice = memberData.PostOffice,
                Country = memberData.Country,
                FederationLicense = memberData.FederationLicense,
                PhotoPermission = memberData.PhotoPermission,
                VideoPermission = memberData.VideoPermission
            };
        }

        public static async Task ExecuteMethod_StoreObjectAsync(Member memberToCreate)
        {
            await StorageSupport.StoreInformationAsync(memberToCreate, InformationContext.CurrentOwner);
        }
    }
}