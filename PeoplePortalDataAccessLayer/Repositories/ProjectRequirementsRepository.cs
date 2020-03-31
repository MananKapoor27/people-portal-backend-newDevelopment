using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalDataAccessLayer.Repositories
{
    public class ProjectRequirementsRepository : IProjectRequirementsRepository
    {
        private readonly PeoplePortalDb _dbContext;
        private bool _disposed = false;
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public ProjectRequirementsRepository(PeoplePortalDb peoplePortalDb)
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
        /// this method is used to add a new project requirement in the database
        /// </summary>
        /// <param name="departments">this object contains project requirement information</param>
        public void AddProjectRequirement(ProjectRequirements projectRequirements)
        {
            _dbContext.ProjectRequirements.Add(projectRequirements);
        }

        /// <summary>
        /// this method is used to delete a requirement in the database
        /// </summary>
        /// <param name="projectRequirements"></param>
        public void DeleteRequirement(ProjectRequirements projectRequirements)
        {
            _dbContext.ProjectRequirements.Remove(projectRequirements);
        }

        /// <summary>
        /// this method is used to get all the project requirement from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<ProjectRequirements>> GetProjectRequirement(Guid projectId)
        {
            var projectRequirement = await _dbContext.ProjectRequirements.Where(x => x.ProjectId == projectId && x.IsFullfilled == false && x.IsDeleted == false).ToListAsync();
            return projectRequirement;
        }

        /// <summary>
        /// this method is used to get all the project requirement from database (open, closed, canceled)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<ProjectRequirements>> GetAllProjectRequirementsAsync(Guid projectId)
        {
            var projectRequirements = await _dbContext.ProjectRequirements.Where(x => x.ProjectId == projectId).ToListAsync();
            return projectRequirements;
        }

        /// <summary>
        /// this method is used to get all the open requirements from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProjectRequirements>> GetAllOpenRequirementsAsync()
        {
            var projectRequirements = await _dbContext.ProjectRequirements.Where(x => x.IsFullfilled == false && x.IsDeleted == false).ToListAsync();
            return projectRequirements;
        }

        /// <summary>
        /// this method will return all requirements from db either fullfilled or not or deleted or not.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProjectRequirements>> GetAllRequirementsAsync()
        {
            var projectRequirements = await _dbContext.ProjectRequirements.ToListAsync();
            return projectRequirements;
        }

        /// <summary>
        /// this method is used to get project requirement for a perticular id from database
        /// </summary>
        /// <param name="requirementId"></param>
        /// <returns></returns>
        public async Task<ProjectRequirements> GetRequirement(Guid requirementId)
        {
            var requirement = await _dbContext.ProjectRequirements.SingleOrDefaultAsync(x => x.Id == requirementId && x.IsDeleted == false);
            return requirement;
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
