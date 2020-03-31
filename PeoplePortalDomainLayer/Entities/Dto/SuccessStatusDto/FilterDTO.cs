using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.SuccessStatusDTO
{
    /// <summary>
    /// this class is used for data related to filters in success response
    /// </summary>
    public class FilterDto
    {
        /// <summary>
        /// this property is used to get and set field in which filter was applied in the success response
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// this property is used to get and set value of the filter in the success response
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// this property is used to get and set operator applied on the filter operation in the success response
        /// </summary>
        public string Operator { get; set; }
    }
}
