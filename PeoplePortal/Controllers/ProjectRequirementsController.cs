using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeoplePortal.Controllers
{
    [Route("api/projectRequirements")]
    public class ProjectRequirementsController : ControllerBase
    {
        private readonly IProjectRequirementsService _projectRequirementsService;
        /// <summary>
        /// this constructor is uesd for services Dependency Injection 
        /// </summary>
        /// <param name="departmentService">department service object</param>
        public ProjectRequirementsController(IProjectRequirementsService projectRequirementsService)
        {
            _projectRequirementsService = projectRequirementsService;
        }

        /// <summary>
        /// this method is used to add new project requirements in the database
        /// </summary>
        /// <param name="departmentDetails">data of new department</param>
        /// <returns>returns newly added object</returns>
        [HttpPost("AddProjectRequirement")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ProjectRequirements>>> AddProjectRequirements([FromBody]ProjectRequirementDto projectRequirementDtoList)
        {
            var result = await _projectRequirementsService.AddProjectRequirements(projectRequirementDtoList);
            return Ok(result);
        }

        /// <summary>
        /// this method is used to delete a project requirement from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteProjectRequirements")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ProjectRequirements>>> DeleteProjectRequirements(Guid projectId)
        {
            var result = await _projectRequirementsService.DeleteProjectRequirements(projectId);
            return Ok(result);
        }

        /// <summary>
        /// this method is used to delete a requirement from database
        /// </summary>
        /// <param name="requirementId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteRequirement")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteRequirement([FromBody]DeleteRequirementDto deleteRequirementDto)
        {
            await _projectRequirementsService.DeleteRequirement(deleteRequirementDto);
            return Ok();
        }

        /// <summary>
        /// this method is used to get all the project requirements for a project from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("GetProjectRequirements/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<EditProjectRequirementDto>>> GetProjectRequirements(Guid projectId)
        {
            var result = await _projectRequirementsService.GetProjectRequirements(projectId);
            return Ok(result);
        }

        /// <summary>
        /// this method is used to get all the project requirement from database (open, closed, canceled)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("GetAllProjectRequirements/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ViewProjectRequirementDto>>> GetAllProjectRequirements(Guid projectId)
        {
            var result = await _projectRequirementsService.GetAllProjectRequirementsAsync(projectId);
            return result;
        }

        /// <summary>
        /// this method is used to update project requirements for a project to database
        /// </summary>
        /// <param name="editProjectRequirement"></param>
        /// <returns></returns>
        [HttpPut("UpdateProjectRequirement")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EditProjectRequirementDto>> UpdateProjectRequirement([FromBody]EditProjectRequirementDto editProjectRequirement)
        {
            var result = await _projectRequirementsService.UpdateProjectRequirement(editProjectRequirement);
            return Ok(result);
        }

        /// <summary>
        /// this method is used to get count of all the open requirements from database
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOpenRequirementsCount")]
        public async Task<ActionResult<int>> GetAllOpenRequirementsCount()
        {
            var projectRequirements = await _projectRequirementsService.GetAllOpenRequirementsAsync();
            return projectRequirements.Count;
        }

        /// <summary>
        /// this method is used to get all the open requirements from database
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOpenRequirements")]
        public async Task<ActionResult<List<ProjectRequirementCsvDto>>> GetAllOpenRequirements()
        {
            var projectRequirements = await _projectRequirementsService.GetAllOpenRequirementsAsync();
            return projectRequirements;
        }

        /// <summary>
        /// this method will return all requirements from db either fullfilled or not or deleted or not.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRequirements")]
        public async Task<List<ProjectRequirementCsvDto>> GetAllRequirements()
        {
            var result = await _projectRequirementsService.GetAllRequirementsAsync();
            return result;
        }
    }
}
