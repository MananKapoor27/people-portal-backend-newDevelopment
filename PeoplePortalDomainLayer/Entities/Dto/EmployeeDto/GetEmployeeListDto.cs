using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
    public class GetEmployeeListDto
    {
        /// <summary>
        /// this property is used to get and set employee id  
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// this property is used to get and set employee name
        /// </summary>
        public string Name { get; set; }
    }
}
