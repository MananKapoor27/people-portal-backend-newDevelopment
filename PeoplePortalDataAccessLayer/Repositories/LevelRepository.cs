using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDomainLayer.Entities.Models;
namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// this class is used to do all employee Level related database operations(level and designation level handled here)
    /// </summary>
    public class LevelRepository : ILevelRepository
    {
        private readonly PeoplePortalDb _dbContext;
        
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public LevelRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }
        /// <summary>
        /// this method is used to add new levels in database
        /// </summary>
        /// <param name="levels">this object contains new level information</param>
        public void AddLevel(Level levels)
        {
            _dbContext.Levels.Add(levels);
        }
        /// <summary>
        /// this method is used to fetching title of last row
        /// </summary>
        /// <returns>returns title of last row</returns>
        public string GetLastRowTitle()
        {
            var lastRowTitle = _dbContext.Levels.LastOrDefault().Title;
            return lastRowTitle;
        }
        /// <summary>
        /// this method is used to fetch all Department Designation Id By Level
        /// </summary>
        /// <param name="levelId">integer to identify level</param>
        /// <returns>returns list of integer ids of departmentDesignation</returns>
        public async Task<List<int>> GetAllDepartmentDesignationIdByLevelAsync(int levelId)
        {
            var departmentDesignationIdList = await _dbContext.DesignationLevel.Where(c => c.LevelId == levelId
                                                                                           && c.IsDeleted == false)
                                             .Select(c => c.DepartmentDesignationId)
                                             .ToListAsync();
            return departmentDesignationIdList;
        }
        /// <summary>
        /// this method is used to fetch all designation department ids if present only among given list
        /// </summary>
        /// <param name="departmentDesignationIdList">list of integers to map departmentDesignationIds</param>
        /// <returns>returns list of departmentDesignationIds if present from among the given ids</returns>
        public async Task<List<int>> GetAllDesignationDepartmentIdMappedToLevelAsync(List<int> departmentDesignationIdList)
        {
            var idList = await _dbContext.DesignationLevel
                                        .Where(c => departmentDesignationIdList.Contains(c.DepartmentDesignationId) && c.IsDeleted == false)
                                        .Select(c => c.DepartmentDesignationId)
                                        .ToListAsync();
            return idList;
        }
        /// <summary>
        /// this method is used to fetch all designation department ids if present only among given list
        /// </summary>
        /// <param name="departmentDesignationIdList">list of integers to map departmentDesignationIds</param>
        /// <returns>returns list of Designation level mapping if present from among the given ids</returns>
        public async Task<List<DesignationLevel>> GetAllDesignationDepartmentMappedToLevelAsync(List<int> departmentDesignationIdList)
        {
            var designationLevelList = await _dbContext.DesignationLevel
                                                        .Include(c => c.DepartmentDesignation)
                                                        .Include(c => c.Level)
                                                        .Where(c => departmentDesignationIdList.Contains(c.DepartmentDesignationId)
                                                            && c.IsDeleted == false)
                                                        .ToListAsync();
            return designationLevelList;
        }
        /// <summary>
        /// this method is used to fetch Level id mapped to given department designation id
        /// </summary>
        /// <param name="departmentDesignationId">integer to identify department designation id</param>
        /// <returns>returns level id mapped to given department designation id</returns>
        public async Task<DesignationLevel> GetLevelIdMappedToDesignationDepartment(int departmentDesignationId)
        {
            var levelId = (await _dbContext.DesignationLevel.FirstOrDefaultAsync(c => c.DepartmentDesignationId == departmentDesignationId));
            return levelId;
        }
        

        public async Task<List<Level>> GetAllLevelsByLevelId(List<int> listOfLevelId)
        {
            var levels = await _dbContext.Levels.Where(c => listOfLevelId.Contains(c.Id)).ToListAsync();               
            return levels;
        }


        public async Task<Level> GetLevelByLevelId(int levelId)
        {
            var level = await _dbContext.Levels.SingleOrDefaultAsync(c => c.Id== levelId && c.IsDeleted == false);
            return level;
        }
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}