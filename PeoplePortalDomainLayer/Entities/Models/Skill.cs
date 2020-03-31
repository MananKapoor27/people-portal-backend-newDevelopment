/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 * No part of this software can be modified or edited without permissions */
using System.ComponentModel.DataAnnotations;

namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from Skill table
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// this property is used to get and set Skill id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set Skill name
        /// </summary>
        [Required(ErrorMessage = "Skill name is required")]
        public string Name { get; set; }
    }
}
