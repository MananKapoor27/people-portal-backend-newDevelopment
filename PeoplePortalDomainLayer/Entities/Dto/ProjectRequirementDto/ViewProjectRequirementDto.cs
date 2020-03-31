using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto
{
    public class ViewProjectRequirementDto
    {
        public Guid Id { get; set; }

        public string DesignationName { get; set; }

        public string SkillName { get; set; }

        public string Comments { get; set; }

        public DateTime RequirementStartDate { get; set; }

        public DateTime RequirementEndDate { get; set; }

        public string RequirementBillingType { get; set; }

        public string Status { get; set; }

        public string DeletionReason { get; set; }
    }
}
