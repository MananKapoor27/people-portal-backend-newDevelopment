/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System.ComponentModel.DataAnnotations;

namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from Designation table
    /// </summary>
    public class Designation
    {
        /// <summary>
        /// this property is used to get and set Designation Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set Designation Title 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
