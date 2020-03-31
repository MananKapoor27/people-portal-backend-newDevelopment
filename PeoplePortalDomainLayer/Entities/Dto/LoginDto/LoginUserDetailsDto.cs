using System;

namespace PeoplePortalDomainLayer.Entities.DTO.LoginDto
{
    public class LoginUserDetailsDto
    {
        /// <summary>
        /// this property is used to get and set employee company email 
        /// </summary>
        public string CompanyEmail { get; set; }

        /// <summary>
        /// this property is used to get and set employee name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// this property is used to get and set jwt token
        /// </summary>
        public string jwtToken { get; set; }

        /// <summary>
        /// this property is used to get and set Guid Id of employee
        /// </summary>
        public Guid employeeId { get; set; }

    }
}