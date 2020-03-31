using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.HelperModels.ElasticSearchModels
{
    public class ElasticEmployeeDetails
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public string DesignationName { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyId { get; set; }

        public List<string> Skills { get; set; }
    }
}
