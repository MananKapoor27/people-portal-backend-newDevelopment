using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.DepartmentDto
{
    /// <summary>
    /// this class is used to update department details
    /// </summary>
    public class DepartmentUpdateDto
    {
        /// <summary>
        /// this property is used to get and set department id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set department name
        /// </summary>
        public string Name { get; set; }
    }
}
