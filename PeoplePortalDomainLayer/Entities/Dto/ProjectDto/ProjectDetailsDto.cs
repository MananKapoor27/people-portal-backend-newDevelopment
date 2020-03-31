using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectDto
{
    /// <summary>
    /// this view-model is used to get data from user, to add new Project in the system 
    /// </summary>
    public class ProjectDetailsDto
    {
        /// <summary>
        /// this property is used to get and set Project  name
        /// </summary> 
        public string Name { get; set; }    
        /// <summary>
        /// this property is used to get and set Client of the Project
        /// </summary>
        public string ProjectClient { get; set; }
        /// <summary>
        /// this property is used to get and set Project  Start Date 
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// this property is used to get and set Project  End Date 
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// this property is used to get and set Description of the Project
        /// </summary>
        public string ProjectDescription { get; set; }
    }
}
