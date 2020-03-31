using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.SuccessStatusDTO
{
    /// <summary>
    /// this class is used provide data related to search in success response
    /// </summary>
    public class SearchDto
    {
        /// <summary>
        /// this property is used to get and set question in the search filter in the success response
        /// </summary>
        public string Question { get; set; }
    }
}
