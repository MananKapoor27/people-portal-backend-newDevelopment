using PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto;
using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Interfaces
{
    public interface IProjectRequirementsService
    {
        /// <summary>
        /// this method is used to add new project requirements in the database
        /// </summary>
        /// <param name="departmentDetails">data of new department</param>
        /// <returns>returns newly added object</returns>
        Task<ProjectRequirementDto> AddProjectRequirements(ProjectRequirementDto projectRequirementDtoList);

        /// <summary>
        /// this method is used to delete a project requirement from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<ProjectRequirements>> DeleteProjectRequirements(Guid projectId);

        /// <summary>
        /// this method is used to delete a requirement from database
        /// </summary>
        /// <param name="requirementId"></param>
        /// <returns></returns>
        Task<ProjectRequirements> DeleteRequirement(DeleteRequirementDto deleteRequirementDto);

        /// <summary>
        /// this method is used to get all the project requirements for a project from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<EditProjectRequirementDto>> GetProjectRequirements(Guid projectId);

        /// <summary>
        /// this method is used to get all the project requirement from database (open, closed, canceled)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<ViewProjectRequirementDto>> GetAllProjectRequirementsAsync(Guid projectId);

        /// <summary>
        /// this method is used to update project requirements for a project to database
        /// </summary>
        /// <param name="editProjectRequirement"></param>
        /// <returns></returns>
        Task<EditProjectRequirementDto> UpdateProjectRequirement(EditProjectRequirementDto editProjectRequirement);

        /// <summary>
        /// this method is used to get the requirement for requirement id
        /// </summary>
        /// <param name="requirementId"></param>
        /// <returns></returns>
        Task<ProjectRequirements> GetRequirement(Guid requirementId);

        /// <summary>
        /// this method is used to get all the open requirements from database
        /// </summary>
        /// <returns></returns>
        Task<List<ProjectRequirementCsvDto>> GetAllOpenRequirementsAsync();

        /// <summary>
        /// this method will return all requirements from db either fullfilled or not or deleted or not.
        /// </summary>
        /// <returns></returns>
        Task<List<ProjectRequirementCsvDto>> GetAllRequirementsAsync();
    }
}
