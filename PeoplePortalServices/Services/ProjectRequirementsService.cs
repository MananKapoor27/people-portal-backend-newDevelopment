using AutoMapper;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Services
{
    public class ProjectRequirementsService : IProjectRequirementsService
    {
        private readonly IProjectRequirementsRepository _projectRequirementsRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// this is the default constructor for the department service class and used for repository and serivces dependency Injection
        /// </summary>
        /// <param name="departmentRepository">department repository object</param>
        /// <param name="departmentDesignationRepository">department designation repository object for dependency injection</param>
        public ProjectRequirementsService(IProjectRequirementsRepository projectRequirementsRepository, IMapper mapper, IEmployeeService employeeService, IProjectRepository projectRepository)
        {
            _projectRequirementsRepository = projectRequirementsRepository;
            _mapper = mapper;
            _employeeService = employeeService;
            _projectRepository = projectRepository;

        }

        /// <summary>
        /// this method is used to add new project requirements in the database
        /// </summary>
        /// <param name="departmentDetails">data of new department</param>
        /// <returns>returns newly added object</returns>
        public async Task<ProjectRequirementDto> AddProjectRequirements(ProjectRequirementDto projectRequirementDto)
        {
            for (var i = 0; i < projectRequirementDto.RequiredEmployee; i++)
            {
                var projReq = new ProjectRequirements
                {
                    ProjectId = projectRequirementDto.ProjectId,
                    DesignationName = projectRequirementDto.DesignationName,
                    SkillName = projectRequirementDto.SkillName,
                    Comments = projectRequirementDto.Comments,
                    CreatedAt = DateTime.UtcNow,
                    RequirementStartDate = projectRequirementDto.RequirementStartDate,
                    RequirementEndDate = projectRequirementDto.RequirementEndDate,
                    RequirementBillingType = projectRequirementDto.RequirementBillingType

                    //CreatedBy = id TODO
                };
                _projectRequirementsRepository.AddProjectRequirement(projReq);
                await _projectRequirementsRepository.SaveChangesAsync();
            }
            return projectRequirementDto;
        }

        /// <summary>
        /// this method is used to delete a project requirement from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<ProjectRequirements>> DeleteProjectRequirements(Guid projectId)
        {
            var projectRequirementList = await _projectRequirementsRepository.GetProjectRequirement(projectId);
            foreach (var projectRequirement in projectRequirementList)
            {
                _projectRequirementsRepository.DeleteRequirement(projectRequirement);
                await _projectRequirementsRepository.SaveChangesAsync();
            }
            return projectRequirementList;
        }

        /// <summary>
        /// this method is used to delete a requirement from database
        /// </summary>
        /// <param name="requirementId"></param>
        /// <returns></returns>
        public async Task<ProjectRequirements> DeleteRequirement(DeleteRequirementDto deleteRequirementDto)
        {
            var requirement = await _projectRequirementsRepository.GetRequirement(deleteRequirementDto.RequirementId);
            requirement.IsDeleted = true;
            requirement.DeletionReason = deleteRequirementDto.DeletionReason;
            await _projectRequirementsRepository.SaveChangesAsync();
            return requirement;
        }

        /// <summary>
        /// this method is used to get all the project requirements for a project from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<EditProjectRequirementDto>> GetProjectRequirements(Guid projectId)
        {
            var projectRequirementList = await _projectRequirementsRepository.GetProjectRequirement(projectId);
            var projectRequirementDto = _mapper.Map<List<EditProjectRequirementDto>>(projectRequirementList);
            return projectRequirementDto;
        }

        /// <summary>
        /// this method is used to get all the project requirement from database (open, closed, canceled)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<ViewProjectRequirementDto>> GetAllProjectRequirementsAsync(Guid projectId)
        {
            var projectRequirementsList = await _projectRequirementsRepository.GetAllProjectRequirementsAsync(projectId);
            var resultProjectRequirementsList = new List<ViewProjectRequirementDto>();
            foreach(var requirement in projectRequirementsList)
            {
                var projectRequirement = _mapper.Map<ViewProjectRequirementDto>(requirement);
                
                if (requirement.IsFullfilled == false && requirement.IsDeleted == false)
                    projectRequirement.Status = "Open";
                else if (requirement.IsFullfilled == true && requirement.IsDeleted == false)
                    projectRequirement.Status = "Closed";
                else if (requirement.IsDeleted == true)
                    projectRequirement.Status = "Canceled";

                resultProjectRequirementsList.Add(projectRequirement);
            }
            return resultProjectRequirementsList;
        }

        /// <summary>
        /// this method is used to get all the open requirements from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProjectRequirementCsvDto>> GetAllOpenRequirementsAsync()
        {
            var projectRequirements = await _projectRequirementsRepository.GetAllOpenRequirementsAsync();
            var formatedData = await FormatDataForCSV(projectRequirements);
            return formatedData;
        }

        /// <summary>
        /// this method will return all requirements from db either fullfilled or not or deleted or not.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProjectRequirementCsvDto>> GetAllRequirementsAsync()
        {
            var projectRequirements = await _projectRequirementsRepository.GetAllRequirementsAsync();
            var formatedData = await FormatDataForCSV(projectRequirements);
            return formatedData;
        }

        /// <summary>
        /// this method is used to format data for csv download purpose.
        /// </summary>
        /// <param name="projectRequirementsList"></param>
        /// <returns></returns>
        public async Task<List<ProjectRequirementCsvDto>> FormatDataForCSV(List<ProjectRequirements> projectRequirementsList)
        {
            var openRequirementsList = new List<ProjectRequirementCsvDto>();
            projectRequirementsList.OrderBy(y => y.ProjectId);
            foreach (var requirement in projectRequirementsList)
            {
                var openRequirements = _mapper.Map<ProjectRequirementCsvDto>(requirement);
                if (requirement.CreatedBy != null)
                    openRequirements.CreatedBy = await _employeeService.GetEmployeeNameById((Guid)requirement.CreatedBy);
                if (requirement.ModifiedBy != null)
                    openRequirements.ModifiedBy = await _employeeService.GetEmployeeNameById((Guid)requirement.ModifiedBy);
                var project = await _projectRepository.GetProjectByIdAsync(requirement.ProjectId);
                if (project != null)
                    openRequirements.ProjectName = project.Name;
                if (requirement.ResourceAllocated != null)
                {
                    openRequirements.ResourceAllocatedId = requirement.ResourceAllocated;
                    openRequirements.ResourceAllocatedName = await _employeeService.GetEmployeeNameById(requirement.ResourceAllocated);
                }

                if (requirement.IsFullfilled == false && requirement.IsDeleted == false)
                    openRequirements.Status = "Open";
                else if (requirement.IsFullfilled == true && requirement.IsDeleted == false)
                    openRequirements.Status = "Closed";
                else if (requirement.IsDeleted == true)
                    openRequirements.Status = "Canceled";

                openRequirementsList.Add(openRequirements);
            }
            return openRequirementsList;
        }

        /// <summary>
        /// this method is used to update project requirements for a project to database
        /// </summary>
        /// <param name="editProjectRequirement"></param>
        /// <returns></returns>
        public async Task<EditProjectRequirementDto> UpdateProjectRequirement(EditProjectRequirementDto editProjectRequirement)
        {
            var oldProjectRequirementList = await _projectRequirementsRepository.GetRequirement(editProjectRequirement.Id);
            _mapper.Map(editProjectRequirement, oldProjectRequirementList);
            oldProjectRequirementList.LastModifiedAt = DateTime.UtcNow;
            //oldProjectRequirementList.ModifiedBy = id; TODO
            await _projectRequirementsRepository.SaveChangesAsync();
            return editProjectRequirement;
        }

        /// <summary>
        /// this method is used to get the requirement for requirement id
        /// </summary>
        /// <param name="requirementId"></param>
        /// <returns></returns>
        public async Task<ProjectRequirements> GetRequirement(Guid requirementId)
        {
            var requirement = await _projectRequirementsRepository.GetRequirement(requirementId);
            return requirement;
        }
    }
}
