using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.ReportingManagerDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;

namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>ReportingManagerController</c> class handles all Project related operations
    /// </summary>
    /// <remark><para>Here we call functions for CRUD operations on Project 
    /// </para></remark>
    [Route("api/ReportingManager")]
    [ApiController]
    public class ReportingManagerController : ControllerBase
    {
        private readonly IReportingManagerService _reportingManagerService;
        public ReportingManagerController(IReportingManagerService reportingManagerService)
        {
            _reportingManagerService = reportingManagerService;
        }
        /// <summary>
        /// this method is used to fetch all Project Manager
        /// </summary>
        /// <param name="employeeId">project id</param>
        /// <returns>return Reporting  manager</returns>
        [HttpGet("ReportingManager/{employeeId}")]
        public async Task<ActionResult<Guid>> GetReportingManger(Guid employeeId)
        {
            return Ok(await _reportingManagerService.GetReportingManager(employeeId));
        }

        /// <summary>
        /// this method is used to fetch all Project Manager
        /// </summary>
        /// <param name="employeeId">project id</param>
        /// <returns>return list of project manager</returns>
        [HttpGet("Reportee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Guid>>> GetAllReportee(Guid employeeId)
        {
            return Ok(await _reportingManagerService.GetAllReporteeAsync(employeeId));

        }
        /// <summary>
        /// this method is used to reporting manager to employee
        /// </summary>
        /// <param name="addingReportingManager">addingReportingManager object</param>
        /// <returns>return list ofsuccesfull added reporting manageer to employee</returns>
        [HttpPost("AssignReportingManager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReportingManagerDto>> AssignRepportingManagertoEmployee(AddingReportingManagerDto addingReportingManager)
        {
            return Ok(await _reportingManagerService.AddEmployeeReportingManagerAsync(addingReportingManager.ReportingManagerId, addingReportingManager.EmployeeId));

        }

        [HttpPut("ChangeReportingManagerOfEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReportingManager>> ChangeReportingManagerOfEmployee(UpdateReportingManagerDto updateReportingManagerDto)
        {
            return Ok(await _reportingManagerService.ChangeEmployeeReportingManagerAsync(updateReportingManagerDto));
        }
    }
}
