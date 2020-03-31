using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectDto
{
    public class GetProjectsDetailsDto
    {
        /// <summary>
        /// this property is used to get and set Project Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// this property is used to get and set Project Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// this property is used to get and set Project  Start Date 
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// this property is used to get and set Project  End Date 
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// this property is used to get and set Description of the Project
        /// </summary>
        public string ProjectDescription { get; set; }       
        /// <summary>
        /// this property is used to get and set Client of the Project
        /// </summary>
        public string ProjectClient { get; set; }
        /// <summary>
        /// this property is used to get and set count of total project members
        /// </summary>
        public int ProjectMembersCount { get; set; }

        public int OpenRequirementsCount { get; set; }

    }
}
