using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.DesignationDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    ///  this class is used to provide contract for designation repository
    /// </summary>
    public class DesignationRepository : IDesignationRepository
    {
        private readonly PeoplePortalDb _dbContext;
        private bool _disposed = false;
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public DesignationRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        /// <summary>
        /// this method is used to get the department details
        /// </summary>
        /// <param name="departmentId">department id</param>
        /// <returns>department details</returns>
        public async Task<Department> GetDepartementDetailsAsync(int departmentId)
        {
            var department = await _dbContext.Departments.SingleOrDefaultAsync(u => u.Id == departmentId && u.IsDeleted == false);
            return department;
        }
        /// <summary>
        /// this method is used to get the designation details
        /// </summary>
        /// <param name="designationId">designation id</param>
        /// <returns>designation details</returns>
        public async Task<Designation> GetDesignationDetailsAsync(int designationId)
        {
            var designation = await _dbContext.Designations.SingleOrDefaultAsync(u => u.Id == designationId && u.IsDeleted == false);
            return designation;
        }

        /// <summary>
        /// this method is used to get the list of all designation
        /// </summary>
        /// <returns></returns>
        public async Task<List<Designation>> GetDesignationList()
        {
            var designationList = await _dbContext.Designations.Where(d => d.IsDeleted == false).ToListAsync();
            return designationList;
        }

        /// <summary>
        /// this method is used to add a designations list to the database
        /// </summary>
        /// <param name="desigList">list of designations</param>
        public void AddDesignations(List<Designation> desigList)
        {
            _dbContext.Designations.AddRange(desigList);
        }
        /// <summary>
        /// this method is used to get all the designation details of the mapped designation ids
        /// </summary>
        /// <param name="designationIds">list that contains all the ids of designations mapped to a particular department</param>
        /// <returns>returns list of designations details</returns>
        public async Task<List<Designation>> GetDesignation(List<int> designationIds)
        {
            var designationList = await _dbContext.Designations.Where(c => designationIds.Contains(c.Id)).ToListAsync();
            return designationList;
        }
        /// <summary>
        /// this method is used to get a list of designation ids
        /// </summary>
        /// <param name="designations"> list of designations</param>
        /// <returns>list of designation ids</returns>
        public IQueryable<int> GetDesignationIds(List<string> designations)
        {
            var idsList = from c in _dbContext.Designations
                          where designations.Contains(c.Title)
                          select c.Id;
            return idsList;
        }
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
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
