using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.DTO.LoginDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalDomainLayer.HelperModels.GoogleSignInHelperModels;
using System.Net.Http;
using Google.Apis.Auth;
using Microsoft.AspNetCore.DataProtection;
using PeoplePortalDomainLayer.Entities.Dto.LoginDto;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace PeoplePortalServices.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;

        static HttpClient client = new HttpClient();

        /// <summary>
        /// this is the default constructor for the Login service
        /// </summary>
        /// <param name="loginRepository">loginRepository interface object for dependency injection</param>
        public LoginService(ILoginRepository loginRepository, IEmployeeRepository employeeRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _employeeRepository = employeeRepository;
            _configuration = configuration;
        }

        public async Task<Login> CheckIfUserExists(string companyEmail)
        {
            //checking whether the companyEmail exists or not and is the user soft-deleted or not. 
            var result = await _loginRepository.checkUser(companyEmail);
            return result;
        }



        public async Task<LoginUserDetailsDto> ValidateCredentials(Login result, LoginCredentialsDto credentials)
        {
            LoginUserDetailsDto loginDetails = new LoginUserDetailsDto();

            var hashedPassword = SHA256Hash(credentials.password);

            if (hashedPassword == result.Password)
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(result.EmployeeId);

                loginDetails.Name = employee.FirstName + " " + employee.LastName;
                loginDetails.jwtToken = await GenerateJwtToken(credentials.companyEmail);
                loginDetails.CompanyEmail = credentials.companyEmail;
                loginDetails.employeeId = result.EmployeeId;

                return loginDetails;
            }

            return null;
        }

        /// <summary>
        /// this method is used to authorize user from google
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        public async Task<GoogleJsonWebSignature.Payload> AuthorizeGoogleSignInUser(string tokenString)
        {
            var postData = new GoogleAuthRequestData
            {
                grant_type = _configuration.GetSection("GoogleSignIn").GetSection("AccessType").Value,
                code = tokenString,
                client_id = _configuration.GetSection("GoogleSignIn").GetSection("ClientId").Value,
                client_secret = _configuration.GetSection("GoogleSignIn").GetSection("ClientSecret").Value,
                redirect_uri = _configuration.GetSection("GoogleSignIn").GetSection("FrontEndURI").Value
            };

            HttpResponseMessage response = await client.PostAsJsonAsync("https://accounts.google.com/o/oauth2/token", postData);
            if (response.IsSuccessStatusCode)
            {
                var googleResponse = await response.Content.ReadAsAsync<GoogleAuthResponseData>();
                var payload = GoogleJsonWebSignature.ValidateAsync(googleResponse.id_token, new GoogleJsonWebSignature.ValidationSettings()).Result;
                return payload;
            }
            return null;
        }

        /// <summary>
        /// this method is used to check if user exists in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<LoginUserDetailsDto> ValidateGoogleSignInUser(Login user)
        {
            LoginUserDetailsDto loginDetails = new LoginUserDetailsDto();
            var employee = await _employeeRepository.GetEmployeeByIdAsync(user.EmployeeId);

            loginDetails.Name = employee.FirstName + " " + employee.LastName;
            loginDetails.jwtToken = await GenerateJwtToken(user.CompanyEmail);
            loginDetails.CompanyEmail = user.CompanyEmail;
            loginDetails.employeeId = user.EmployeeId;

            return loginDetails;
        }

        public async Task<bool> UpdateResetPassword(string companyEmail, string newPassword)
        {
            var loginUser = await _loginRepository.checkUser(companyEmail);
            if (loginUser != null)
            {
                var newHashedPassword = SHA256Hash(newPassword);
                var passwordUpdated = await _loginRepository.UpdatePassword(loginUser, newHashedPassword);
                return passwordUpdated;
            }


            return false;
        }

        public async Task<string> GenerateJwtToken(string companyEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSecretKey")));
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, companyEmail) }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public async Task<string> GeneratePasswordResetToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSecretKey")));
            var descriptor = new SecurityTokenDescriptor
            {
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public string SHA256Hash(string password)
        {
            //Create SHA256 Hash of the password and return byte-array

            using (SHA256 sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public bool CheckTokenExpiryTime(string resetPasswordtoken)
        {
            JwtSecurityToken decodedToken= null;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            try
            {
                 decodedToken = handler.ReadJwtToken(resetPasswordtoken);
            }
            catch (Exception ex)

            { var a =ex.Message; }

            if (DateTime.UtcNow > decodedToken.ValidTo.ToUniversalTime())
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SendEmailToUser(Login userDetails, string resetPasswordToken)
        {
            bool emailSent = false;

            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(userDetails.EmployeeId);
                var url = "https://netleaps.firebaseapp.com/new?token=" + resetPasswordToken + "&Email=" + employee.CompanyEmail + "&Name=" + employee.FirstName + " " + employee.LastName;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("PeoplePortal", "peopleportal123@gmail.com"));
                message.To.Add(new MailboxAddress(userDetails.CompanyEmail));
                message.Subject = "Password Reset Link";
                message.Body = new TextPart("plain")
                {
                    Text = url,
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    //SMTP server authentication if needed
                    client.Authenticate("peopleportal123@gmail.com", "Pa$$word1!");
                    client.Send(message);
                    client.Disconnect(true);
                }

                emailSent = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return emailSent;
        }

    }
}
