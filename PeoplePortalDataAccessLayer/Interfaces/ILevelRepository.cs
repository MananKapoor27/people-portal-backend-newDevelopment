using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;
namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// this interface is used to provide contract for level repository
    /// </summary>
    public interface ILevelRepository 
    {
        /// <summary>
        /// this method is used to add new levels in database
        /// </summary>
        /// <param name="levels">this object contains new level information</param>
        void AddLevel(Level levels);
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// this method is used to fetching title of last row
        /// </summary>
        /// <returns>returns title of last row</returns>
        string GetLastRowTitle();
        /// <summary>
        /// this method is used to fetch all Department Designation Id By Level
        /// </summary>
        /// <param name="levelId">integer to identify level</param>
        /// <returns>returns list of integer ids of departmentDesignation</returns>
        Task<List<int>> GetAllDepartmentDesignationIdByLevelAsync(int levelId);
        /// <summary>
        /// this method is used to fetch all designation department ids if present only among given list
        /// </summary>
        /// <param name="departmentDesignationIdList">list of integers to map departmentDesignationIds</param>
        /// <returns>returns list of departmentDesignationIds if present from among the given ids</returns>
        Task<List<int>> GetAllDesignationDepartmentIdMappedToLevelAsync(List<int> departmentDesignationIdList);
        /// <summary>
        /// this method is used to fetch Level id mapped to given department designation id
        /// </summary>
        /// <param name="departmentDesignationId">integer to identify department designation id</param>
        /// <returns>returns level id mapped to given department designation id</returns>
        Task<DesignationLevel> GetLevelIdMappedToDesignationDepartment(int departmentDesignationId);
        /// <summary>
        /// this method is used to fetch all designation department ids if present only among given list
        /// </summary>
        /// <param name="departmentDesignationIdList">list of integers to map departmentDesignationIds</param>
        /// <returns>returns list of Designation level mapping if present from among the given ids</returns>
        Task<List<DesignationLevel>> GetAllDesignationDepartmentMappedToLevelAsync(List<int> departmentDesignationIdList);

        Task<List<Level>> GetAllLevelsByLevelId(List<int> listOfLevelId);
        Task<Level> GetLevelByLevelId(int levelId);
    }
}