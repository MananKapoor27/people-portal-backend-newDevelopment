using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeSkillDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;

namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>EmployeeSkillsController</c> class handles all Employee skill related operations
    /// </summary>
    /// <remark><para>Here we call adding, deleting and updating Employee skills functions 
    /// </para></remark>

    [Route("api/employeeSkills")]
    public class EmployeeSkillsController : ControllerBase
    {
        private readonly IEmployeeSkillsService _employeeSkillsService;

        /// <summary>
        /// this is the default constructor for the Employee Skill service
        /// </summary>
        /// <param name="employeeSkillsService">Employee skillRepository interface object for dependency injection</param>
        public EmployeeSkillsController(IEmployeeSkillsService employeeSkillsService)
        {
            _employeeSkillsService = employeeSkillsService;
        }

        /// <summary>
        /// This method is used to create new Employee Skills
        /// </summary>
        /// <param name="employeeSkill"> Employee skill information</param>
        /// <returns>returns new Employee skill with success status</returns>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeSkillDto>> AddEmployeeSkillAsync([FromBody]EmployeeSkillDto employeeSkill)
        {
            var result = await _employeeSkillsService.AddEmployeeSkillAsync(employeeSkill);
            return Created("", result);
        }

        /// <summary>
        /// This method is used for updating Employee Skills
        /// </summary>
        /// <param name="employeeSkill">Employee  skill information</param>
        /// <returns>updates Employee skill with ok status</returns>        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeSkillDto>> UpdateEmployeeSkillAsync([FromBody]EmployeeSkillDto employeeSkill)
        {
            var result = await _employeeSkillsService.UpdateEmployeeSkillAsync(employeeSkill);
            return Ok(result);
        }

        /// <summary>
        /// This method is used for deleting Employee Skills
        /// </summary>
        /// <param name="employeeSkill">Employee  skill information</param>
        /// <returns>deletes Employee skill with ok status</returns>        
        [HttpDelete]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteEmployeeSkillAsync([FromBody]Guid employeeId)
        {
            await _employeeSkillsService.DeleteEmployeeSkillAsync(employeeId);
            return Ok();
        }

    }
}