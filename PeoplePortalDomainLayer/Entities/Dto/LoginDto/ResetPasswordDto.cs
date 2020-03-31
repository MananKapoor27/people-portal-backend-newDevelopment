using System.ComponentModel.DataAnnotations;

namespace PeoplePortalDomainLayer.Entities.Dto.LoginDto
{
    public class ResetPasswordDto
    {
        [Required]
        public string token { get; set; }

        [Required]
        public string companyEmail { get; set; }

        [Required(ErrorMessage = "Please enter the email address.")]
        public string newPassword { get; set; }

    }
}
