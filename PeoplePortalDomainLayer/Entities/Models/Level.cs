using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from Level table
    /// </summary>
    public class Level
    {
        /// <summary>
        /// this property is used to get and set Level id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// this property is used to get and set Level title
        /// </summary>
        [Required(ErrorMessage = "level title is required")]
        public string Title { get; set; }
        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
