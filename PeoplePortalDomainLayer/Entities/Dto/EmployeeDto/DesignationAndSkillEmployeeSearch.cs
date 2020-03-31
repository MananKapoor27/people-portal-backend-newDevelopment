using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
    public class DesignationAndSkillEmployeeSearch
    {
        public int? DesignationId { get; set; }

        public string SkillName { get; set; }

        public string searchString { get; set; }
    }
}
