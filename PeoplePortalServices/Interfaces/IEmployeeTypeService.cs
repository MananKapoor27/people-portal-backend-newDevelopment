using PeoplePortalDomainLayer.Entities.Dto.EmployeeTypeDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Interfaces
{
    public interface IEmployeeTypeService
    {
        Task<string> AddEmployeeTypeAsync(string employeeType);

        Task<List<EmployeeTypeDto>> GetAllEmployeeTypeAsync();

        Task<EmployeeTypeDto> UpdateEmployeeTypeAsync(EmployeeTypeDto employeeType);

        Task<EmployeeTypeDto> DeleteEmployeeTypeAsync(Guid id);
    }
}
