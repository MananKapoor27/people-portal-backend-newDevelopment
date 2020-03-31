using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    public interface IEmployeeTypeRepository
    {
        void AddEmployeeType(EmployeeType employeeType);

        Task<EmployeeType> GetEmployeeTypeAsync(Guid id);

        Task<EmployeeType> GetEmployeeTypeByNameAsync(string name);

        Task<List<EmployeeType>> GetAllEmployeeTypeAsync();

        void DeleteEmployeeType(EmployeeType employeeType);

        Task SaveChangesAsync();
    }
}
