using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeTypeDto;
using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeoplePortal.Controllers
{
    [Route("api/EmployeeType")]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IEmployeeTypeService _employeeTypeService;

        public EmployeeTypeController(IEmployeeTypeService employeeTypeService)
        {
            _employeeTypeService = employeeTypeService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeTypeDto>> AddEmployeeTypeAsync([FromBody]AddEmployeeTypeDto employeeType)
        {
            try
            {
                if (employeeType != null)
                {
                    var result = await _employeeTypeService.AddEmployeeTypeAsync(employeeType.EmployeeTypeName);
                    return Created("Employee Type successfully added.", result);
                }
                else
                    return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeTypeDto>> UpdateEmployeeTypeAsync([FromBody]EmployeeTypeDto employeeType)
        {
            var result = await _employeeTypeService.UpdateEmployeeTypeAsync(employeeType);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<EmployeeTypeDto>>> GetAllEmployeeTypeAsync()
        {
            var result = await _employeeTypeService.GetAllEmployeeTypeAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteEmployeeTypeAsync(Guid id)
        {
            await _employeeTypeService.DeleteEmployeeTypeAsync(id);
            return Ok();
        }
    }
}
