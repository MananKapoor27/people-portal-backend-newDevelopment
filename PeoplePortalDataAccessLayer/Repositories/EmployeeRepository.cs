using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// This class is used to interact with the database for employee related queries
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PeoplePortalDb _dbContext;
        
        /// <summary>
        /// constructor for this class to assign value to database context
        /// </summary>
        public EmployeeRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        public async Task SaveChangesAsync()
        {           
                await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// this method is used to add a new employee in the database
        /// </summary>
        /// <param name="newEmployeeDetails">model to data in employee table</param>
        public void AddNewEmployee(Employee newEmployeeDetails)
        {
            try
            {
                //_dbContext.Entry(newEmployeeDetails).CurrentValues.SetValues
                _dbContext.Employees.Add(newEmployeeDetails);
                return;
            }
            catch(Exception e)
            {
                return;
            }
          
        }

        /// <summary>
        /// this method is used to fetch employee id from the employee table
        /// </summary>
        /// <param name="empId">fetched employee id</param>
        /// <returns>this method will return a single id from database</returns>
        public async Task<Employee> GetEmployeeByIdAsync(Guid empId)
        {
            var result = await _dbContext.Employees.SingleOrDefaultAsync(c => c.Id == empId && c.IsDeleted == false);
            return result;
        }

        //public async Task<BulkAddEmployeesDto> checkIfEmployeeExists(string companyId)
        //{
        //    var result = await _dbContext.Employees.SingleOrDefaultAsync(c => c.CompanyId == companyId && c.IsDeleted == false);
        //    return (BulkAddEmployeesDto)result;
        //}

        /// <summary>
        /// this method is used to get employee details by company id 
        /// </summary>
        /// <param name="companyId">string containing the value of company id</param>
        /// <returns>returns employee details corresponding to that email id</returns>
        public async Task<Employee> GetEmployeeByCompanyIdAsync(string companyId)
        {
            var employeeDetails = await _dbContext.Employees.FirstOrDefaultAsync(c => c.CompanyId == companyId);
            return employeeDetails;
        }

        /// <summary>
        /// this method is used to get employeeInformation
        /// </summary>
        /// <param name="id">id</param>
        public async Task<Employee> GetEmployeeInformationAsync(Guid? id)
        {
            var employeeInfo = await _dbContext.Employees.SingleOrDefaultAsync(s => s.Id == id && s.IsDeleted == false);
            return employeeInfo;
        }

        public async Task<List<DepartmentDesignation>> GetDepartmentDesignationTable()
        {
            List<DepartmentDesignation> data = await _dbContext.DepartmentDesignations.ToListAsync<DepartmentDesignation>();
            return data;
        }

        /// <summary>
        /// this method is used to get employeeinformation
        /// </summary>
        /// <param name="companyEmail">id</param>
        public async Task<Employee> GetEmployeeByCompanyEmailAsync(string companyEmail)
        {
            
            var getEmployeeInfo = await _dbContext.Employees.SingleOrDefaultAsync(s => s.CompanyEmail == companyEmail);
            return getEmployeeInfo;
        }
        
        /// <summary>
        /// this method is used to get list of employee whose is deleted is false
        /// </summary>
        public async Task<PagedList<Employee>> GetListOfEmployeeAsync(PagingParams pagingParams)
        {
            var listOfEmployee = await PagedList<Employee>.Create(_dbContext.Employees.Where(s => s.IsDeleted == false).OrderByDescending(f => f.CreatedAt), pagingParams.PageNumber, pagingParams.PageSize);
            return listOfEmployee;
        }

        /// <summary>
        /// this method is used to get list of employee whose is deleted is false
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetListOfEmployeeAsync()
        {
            var result = await _dbContext.Employees.Where(e => e.IsDeleted == false).ToListAsync();
            return result;
        }

        /// <summary>
        /// this method is used to search for searchString in employee's name
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>List of Employee</returns>
        public async Task<List<Employee>> EmployeeSearch(string searchString)
        {
            var employeeList = await _dbContext.Employees.Where(e => e.IsDeleted == false && e.FirstName.Contains(searchString) || e.MiddleName.Contains(searchString) || e.LastName.Contains(searchString)).ToListAsync();
            return employeeList;
        }
    }
}
