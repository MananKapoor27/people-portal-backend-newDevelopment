using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.DepartmentDto
{

    /// <summary>
    /// this view-model is used for department input 
    /// </summary>
    public class DepartmentCreateDto
    {
        /// <summary>
        /// this property is used to get and set department name
        /// </summary>
        public string Name { get; set; }
    }
}
