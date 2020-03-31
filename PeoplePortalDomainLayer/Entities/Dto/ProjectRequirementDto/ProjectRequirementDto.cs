using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto
{
    public class ProjectRequirementDto
    {
        public Guid ProjectId { get; set; }

        public string DesignationName { get; set; }

        public string SkillName { get; set; }

        public int RequiredEmployee { get; set; }

        public string Comments { get; set; }

        public DateTime RequirementStartDate { get; set; }

        public DateTime RequirementEndDate { get; set; }

        public string RequirementBillingType { get; set; }
    }
}
