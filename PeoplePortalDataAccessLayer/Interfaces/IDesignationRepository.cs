using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// this interface is used to provide contract for designation repository
    /// </summary>
    public interface IDesignationRepository : IDisposable
    {
        /// <summary>
        /// this method is used to get the department details
        /// </summary>
        /// <param name="deptId">department id</param>
        /// <returns>department details</returns>
        Task<Department> GetDepartementDetailsAsync(int deptId);

        /// <summary>
        /// this method is used to add a designations list to the database
        /// </summary>
        /// <param name="desigList">list of designations</param>
        void AddDesignations(List<Designation> desigList);

        /// <summary>
        /// this method is used to get the list of all designation
        /// </summary>
        /// <returns></returns>
        Task<List<Designation>> GetDesignationList();

        /// <summary>
        /// this method is used to get a list of designation ids
        /// </summary>
        /// <param name="designations"> list of designations</param>
        /// <returns>list of designation ids</returns>
        IQueryable<int> GetDesignationIds(List<string> designations);

        /// <summary>
        /// this method is used to get the designation details
        /// </summary>
        /// <param name="designationId">designation id</param>
        /// <returns>designation details</returns>
        Task<Designation> GetDesignationDetailsAsync(int designationId);

        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// this method is used to get all the designation details of the mapped designation ids
        /// </summary>
        /// <param name="designationIds">list that contains all the ids of designations mapped to a particular department</param>
        /// <returns>returns list of designations details</returns>
        Task<List<Designation>> GetDesignation(List<int> designationIds);
    }
}
