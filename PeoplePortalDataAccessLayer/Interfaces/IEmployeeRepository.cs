using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// This interface is used to provide contract for employee repository
    /// </summary>
    public interface IEmployeeRepository 
    {
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// this method is used to add a new employee in the database
        /// </summary>
        /// <param name="newEmployeeDetails">model to add data in employee table</param>
        void AddNewEmployee(Employee newEmployeeDetails);
      
        /// <summary>
        /// this method is used to get employee details by company id 
        /// </summary>
        /// <param name="companyId">string containing the value of company id</param>
        /// <returns>returns employee details corresponding to that email id</returns>
        Task<Employee> GetEmployeeByCompanyIdAsync(string companyId);

        /// <summary>
        /// this method is used to get employee details by company id 
        /// </summary>
        /// <param name="companyEmail">string containing the value of company id</param>
        /// <returns>returns employee details corresponding to that email id</returns>
        Task<Employee> GetEmployeeByCompanyEmailAsync(string companyEmail);
        /// <summary>
        /// this method is used to fetch id from database 
        /// </summary>
        /// <param name="id"> employee Id</param>
        /// <returns>this method will return a single id from database</returns>
        Task<Employee> GetEmployeeByIdAsync(Guid id);

        Task<List<DepartmentDesignation>> GetDepartmentDesignationTable();

        /// <summary>
        /// this method is used to get employeeInformation
        /// </summary>
        /// <param name="id">id</param>
        Task<Employee> GetEmployeeInformationAsync(Guid? id);
        /// <summary>
        /// this method is used to get list of employee whose is deleted is false
        /// </summary>
        Task<PagedList<Employee>> GetListOfEmployeeAsync(PagingParams pagingParams);

        /// <summary>
        /// this method is used to get list of employee whose is deleted is false
        /// </summary>
        /// <returns></returns>
        Task<List<Employee>> GetListOfEmployeeAsync();

        /// <summary>
        /// this method is used to search for searchString in employee's name
        /// </summary>
        Task<List<Employee>> EmployeeSearch(string searchString);
    }
}
