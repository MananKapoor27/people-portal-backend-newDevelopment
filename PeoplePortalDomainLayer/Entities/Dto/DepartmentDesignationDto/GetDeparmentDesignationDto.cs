using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.DepartmentDesignationDto
{
    public class GetDeparmentDesignationDto
    {

        /// <summary>
        /// this property is used to get department id 
        /// </summary>
        public int departmentId { get; set; }
        /// <summary>
        /// this property is used to get list of new designation titles
        /// </summary>
        public int designationId { get; set; }
    }
}
