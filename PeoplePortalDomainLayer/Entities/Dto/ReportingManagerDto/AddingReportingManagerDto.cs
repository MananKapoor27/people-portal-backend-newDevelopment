using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ReportingManagerDto
{
    public class AddingReportingManagerDto
    {
        /// <summary>
        /// this property is used to get and set Reporting Manager
        /// </summary>
        public Guid ReportingManagerId { get; set; }
        /// <summary>
        /// this property is used to get and set Reporting Manager
        /// </summary>
        public List<Guid> EmployeeId { get; set; }
    }
}
