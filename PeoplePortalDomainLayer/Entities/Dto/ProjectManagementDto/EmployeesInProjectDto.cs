using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto

{
    public class EmployeesInProjectDto
    {
        public Guid ProjectId { get; set; }

        public Guid? RequirementId { get; set; }

        public GetEmployeeDetailDto EmployeeDetails { get; set; }
    }
}
