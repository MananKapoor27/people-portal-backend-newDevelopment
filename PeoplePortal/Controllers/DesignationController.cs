/* Copyright © 2020 NineLeaps Technologies Pvt Ltd ALL Rights Reserved, 
 * No part of this software can be modified or edited without permissions */
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.DesignationDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;
namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>DesignationController</c> class handles all designation related operations
    /// </summary>    
    /// <remark><para>Here we call functions for CRUD operations on designations
    /// </para></remark>

    [Route("api/designation")]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;
        /// <summary>
        /// this constructor is uesd for services Dependency Injection 
        /// </summary>
        /// <param name="designationService">designation service object</param>
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }
        /// <summary>
        /// this method is used for deleting the 
        /// </summary>
        /// <param name="designationId">designation id</param>
        /// <returns>string statting the status of the method</returns>
        [HttpDelete]
        [Route("{designationId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Designation>> DeleteDesignationAsync(int designationId)
        {
            var result = await _designationService.DeleteDesignationAsync(designationId);
            return Ok(result);
        }

        /// <summary>
        /// this method is used to get the list of all designation
        /// </summary>
        /// <returns></returns>
        [HttpGet("getDesignationList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<DesignationListDto>> GetDesignationList()
        {
            var result = await _designationService.GetDesignationList();
            return result;
        }
    }
}