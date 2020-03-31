using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDomainLayer.Entities.Dto.ConfigurationDto
{
    /// <summary>
    /// this dto is used as the object for adding basic configuration to db
    /// </summary>
    public class BasicConfigurationDto
    {
        /// <summary>
        /// this property is used to get and set LevelId
        /// </summary>
        public int LevelId { get; set; }
        /// <summary>
        /// this property is used to save the list of accessible features for a particular level
        /// </summary>
        public List<string> AccessibleFeaturesList { get; set; }
    }
}
