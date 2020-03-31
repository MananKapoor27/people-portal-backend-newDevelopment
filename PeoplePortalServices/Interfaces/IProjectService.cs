using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// This interface is used to provide contract for Project service
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// this method is used to add skill into database
        /// </summary>
        /// <param name="project">new project object contains name only</param>
        /// <returns>return newly created skill object</returns>
        Task<Project> AddProjectAsync(ProjectDetailsDto project);

        /// <summary>
        /// this method is used to Delete Project into database
        /// </summary>
        /// <param name="id">Id of the project</param>
        /// <returns>Soft deletes the project </returns>
        Task<Project> DeleteProjectAsync(Guid id);

        /// <summary>
        /// this method is used for updating Project details into database
        /// </summary>
        /// <param name="project">Id of the project </param>
        /// <returns>updates project object</returns>
        Task<Project> UpdateProjectDetailsAsync(UpdateProjectDto project);

        /// <summary>
        /// this method is used to fetch all the Projects
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="project">Object of the project </param>
        /// <returns>returns list of all the Projects</returns>
        Task<PagedList<Project>> GetAllProjects(PagingParams pagingParams);

        /// <summary>
        /// this method is used to fetch list all project
        /// </summary>
        /// <returns></returns>
        Task<List<GetProjectsDetailsDto>> GetProjectList();


        /// <summary>
        /// this method is used to fetch the details of Project
        /// </summary>
        /// <param name="id">id of the project </param>
        /// <returns>returns details of  the Project</returns>
        Task<GetProjectsDetailsDto> GetProjectById(Guid id);

        /// <summary>
        /// this method is used to fetch the details of Project
        /// </summary>
        /// <param name="name">id of the project </param>
        /// <returns>returns details of  the Project</returns>
        Task<Project> GetProjectByName(String name);
    }
}
