/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from EmployeeLevel table
    /// </summary> 
    public class DesignationLevel
    {
        /// <summary>
        /// this property is used to get and set Employee class
        /// </summary>
        /// <remarks>
        /// this property is used to create a navigation property for the Employee class 
        /// </remarks>
        public DepartmentDesignation DepartmentDesignation { get; set; }
        /// <summary>
        /// this property is used to get and set Employee id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key, Column(Order = 0), ForeignKey("DepartmentDesignation")]
        public int DepartmentDesignationId { get; set; }
        /// <summary>
        /// this property is used to get and set Level class
        /// </summary>
        /// <remarks>
        /// this property is used to create a navigation property for the Level class 
        /// </remarks>
        public Level Level { get; set; }
        /// <summary>
        /// this property is used to get and set Level id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key, Column(Order = 1), ForeignKey("Level")]
        public int LevelId { get; set; }
        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}