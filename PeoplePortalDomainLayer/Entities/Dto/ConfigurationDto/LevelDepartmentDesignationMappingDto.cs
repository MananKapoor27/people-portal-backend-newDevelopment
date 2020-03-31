using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.ConfigurationDto
{
    /// <summary>
    /// this class is used to get details of configuration related to department, levels and designations mapping
    /// </summary>
    public class LevelDepartmentDesignationMappingDto
    {
        /// <summary>
        /// this property is used to get and set department id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// this property is used to get and set department name
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// this property is used to get and set list of level designation mapping
        /// </summary>
        public List<LevelDesignationDto> LevelDesignations { get; set; }
    }
}
