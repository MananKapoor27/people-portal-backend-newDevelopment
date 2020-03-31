using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
    /// <summary>
    /// this view-model is used to get data from user, to add new Employee's Official Informations in the system 
    /// </summary> 
    public class AddEmployeeOfficialInformationDto
    {
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
        /// this property is used to get and set employee's Date of joining the company
        /// </summary>
        public DateTime DateOfJoining { get; set; }

        /// <summary>
        /// this property is used to get and set employee phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// this property is used to get and set employee Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// this property is used to get and set employee Company Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// this property is used to get and set employee Personal Email
        /// </summary>
        public string PersonalEmail { get; set; }

        /// <summary>
        /// this property is used to get and set employee Company Email
        /// </summary>
        public string CompanyEmail { get; set; }

        /// <summary>
        /// this property is used to get and set employee Primary Skill Id
        /// </summary>
        public List<string> PrimarySkill { get; set; }

        /// <summary>
        /// this property is used to get and set employee Secondary Skill Id
        /// </summary>
        public List<string> SecondarySkill { get; set; }
        
        /// <summary>
        /// this property is used to get and set employee Primary Skill's Experience in years
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// this property is used to get and set employee Department Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// this property is used to get and set employee Designation Id
        /// </summary>
        public int DesignationId { get; set; }

        /// <summary>
        /// this property is used to get and set employee primary type
        /// </summary>
        public string EmployeePrimaryType { get; set; }

        /// <summary>
        /// this property is used to get and set employee secondary type
        /// </summary>
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
        /// this property is used to get and set languages employee know
        /// </summary>
        public List<string> Languages { get; set; }

        /// <summary>
        /// this property is used to get and set employee current office location  
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// this property is used to get and set Reporting Manager
        /// </summary>
        public Guid? ReportingManager { get; set; }
    }
}
