using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto;
using PeoplePortalDomainLayer.Entities.Models;
namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// this class is used to handle operations related to employee Features
    /// </summary>
    public interface IFeatureService
    { /// <summary>
      /// this method is used to remove new feature to given employee 
      /// </summary>
      /// <param name="getEmployeeFeatureUpdate">employee and feature id</param>
      /// <returns>returns given feature permission details for given employee  </returns>
        Task<EmployeeFeatureUpdationDetailDto> RemoveFeatureFromEmployeeAsync(GetEmployeeFeatureUpdateDto getEmployeeFeatureUpdate);
        /// <summary>
        /// this method is used to add new feature to given employee 
        /// </summary>
        /// <param name="getEmployeeFeatureUpdate">employee and feature id</param>
        /// <returns>returns given feature permission details for given employee  </returns>
        Task<EmployeeFeatureUpdationDetailDto> AddFeatureFromEmployeeAsync(GetEmployeeFeatureUpdateDto getEmployeeFeatureUpdate);
        /// <summary>
        /// this method is used to fetch all the features list, which are accessible by current user
        /// </summary>
        /// <param name="employeeId">employee Guid</param>
        /// <returns>returns the list of features </returns>
        Task<List<string>> GetEmployeeAccessibleFeaturesAsync(Guid employeeId);
        /// <summary>
        /// this method is used to get all the base features of given employee
        /// </summary>
        /// <param name="employeeId">employee guid</param>
        /// <returns>it will return all base features list </returns>
        Task<List<string>> GetEmployeeBaseFeaturesAsync(Guid employeeId);
        /// <summary>
        /// this method will fetch all the features
        /// </summary>
        /// <returns>returns list of features</returns>
        Task<List<Feature>> GetAllFeatureAsync();

        Task<BasicConfiguration> AddBasicConfigurationAsync(BasicConfigurationDto basicConfigurationDto);

        Task<BasicConfigurationDto> GetLevelBasicConfiguration(int levelId);


    }
}