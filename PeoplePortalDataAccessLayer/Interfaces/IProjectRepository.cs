using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// This interface is used to provide contract for Project repository
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// this method is used to add new exception in database
        /// </summary>
        /// <param name="project">project table object</param>
        void AddProject(Project project);
        /// <summary>
        /// this method is used to get list of project whose is deleted is false
        /// </summary>
        Task<PagedList<Project>> GetListOfProjectAsync(PagingParams pagingParams);
        /// <summary>
        /// this method is used to get list of project whose is deleted is false
        /// </summary>
        Task<List<Project>> GetListOfProjectAsync();
        /// <summary>
        /// this method is used to retrieve Project details
        /// </summary>
        /// <param name="id"> id of the project </param>
        /// <returns>returns Project details</returns>
        Task<Project> GetProjectByIdAsync(Guid id);
        /// <summary>
        /// this method is used to retrieve Project details
        /// </summary>
        /// <param name="name"> name of the project </param>
        /// <returns>returns Project details</returns>
        Task<Project> GetProjectByNameAsync(string name);
    }
}
