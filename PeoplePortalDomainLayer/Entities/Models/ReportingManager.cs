using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Models
{
   public class ReportingManager
    {

        /// <summary>
        /// this property is used to get and set Employee class
        /// </summary>
        /// <remarks>
        /// this property is used to create a navigation property for the employee class 
        /// </remarks>
        public Employee Employee { get; set; }
        /// <summary>
        /// this property is used to get and set Reporting Manager
        /// </summary>
        [Key, ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// this property is used to get and set Reporting Manager
        /// </summary>
        public Guid? ReportingManagerId { get; set; }


    }
}
