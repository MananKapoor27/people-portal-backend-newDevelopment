using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto
{
    public class GetReportingManagerDto
    {
        /// <summary>
        /// this property is used to get and set employee id  
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// this property is used to get and set employee name
        /// </summary>
        public string Name { get; set; }

        public Guid? ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
