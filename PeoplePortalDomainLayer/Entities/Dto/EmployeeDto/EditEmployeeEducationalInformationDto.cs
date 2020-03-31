using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
    public class EditEmployeeEducationalInformationDto
    {
        /// <summary>
        /// this property is used to get and set employee ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
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
