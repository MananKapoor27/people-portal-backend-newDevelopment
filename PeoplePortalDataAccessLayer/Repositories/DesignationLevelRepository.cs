using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalDataAccessLayer.Repositories
{
    public class DesignationLevelRepository : IDesignationLevelRepository
    {
        private readonly PeoplePortalDb _dbContext;

        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public DesignationLevelRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        public async Task<List<int>> ListOfLevelsAssociatedWithListOfDepartmentDesignationId(List<int> listOfDepartmentDesignationId)
        {
            var levels = await _dbContext.DesignationLevel.Where(c => listOfDepartmentDesignationId.Contains(c.DepartmentDesignationId))
                .Select(c => c.LevelId).Distinct().ToListAsync();
            return levels;
        }

    }
}
