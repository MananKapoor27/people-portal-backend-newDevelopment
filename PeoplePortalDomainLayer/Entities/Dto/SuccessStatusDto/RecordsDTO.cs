using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.SuccessStatusDTO
{
    /// <summary>
    /// this class is used for data related to records in success response
    /// </summary>
    public class RecordsDto
    {
        /// <summary>
        /// this property is used to get and set total records data in the success response
        /// </summary>       
        public int Total { get; set; }
        /// <summary>
        /// this property is used to get and set current page number data in the success response
        /// </summary>        
        public int PageNo { get; set; }
        /// <summary>
        /// this property is used to get and set number of records returned in the success response
        /// </summary>
        public int Returned { get; set; }
    }
}
