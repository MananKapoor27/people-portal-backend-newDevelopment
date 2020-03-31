using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// this class is used to do all department related operations
    /// </summary>
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly PeoplePortalDb _dbContext;
        private bool _disposed = false;
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public DepartmentRepository(PeoplePortalDb peoplePortalDb)
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
        /// this method is used to add a new department in the database
        /// </summary>
        /// <param name="departments">this object contains departments information</param>
        public void AddDepartments(Department departments)
        {
            _dbContext.Departments.Add(departments);
        }
        /// <summary>
        /// this method is used to get the department details by it's Id
        /// </summary>
        /// <param name="departmentId">integer to identify department information</param>
        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            var department = await _dbContext.Departments.SingleOrDefaultAsync(s => s.Id == departmentId && s.IsDeleted == false);
            return department;
        }
        /// <summary>
        /// this method is used to get the department details by it's Name
        /// </summary>
        /// <param name="departmentName">integer to identify department information</param>
        public async Task<Department> GetDepartmentByNameAsync(string departmentName)
        {
            var department = await _dbContext.Departments.SingleOrDefaultAsync(s => s.Name == departmentName);
            return department;
        }
        /// <summary>
        /// this method is used to get the department details by it's Id
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <returns>returns department details</returns>
        public async Task<Department> GetDepartmentDetailsAsync(int id)
        {
            var getDepartment = await _dbContext.Departments.SingleOrDefaultAsync(s => s.Id == id);
            return getDepartment;
        }
        /// <summary>
        /// this method is used to fetch list of all departments in the database
        /// </summary>
        /// <returns>returns list of departments</returns>
        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _dbContext.Departments.Where(c => c.IsDeleted == false).ToListAsync();
        }
        /// <summary>
        /// this function is used to implement dispose pattern callable by consumers
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// this function is used to free all managed objects.
        /// </summary>
        /// <param name="disposing"> bool value which tells whether to dispose or not </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }
    }
}
