/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from Basic_Configurations table
    /// </summary>   
    public class BasicConfiguration
    {
        /// <summary>
        /// this property is used to get and set Level Data
        /// </summary>        
        /// <remarks>
        /// this property is used to create a navigation property for the Level class 
        /// </remarks>
        public Level Level { get; set; }
        /// <summary>
        /// this property is used to get and set LevelId
        /// </summary>
        [Key, Column(Order = 1), ForeignKey("Level")]
        public int LevelId { get; set; }
        /// <summary>
        /// this property is used to save the list of accessible features for a particular level
        /// </summary>
        public string AccessibleFeaturesList { get; set; }
        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
