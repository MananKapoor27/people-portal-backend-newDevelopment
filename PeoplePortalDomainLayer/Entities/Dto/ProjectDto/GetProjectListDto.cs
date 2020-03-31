using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.ProjectDto
{
    public class GetProjectListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set Project  name
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// this property is used to get and set Client of the Project
        /// </summary>
        public string ProjectClient { get; set; }
    }
}
