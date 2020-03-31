using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.DepartmentDesignationDto
{
    /// <summary>
    /// this class is used to get new designations of a department
    /// </summary>
    public class AddNewDesignationDto
    {
        /// <summary>
        /// this property is used to get department id 
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// this property is used to get list of new designation titles
        /// </summary>
        public List<string> NewDesignations { get; set; }
    }
}
