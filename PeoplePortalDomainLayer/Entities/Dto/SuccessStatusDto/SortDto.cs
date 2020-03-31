using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.SuccessStatusDTO
{
    /// <summary>
    /// this class is used for data related to sorting in success response
    /// </summary>
    public class SortDto
    {
        /// <summary>
        /// this property is used to get and set field in which sorting is applied in the success response
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// this property is used to get and set order in which sorting is applied in the success response
        /// </summary>
        public string Order { get; set; }
    }
}
