using System.Threading.Tasks;
using Google.Apis.Auth;
using PeoplePortalDomainLayer.Entities.Dto.LoginDto;
using PeoplePortalDomainLayer.Entities.DTO.LoginDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalServices.Interfaces
{
    public interface ILoginService
    {

        Task<Login> CheckIfUserExists(string companyEmail);

        Task<LoginUserDetailsDto> ValidateCredentials(Login result, LoginCredentialsDto credentials);

        /// <summary>
        /// this method is used to authorize user from google
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        Task<GoogleJsonWebSignature.Payload> AuthorizeGoogleSignInUser(string tokenString);

        /// <summary>
        /// this method is used to check if user exists in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<LoginUserDetailsDto> ValidateGoogleSignInUser(Login user);

        Task<string> GenerateJwtToken(string companyEmail);

        Task<string> GeneratePasswordResetToken();

        bool CheckTokenExpiryTime(string resetPasswordtoken);

        Task<bool> SendEmailToUser(Login userDetails, string resetPasswordToken);

        Task<bool> UpdateResetPassword(string companyEmail, string newPassword);

    }
}