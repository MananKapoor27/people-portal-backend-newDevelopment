using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectDto
{
    /// <summary>
    /// this view-model is used to fetch data of employee that are related to projects
    /// </summary> 
    public class EmployeeBasicDetailsDto
    {
        /// <summary>
        /// this property is used to get and set employee first name
        /// </summary>
        public string Name { get; set; }     

        /// <summary>
        /// this property is used to get and set Employee that exist in project
        /// </summary>
        public Guid Id { get; set; }

        public string CompanyId { get; set; }

        public GetEmployeeListDto ProjectManagerDetails { get; set; }

        public GetEmployeeListDto ReportingManagerDetails { get; set; }

        public string Designation { get; set; }
        public string Department { get; set; }
        public string PrimaryStatus { get; set; }

        public string SecondaryStatus { get; set; }

        /// <summary>
        /// this property is used to get and set Project Allocated Start Date
        /// </summary>
        public DateTime? AllocationStartDate { get; set; }

        /// <summary>
        /// this property is used to get and set Project Allocated End Date
        /// </summary>
        public DateTime? AllocationEndDate { get; set; }

        public string Role { get; set; }

        public bool IsManager { get; set; }

    }
}
