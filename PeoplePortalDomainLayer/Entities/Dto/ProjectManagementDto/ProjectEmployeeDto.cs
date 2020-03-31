using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto
{
    public class ProjectEmployeeDto
    {
        public int ProjectId { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
