/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from EmployeeSkill table
    /// </summary> 
    public class EmployeeSkill
    {
        /// <summary>
        /// this property is used to get and set Employee class
        /// </summary>
        /// <remarks>
        /// this property is used to create a navigation property for the Employee class 
        /// </remarks>
        public Employee Employee { get; set; }
        /// <summary>
        /// this property is used to get and set EmployeeID
        /// </summary>
        [Key, Column(Order = 0), ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        public string PrimarySkills { get; set; }

        public string SecondarySkills { get; set; }

        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
       
    }
}
