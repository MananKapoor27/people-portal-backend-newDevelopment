using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;

namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>ProjectManagementController</c> class handles all ProjectManagement related operations
    /// </summary>
    /// <remark><para>Here we call functions for CRUD operations on Project 
    /// </para></remark>
    [Route("api/ProjectManagement")]
    public class ProjectManagementController : ControllerBase
    {
        private readonly IProjectManagementService _projectManagementService;
        /// <summary>
        /// this constructor is used for services Dependency Injection 
        /// </summary>
        /// <param name="projectManagementService">projectManagement service object</param>
        public ProjectManagementController(IProjectManagementService projectManagementService)
        {
            _projectManagementService = projectManagementService;
        }

        /// <summary>
        /// this method will add employee to  project
        /// </summary>
        /// <param name="addEmployeeToProject"></param>
        /// <returns>add employee to the project</returns>
        [HttpPost("AddEmployeeToProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeesInProjectDto>> AddEmployeeToProject([FromBody] EmployeesInProjectDto addEmployeeToProject)
        {
            try
            {
                var result = await _projectManagementService.AddEmployeeToProject(addEmployeeToProject);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// this method will fetch all projects in which the employee is  
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>returns the list of projects of employee</returns>
        [HttpGet("GetProjectManagersOfEmployee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Guid>>> GetProjectManagersOfEmployee(Guid employeeId)
        {
            return Ok(await _projectManagementService.GetProjectManagerOfEmployee(employeeId));
        }

        /// <summary>
        /// this method will fetch all projects in which the employee is  
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>returns the list of projects of employee</returns>
        [HttpGet("GetProjectManagersOfProject/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GetEmployeeListDto>>> GetProjectManagersOfProject(Guid projectId)
        {
            return Ok(await _projectManagementService.GetProjectManagersOfProject(projectId));
        }

        /// <summary>
        /// this method will fetch all the employees of a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>returns the list of projects of employee</returns>
        [HttpGet("GetProjectMembers/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<EmployeeBasicDetailsDto>>> GetProjectMembers(Guid projectId)
        {
            return Ok(await _projectManagementService.GetProjectMembers(projectId));
        }

        /// <summary>
        /// this method is used to get all project members name and id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("GetProjectMembersList/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GetEmployeeListDto>>> GetProjectMembersList(Guid projectId)
        {
            return Ok(await _projectManagementService.GetProjectMembersList(projectId));
        }

        /// <summary>
        /// this method will fetch all the projects of an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("GetAllProjectsOfEmployee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ProjectManagementViewDto>>> GetAllProjectsOfEmployee(Guid employeeId)
        {
            var result = await _projectManagementService.GetAllProjectsOfEmployeeAsync(employeeId);
            return Ok(result);
        }
        /// <summary>
        /// this method is used to fetch all Project Manager
        /// </summary>
        /// <param name="employeeId">project id</param>
        /// <returns>return Reporting  manager</returns>
        [HttpGet("ProjectReportingManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> GetProjectReportingManger(Guid employeeId, Guid projectId)
        {
            return Ok(await _projectManagementService.GetReportingManager(employeeId, projectId));
        }
        /// <summary>
        /// this method is used to fetch all Project Manager
        /// </summary>
        /// <param name="employeeId">project id</param>
        /// <returns>return Reporting  manager</returns>
        [HttpGet("AllReportingManagers/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> GetAllReportingManagers(Guid employeeId)
        {
            return Ok(await _projectManagementService.GetAllReportingManagers(employeeId));
        }
        /// <summary>
        /// this method is used to fetch all Project Manager
        /// </summary>
        /// <param name="employeeId">project id</param>
        /// <returns>return list of project manager</returns>
        [HttpGet("Reportee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ProjectManagement>>> GetAllReportee(Guid employeeId)
        {
            return Ok(await _projectManagementService.GetAllReporteeAsync(employeeId));

        }
        /// <summary>
        /// this method is used to get list of all type of employee status
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetEmployeeStatusTypes")]
        public async Task<List<string>> GetEmployeeStatusTypes(bool isNonBillable)
        {
            List<string> result;
            if (!isNonBillable)
            {
                result = new List<string>
            {
                "Billable",
                "Non-Billable",
                "Internal",
                "Bench",
                "Enablers",
                "Management"
            };
            }
            else
            {
                result = new List<string>
            {
                "Billable",
                "Internal",
                "Bench",
                "Resignation",
                "Non-Billable 1",
                "Non-Billable 2",
                "Non-Billable 3",
                "Enablers",
                "Management"
            };
            }
            return result;
        }
        /// <summary>
        /// this method will delete an employee from a project
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete("RemoveEmployeeFromProject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveEmployeeFromProject(Guid projectId, Guid employeeId)
        {
            var result = await _projectManagementService.RemoveEmployeeFromProject(projectId, employeeId);
            return Ok("Employee deleted from project.");
        }

        [HttpPut("UpdateEmployeeInProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GetEmployeeDetailDto>>> UpdateEmployeesInProject([FromBody] EmployeesInProjectDto employeesInProject)
        {

            var updatedListOfEmployees = await _projectManagementService.UpdateEmployeesInProject(employeesInProject);

            return Ok(updatedListOfEmployees);
        }

    }
}
