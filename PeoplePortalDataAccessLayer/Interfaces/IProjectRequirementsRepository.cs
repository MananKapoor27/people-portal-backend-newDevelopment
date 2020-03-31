using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    public interface IProjectRequirementsRepository
    {
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// this method is used to add a new project requirement in the database
        /// </summary>
        /// <param name="departments">this object contains project requirement information</param>
        void AddProjectRequirement(ProjectRequirements projectRequirements);

        /// <summary>
        /// this method is used to delete a project requirement in the database
        /// </summary>
        /// <param name="projectRequirements"></param>
        void DeleteRequirement(ProjectRequirements projectRequirements);

        /// <summary>
        /// this method is used to get all the project requirement from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<ProjectRequirements>> GetProjectRequirement(Guid projectId);

        /// <summary>
        /// this method is used to get all the project requirement from database (open, closed, canceled)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<ProjectRequirements>> GetAllProjectRequirementsAsync(Guid projectId);

        /// <summary>
        /// this method is used to get all the open requirements from database
        /// </summary>
        /// <returns></returns>
        Task<List<ProjectRequirements>> GetAllOpenRequirementsAsync();

        /// <summary>
        /// this method will return all requirements from db either fullfilled or not or deleted or not.
        /// </summary>
        /// <returns></returns>
        Task<List<ProjectRequirements>> GetAllRequirementsAsync();

        /// <summary>
        /// this method is used to get project requirement for a perticular id from database
        /// </summary>
        /// <param name="requirementId"></param>
        /// <returns></returns>
        Task<ProjectRequirements> GetRequirement(Guid requirementId);
    }
}
