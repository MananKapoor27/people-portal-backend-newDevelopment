using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.EmployeeDto
{
    /// <summary>
    /// this view-model is used for Employee's social handle Links
    /// </summary> 
    public class EmployeeLinksDto
    {
        /// <summary>
        /// this property is used to get and set employee's social handles title eg: github, linkedin, facebook
        /// </summary>
        public string HandleTitle { get; set; }
        /// <summary>
        /// this property is used to get and set employee's social handles link
        /// </summary>
        public string Link { get; set; }
    }
}
