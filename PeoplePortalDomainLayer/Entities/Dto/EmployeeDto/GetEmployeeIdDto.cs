using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.EmployeeDto
{
    /// <summary>
    /// this view-model is used for getting Id set as the primary key
    /// </summary>
    public class GetEmployeeIdDto
    {
        /// <summary>
        /// this property is used to get and set employee id  
        /// </summary>
        public Guid Id { get; set; }
    }
}
