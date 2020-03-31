using PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectDto
{
    public class AddProjectDto
    {
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set Project  name
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
        /// this property is used to get and set when Project was added to the system
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// this property is used to get and set Project Manager
        /// </summary>
        public List<GetEmployeeDetailDto> ProjectManager { get; set; }
        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// this property is used to get and set Client of the Project
        /// </summary>
        public string ProjectClient { get; set; }
        /// <summary>
        /// this property is used to get and set Description of the Project
        /// </summary>
        public string ProjectDescription { get; set; }
    }
}
