using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.DepartmentDesignationDto
{
    /// <summary>
    /// this class is used to get data related to department and all its designations
    /// </summary>
    public class DepartmentDesignationDto
    {
        /// <summary>
        /// this property is used to get department id 
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// this property is used to get and set new level name
        /// </summary>
        public string NewLevelName { get; set; }
        /// <summary>
        /// this property is used to get list of designation id 
        /// </summary>
        public List<int> DesignationId { get; set; }
    }
}
