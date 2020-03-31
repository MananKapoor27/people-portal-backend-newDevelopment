using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
    public class DownloadListOfAllEmployeesDto
    {
        public string? CompanyId { get; set; }
        public string? Name { get; set; }
        public string? ActivityStatus { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public string? EmployeePrimaryType { get; set; }
        public string? EmployeeSecondaryType { get; set; }
        public string? ReportingManager { get; set; }
        //[NotMapped]
        //public List<string> PrimarySkills { get; set; }
        //[NotMapped]
        //public List<string> SecondarySkills { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfExit { get; set; }
        public string? ProjectName { get; set; }
        public string? ClientName { get; set; }
        public string? PrimaryStatus { get; set; }
        public string? SecondaryStatus { get; set; }
        public DateTime? AllocationStartDate { get; set; }
        public DateTime? AllocationEndDate { get; set; }
        public int? TotalDaysInProject { get; set; }
        public int? TotalMonthsInNineleaps { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }

    }
}
