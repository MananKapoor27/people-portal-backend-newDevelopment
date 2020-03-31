using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePortalDomainLayer.Entities.Dto.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.DepartmentDesignationDto;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;

namespace PeoplePortal.Controllers
{
    /// <summary>
    /// this Level Controller class handles all department and designation and level mapping related operations
    /// </summary>

    [Route("api/level")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;

        /// <summary>
        /// this is a default constructor for LevelController
        /// </summary>
        /// <param name="levelService">object of levelService service for dependency injection</param>
        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        /// <summary>
        /// this method is used to add new level in the database
        /// </summary>
        /// <param name="departmentDesignationViewModel">model that contains department and designation details to be mapped in a level</param>
        /// <returns>returns success message if successfully added</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddNewLevelAsync([FromBody]DepartmentDesignationDto departmentDesignationViewModel)
        {
            //var departmentDesignationList = await _levelService.GetDepartmentDesignationIdList(departmentDesignationViewModel.DepartmentId);
            var levelExists = await _levelService.CheckIfLevelAlreadyExistsInTheDepartment(departmentDesignationViewModel.DepartmentId,departmentDesignationViewModel.NewLevelName);
            if (levelExists)
                return BadRequest("Level with this name already exists in this department !");
            await _levelService.AddNewLevelAsync(departmentDesignationViewModel);
            return Created("", "");
        }       

        /// <summary>
        /// this method is used to fetch all designations of a department which are not mapped to a level
        /// </summary>
        /// <param name="departmentId">integer to map the department</param>
        /// <returns>returns list of designations details not mapped to a level</returns>
        [HttpGet]
        [Route("designation/notMapped/{departmentId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentDesignationDto>> GetAllDesignationsNotMappedWithLevelAsync(int departmentId)
        {
            var result = await _levelService.GetAllDesignationsNotMappedWithLevelAsync(departmentId);
            return Ok(result);
        }

        /// <summary>
        /// this method is used to fetch all designations of a department which are mapped to a level
        /// </summary>
        /// <param name="departmentId">integer to map the department</param>
        /// <returns>returns list of designations details mapped to a level</returns>
        [HttpGet]
        [Route("designation/mapped/{departmentId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentDesignationDto>> GetAllDesignationsMappedWithLevelAsync(int departmentId)
        {
            var result = await _levelService.GetAllDesignationsMappedWithLevelAsync(departmentId);
            return Ok(result);
        }

        /// <summary>
        /// this method is used to fetch all features related to a level
        /// </summary>
        /// <param name="levelId">integer to identify level</param>
        /// <returns>returns features mapped to a level</returns>
        [HttpGet]
        [Route("feature/mapped/{levelId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FeatureLevelDetailsDto>> GetFeaturesMappedToLevelAsync(int levelId)
        {
            var result = await _levelService.GetFeaturesMappedToLevelAsync(levelId);
            return Ok(result);
        }

        /// <summary>
        /// This method is used to update the existing project details
        /// </summary>
        /// <param name="projectDetails">projects details</param>
        /// <returns>successfully updated Project data with success status</returns>
        [HttpPut("EditDepartmentDesignationLevel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditDepartmentDesignationLevelAsync([FromBody] EditLevelDto editLevel)
        {
            return Ok(await _levelService.EditDepartmentDesignationLevel(editLevel));
        }
        /// <summary>
        /// this method is used to soft delete Project  
        /// </summary>
        /// <param name="projectId">Id of employee to be deleted</param>
        /// <returns>returns no content</returns>
        [HttpDelete("DeleteDepartmentDesignationLevel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteDepartmentDesignationLevelAsync([FromBody]EditLevelDto editLevel)
        {
            await _levelService.DeleteDepartmentDesignationLevel(editLevel);
            return Ok("Designation Department has been deleted.");
        }

        /// <summary>
        /// this method is used to soft delete Project  
        /// </summary>
        /// <param name="projectId">Id of employee to be deleted</param>
        /// <returns>returns no content</returns>
        //[HttpDelete("DeleteLevel")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> DeleteDepartmentDesignationLevelAsync(int levelId)
        //{
        //    await _levelService.DeleteLevel(levelId);
        //    return Ok("Level has been deleted.");
        //}
    }
}