/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved,
 No part of this software can be modified or edited without permissions */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeoplePortalDomainLayer.Entities.Models
{
    /// <summary>
    /// this class is used to map the data from Employee table
    /// </summary>  
    public class Employee
    {
        /// <summary>9
        /// this property is used to get and set Employee id
        /// </summary>
        /// <remarks>
        /// this property is used as primary key
        /// </remarks>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// this property is used to get and set DepartmentDesignation class
        /// </summary>
        /// <remarks>
        /// this property is used to create a navigation property for the Department_Designation class 
        /// </remarks>
        public DepartmentDesignation DepartmentDesignation { get; set; }

        /// <summary>
        /// this property is used to get and set Department_Designation id
        /// </summary>
        [Required(ErrorMessage = "Employee Designation needed"), Column(Order = 1), ForeignKey("DepartmentDesignation")]
        public int DepartmentDesignationId { get; set; }

        /// <summary>
        /// this property is used to get and set employee first name 
        /// </summary>
        [Required(ErrorMessage = "Employee First Name is required")]
        public string FirstName { get; set; }

        /// <summary>
        /// this property is used to get and set employee last name 
        /// </summary>
        [Required(ErrorMessage = "Employee Last Name is required")]
        public string LastName { get; set; }

        /// <summary>
        /// this property is used to get and set employee middle name 
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// this property is used to get and set employee Date of Joining  
        /// </summary>
        [Required(ErrorMessage = "Date Of Joining is Required")]
        public DateTime DateOfJoining { get; set; }

        /// <summary>
        /// this property is used to get and set employee company email 
        /// </summary>
        [EmailAddress]
        public string CompanyEmail { get; set; }

        /// <summary>
        /// this property is used to get and set employee personal email
        /// </summary>
        [EmailAddress]
        public string PersonalEmail { get; set; }

        /// <summary>
        /// this property is used to get and set employee phone number
        /// </summary>
        [Required(ErrorMessage = "Phone Number is Required"), Phone(ErrorMessage = "Phone number not valid")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// this property is used to get and set employee's permanent address
        /// </summary>
        public string PermanentAddress { get; set; }

        /// <summary>
        /// this property is used to get and set employee's temporary address
        /// </summary>
        public string TemporaryAddress { get; set; }



        /// <summary>
        /// this property is used to get and set employee current office location  
        /// </summary>
        public string Location { get; set; }

        /// <summary>       
        /// this property is used to get and set employee Date of birth 
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// this property is used to get and set employee gender
        /// </summary>
        [Required]
        public string Gender { get; set; }

        /// <summary>
        /// this property is used to get and set employee personal details like hobbies,achievements,etc
        /// </summary>
        public string BioGraphy { get; set; }

        /// <summary>
        /// this property is used to get and set Date when employee is added to the system  
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// this property is used to get and set Date when employee details edited
        /// </summary>
        [Required]
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// this property is used to get and set languages employee know
        /// </summary>
        public string Languages { get; set; }

        /// <summary>
        /// this property is used to get and set employee nationality
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// this property is used to get and set employee working experience
        /// </summary>
        public int? Experience { get; set; }

        /// <summary>
        /// this property is used to get and set IsDeleted for soft deletion 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// this property is used to get and set employee company id
        /// </summary>
        [Required(ErrorMessage = "Company Id is Required")]
        public string CompanyId { get; set; }

        /// <summary>
        /// this property is used to get and set employee type
        /// </summary>
        [Required(ErrorMessage = "Employee Type is Required")]
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
        /// this property is used to get and set employee's pan number
        /// </summary>
        public string PanNumber { get; set; }

        /// <summary>
        /// this property is used to get and set employee's aadhaar number
        /// </summary>
        public string AadhaarNumber { get; set; }

        /// <summary>
        /// this property is used to get and set employee's father's name
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// this property is used to get and set employee's mother's name
        /// </summary>
        public string MotherName { get; set; }

        /// <summary>
        /// this property is used to get and set employee's blood group
        /// </summary>
        public string BloodGroup { get; set; }

        /// <summary>
        /// this property is used to get and set employee's emergency contact name
        /// </summary>
        public string EmergencyContactName { get; set; }

        /// <summary>
        /// this property is used to get and set employee's emergency contact relation
        /// </summary>
        public string EmergencyContactRelation { get; set; }

        /// <summary>
        /// this property is used to get and set employee's emergency contact number
        /// </summary>
        public string EmergencyContactNumber { get; set; }

        /// <summary>
        /// this property is used to get and set employee's marital status
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// this property is used to get and set employee's spouse's name
        /// </summary>
        public string SpouseName { get; set; }

        /// <summary>
        /// this property is used to get and set employee's spouse's date of birth
        /// </summary>
        public DateTime? DateOfBirthOfSpouse { get; set; }

        /// <summary>
        /// this property is used to get and set employee's number of children
        /// </summary>
        public int NumberOfChildren { get; set; }

        /// <summary>
        /// this property is used to get and set employee's names of children
        /// </summary>
        public string NamesOfChildren { get; set; }

        /// <summary>
        /// this property is used to get and set employee's children's date of birth
        /// </summary>
        public string DateOfBirthOfChildren { get; set; }

        /// this property is used to get and set employee Senior Secondary Institute Name
        /// </summary>
        public string SscInstituteName { get; set; }

        /// <summary>
        /// this property is used to get and set employee Senior Secondary Year of Passout
        /// </summary>
        public string SscYearOfPassout { get; set; }

        /// <summary>
        /// this property is used to get and set employee Senior Secondary Branch of Study
        /// </summary>
        public string SscBranchOfStudy { get; set; }

        /// <summary>
        /// this property is used to get and set employee Higher Secondary Institute Name
        /// </summary>
        public string HscInstituteName { get; set; }

        /// <summary>
        /// this property is used to get and set employee Higher Secondary Year of Passout
        /// </summary>
        public string HscYearOfPassout { get; set; }

        /// <summary>
        /// this property is used to get and set employee Higher Secondary Branch of Study
        /// </summary>
        public string HscBranchOfStudy { get; set; }

        /// <summary>
        /// this property is used to get and set employee Under Graduation Institute Name
        /// </summary>
        public string UGInstituteName { get; set; }

        /// <summary>
        /// this property is used to get and set employee Under Graduation Year of Passout
        /// </summary>
        public string UGYearOfPassout { get; set; }

        /// <summary>
        /// this property is used to get and set employee Under Graduation Branch of Study
        /// </summary>
        public string UGBranchOfStudy { get; set; }

        /// <summary>
        /// this property is used to get and set employee Post Graduation Institute Name
        /// </summary>
        public string PGInstituteName { get; set; }

        /// <summary>
        /// this property is used to get and set employee Post Graduation Year of Passout
        /// </summary>
        public string PGYearOfPassout { get; set; }

        /// <summary>
        /// this property is used to get and set employee Post Graduation Branch of Study
        /// </summary>
        public string PGBranchOfStudy { get; set; }
    }

}
