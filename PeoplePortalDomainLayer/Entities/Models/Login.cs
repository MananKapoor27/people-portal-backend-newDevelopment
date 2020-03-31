/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeoplePortalDomainLayer.Entities.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// this property is used to get and set Employee class
        /// </summary>
        /// <remarks>
        /// this property is used to create a navigation property for the Employee class 
        /// </remarks>
        public Employee Employee { get; set; }
        /// <summary>
        /// this property is used to get and set Employee id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// this property is used to get and set employee company id 
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// this property is used to get and set employee company email 
        /// </summary>
        [Required(ErrorMessage = "Company Email Is Required."), EmailAddress(ErrorMessage = "Not A Valid Email Address")]
        public string CompanyEmail { get; set; }

        /// <summary>
        /// this property is used to get and set employee login password 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}
