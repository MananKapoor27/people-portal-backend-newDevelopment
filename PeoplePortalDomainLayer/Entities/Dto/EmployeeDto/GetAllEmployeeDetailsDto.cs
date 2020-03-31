using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.EmployeeDto
{
    public class GetAllEmployeeDetailsDto
    {
        /// <summary>
        /// this property is used to get and set employee Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// this property is used to get and set employee first name
        /// </summary>
        public string Name { get; set; }
        ///// <summary>
        ///// this property is used to get and set employee middle name
        ///// </summary>
        //public string MiddleName { get; set; }
        ///// <summary>
        ///// this property is used to get and set employee last name
        ///// </summary>
        //public string LastName { get; set; }
        /// <summary>
        /// this property is used to get and set employee Primary Skill's Experience in years
        /// </summary>
        public int? Experience { get; set; }
        /// <summary>
        /// this property is used to get and set experience of the employee
        /// </summary>
        public string DesignationTitle { get; set; }
        /// <summary>
        /// this property is used to get and set employee Company Id
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// this property is used to get and set employee's primary status
        /// </summary>
        public string PrimaryStatus { get; set; }
    }
}
