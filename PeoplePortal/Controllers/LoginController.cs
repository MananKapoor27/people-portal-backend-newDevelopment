using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.LoginDto;
using PeoplePortalDomainLayer.Entities.DTO.LoginDto;
using PeoplePortalServices.Interfaces;
using System.Net.Http;
using PeoplePortalDomainLayer.HelperModels.GoogleSignInHelperModels;

namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>LoginController</c> class handles all login related operations
    /// </summary>
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        static HttpClient client = new HttpClient();

        /// <summary>
        /// this is a default constructor for LoginController
        /// </summary>
        /// <param name="loginService">object of login service for dependency injection</param>
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginUserDetailsDto>> LoginCheck([FromBody]LoginCredentialsDto loginCredentials)
        {
            var result = await _loginService.CheckIfUserExists(loginCredentials.companyEmail);

            if (result == null)
            {
                return BadRequest("Given email address doesn't exists.");
            }

            var validateCredentials = await _loginService.ValidateCredentials(result, loginCredentials);

            if (validateCredentials == null)
            {
                return BadRequest("Incorrect Password.");
            }

            return Ok(validateCredentials);
        }


        [HttpPost("forgotPassword")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ForgotPassword([FromBody]ForgotPasswordDto forgotPasswordDto)
        {
            var result = await _loginService.CheckIfUserExists(forgotPasswordDto.companyEmail);

            if (result == null)
            {
                return BadRequest("Given email address doesn't exists.");
            }

            var token = await _loginService.GeneratePasswordResetToken();
            //var token = _loginService.SendEmailToUser(forgotPasswordDto);

            if (token != null)
            {
                var emailSentToUser = await _loginService.SendEmailToUser(result, token);

                if (emailSentToUser)
                    return Ok("Password Reset Link has been sent to your email address.");
            }

            return BadRequest("Some error occured !");
        }

        [HttpPost("resetPassword")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ResetPassword([FromBody]ResetPasswordDto resetPasswordDto)
        {
            var checkTokenStatus = _loginService.CheckTokenExpiryTime(resetPasswordDto.token);

            if(!checkTokenStatus)
            {
                return BadRequest("Your token has expired. Please reset your password again.");
            }

            var result = await _loginService.UpdateResetPassword(resetPasswordDto.companyEmail, resetPasswordDto.newPassword);

            if (result == true)
            {
                return Ok("Your password has been changed successfully.");
            }

            return BadRequest("We encountered an error while changing your password !!");
        }

        [HttpPost("googleSignIn")]
        public async Task<ActionResult<LoginUserDetailsDto>> GoogleSignIn([FromBody]GoogleSignInDto googleSignInDto)
        {
            try
            {
                var payload = await _loginService.AuthorizeGoogleSignInUser(googleSignInDto.tokenString);
                if (payload != null)
                {
                    var user = await _loginService.CheckIfUserExists(payload.Email);

                    if (user == null)
                    {
                        return BadRequest("Given email address doesn't exists.");
                    }

                    var validateCredentials = await _loginService.ValidateGoogleSignInUser(user);

                    if (validateCredentials == null)
                    {
                        return BadRequest("Incorrect Password.");
                    }

                    return Ok(validateCredentials);
                }
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return BadRequest();
        }
    }
}
