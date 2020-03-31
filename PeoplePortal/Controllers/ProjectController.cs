using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;
using Newtonsoft.Json;
using AutoMapper;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;

namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>ProjectController</c> class handles all Project related operations
    /// </summary>
    /// <remark><para>Here we call functions for CRUD operations on Project 
    /// </para></remark>
    [Route("api/Project")]

    public class ProjectController : ControllerBase
    {

        private readonly IProjectService _projectService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IProjectManagementService _projectManagementService;
        private readonly IProjectRequirementsService _projectRequirementsService;

        /// <summary>
        /// this constructor is used for services Dependency Injection 
        /// </summary>
        /// <param name="projectService">project service object</param>
        /// <param name="employeeService">project service object</param>

        public ProjectController(IProjectService projectService, IEmployeeService employeeService, IMapper mapper, IProjectManagementService projectManagementService, IProjectRequirementsService projectRequirementsService)
        {
            _projectService = projectService;
            _employeeService = employeeService;
            _mapper = mapper;
            _projectManagementService = projectManagementService;
            _projectRequirementsService = projectRequirementsService;
        }

        /// <summary>
        /// This method is used to add new project into the database
        /// </summary>
        /// <param name="projectDetails"> project details </param>
        /// <returns>returns successfully added project data with success status</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Project>> AddProjectAsync([FromBody]ProjectDetailsDto projectDetails)
        {
            return Ok(await _projectService.AddProjectAsync(projectDetails));
        }

        /// <summary>
        /// This method is used to update the existing project details
        /// </summary>
        /// <param name="projectDetails">projects details</param>
        /// <returns>successfully updated Project data with success status</returns>
        [HttpPut("UpdateProjectDetails")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Project>> UpdateProjectAsync([FromBody]UpdateProjectDto projectDetails)
        {
            return Ok(await _projectService.UpdateProjectDetailsAsync(projectDetails));
        }

        /// <summary>
        /// this method is used to soft delete Project  
        /// </summary>
        /// <param name="projectId">Id of employee to be deleted</param>
        /// <returns>returns no content</returns>
        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteProjectAsync(Guid projectId)
        {
            await _projectService.DeleteProjectAsync(projectId);
            return Ok("Project has been deleted.");
        }

        /// <summary>
        /// this method is used to fetch all the details of a Project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>return details of a project</returns>
        [HttpGet("GetProjectDetails/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProjectsDetailsDto>> GetProjectDetails(Guid projectId)
        {
            return Ok(await _projectService.GetProjectById(projectId));
        }

        /// <summary>
        /// this method is used to fetch all the details of all the Projects
        /// </summary>
        /// <returns>return details of a project</returns>
        [HttpGet("GetAllProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Project>> GetAllProjectDetails(PagingParams pagingParams)
        {
            var projectDetailsList = await _projectService.GetAllProjects(pagingParams);

            var paginationHeaders = new PaginationHeaderModel
            {
                TotalCount = projectDetailsList.TotalCount,
                TotalPages = projectDetailsList.TotalPages,
                CurrentPage = projectDetailsList.CurrentPage,
                PageSize = projectDetailsList.PageSize
            };
            Response.Headers.Add("pagination", JsonConvert.SerializeObject(paginationHeaders));

            string projectManager = string.Empty;
            var listOfProject = new List<GetProjectsDetailsDto>();
            foreach (var p in projectDetailsList)
            {
                var projectDetails = _mapper.Map<GetProjectsDetailsDto>(p);

                var projectMembersCount = await _projectManagementService.GetProjectMembersCount(p.Id);
                projectDetails.ProjectMembersCount = projectMembersCount;

                var projectRequirements = await _projectRequirementsService.GetProjectRequirements(p.Id);
                projectDetails.OpenRequirementsCount = projectRequirements.Count;

                listOfProject.Add(projectDetails);
            }
            return Ok(listOfProject);
        }

        [HttpGet("GetProjectList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProjectListDto>> GetProjectList()
        {
            return Ok(await _projectService.GetProjectList());
        }
    }
}