using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.ErrorstatusDto
{
    /// <summary>
    /// this class is used to create error
    /// </summary>
    public class ErrorStatusDto
    {
        /// <summary>
        /// this property is used to get and set status of the response
        /// </summary>
        public string Status { get; set; } = "error";
        /// <summary>
        /// this property is used to get and set error class
        /// </summary>
        public ErrorDto Error { get; set; }
        /// <summary>
        /// this constructor is used to initialize error object
        /// </summary>
        /// <param name="code">error code</param>
        /// <param name="message">error message</param>
        public ErrorStatusDto(string code, string message)
        {
            this.Error = new ErrorDto
            {
                ErrorCode = code,
                Message = message
            };
        }
    }
}
