/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeoplePortalDomainLayer.Entities.Models
{
    public class Project
    {
        /// this property is used to get and set Project ID
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// this property is used to get and set Project  name
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// this property is used to get and set Project  Start Date 
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// this property is used to get and set Project  End Date 
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// this property is used to get and set when Project was added to the system
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }      
        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// this property is used to get and set Client of the Project
        /// </summary>
        public string ProjectClient { get; set; }
        /// <summary>
        /// this property is used to get and set Description of the Project
        /// </summary>
        public string ProjectDescription { get; set; }

    }
}