using System;
using System.Collections.Generic;
using System.Text;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDomainLayer.Entities.DTO.ConfigurationDto
{ /// <summary>
    /// this class is used to get details of configuration related to levels and designations mapping
    /// </summary>
    public class LevelDesignationDto
    {
        /// <summary>
        /// this property is used to get and set level id
        /// </summary>
        public int LevelId { get; set; }
        /// <summary>
        /// this property is used to get and set level name
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// this property is used to get and set list of designation details
        /// </summary>
        public List<Designation> Designations { get; set; }
    }
}
