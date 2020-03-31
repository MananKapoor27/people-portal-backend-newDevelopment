using CsvHelper.Configuration;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;

namespace PeoplePortalServices.Shared.Helpers
{
    public class BulkAddEmployeesDtoMapper : ClassMap<BulkAddEmployeesDto>
    {
        public BulkAddEmployeesDtoMapper()
        {
            Map(x => x.Department);
            Map(x => x.Designation);
            Map(x => x.DepartmentDesignationId).Ignore();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.MiddleName); 
            Map(x => x.DateOfJoining);
            Map(x => x.CompanyEmail);
            Map(x => x.CompanyId);
            Map(x => x.PersonalEmail);
            Map(x => x.PhoneNumber);
            Map(x => x.Experience);
            Map(x => x.Links);
            Map(x => x.Nationality);
            Map(x => x.EmployeePrimaryType);
            Map(x => x.EmployeeSecondaryType);
            Map(x => x.GitHubId);
            Map(x => x.LinkedinId);
            Map(x => x.PrimaryStatus);
            Map(x => x.SecondaryStatus);
            Map(x => x.Location);
            Map(x => x.DateOfBirth);
            Map(x => x.Gender);
            Map(x => x.BioGraphy);
            Map(x => x.Languages);
            Map(x => x.PrimarySkills);
            Map(x => x.SecondarySkills);
        }
    }
}
