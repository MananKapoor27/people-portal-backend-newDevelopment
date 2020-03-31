using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.FailureStatusDto
{
    /// <summary>
    /// this class is used to  create failure response
    /// </summary>
    public class FailureStatusDto
    {
        /// <summary>
        /// this property is used to get and set status of the failure
        /// </summary>
        public string Status { get; set; } = "fail";
        /// <summary>
        /// this property is used to get and set data for the failure
        /// </summary>
        public List<Dictionary<string, List<FailMessageDto>>> Data { get; set; }
    }
}
