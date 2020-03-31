/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved, 
 * No part of this software can be modified or edited without permissions */
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;
namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>DepartmentController</c> class handles all department related operations
    /// </summary>
    /// <remark><para>Here we call functions for CRUD operations on department 
    /// </para></remark>

    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        /// <summary>
        /// this constructor is uesd for services Dependency Injection 
        /// </summary>
        /// <param name="departmentService">department service object</param>
        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method is used to add new departments into the database
        /// </summary>
        /// <param name="departmentDetails"> department details </param>
        /// <returns>returns successfully added department data with success status</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentUpdateDto>> AddDepartmentsAsync([FromBody]DepartmentCreateDto departmentDetails)
        {
            var result = await _departmentService.AddDepartmentsAsync(departmentDetails);

            if (result != null)
                return Created("", result);
            return BadRequest("Department already exists.");
        }

        /// <summary>
        /// This method is used to update the existing department
        /// </summary>
        /// <param name="department">department details</param>
        /// <returns>successfully updated department data with success status</returns>
        [HttpPut(Name = "UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentUpdateDto>> UpdateDepartmentAsync(Department department)
        {
            var result = await _departmentService.UpdateDepartmentAsync(department);
            if (result != null)
                return Created("", result);
            return BadRequest(string.Format("No department exists with department id = {0}", department.Id));
        }

        /// <summary>
        /// This method used to delete department
        /// </summary>
        /// <param name="departmentId"> Id of the department to be deleted</param>
        /// <returns>successfully deleted data with success status</returns>
        [HttpDelete]
        [Route("{departmentId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentUpdateDto>> DeleteDepartmentAsync(int departmentId)
        {
            var result = await _departmentService.DeleteDepartmentAsync(departmentId);
            if (result != null)
                return Ok(result);
            return BadRequest(string.Format("No department exists with department id = {0}", departmentId));
        }

        /// <summary>
        /// this method is used to fetch list of all departments in the database
        /// </summary>
        /// <returns>returns list of departments</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentUpdateDto>> GetAllDepartmentAsync()
        {
            var result = await _departmentService.GetAllDepartmentAsync();
            if (result != null)
                return Ok(result);
            return NoContent();
        }

        /// <summary>
        /// this method is used to fetch a department by id in the database
        /// </summary>
        /// <returns>returns department for a specific id</returns>
        [HttpGet("{departmentId:int}", Name = "GetDepartmentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentUpdateDto>> GetDepartmentByIdAsync(int departmentId)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(departmentId);

            if (result != null)
                return Ok(result);
            return BadRequest(string.Format("No department exists with department id = {0}", departmentId));
        }

        /// <summary>
        /// this method is used to fetch a department by name in the database
        /// </summary>
        /// <returns>returns department for a specific name</returns>
        [HttpGet("{departmentName}", Name = "GetDepartmentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentUpdateDto>> GetDepartmentByNameAsync(string departmentName)
        {
            var result = await _departmentService.GetDepartmentByNameAsync(departmentName);

            if (result != null)
                return Ok(result);
            return BadRequest(string.Format("No department exists with department name = {0}", departmentName));
        }
    }
}
