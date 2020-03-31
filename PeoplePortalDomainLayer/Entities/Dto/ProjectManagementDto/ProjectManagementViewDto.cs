using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto
{
    public class ProjectManagementViewDto
    {
        /// <summary>
        /// this property is used to get and set Reporting Manager
        /// </summary>
        public Guid EmployeeId { get; set; }
        
        /// <summary>
        /// this property is used to get and set Project Id
        /// </summary>
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// this property is used to get and set Project  name
        /// </summary> 
        public string ProjectName { get; set; }

        /// <summary>
        /// this property is used to get and set Project Allocated Start Date
        /// </summary>
        public DateTime? AllocationStartDate { get; set; }

        /// <summary>
        /// this property is used to get and set Project Allocated End Date
        /// </summary>
        public DateTime? AllocationEndDate { get; set; }

        public GetEmployeeListDto ProjectManagerDetails { get; set; }

        public GetEmployeeListDto ReportingManagerDetails { get; set; }

        public string PrimaryStatus { get; set; }

        public string SecondaryStatus { get; set; }

        public string Role { get; set; }
    }
}
