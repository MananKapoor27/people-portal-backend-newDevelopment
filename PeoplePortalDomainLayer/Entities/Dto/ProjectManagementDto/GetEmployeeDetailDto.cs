using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto

{
    public class GetEmployeeDetailDto
    {
        /// <summary>
        /// this property is used to get and set Employee that exist in project
        /// </summary>
        public Guid EmployeeId { get; set; }

        public Guid? ProjectManager { get; set; }

        public Guid? ProjectReportingManager { get; set; }

        /// <summary>
        /// this property is used to get and set Project Allocated Start Date
        /// </summary>
        public DateTime? AllocationStartDate { get; set; }

        public DateTime? AllocationEndDate { get; set; }

        public string PrimaryStatus { get; set; }

        public string SecondaryStatus { get; set; }

        public string Role { get; set; }

        public bool IsManager { get; set; }
    }
}
