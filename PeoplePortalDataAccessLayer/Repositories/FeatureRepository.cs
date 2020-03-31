using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// this class is used to do all features related database operations(feature, basic configuration and exceptions handled here)
    /// </summary>
    public class FeatureRepository : IFeatureRepository
    {
        private readonly PeoplePortalDb _dbContext;
        private bool _disposed = false;
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public FeatureRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }
        /// <summary>
        ///  this method is used to fetch all the Features
        /// </summary>
        /// <returns>returns list of all the feature</returns>
        public async Task<List<Feature>> GetAllFeatureAsync()
        {
            return await _dbContext.Features.Where(c => c.IsDeleted == false).ToListAsync();
        }
        /// <summary>
        /// this method is used to retrieve feature information
        /// </summary>
        /// <param name="featureId">feature id </param>
        /// <returns>returns feature information</returns>
        public async Task<Feature> GetFeatureAsync(int featureId)
        {
            var result = await _dbContext.Features.SingleOrDefaultAsync(c => c.Id == featureId);
            return result;
        }
        /// <summary>
        /// this method is used to add new exception in database
        /// </summary>
        /// <param name="exception">exception table object</param>
        public void AddException(FeatureException exception)
        {
            _dbContext.FeatureException.Add(exception);
        }
        /// <summary>
        /// this method is used to remove exception from database
        /// </summary>
        /// <param name="exception">exception table object</param>
        public void RemoveException(FeatureException exception)
        {
            _dbContext.FeatureException.Remove(exception);
        }
        /// <summary>
        /// this method is used to get all features of given level
        /// </summary>
        /// <param name="levelId">level id </param>
        /// <returns>this method will return features id list inside given level </returns>
        public async Task<BasicConfiguration> GetLevelFeaturesAsync(int levelId)
        {
            var result = await _dbContext.BasicConfigurations.SingleOrDefaultAsync(c => c.LevelId == levelId);
            return result;
        }
        /// <summary>
        /// this method is used to get employee exception features details
        /// </summary>
        /// <param name="employeeId">employee id </param>
        /// <returns>returns employee exception feature details</returns>
        public async Task<FeatureException> GetEmployeeExceptionAsync(Guid employeeId, string exceptionFeature)
        {
            var result = await _dbContext.FeatureException.SingleOrDefaultAsync(c => c.EmployeeId == employeeId && c.ExceptionFeature == exceptionFeature);
            return result;
        }
        /// <summary>
        /// this method is used to get employee all exception details
        /// </summary>
        /// <param name="employeeId">employee id </param>
        /// <returns>returns employee exception details</returns>
        public async Task<IEnumerable<FeatureException>> GetEmployeeAllExceptionAsync(Guid employeeId)
        {
            var result = await _dbContext.FeatureException.Where(c => c.EmployeeId == employeeId).ToListAsync();
            return result;
        }
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// this method is used to add a list of new basic configurations in the system
        /// </summary>
        /// <param name="basicConfigurations">list of basic configurations</param>
        public void AddBasicConfigurationToLevel(BasicConfiguration basicConfigurations)
        {
            _dbContext.BasicConfigurations.Add(basicConfigurations);
        }
        /// <summary>
        /// this method is used to get basic configurations related to a level
        /// </summary>
        /// <param name="levelId">integer to identify level</param>
        /// <returns>returns list of features mapped to a level</returns>
        public async Task<BasicConfiguration> GetBasicConfigurationsFeaturesAsync(int levelId)
        {
            var basicConfigurations = (await _dbContext.BasicConfigurations.SingleOrDefaultAsync(c => c.LevelId == levelId && c.IsDeleted == false));
            return basicConfigurations;
        }
        /// <summary>
        /// this function is used to implement dispose pattern callable by consumers
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// this function is used to free all managed objects.
        /// </summary>
        /// <param name="disposing"> bool value which tells whether to dispose or not </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }
    }
}