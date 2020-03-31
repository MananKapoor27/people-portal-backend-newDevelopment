using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ReportingManagerDto
{
    public class ReportingManagerDto
    {
        /// <summary>
        /// this property is used to get and set Reporting Manager
        /// </summary>
        public List<ReporteeDetailsDto> EmployeeDetail { get; set; }
        public Guid ReportingManagerId { get; set; }
        public string Message { get; set; }
        public bool Successful { get; set; }
        public int Count { get; set; }
    }
}
