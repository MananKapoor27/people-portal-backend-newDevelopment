using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto
{
    /// <summary>
    /// this class is used to get data for adding feature to employee
    /// </summary>
    public class GetEmployeeFeatureUpdateDto
    {
        /// <summary>
        /// this property is used to get and set feature id
        /// </summary>
        public string FeatureName { get; set; }
        /// <summary>
        /// this property is used to get and set employee id
        /// </summary>
        public Guid EmployeeId { get; set; }
    }
}
