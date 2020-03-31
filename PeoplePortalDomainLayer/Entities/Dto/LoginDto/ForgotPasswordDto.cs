using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.LoginDto
{
    public  class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Please enter the email address.")]
        public string companyEmail { get; set; }
    }
}
