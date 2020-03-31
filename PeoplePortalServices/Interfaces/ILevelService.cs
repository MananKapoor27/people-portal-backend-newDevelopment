using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.DepartmentDesignationDto;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// this class provides contract for level service
    /// </summary>
    public interface ILevelService 
    {
        /// <summary>
        /// this method is used to add new level in database and map it to designation department
        /// </summary>
        /// <param name="departmentDesignationViewModel">model that contains the mapping of department and designation to be mapped to new level</param>
        Task AddNewLevelAsync(DepartmentDesignationDto departmentDesignationDto);
        ///// <summary>
        ///// this method is used to add new Level Feature Mapping
        ///// </summary>
        ///// <param name="featureLevelDetails">new feature level mapping details</param>
        //Task AddNewLevelFeatureMappingAsync(FeatureLevelDetailsDto featureLevelDetailsDto);
        /// <summary>
        /// this method is used to fetch all designations not mapped to a level
        /// </summary>
        /// <param name="departmentId">integer to map the designations mapped to required department</param>
        /// <returns>returns list of designation details</returns>
        Task<List<Designation>> GetAllDesignationsNotMappedWithLevelAsync(int departmentId);
        /// <summary>
        /// this method is used to fetch all designation department and level mappings
        /// </summary>
        /// <param name="departmentId">integer to identify department</param>
        /// <returns>returns designation department and level mappings</returns>
        Task<LevelDepartmentDesignationMappingDto> GetAllDesignationsMappedWithLevelAsync(int departmentId);
        /// <summary>
        /// this method is used to fetch all features related to a level
        /// </summary>
        /// <param name="levelId">integer to identify level</param>
        /// <returns>returns features mapped to a level</returns>
        Task<BasicConfiguration> GetFeaturesMappedToLevelAsync(int levelId);
        Task<DesignationLevel> EditDepartmentDesignationLevel(EditLevelDto editLevel);
        Task<bool> CheckIfLevelAlreadyExistsInTheDepartment(int departmentId, string newLevelName);
        Task<DesignationLevel> DeleteDepartmentDesignationLevel(EditLevelDto editLevel);
       // Task<Level> DeleteLevel(int levelId);
    }
}
