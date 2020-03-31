using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeSkillDto
{
    public class EmployeeSkillDto
    {
        public Guid EmployeeId { get; set; }

        public List<string> PrimarySkills { get; set; }

        public List<string> SecondarySkills { get; set; }

        public int ExpertiseLevel { get; set; }
    }
}
