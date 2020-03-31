using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.DepartmentDesignationDto
{
    public class DesignationUpdateDto
    {
        /// <summary>
        /// this property is used to get and set designation id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set designation name
        /// </summary>
        public string Title { get; set; }
    }
}
