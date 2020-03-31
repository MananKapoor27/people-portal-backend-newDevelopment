using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.LoginDto
{
    public class LoginCredentialsDto
    {
        /// <summary>
        /// this property is used to get and set employee company email 
        /// </summary>
        public string companyEmail { get; set; }

        /// <summary>
        /// this property is used to get and set employee login password 
        /// </summary>
        public string password { get; set; }

    }
}
