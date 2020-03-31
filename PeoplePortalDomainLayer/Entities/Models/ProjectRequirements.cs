using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from ProjectRequirements table
    /// </summary>
    public class ProjectRequirements
    {
        /// <summary>
        /// this property is used to get and set ProjectRequirements id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key]
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        /// <summary>
        /// this property is used to get and set Skill name
        /// </summary>
        [Required(ErrorMessage = "Designation name is required")]
        public string DesignationName { get; set; }

        public string SkillName { get; set; }

        public DateTime RequirementStartDate { get; set; }

        public DateTime RequirementEndDate { get; set; }

        public string RequirementBillingType { get; set; }

        public string Comments { get; set; }

        public Guid ResourceAllocated { get; set; }

        public bool IsFullfilled { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletionReason { get; set; }

        public Guid? CreatedBy {get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }
    }
}
