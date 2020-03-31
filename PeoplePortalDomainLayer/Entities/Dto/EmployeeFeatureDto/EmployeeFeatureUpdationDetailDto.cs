using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto
{
    /// <summary>
    /// this class is used to send details of change in particular feature permission for a particulsr employee
    /// </summary>
    public class EmployeeFeatureUpdationDetailDto
    {
        /// <summary>
        /// this property is used to get and set feature id
        /// </summary>
        public string FeatureName { get; set; }
        /// <summary>
        /// this property is used to get and set employee id
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// this property is used to get and set feature permission modification detail(feature added or removed)
        /// </summary>
        public string FeatureModification { get; set; }
    }
}
