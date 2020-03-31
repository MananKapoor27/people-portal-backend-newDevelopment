using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.DesignationDto
{
    public class DesignationListDto
    {
        /// <summary>
        /// this property is used to get and set Designation Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set Designation Title 
        /// </summary>
        public string Title { get; set; }
    }
}
