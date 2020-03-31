using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.HelperModels.ElasticSearchModels
{
    public class ElasticEmployeeDetailsDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }

        public string CompanyId { get; set; }

        public string CompanyEmail { get; set; }

        public List<string> PrimarySkill { get; set; }

        public List<string> SecondarySkill { get; set; }

        public int DepartmentId { get; set; }

        public int DesignationId { get; set; }
    }
}
