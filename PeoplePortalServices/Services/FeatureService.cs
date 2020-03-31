using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Shared.Helpers;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// this class is used to handle operations related to employee Features
    /// </summary>
    public class FeatureService : IFeatureService
    {
        private readonly Dictionary<int, string> modification = new Dictionary<int, string> { { 0, "Added" }, { 1, "Removed" } };
        private readonly IFeatureRepository _featureRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILevelService _levelService;
        /// <summary>
        /// this is the default constructor for the Skill service
        /// </summary>
        /// <param name="unityContainer">object of unity container interface for dependency injection </param>    
        public FeatureService(IFeatureRepository featureRepository, ILevelRepository levelRepository, IEmployeeRepository employeeRepository, ILevelService levelService)
        {
            _levelRepository = levelRepository;
            _featureRepository = featureRepository;
            _employeeRepository =employeeRepository;
            _levelService =levelService;
        }
        /// <summary>
        /// this method is used to fetch all the Features
        /// </summary>
        /// <returns>returns list of all the feature</returns>
        public async Task<List<Feature>> GetAllFeatureAsync()
        {
            return await _featureRepository.GetAllFeatureAsync();
        }
        /// <summary>
        /// this method is used to get all the base features of given employee
        /// </summary>
        /// <param name="employeeId">employee guid</param>
        /// <returns>it will return all base features list </returns>
        public async Task<List<string>> GetEmployeeBaseFeaturesAsync(Guid employeeId)
        {
            var basicConfiguration = new BasicConfiguration();
            var employeeDepartmentDesignationId = (await _employeeRepository.GetEmployeeByIdAsync(employeeId))?.DepartmentDesignationId;
            if (employeeDepartmentDesignationId.HasValue)
            {
                var employeeLevel = (await _levelRepository.GetLevelIdMappedToDesignationDepartment(employeeDepartmentDesignationId.Value)).LevelId;

                if (employeeLevel>0)
                {
                    basicConfiguration = await _levelService.GetFeaturesMappedToLevelAsync(employeeLevel);
                }
            }
            var result = new List<string>();
            if(basicConfiguration != null)
                result = JsonConvert.DeserializeObject<List<string>>(basicConfiguration.AccessibleFeaturesList);
            return result;
        }

        public async Task<BasicConfiguration> AddBasicConfigurationAsync(BasicConfigurationDto basicConfigurationDto)
        {
            var basicConfiguration = new BasicConfiguration
            {
                LevelId = basicConfigurationDto.LevelId,
                AccessibleFeaturesList = JsonConvert.SerializeObject(basicConfigurationDto.AccessibleFeaturesList),
                IsDeleted = false
            };
            _featureRepository.AddBasicConfigurationToLevel(basicConfiguration);
            await _featureRepository.SaveChangesAsync();
            return basicConfiguration;
        }

        public async Task<BasicConfigurationDto> GetLevelBasicConfiguration(int levelId)
        {
            var basicConfiguration = await _featureRepository.GetBasicConfigurationsFeaturesAsync(levelId);
            var result = new BasicConfigurationDto
            {
                LevelId = basicConfiguration.LevelId,
                AccessibleFeaturesList = JsonConvert.DeserializeObject<List<string>>(basicConfiguration.AccessibleFeaturesList)
            };
            return result;
        }

        /// <summary>
        /// this method is used to remove new feature to given employee 
        /// </summary>
        /// <param name="getEmployeeFeatureUpdate">employee and feature id</param>
        /// <returns>returns given feature permission details for given employee  </returns>
        public async Task<EmployeeFeatureUpdationDetailDto> RemoveFeatureFromEmployeeAsync(GetEmployeeFeatureUpdateDto getEmployeeFeatureUpdate)
        {
            var baseFeatures = await GetEmployeeBaseFeaturesAsync(getEmployeeFeatureUpdate.EmployeeId);
            if (baseFeatures != null && baseFeatures.Contains(getEmployeeFeatureUpdate.FeatureName))
            {
                var exceptionFeature = new FeatureException
                {
                    AdditionalFeatureIsDeleted = true,
                    EmployeeId = getEmployeeFeatureUpdate.EmployeeId
                };
                _featureRepository.AddException(exceptionFeature);
                await _featureRepository.SaveChangesAsync();
            }
            else
            {
                var exception = await _featureRepository.GetEmployeeExceptionAsync(getEmployeeFeatureUpdate.EmployeeId, getEmployeeFeatureUpdate.FeatureName);
                if (exception?.AdditionalFeatureIsDeleted == false)
                {
                    _featureRepository.RemoveException(exception);
                    await _featureRepository.SaveChangesAsync();
                }
                else
                {
                    return null;
                    //TODO excptionHandeling
                    var response = CreateResponseService.CreateErrorResponse(HttpStatusCode.BadRequest, "Feature not found!", "Employee already does not have this feature.");
                    throw new HttpResponseException(response);
                }
            }
            return new EmployeeFeatureUpdationDetailDto()
            {
                EmployeeId = getEmployeeFeatureUpdate.EmployeeId,
                FeatureName = getEmployeeFeatureUpdate.FeatureName,
                FeatureModification = modification[1]
            };
        }
        /// <summary>
        /// this method is used to add new feature to given employee 
        /// </summary>
        /// <param name="getEmployeeFeatureUpdate">employee and feature id</param>
        /// <returns>returns given feature permission details for given employee  </returns>
        public async Task<EmployeeFeatureUpdationDetailDto> AddFeatureFromEmployeeAsync(GetEmployeeFeatureUpdateDto getEmployeeFeatureUpdate)
        {
            var baseFeatures = await GetEmployeeBaseFeaturesAsync(getEmployeeFeatureUpdate.EmployeeId);
            if (baseFeatures.Count > 0 && baseFeatures.Contains(getEmployeeFeatureUpdate.FeatureName))
            {
                var exceptionFeature = await _featureRepository.GetEmployeeExceptionAsync(getEmployeeFeatureUpdate.EmployeeId, getEmployeeFeatureUpdate.FeatureName);
                if (exceptionFeature?.AdditionalFeatureIsDeleted == true)
                {
                    _featureRepository.RemoveException(exceptionFeature);
                    await _featureRepository.SaveChangesAsync();
                }
                else
                {
                    return null;
                    //TODO excptionHandeling
                    var response = CreateResponseService.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request!", "Employee already have this feature.");
                    throw new HttpResponseException(response);
                }
            }
            else
            {
                var exceptionFeature = new FeatureException
                {
                    ExceptionFeature = getEmployeeFeatureUpdate.FeatureName,
                    AdditionalFeatureIsDeleted = false,
                    EmployeeId = getEmployeeFeatureUpdate.EmployeeId
                };
                _featureRepository.AddException(exceptionFeature);
                await _featureRepository.SaveChangesAsync();
            }
            return new EmployeeFeatureUpdationDetailDto()
            {
                EmployeeId = getEmployeeFeatureUpdate.EmployeeId,
                FeatureName = getEmployeeFeatureUpdate.FeatureName,
                FeatureModification = modification[0]
            };
        }
        /// <summary>
        /// this method is used to fetch all the features list, which are accessible by current user
        /// </summary>
        /// <param name="employeeId">employee Guid</param>
        /// <returns>returns the list of features </returns>
        public async Task<List<string>> GetEmployeeAccessibleFeaturesAsync(Guid employeeId)
        {
            var accessibleFeaturesList = await GetEmployeeBaseFeaturesAsync(employeeId);

            var exceptions = await _featureRepository.GetEmployeeAllExceptionAsync(employeeId);
            foreach (var exception in exceptions)
            {
                if (exception.AdditionalFeatureIsDeleted && accessibleFeaturesList.Contains(exception.ExceptionFeature))
                {
                    accessibleFeaturesList.Remove(exception.ExceptionFeature);
                }
                else if (!exception.AdditionalFeatureIsDeleted && accessibleFeaturesList.Contains(exception.ExceptionFeature))
                {
                    accessibleFeaturesList.Add(exception.ExceptionFeature);
                }
            }
            return accessibleFeaturesList;
        }
    }
}