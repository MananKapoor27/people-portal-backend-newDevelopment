/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeoplePortalDomainLayer.Entities.Models
{
    public class ProjectManagement
    {
        /// this property is used to get and set ProjectManagement ID
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key]
        public Guid ProjectManagementId { get; set; }
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
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// this property is used to get and set Project Id
        /// </summary>
        public Guid? ProjectId { get; set; }

        public Guid RequirementId { get; set; }
        /// <summary>
        /// this property is used to get and set Project Manager
        /// </summary>
        public Guid? ProjectManager { get; set; }

        public Guid? ProjectReportingManager { get; set; }
        
        public string PrimaryStatus { get; set; }

        public string SecondaryStatus { get; set; }

        /// <summary>
        /// this property is used to get and set Project Allocated Start Date
        /// </summary>
        public DateTime? AllocationStartDate { get; set; }

        /// <summary>
        /// this property is used to get and set Project Allocated End Date
        /// </summary>
        public DateTime? AllocationEndDate { get; set; }

        public string Role { get; set; }
        public bool IsManager { get; set; }

        /// <summary>
        /// this property is used to get and set IsDeleted 
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}
