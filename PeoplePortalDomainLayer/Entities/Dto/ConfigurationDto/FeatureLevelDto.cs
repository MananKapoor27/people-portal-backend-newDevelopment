using System;
using System.Collections.Generic;
using System.Text;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDomainLayer.Entities.DTO.ConfigurationDto
{
    /// <summary>
    /// this class is used to get details of configuration related to levels and feature mapping
    /// </summary>
    public class FeatureLevelDto
    {
        /// <summary>
        /// this property is used to get and set level id
        /// </summary>
        public int LevelId { get; set; }
        ///// <summary>
        ///// this property is used to get and set list of feature details
        ///// </summary>
        //public List<Feature> Features { get; set; }
    }
}
