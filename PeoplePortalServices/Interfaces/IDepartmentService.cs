using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    ///  this interface is used to provide contract for department service
    /// </summary>
    public interface IDepartmentService 
    {
        /// <summary>
        /// this method is used to add a new department
        /// </summary>
        /// <param name="departmentDetails">new slot's data</param>
        /// <returns>returns null if successfully added</returns>
        Task<DepartmentUpdateDto> AddDepartmentsAsync(DepartmentCreateDto departmentDetails);

        /// <summary>
        /// this method is used to update the existing department
        /// </summary>
        /// <param name="department">new updated data</param>
        Task<Department> UpdateDepartmentAsync(Department department);

        /// <summary>
        /// this method is used to delete the department
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <returns>returns department details</returns>
        Task<Department> DeleteDepartmentAsync(int id);
        /// <summary>
        /// this method is used to fetch list of all departments in the database
        /// </summary>
        /// <returns>returns list of departments</returns>
        Task<List<Department>> GetAllDepartmentAsync();

        Task<Department> GetDepartmentByIdAsync(int departmentId);

        Task<Department> GetDepartmentByNameAsync(string departmentName);

    }
}
