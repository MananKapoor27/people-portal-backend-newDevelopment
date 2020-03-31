/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System.ComponentModel.DataAnnotations;


namespace PeoplePortalDomainLayer.Entities.Models
{

    /// <summary>
    /// this class is used to map data from Department table
    /// </summary>
    public class Department
    {
        /// <summary>
        /// this property is used to get and set department id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set department name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
