using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;
namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// this interface is used to provide contract for Feature repository
    /// </summary>
    public interface IFeatureRepository : IDisposable
    {/// <summary>
     /// this method is used to save the changes done in database
     /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// this method is used to get all features of given level
        /// </summary>
        /// <param name="levelId">level id </param>
        /// <returns>this method will return features id list inside given level </returns>
        Task<BasicConfiguration> GetLevelFeaturesAsync(int levelId);
        /// <summary>
        /// this method is used to remove excption from database
        /// </summary>
        /// <param name="exception">exception table object</param>
        void RemoveException(FeatureException exception);
        /// <summary>
        /// this method is used to add new excption in database
        /// </summary>
        /// <param name="exception">exception table object</param>
        void AddException(FeatureException exception);
        /// <summary>
        /// this method is used to retrieve feature information
        /// </summary>
        /// <param name="featureId">feature id </param>
        /// <returns>returns feature information</returns>
        Task<Feature> GetFeatureAsync(int featureId);
        /// <summary>
        /// this method will fetch all the available feature
        /// </summary>
        /// <returns>returns the features</returns>
        Task<List<Feature>> GetAllFeatureAsync();
        /// <summary>
        /// this method is used to get employee exception features details
        /// </summary>
        /// <param name="employeeId">employee id </param>
        /// <returns>returns employee exception feature details</returns>
        Task<FeatureException> GetEmployeeExceptionAsync(Guid employeeId, string exceptionFeature);
        /// <summary>
        /// this method is used to add a list of new basic configurations in the system
        /// </summary>
        /// <param name="basicConfigurationsList">list of basic configurations</param>
        void AddBasicConfigurationToLevel(BasicConfiguration basicConfigurationsList);
        /// <summary>
        /// this method is used to get basic configurations related to a level
        /// </summary>
        /// <param name="levelId">integer to identify level</param>
        /// <returns>returns list of features mapped to a level</returns>
        Task<BasicConfiguration> GetBasicConfigurationsFeaturesAsync(int levelId);
        /// <summary>
        /// this method is used to get employee all exception details
        /// </summary>
        /// <param name="employeeId">employee id </param>
        /// <returns>returns employee exception details</returns>
        Task<IEnumerable<FeatureException>> GetEmployeeAllExceptionAsync(Guid employeeId);
    }
}