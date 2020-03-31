using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.ErrorstatusDto
{
    /// <summary>
    /// this class is used to create error details
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// this property is used to get and set error code
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// this property is used to get and set error message
        /// </summary>
        public string Message { get; set; }
    }
}
