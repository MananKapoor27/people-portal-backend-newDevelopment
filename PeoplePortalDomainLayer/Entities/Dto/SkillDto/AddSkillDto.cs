using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.SkillDto
{
    /// <summary>
    /// this class is used to update skills
    /// </summary>
    public class AddSkillDto
    {
        /// <summary>
        /// this property is used to get and set Skill name
        /// </summary>
        [Required(ErrorMessage = "Skill name is required")]
        public string Name { get; set; }
    }
}
