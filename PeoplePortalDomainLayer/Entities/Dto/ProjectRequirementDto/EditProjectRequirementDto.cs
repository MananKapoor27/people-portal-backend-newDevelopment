using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto
{
    public class EditProjectRequirementDto
    {
        public Guid Id { get; set; }

        public string DesignationName { get; set; }

        public string SkillName { get; set; }

        public string Comments { get; set; }

        public DateTime RequirementStartDate { get; set; }

        public DateTime RequirementEndDate { get; set; }

        public string RequirementBillingType { get; set; }
    }
}
