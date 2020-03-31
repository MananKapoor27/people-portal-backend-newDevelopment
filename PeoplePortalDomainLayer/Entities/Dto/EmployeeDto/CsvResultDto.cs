using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.Entities.Dto.EmployeeDto
{
    public class CsvResultDto
    {
        public string companyEmail { get; set; }

        public bool Successful { get; set; }

        public string errorMessage { get; set; }
    }
}
