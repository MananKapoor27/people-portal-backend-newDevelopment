using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.DesignationDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// this interface is used to provide contract for departmentDesignation service
    /// </summary>
    public interface IDepartmentDesignationService 
    {
        /// <summary>
        /// this method is used to fetch all designations of a department
        /// </summary>
        /// <param name="departmentId">integer to identity department</param>
        /// <returns>returns list of designation of that department</returns>
        Task<List<DesignationListDto>> GetAllDesignationAsync(int departmentId);

        /// <summary>
        /// this method is used to add designations to the department
        /// </summary>
        /// <param name="departmentId">department id</param>
        /// <param name="designations">list of designations</param>
        /// <returns>string stating the status of the method</returns>
        Task<string> AddDesignationToDepartmentAsync(int departmentId, List<string> designations);

        Task<Department> GetDepartment(int departmentDesignationId);

        Task<Designation> GetDesignation(int departmentDesignationId);
        Task<int> GetDepartmentDesignationId(int departmentId, int designationId);

        /// <summary>
        /// this method is used to get the designation title in list
        /// </summary>
        /// <param name="Id">departmentdesignation Id</param>
        Task<string> GetDesignationByDepartmentDesignationId(int id);
    }
}
