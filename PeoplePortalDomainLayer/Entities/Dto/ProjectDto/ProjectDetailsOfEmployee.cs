using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectDto
{
    public class ProjectDetailsOfEmployee
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string AvailablityType { get; set; }
        public string SecondaryAvailablityType { get; set; }
        public DateTime AssignmentStartDate { get; set; }
        public DateTime? AssignmentEndDate { get; set; }
        public DateTime TotalTimeInProject { get; set; }
    }
}
