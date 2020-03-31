using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto
{
    /// <summary>
    /// this class is used to get feature and level mappings
    /// </summary>
    public class FeatureLevelDetailsDto
    {
        /// <summary>
        /// this property is used to get and set level id
        /// </summary>
        public int LevelId { get; set; }
        ///// <summary>
        ///// this property is used to get and set list of feature is corresponding to that level
        ///// </summary>
        //public List<int> FeatureId { get; set; }
    }
}
