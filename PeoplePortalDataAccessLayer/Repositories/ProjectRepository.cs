using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// this class is used to do all Project related database operations(feature, basic configuration and exceptions handled here)
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly PeoplePortalDb _dbContext;
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public ProjectRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        /// <summary>
        /// this method is used to add new exception in database
        /// </summary>
        /// <param name="project">project table object</param>
        public void AddProject(Project project)
        {
            _dbContext.Project.Add(project);
        }
        /// <summary>
        /// this method is used to retrieve Project details
        /// </summary>
        /// <param name="id"> id of the project </param>
        /// <returns>returns Project details</returns>
        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            var result = await _dbContext.Project.SingleOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            return result;
        }

        /// <summary>
        /// this method is used to retrieve Project details
        /// </summary>
        /// <param name="name"> name of the project </param>
        /// <returns>returns Project details</returns>
        public async Task<Project> GetProjectByNameAsync(string name)
        {
            try
            {
                var result = await _dbContext.Project.FirstOrDefaultAsync(c => c.Name == name && c.IsDeleted == false);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// this method is used to get list of project whose is deleted is false
        /// </summary>
        public async Task<PagedList<Project>> GetListOfProjectAsync(PagingParams pagingParams)
        {
            var listOfProject = await PagedList<Project>.Create(_dbContext.Project.Where(s => s.IsDeleted == false).OrderBy(c => c.CreatedAt), pagingParams.PageNumber, pagingParams.PageSize);
            return listOfProject;
        }

        /// <summary>
        /// this method is used to get list of project whose is deleted is false
        /// </summary>
        public async Task<List<Project>> GetListOfProjectAsync()
        {
            var listOfProject = await _dbContext.Project.Where(s => s.IsDeleted == false).ToListAsync();
            return listOfProject;
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
