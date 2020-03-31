using System;
using System.Collections.Generic;
using System.Text;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDesignationDto;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDto;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
    public class EditEmployeeOfficialInformationDto
    {
        /// <summary>
        /// this property is used to get and set employee Id
        /// </summary>
        public Guid Id { get; set; }
      
        /// <summary>
        /// this property is used to get and set employee first name
        /// </summary>
        public string FirstName { get; set; }
       
        /// <summary>
        /// this property is used to get and set employee middle name
        /// </summary>
        public string MiddleName { get; set; }
      
        /// <summary>
        /// this property is used to get and set employee last name
        /// </summary>
        public string LastName { get; set; }
      
        /// <summary>
        /// this property is used to get and set employee Personal Email
        /// </summary>
        public string PersonalEmail { get; set; }

        /// <summary>
        /// this property is used to get and set employee Gender
        /// </summary>
        public string Gender { get; set; }
      
        /// <summary>
        /// this property is used to get and set employee Primary Skill's Experience in years
        /// </summary>
        public int? Experience { get; set; }
       
        /// <summary>
        /// this property is used to get and set employee Company Id
        /// </summary>
        public string CompanyId { get; set; }
       
        /// <summary>
        /// this property is used to get and set employee phone number
        /// </summary>
        public string PhoneNumber { get; set; }
      
        /// <summary>
        /// this property is used to get and set employee languages
        /// </summary>
        public List<string> Languages { get; set; }
    
        /// <summary>
        /// this property is used to get and set comapany email
        /// </summary>
        public string CompanyEmail { get; set; }
        
        public DepartmentUpdateDto Department { get; set; }
    
        /// <summary>
        /// this property is used to get and set the designation
        /// </summary>
        public DesignationUpdateDto Designation { get; set; }
      
        /// <summary>
        /// this property is used to get and set Primary skill
        /// </summary>
        public List<string> PrimarySkill { get; set; }
       
        /// <summary>
        /// this property is used to get and set secondary skill
        /// </summary>
        public List<string> SecondarySkill { get; set; }
       
        /// <summary>
        /// this property is used to get and set employee's Date of joining the company
        /// </summary>
        public DateTime DateOfJoining { get; set; }

        /// <summary>
        /// this property is used to get and set employee type
        /// </summary>
        public string EmployeePrimaryType { get; set; }
       
        public string EmployeeSecondaryType { get; set; }

        /// <summary>
        /// this property is used to get and set employee github id
        /// </summary>
        public string GitHubId { get; set; }
      
        /// <summary>
        /// this property is used to get and set employee linkedin id
        /// </summary>
        public string LinkedinId { get; set; }
      
        /// <summary>
        /// this property is used to get and set employee current office location  
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// this property is used to get and set Reporting Manager
        /// </summary>
        public GetEmployeeListDto ReportingManager { get; set; }
    }
}
