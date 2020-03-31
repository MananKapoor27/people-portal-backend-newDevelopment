using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    public interface ILoginRepository 
    {
        Task<Login> checkUser(string companyEmail);

        Task SaveChangesAsync();

        Task<bool> UpdatePassword(Login userDetails,string newPassword);
    }
}