using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// this interface is used to provide contract for department repository
    /// </summary>
    public interface IDepartmentRepository : IDisposable
    {
        /// <summary>
        /// this method is used to add a new department in database
        /// </summary>
        /// <param name="departments">this object contains departments information</param>
        void AddDepartments(Department departments);

        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// this method is used to get the department details by it's Id
        /// </summary>
        /// <param name="departmentId">integer to indentify department information</param>
        Task<Department> GetDepartmentByIdAsync(int departmentId);

        /// <summary>
        /// this method is used to get the department details by it's Name
        /// </summary>
        /// <param name="departmentId">integer to indentify department information</param>
        Task<Department> GetDepartmentByNameAsync(string departmentName);

        /// <summary>
        /// this method gives all the details of department by it's Id
        /// </summary>
        /// <param name="id">Department iD</param>
        Task<Department> GetDepartmentDetailsAsync(int id);
        /// <summary>
        /// this method is used to fetch list of all departments in the database
        /// </summary>
        /// <returns>returns list of departments</returns>
        Task<List<Department>> GetAllDepartmentsAsync();
    }
}
