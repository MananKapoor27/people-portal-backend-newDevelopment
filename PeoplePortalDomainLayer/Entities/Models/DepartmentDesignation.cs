/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from Department_Designation table
    /// </summary> 
    public class DepartmentDesignation
    {
        /// <summary>
        /// this property is used to get and set DepartmentDesignation id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set Department class
        /// </summary>
        /// <remarks>
        /// this property is used to create a navigation property for the Department class 
        /// </remarks>
        public Department Department { get; set; }
        /// <summary>
        /// this property is used to get and set Department Id
        /// </summary>
        [Column(Order = 1), ForeignKey("Department")]
        public int DepartmentId { get; set; }
        /// <summary>
        /// this property is used to get and set Designation class
        /// </summary>
        /// <remarks>
        /// this property is used to create a navigation property for the Designation class 
        /// </remarks>
        public Designation Designation { get; set; }
        /// <summary>
        /// this property is used to get and set Designation Id
        /// </summary>
        [Column(Order = 2), ForeignKey("Designation")]
        public int DesignationId { get; set; }
        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
