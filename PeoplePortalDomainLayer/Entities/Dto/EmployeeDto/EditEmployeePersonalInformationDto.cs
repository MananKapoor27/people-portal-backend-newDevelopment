using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
   public  class EditEmployeePersonalInformationDto
    {
        /// <summary>
        /// this property is used to get and set employee Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// this property is used to get and set employee's permanent address
        /// </summary>
        public string PermanentAddress { get; set; }

        /// <summary>
        /// this property is used to get and set employee's temporary address
        /// </summary>
        public string TemporaryAddress { get; set; }

        /// <summary>       
        /// this property is used to get and set employee Date of birth 
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// this property is used to get and set employee personal details like hobbies,achievements,etc
        /// </summary>
        public string BioGraphy { get; set; }

        /// <summary>
        /// this property is used to get and set employee nationality
        /// </summary>
        public string Nationality { get; set; }

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
        /// this property is used to get and set data of each child
        /// </summary>
        public List<ChildrenData> childrenData { get; set; }

      
    }

   /// <summary>
   /// this class is used to contain each child's data
   /// </summary>
    public partial class ChildrenData
   {
       /// <summary>
       /// this property is used to get and set employee's child's name
       /// </summary>
        public string Name { get; set; }

       /// <summary>
       /// this property is used to get and set employee's child's date of birth
       /// </summary>
        public DateTime DateOfBirth { get; set; }
   }
}
