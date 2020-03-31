using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;
using PeoplePortalDomainLayer.Entities.Dto.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.FeatureDto;

namespace PeoplePortal.Controllers
{
    [Route("api/featureList")]
    public class FeatureController : ControllerBase
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
        private readonly IFeatureService _featureService;


        public FeatureController(IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IFeatureService featureService)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
            _featureService = featureService;
        }

        /// <summary>
        /// this method is used to fetch list of all the features
        /// </summary>
        /// <returns>returns list of features</returns>
        [HttpGet("AllFeatures")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Dictionary<string, List<string>> GetAllFeatures()
        {
            // get the IApiExplorer registered automatically
            // loop, convert and return all descriptions
            var resultDictnary = new Dictionary<string, List<string>>();
            var resultList = new List<APIDescriptionResult>();
            var items = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>();
            foreach (var item in items)
            {
                var controllerActionDescriptor = (ControllerActionDescriptor)item;
                var result = new APIDescriptionResult
                {
                    ActionName = controllerActionDescriptor.ActionName,
                    ControllerName = controllerActionDescriptor.ControllerName,
                    RelativePath = item.AttributeRouteInfo.Template,
                    Method = controllerActionDescriptor.ActionConstraints.OfType<HttpMethodActionConstraint>().SingleOrDefault().
                    HttpMethods.FirstOrDefault()
                };
                resultList.Add(result);
            }

            var groupedResultList = resultList
                .GroupBy(u => u.ControllerName)
                .Select(grp => grp.ToList())
                .ToList();

            foreach(var groupedResult in groupedResultList)
            {
                var tempList = new List<string>();
                foreach (var result in groupedResult)
                {
                    tempList.Add(result.ActionName);
                }
                resultDictnary.Add(groupedResult.First().ControllerName, tempList);
            }

            return resultDictnary;
        }

        /// <summary>
        /// this method is used to enter the list of all accessible features
        /// </summary>
        /// <returns>returns list of accessible features</returns>
        [HttpPost("AddAccessibleFeaturesToLevel")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BasicConfiguration>> AddAccessibleFeaturesToLevel([FromBody]BasicConfigurationDto basicConfigurationDto)
        {
            var result = await _featureService.AddBasicConfigurationAsync(basicConfigurationDto);
            return result;
        }

        /// <summary>
        /// this method will provide the accessible features for a particular level
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
        [HttpGet("FeaturesForLevel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BasicConfigurationDto>> GetAccessibleFeaturesForLevel(int levelId)
        {
            var result = await _featureService.GetLevelBasicConfiguration(levelId);
            return Ok(result);
        }

        [HttpGet("FeaturesForEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BasicConfiguration>> GetEmployeeAccessibleFeatures(Guid employeeId)
        {
            var result = await _featureService.GetEmployeeAccessibleFeaturesAsync(employeeId);
            return Ok(result);
        }

        [HttpPost("AddEmployeeFeatureException")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeFeatureUpdationDetailDto>> AddEmployeeFeatureException(GetEmployeeFeatureUpdateDto getEmployeeFeatureUpdateDto)
        {
            var result = await _featureService.AddFeatureFromEmployeeAsync(getEmployeeFeatureUpdateDto);
            if (result == null)
                return BadRequest("Employee already have this feature.");
            return Ok(result);
        }

        [HttpPost("RemoveEmployeeFeatureException")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeFeatureUpdationDetailDto>> RemoveEmployeeFeatureException(
            GetEmployeeFeatureUpdateDto getEmployeeFeatureUpdateDto)
        {
            var result = await _featureService.RemoveFeatureFromEmployeeAsync(getEmployeeFeatureUpdateDto);
            if (result == null)
                return BadRequest("Employee already does not have this feature.");
            return Ok(result);
        }
    }
}
