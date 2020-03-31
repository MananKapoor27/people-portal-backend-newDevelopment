using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto
{
    public class ProjectRequirementCsvDto
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public string ProjectName { get; set; }

        public Guid? ResourceAllocatedId { get; set; }

        public string ResourceAllocatedName { get; set; }

        public string DesignationName { get; set; }

        public string SkillName { get; set; }

        public string Comments { get; set; }

        public DateTime RequirementStartDate { get; set; }

        public DateTime RequirementEndDate { get; set; }

        public string RequirementBillingType { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }
    }
}
