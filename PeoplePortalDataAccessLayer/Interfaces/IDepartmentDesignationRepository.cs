using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Interfaces
{

    /// <summary>
    /// this interface is used to provide contract for department designation repository
    /// </summary>
    public interface IDepartmentDesignationRepository : IDisposable
    {
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// this method is used to fetch all the details of designation falling under a department
        /// </summary>
        /// <param name="departmentId">integer to identify department</param>
        /// <returns>returns list of Designation ids</returns>
        Task<List<int>> GetAllDesignationsAsync(int departmentId);
        /// <summary>
        /// this method is used to get all the designation details of the mapped designation ids
        /// </summary>
        /// <param name="designationIds">list that contains all the ids of designations mapped to a particular department</param>
        /// <param name="departmentId">integer to identify department</param>
        /// <returns>returns list of designations details</returns>
        Task<List<int>> GetDesignationDepartmentId(List<int> designationIds, int departmentId);
        /// <summary>
        /// this method is used to add new list of department designation level mapping
        /// </summary>
        /// <param name="designationLevelList">new list of department designation level mapping</param>
        void AddDesignationDepartmentLevelList(List<DesignationLevel> designationLevelList);
        /// <summary>
        /// this method is used to get all the designation details of the given department id
        /// </summary>
        /// <param name="departmentId">integer to identify department</param>
        /// <returns>returns list of designations department ids</returns>
        Task<List<int>> GetDepartmentDesignationIdListAsync(int departmentId);
        /// <summary>
        /// this method is used to get All Designation Ids By Department Designation Id
        /// </summary>
        /// <param name="departmentDesignationIdList">list of integers to map into department designation ids</param>
        /// <returns>returns list of required designation ids</returns>
        Task<List<int>> GetAllDesignationIdsByDepartmentDesignationIdAsync(List<int> departmentDesignationIdList);
        /// <summary>
        /// this method is used to get all the designation details of the given department id list
        /// </summary>
        /// <param name="departmentDesignationId">integer to identify department designation mapping</param>
        /// <returns>returns list of designations </returns>
        Task<IEnumerable<Designation>> GetDesignationListAsync(List<int> departmentDesignationId);
        /// <summary>
        /// this method is used to get the list of designation which are mapped with department
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <returns>returns list of designation designation mappings</returns>
        Task<List<DepartmentDesignation>> GetDesignationList(int id);

        /// <summary>
        /// this method is used to add the department id and designation id in the mapper table
        /// </summary>
        /// <param name="departmentDesignations">list of departmentDesignations</param>
        void AddDepartmentDesignation(List<DepartmentDesignation> departmentDesignations);

        /// <summary>
        /// this method gets the details of the DepartmentDesignation
        /// </summary>
        /// <param name="designationId">designation id</param>
        /// <returns>DepartmentDesignation details</returns>
        Task<DepartmentDesignation> GetDepartmentDesignationDetailsAsync(int designationId);
        /// <summary>
        /// this method is used to get the data related to a given department and designation
        /// </summary>
        /// <param name="departmentId">integer to indentify department</param>
        /// <param name="designationId">integer to indentify designation</param>
        /// <returns>returns details related to required designation and department mapping</returns>
        Task<DepartmentDesignation> GetDepartmentDesignationAsync(int departmentId, int designationId);
        /// <summary>
        /// this method is used to get department id by departmentdesignationId
        /// </summary>
        /// <param name="departmentDesignationId">Id</param>
        Task<int> GetDepartmentByDepartmentDesignation(int departmentDesignationId);
        /// <summary>
        /// this method is used to get department name by id
        /// </summary>
        /// <param name="departmentId">Id</param>
        Task<Department> GetDepartmentById(int departmentId);
        /// <summary>
        /// this method is used to get designationId by Id
        /// </summary>
        /// <param name="departmentDesignationId">Id</param>
        Task<int> GetDesignationByDepartmentDesignation(int departmentDesignationId);
        /// <summary>
        /// this method is used to get the designation name
        /// </summary>
        /// <param name="designationId">Id</param>
        Task<Designation> GetDesignationById(int designationId);
        /// <summary>
        /// this method is used to get the designation title in list
        /// </summary>
        /// <param name="Id">departmentdesignation Id</param>
        Task<string> GetDesignationByDepartmentDesignationId(int Id);
        /// <summary>
        /// this method is used to get the department details
        /// </summary>
        /// <param name="departmentName">name of the department</param>
        Task<Department> GetDepartmentDetailsAsync(string departmentName);
        /// <summary>
        /// this method is used to get the designation details
        /// </summary>
        /// <param name="designationName">name of the department</param>
        Task<Designation> GetDesignationDetailsAsync(string designationName);

        /// <summary>
        /// this method is used to get departmentDesignation Id for a designationId
        /// </summary>
        /// <param name="designationId"></param>
        /// <returns></returns>
        Task<DepartmentDesignation> GetDepartmentDesignationForDesignation(int designationId);
    }
}
