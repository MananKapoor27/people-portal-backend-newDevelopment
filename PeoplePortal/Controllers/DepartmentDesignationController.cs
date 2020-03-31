/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved, 
 * No part of this software can be modified or edited without permissions */
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDesignationDto;
using PeoplePortalDomainLayer.Entities.Dto.DesignationDto;
using PeoplePortalDomainLayer.Entities.DTO.DepartmentDesignationDto;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;
namespace PeoplePortal.Controllers
{
    /// <summary>
    /// this DepartmentDesignationController class handles all department and designation's mapping related operations
    /// </summary>

    [Route("api/departmentDesignation")]
    public class DepartmentDesignationController : ControllerBase
    {
        private readonly IDepartmentDesignationService _departmentDesignationService;
        /// <summary>
        /// this is a default constructor for DepartmentDesignationController
        /// </summary>
        /// <param name="departmentDesignationService">object of DepartmentDesignation service for dependency injection</param>
        public DepartmentDesignationController(IDepartmentDesignationService departmentDesignationService)
        {
            _departmentDesignationService = departmentDesignationService;
        }
        /// <summary>
        /// this method is used to fetch all designations of a particular department
        /// </summary>
        /// <param name="departmentId">integer to identify a department</param>
        /// <returns>returns list of designations</returns>
        [HttpGet]
        [Route("designation/all/{departmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DesignationListDto>> GetAllDesignationAsync(int departmentId)
        {
            var result = await _departmentDesignationService.GetAllDesignationAsync(departmentId);
            if (result == null)
                return NoContent();
            return Ok(result);
        }
        /// <summary>
        /// this method is used to add designations to the department
        /// </summary>
        /// <param name="addNewDesignationModel">department id and new designations list</param>
        /// <returns>string </returns>
        [HttpPost]
        [Route("add/designations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentDesignationDto>> AddDesignationsToDepartmentAsync([FromBody]AddNewDesignationDto addNewDesignationModel)
        {
            var result =
                await _departmentDesignationService.AddDesignationToDepartmentAsync(addNewDesignationModel.DepartmentId,
                    addNewDesignationModel.NewDesignations);
            return Created("", result);
        }
        [HttpPost]
        [Route("DepartmentDesignationId")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> GetDepartmentDesignationAsync([FromBody]GetDeparmentDesignationDto getDeparmentDesignationDto)
        {
            return await _departmentDesignationService.GetDepartmentDesignationId(getDeparmentDesignationDto.departmentId, getDeparmentDesignationDto.designationId);
        }
    }
}
