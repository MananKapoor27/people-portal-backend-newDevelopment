using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Models
{
    public class EmployeeType
    {
        [Key]
        public Guid Id { get; set; }

        public string EmployeeTypeName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
