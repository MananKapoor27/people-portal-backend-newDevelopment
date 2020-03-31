using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto
{
    public class DeleteRequirementDto
    {
        public Guid RequirementId { get; set; }

        public string DeletionReason { get; set; }
    }
}
