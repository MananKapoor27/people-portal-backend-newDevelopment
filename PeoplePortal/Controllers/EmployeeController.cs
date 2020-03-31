/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved, 
 * No part of this software can be modified or edited without permissions */
using Microsoft.AspNetCore.Mvc;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeDto;
using PeoplePortalServices.Shared.AWS;
using System.Linq;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.HelperModels;
using Newtonsoft.Json;
using AutoMapper;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;
using PeoplePortalServices.Shared.ElasticSearch;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.ElasticSearchModels;

namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>EmployeeController</c> class handles all employee related operations
    /// </summary>

    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAWSServices _awsService;
        private readonly IMapper _mapper;
        private readonly IDepartmentDesignationService _departmentDesignationService;
        private readonly IElasticSearchService _elasticSearchService;

        /// <summary>
        /// this is a default constructor for EmployeeController
        /// </summary>
        /// <param name="employeeService">object of employee service for dependency injection</param>
        public EmployeeController(IEmployeeService employeeService, IAWSServices awsService, IMapper mapper, IDepartmentDesignationService departmentDesignationService, IElasticSearchService elasticSearchService)
        {
            _employeeService = employeeService;
            _awsService = awsService;
            _mapper = mapper;
            _departmentDesignationService = departmentDesignationService;
            _elasticSearchService = elasticSearchService;
        }

        /// <summary>
        /// this method is used to add new employee's official information
        /// </summary>
        /// <param name="employeeOfficialDetails">Employee Official Details</param>
        /// <returns>returns newly created employee details</returns>
        [HttpPost("add/official-information")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AddNewEmployeeOfficialInformationAsync([FromBody]AddEmployeeOfficialInformationDto employeeOfficialDetails)
        {
            var id = await _employeeService.AddNewEmployeeOfficialInformationAsync(employeeOfficialDetails);
            if (id.Equals(null))
                return BadRequest("Employee already exists.");
            return Created("", id);
        }


        /// <summary>
        /// this method is used to add new employee's official details or edit existing employee's official details
        /// </summary>
        /// <param name="employeeOfficialDetails">Employee Official Details</param>
        /// <returns>returns no content</returns>
        ///</summary>
        [HttpPut]
        [Route("edit/official-information")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditEmployeeOfficialDetailsAsync([FromBody]EditEmployeeOfficialInformationDto employeeOfficialDetails)
        {
            await _employeeService.EditEmployeeOfficialDetailsAsync(employeeOfficialDetails);
            return Ok();
        }


        /// <summary>
        /// this method is used to add new employee's personal details or edit existing employee's personal details
        /// </summary>
        /// <param name="employeePersonalDetails">#mployee Personal Details</param>
        /// <returns>returns no content</returns>
        ///</summary>
        [HttpPut]
        [Route("edit/personal-information")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditEmployeePersonalDetailsAsync([FromBody]EditEmployeePersonalInformationDto employeePersonalDetails)
        {
            await _employeeService.EditEmployeePersonalDetailsAsync(employeePersonalDetails);
            return Ok();
        }


        /// <summary>
        /// this method is used to add new employee's educational details or edit existing employee's educational details
        /// </summary>
        /// <param name="employeeEducationalDetails">Employee Educational Details</param>
        /// <returns>returns newly created employee educational details</returns>
        [HttpPut]
        [Route("edit/educational-information")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditEmployeeEducationalDetailsAsync([FromBody]EditEmployeeEducationalInformationDto employeeEducationalDetails)
        {
             await _employeeService.EditEmployeeEducationalDetailsAsync(employeeEducationalDetails);
             return Ok();
        }


        /// <summary>
        /// this method is used to fetch the official details of the employee
        /// </summary>
        /// <param name="employeeId">employee id</param>
        /// <returns>return list of information about the employee</returns>
        [HttpGet("official-information/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EditEmployeeOfficialInformationDto>> GetEmployeeOfficialDetails(Guid employeeId)
        {
            var result = await _employeeService.GetEmployeeOfficialDetails(employeeId);
            return Ok(result);
        }


        /// <summary>
        /// this method is used to fetch the personal details of the employee
        /// </summary>
        /// <param name="employeeId">employee id</param>
        /// <returns>return list of information about the employee</returns>
        [HttpGet("personal-information/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EditEmployeePersonalInformationDto>> GetEmployeePersonalDetailsById(Guid employeeId)
        {
            var result = await _employeeService.GetEmployeePersonalDetails(employeeId);
            return Ok(result);
        }


        /// <summary>
        /// this method is used to fetch the educational details of the employee 
        /// </summary>
        /// <param name="employeeEducationDetails"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("educational-information/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EditEmployeeEducationalInformationDto>> GetEmployeeEducationalDetailsAsync(Guid employeeId)
        {
            var result = await _employeeService.GetEmployeeEducationalDetails(employeeId);
            if (result == null)
                return BadRequest("Cannot fetch Employee Education Details");
            return Ok(result);
        }


        /// <summary>
        /// this method is used to fetch list of gender for the employees
        /// </summary>
        /// <returns>returns list of gender</returns>
        [HttpGet]
        [Route("gender")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<string>>> GetGenderList()
        {
            return Ok(_employeeService.GenderList());
        }
        /// <summary>
        /// this method is used to fetch list of Social Handle Title for the employees
        /// </summary>
        /// <returns>returns list of Social Handle Title</returns>
        [HttpGet]
        [Route("socialHandles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<string>>> SocialHandleTitleList()
        {
            return Ok(_employeeService.SocialHandlesTitle());
        }
     
        /// <summary>
        /// this method is used to fetch all the details of a employee
        /// </summary>
        /// <param name="companyEmail">company Email</param>
        /// <returns>return list of information about the employee</returns>
        [HttpGet("email/{companyEmail}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EditEmployeeDto>> GetEmployeeDetailsByEmail(string companyEmail)
        {
            var result = await _employeeService.GetEmployeeDetailsByEmail(companyEmail);
            return Ok(result);
        }
        /// <summary>
        /// this method is used to fetch list of record of all the employee
        /// </summary>
        /// <param name="pageNumber">pagenumber</param>
        /// <returns>return list of employee details</returns>         
        /// <summary>
        [HttpGet]
        [Route("allEmployeeDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GetAllEmployeeDetailsDto>>> GetAllEmployeeDetailsAsync(PagingParams pagingParams)
        {
            var result = await _employeeService.GetAllEmployeeDetails(pagingParams);
            var paginationHeaders = new PaginationHeaderModel
            {
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize
            };
            Response.Headers.Add("pagination", JsonConvert.SerializeObject(paginationHeaders));

            var employeeList = new List<GetAllEmployeeDetailsDto>();
            foreach (var emp in result)
            {
                var designationDetails = await _departmentDesignationService.GetDesignationByDepartmentDesignationId(emp.DepartmentDesignationId);
                var employeeDetails = _mapper.Map<GetAllEmployeeDetailsDto>(emp);
                employeeDetails.DesignationTitle = designationDetails;
                var employeeName = string.Format("{0} {1} {2}", emp.FirstName, emp.MiddleName, emp.LastName);
                employeeName = string.Join(" ", employeeName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                employeeDetails.Name = employeeName;
                employeeList.Add(employeeDetails);
            }

            return Ok(employeeList);
        }

        /// this method is used to edit the employee details
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns>returns no content</returns>
        [HttpPost("csvUpload")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<List<CsvResultDto>> BulkAddEmployees(IFormFile formFile)
        {
            var resultTable = _employeeService.GetListOfEmployees(formFile).ToList();
            //serialize and split
            var finalTable = _employeeService.ProcessListOfEmployees(resultTable);
            var InsertIntoDatabase = _employeeService.AddEmployeesToDatabase(finalTable);
            return InsertIntoDatabase;
        }













        /// <summary>
        /// this method is used to soft delete employee  
        /// </summary>
        /// <param name="employeeId">Id of employee to be deleted</param>
        /// <returns>returns no content</returns>
        [HttpDelete("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid employeeId)
        {
            await _employeeService.DeleteEmployeeAsync(employeeId);
            return Ok();
        }

        /// <summary>
        /// this method is used to fetch employeeList
        /// </summary>
        /// <returns></returns>
        [HttpGet("allEmployeeList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetEmployeeListDto>> GetEmployeeList()
        {
            var result = await _employeeService.GetEmployeeList();
            return Ok(result);
        }

        /// <summary>
        /// this method is used to search for searchString in employee's name
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>List of employee</returns>
        [HttpGet("employeeSearch/{searchString}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetEmployeeListDto>> EmployeeSearch(string searchString)
        {
            var result = await _employeeService.EmployeeSearch(searchString);
            return Ok(result);
        }

        /// <summary>
        /// this method is used to upload and update profile picture of the employee
        /// </summary>
        /// <param name="image"></param>
        /// <returns>string telling the status of the profile picture</returns>
        [HttpPost("image/upload/{employeeId}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UploadImageAsync(Guid employeeId, IFormFile image)
        {
            return Ok(await _awsService.UploadObject(employeeId, image));
        }
        /// <summary>
        /// this method is used to delete the profile picture of the employee
        /// </summary>
        /// <param name="userId">guid of the employee</param>
        /// <returns>string telling the status of the deletion of the profile picture</returns>
        [HttpDelete]
        [Route("image/delete/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteImageAsync(Guid employeeId)
        {
            return Ok(await _awsService.RemoveObject(employeeId));
        }

        [HttpPost("designationAndSkillEmployeeSearch")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GetAllEmployeeDetailsDto>>> DesignationAndSkillEmployeeSearch([FromBody]DesignationAndSkillEmployeeSearch designationAndSkillEmployeeSearch)
        {
            var result = await _employeeService.DesignationAndSkillEmployeeSearch(designationAndSkillEmployeeSearch);
            return Ok(result);
        }

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("AddEmployeeToElasticSearch")]
        public async Task<ActionResult<bool>> AddEmployeeToElasticSearch([FromBody]ElasticEmployeeDetails employee)
        {
            var result = await _elasticSearchService.AddUserToElasticSearch(employee);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet("ElasticSearch")]
        public async Task<ActionResult<List<ElasticEmployeeDetails>>> ElasticSearch(string searchString)
        {
            var result = await _elasticSearchService.Search(searchString);
            return result.Documents.ToList<ElasticEmployeeDetails>();
        }
    }
}
