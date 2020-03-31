using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
    public class BulkAddEmployeesDto
    {
        public int Department { get; set; }
        public int Designation { get; set; }
        public int DepartmentDesignationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfJoining { get; set; } 
      
        public string CompanyEmail { get; set; }

        public string CompanyId { get; set; }

        public List<string> PrimarySkills { get; set; }

        public List<string> SecondarySkills { get; set; }

        [EmailAddress]
        public string PersonalEmail { get; set; }
       
        [Required(ErrorMessage = "Phone Number is Required"), Phone(ErrorMessage = "Phone number not valid")]
        public string PhoneNumber { get; set; }

        public int Experience { get; set; }

        public string Links { get; set; }

        public string Nationality { get; set; }

        [Required(ErrorMessage = "Employee Type is Required")]
        public string EmployeePrimaryType { get; set; }
        public string EmployeeSecondaryType { get; set; }


        public string GitHubId { get; set; }

        public string LinkedinId { get; set; }

        public string PrimaryStatus { get; set; }

        public string SecondaryStatus { get; set; }
       
        public string Location { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public string Gender { get; set; }
       
        public string BioGraphy { get; set; }
        public List<string> Languages { get; set; }
        
    }
}
