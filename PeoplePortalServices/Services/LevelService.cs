using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.DTO.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.DepartmentDesignationDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.Entities.Dto.ConfigurationDto;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// this class is used to handle all level related operations
    /// </summary>
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IDepartmentDesignationRepository _departmentDesignationRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IDesignationRepository _designationRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDesignationLevelRepository _designationLevelRepository;

        /// <summary>
        /// this is the default constructor for level controller
        /// </summary>
        public LevelService(ILevelRepository levelRepository, IDepartmentDesignationRepository departmentDesignationRepository, IFeatureRepository featureRepository, IDesignationRepository designationRepository, IDepartmentRepository departmentRepository, IDesignationLevelRepository designationLevelRepository)
        {
            _levelRepository = levelRepository;
            _departmentDesignationRepository = departmentDesignationRepository;
            _featureRepository = featureRepository;
            _designationRepository = designationRepository;
            _departmentRepository = departmentRepository;
            _designationLevelRepository = designationLevelRepository;
        }

        /// <summary>
        /// this method is used to add new level in database and map it to designation department
        /// </summary>
        /// <param name="departmentDesignationViewModel">model that contains the mapping of department and designation to be mapped to new level</param>
        public async Task AddNewLevelAsync(DepartmentDesignationDto departmentDesignationViewModel)
        {
            if (departmentDesignationViewModel.DesignationId != null)
            {
                var designationDepartmentIdList = await _departmentDesignationRepository.GetDesignationDepartmentId(departmentDesignationViewModel.DesignationId, departmentDesignationViewModel.DepartmentId);

                var level = new Level
                {
                    IsDeleted = false,
                    Title = departmentDesignationViewModel.NewLevelName
                };

                _levelRepository.AddLevel(level);
                await _levelRepository.SaveChangesAsync();

                var designationDepartmentLevelMapping = new List<DesignationLevel>();
                foreach (var id in designationDepartmentIdList)
                {
                    var designationLevel = new DesignationLevel
                    {
                        LevelId = level.Id,
                        DepartmentDesignationId = id,
                        IsDeleted = false
                    };
                    designationDepartmentLevelMapping.Add(designationLevel);
                }

                _departmentDesignationRepository.AddDesignationDepartmentLevelList(designationDepartmentLevelMapping);
                await _departmentDesignationRepository.SaveChangesAsync();
            }

            else
                throw new Exception("Department can not be empty");
        }

        ///// <summary>
        ///// this method is used to add new Level Feature Mapping
        ///// </summary>
        ///// <param name="featureLevelDetails">new feature level mapping details</param>
        //public async Task AddNewLevelFeatureMappingAsync(FeatureLevelDetailsDto featureLevelDetails)
        //{
        //    var departmentDesignationIdsList = await _levelRepository.GetAllDepartmentDesignationIdByLevelAsync(
        //                                                                        featureLevelDetails.LevelId);

        //    //todo optimise storing either by storing json in feature or by updating the code below

        //    var basicConfigurationList = new List<BasicConfiguration>();
        //    foreach (var id in departmentDesignationIdsList)
        //    {
        //        foreach (var featureId in featureLevelDetails.FeatureId)
        //        {
        //            var basicConfigurationDetails = new BasicConfiguration
        //            {
        //                LevelId = featureLevelDetails.LevelId,
        //                FeatureId = featureId,
        //                DepartmentDesignationId = id
        //            };
        //            basicConfigurationList.Add(basicConfigurationDetails);
        //        }
        //    }

        //    _featureRepository.AddBasicConfigurationList(basicConfigurationList);
        //    await _featureRepository.SaveChangesAsync();
        //}

        /// <summary>
        /// this method is used to fetch all designations not mapped to a level
        /// </summary>
        /// <param name="departmentId">integer to map the designations mapped to required department</param>
        /// <returns>returns list of designation details</returns>
        public async Task<List<Designation>> GetAllDesignationsNotMappedWithLevelAsync(int departmentId)
        {
            var departmentDesignationId = await _departmentDesignationRepository.GetDepartmentDesignationIdListAsync(departmentId);

            var mappedDepartmentDesignationIds = await _levelRepository.GetAllDesignationDepartmentIdMappedToLevelAsync(departmentDesignationId);

            var notMappedDepartmentDesignationIds = departmentDesignationId.Except(mappedDepartmentDesignationIds).ToList();

            var designationIdList = await _departmentDesignationRepository.GetAllDesignationIdsByDepartmentDesignationIdAsync(notMappedDepartmentDesignationIds);

            return await _designationRepository.GetDesignation(designationIdList);
        }

        /// <summary>
        /// this method is used to fetch all designation department and level mappings
        /// </summary>
        /// <param name="departmentId">integer to identify department</param>
        /// <returns>returns designation department and level mappings</returns>
        public async Task<LevelDepartmentDesignationMappingDto> GetAllDesignationsMappedWithLevelAsync(int departmentId)
        {
            var departmentDesignationId = await _departmentDesignationRepository.GetDepartmentDesignationIdListAsync(departmentId);

            var departmentDesignationLevelMapping = (await _levelRepository.GetAllDesignationDepartmentMappedToLevelAsync(departmentDesignationId)).GroupBy(c => c.LevelId);

            var levelList = new List<LevelDesignationDto>();

            foreach (var levelId in departmentDesignationLevelMapping)
            {
                var departmentDesignationList = new List<int>();
                var levelName = "";
                var id = 0;
                foreach (var departmentDesignationLevel in levelId)
                {
                    levelName = departmentDesignationLevel.Level.Title;
                    id = departmentDesignationLevel.LevelId;
                    departmentDesignationList.Add(departmentDesignationLevel.DepartmentDesignationId);
                }

                var designationList = await _departmentDesignationRepository.GetDesignationListAsync(departmentDesignationList);
                //todo validate

                var levelListModel = new LevelDesignationDto()
                {
                    LevelId = id,
                    LevelName = levelName,
                    Designations = designationList.ToList()
                };
                levelList.Add(levelListModel);
            }

            return new LevelDepartmentDesignationMappingDto()
            {
                DepartmentId = departmentId,
                DepartmentName = (await _departmentRepository.GetDepartmentByIdAsync(departmentId)).Name,
                LevelDesignations = levelList
            };
        }

        /// <summary>
        /// this method is used to fetch all features related to a level
        /// </summary>
        /// <param name="levelId">integer to identify level</param>
        /// <returns>returns features mapped to a level</returns>
        public async Task<BasicConfiguration> GetFeaturesMappedToLevelAsync(int levelId)
        {
            var featureLevelMapping = (await _featureRepository.GetBasicConfigurationsFeaturesAsync(levelId));
            return featureLevelMapping;
        }


        public async Task<bool> CheckIfLevelAlreadyExistsInTheDepartment(int departmentId, string newLevelName)
        {
            var departmentDesignationIdList = await _departmentDesignationRepository.GetDepartmentDesignationIdListAsync(departmentId);

            if (departmentDesignationIdList.Count == 0)
            {
                throw new Exception("There are no designations in the given department.");
            }

            var listOfLevelId = await _designationLevelRepository.ListOfLevelsAssociatedWithListOfDepartmentDesignationId(departmentDesignationIdList);

            if (listOfLevelId.Count == 0)
                return false;

            var listOfLevels = await _levelRepository.GetAllLevelsByLevelId(listOfLevelId);
            var levelExists = listOfLevels.FirstOrDefault(c => c.Title.Replace(" ", String.Empty).ToLower().Equals(newLevelName.Replace(" ", String.Empty).ToLower()));

            if (levelExists != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// this method is used to add a new departmentdesignation  to level
        /// </summary>
        /// <param name="editLevel"></param>
        /// <returns>add the new departmentdesignation  to level </returns>
        public async Task<DesignationLevel> EditDepartmentDesignationLevel(EditLevelDto editLevel)
        {
            var existingLevel = await _levelRepository.GetLevelIdMappedToDesignationDepartment(editLevel.DepartmentDesignationId);

            if (existingLevel == null)
            {
                existingLevel.DepartmentDesignationId = editLevel.DepartmentDesignationId;
                existingLevel.LevelId = editLevel.LevelId;
                await _levelRepository.SaveChangesAsync();
                return existingLevel;
            }

            else
                throw new Exception("This data alreay exists."); ;
        }

        public async Task<DesignationLevel> DeleteDepartmentDesignationLevel(EditLevelDto editLevel)
        {
            var existingLevel = await _levelRepository.GetLevelIdMappedToDesignationDepartment(editLevel.DepartmentDesignationId);
            if (existingLevel != null)
            {
                existingLevel.IsDeleted = true;
                var levelDetails = await _levelRepository.GetAllDepartmentDesignationIdByLevelAsync(editLevel.LevelId);
                if (levelDetails == null)
                {
                    var level = await _levelRepository.GetLevelByLevelId(editLevel.LevelId);
                    level.IsDeleted = true;
                    await _levelRepository.SaveChangesAsync();
                }
                else
                    throw new Exception("this level have department Designation linked to it ");
                return existingLevel;
            }
            else
                throw new Exception("Department Dessignation Doesn't exist");
        }
    }
    //public async Task<Level> DeleteLevel(int levelId)
    //{
    //    var levelDetails = await _levelRepository.GetAllDepartmentDesignationIdByLevelAsync(levelId);
    //    if(levelDetails==null)
    //    {
    //        var level =await  _levelRepository.GetLevelByLevelId(levelId);
    //        level.IsDeleted = true;
    //        await _levelRepository.SaveChangesAsync();
    //        return level;
    //    }
    //else
    //        throw new Exception("this level have department Designation linked to it ");

    //}
}

