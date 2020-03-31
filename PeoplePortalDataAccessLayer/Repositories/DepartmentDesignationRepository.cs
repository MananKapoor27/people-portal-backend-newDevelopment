using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// this class is used to interact with the database with departmentdesignation related queries
    /// </summary>
    public class DepartmentDesignationRepository : IDepartmentDesignationRepository
    {
        private readonly PeoplePortalDb _dbContext;
        bool disposed = false;
        /// <summary>
        /// constructor for this class to assign value to database context
        /// </summary>
        public DepartmentDesignationRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        /// <summary>
        /// this method is used to fetch all the details of designations falling under a department
        /// </summary>
        /// <param name="departmentId">id to identify department</param>
        /// <returns>returns list of Designation ids</returns>
        public async Task<List<int>> GetAllDesignationsAsync(int departmentId)
        {
            var departmentDesignationData = await _dbContext.DepartmentDesignations
                                                      .Where(c => c.DepartmentId == departmentId && c.IsDeleted == false)
                                                      .Select(c => c.DesignationId)
                                                      .ToListAsync();
            return departmentDesignationData;
        }
        /// <summary>
        /// this method is used to get all the designation details of the mapped designation ids
        /// </summary>
        /// <param name="designationIds">list that contains all the ids of designations mapped to a particular department</param>
        /// <param name="departmentId">integer to identify department</param>
        /// <returns>returns list of designations department ids</returns>
        public async Task<List<int>> GetDesignationDepartmentId(List<int> designationIds, int departmentId)
        {
            var designationDepartmentIdList = await _dbContext.DepartmentDesignations
                                                              .Where(c => designationIds.Contains(c.DesignationId)
                                                                        && c.DepartmentId == departmentId
                                                                        && c.IsDeleted == false)
                                                              .Select(c => c.Id)
                                                              .ToListAsync();
            return designationDepartmentIdList;
        }
        /// <summary>
        /// this method is used to add new list of department designation level mapping
        /// </summary>
        /// <param name="designationLevelList">new list of department designation level mapping</param>
        public void AddDesignationDepartmentLevelList(List<DesignationLevel> designationLevelList)
        {
            _dbContext.DesignationLevel.AddRange(designationLevelList);
        }
        /// <summary>
        /// this method is used to get all the designation details of the given department id
        /// </summary>
        /// <param name="departmentId">integer to identify department</param>
        /// <returns>returns list of designations department ids</returns>
        public async Task<List<int>> GetDepartmentDesignationIdListAsync(int departmentId)
        {
            var designationDepartmentIdList = await _dbContext.DepartmentDesignations
                                                              .Where(c => c.DepartmentId == departmentId
                                                                        && c.IsDeleted == false)
                                                              .Select(c => c.Id)
                                                              .ToListAsync();
            return designationDepartmentIdList;
        }
        /// <summary>
        /// this method is used to get all the designation details of the given department id list
        /// </summary>
        /// <param name="departmentDesignationId">integer to identify department designation mapping</param>
        /// <returns>returns list of designations </returns>
        public async Task<IEnumerable<Designation>> GetDesignationListAsync(List<int> departmentDesignationId)
        {
            var designationDepartmentIdList = (await _dbContext.DepartmentDesignations
                                                              .Include(c => c.Designation)
                                                              .Where(c => departmentDesignationId.Contains(c.Id))
                                                              .ToListAsync())
                                                              .Select(c => new Designation
                                                              {
                                                                  Id = c.Designation.Id,
                                                                  IsDeleted = c.Designation.IsDeleted,
                                                                  Title = c.Designation.Title
                                                              });
            return designationDepartmentIdList;
        }
        /// <summary>
        /// this method is used to get All Designation Ids By Department Designation Id
        /// </summary>
        /// <param name="departmentDesignationIdList">list of integers to map into department designation ids</param>
        /// <returns>returns list of required designation ids</returns>
        public async Task<List<int>> GetAllDesignationIdsByDepartmentDesignationIdAsync(List<int> departmentDesignationIdList)
        {
            var designationIdList = await _dbContext.DepartmentDesignations
                                                    .Where(c => departmentDesignationIdList.Contains(c.Id))
                                                    .Select(c => c.DesignationId)
                                                    .ToListAsync();
            return designationIdList;
        }
        /// <summary>
        /// this method is used to get the list of id which is mapped with department table
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <returns>returns list of designation department mapping data</returns>
        public async Task<List<DepartmentDesignation>> GetDesignationList(int id)
        {
            var departmentDesignation = await _dbContext.DepartmentDesignations
                                                      .Where(c => c.DepartmentId == id)
                                                      .ToListAsync();
            return departmentDesignation;
        }
        /// <summary>
        /// this method is used to add the department id and designation id in the mapper table
        /// </summary>
        /// <param name="departmentDesignations">list of departmentDesignations</param>
        public void AddDepartmentDesignation(List<DepartmentDesignation> departmentDesignations)
        {
            _dbContext.DepartmentDesignations.AddRange(departmentDesignations);
        }
        /// <summary>
        /// this method gets the details of the DepartmentDesignation
        /// </summary>
        /// <param name="designationId">designation id</param>
        /// <returns>DepartmentDesignation details</returns>
        public async Task<DepartmentDesignation> GetDepartmentDesignationDetailsAsync(int designationId)
        {
            var departmentDesignation = await _dbContext.DepartmentDesignations.SingleOrDefaultAsync(u => u.DesignationId == designationId && u.IsDeleted == false);
            return departmentDesignation;
        }
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// this method is used to get the data related to a given department and designation
        /// </summary>
        /// <param name="departmentId">integer to indentify department</param>
        /// <param name="designationId">integer to indentify designation</param>
        /// <returns>returns details related to required designation and department mapping</returns>
        public async Task<DepartmentDesignation> GetDepartmentDesignationAsync(int departmentId, int designationId)
        {
            var departmentDesignationDetails = await _dbContext.DepartmentDesignations.SingleOrDefaultAsync(c => c.DepartmentId == departmentId && c.DesignationId == designationId);
            return departmentDesignationDetails;
        }
        /// <summary>
        /// this method is used to get department id by departmentdesignationId
        /// </summary>
        /// <param name="departmentDesignationId">Id</param>
        public async Task<int> GetDepartmentByDepartmentDesignation(int departmentDesignationId)
        {
            var departmentId = (await _dbContext.DepartmentDesignations
                                                .SingleOrDefaultAsync(c => c.Id == departmentDesignationId))
                                                .DepartmentId;
            return departmentId;

        }
        /// <summary>
        /// this method is used to get department name by id
        /// </summary>
        /// <param name="departmentId">Id</param>
        public async Task<Department> GetDepartmentById(int departmentId)
        {
            var department = (await _dbContext.Departments
                                                 .SingleOrDefaultAsync(c => c.Id == departmentId));
            return department;
        }
        /// <summary>
        /// this method is used to get designationId by Id
        /// </summary>
        /// <param name="departmentDesignationId">Id</param>
        public async Task<int> GetDesignationByDepartmentDesignation(int departmentDesignationId)
        {
            var designationId = (await _dbContext.DepartmentDesignations
                                                .SingleOrDefaultAsync(c => c.Id == departmentDesignationId))
                                                .DesignationId;
            return designationId;
        }
        /// <summary>
        /// this method is used to get the designation name
        /// </summary>
        /// <param name="designationId">Id</param>
        public async Task<Designation> GetDesignationById(int designationId)
        {
            var designation = (await _dbContext.Designations
                                                  .SingleOrDefaultAsync(c => c.Id == designationId));
            return designation;
        }
        /// <summary>
        /// this method is used to get the designation title in list
        /// </summary>
        /// <param name="Id">departmentdesignation Id</param>
        public async Task<string> GetDesignationByDepartmentDesignationId(int id)
        {
            var designationTitle = (await _dbContext.DepartmentDesignations.Include(c => c.Designation).SingleOrDefaultAsync(c => c.Id == id)).Designation.Title;
            return designationTitle;
        }
        /// <summary>
        /// this method is used to get the department details
        /// </summary>
        /// <param name="departmentName">name of the department</param>
        public async Task<Department> GetDepartmentDetailsAsync(string departmentName)
        {
            var department = await _dbContext.Departments.SingleOrDefaultAsync(c => c.Name == departmentName);
            return department;
        }
        /// <summary>
        /// this method is used to get the designation details
        /// </summary>
        /// <param name="designationName">name of the department</param>
        public async Task<Designation> GetDesignationDetailsAsync(string designationName)
        {
            var designation = await _dbContext.Designations.SingleOrDefaultAsync(c => c.Title == designationName);
            return designation;
        }

        /// <summary>
        /// this method is used to get departmentDesignation Id for a designationId
        /// </summary>
        /// <param name="designationId"></param>
        /// <returns></returns>
        public async Task<DepartmentDesignation> GetDepartmentDesignationForDesignation(int designationId)
        {
            var departmentDesignation = await _dbContext.DepartmentDesignations.SingleOrDefaultAsync(d => d.DesignationId == designationId);
            return departmentDesignation;
        }

        /// <summary>
        /// this method is used to implement dispose pattern callable by consumers
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// this method is used to free all managed objects
        /// </summary>
        /// <param name="disposing">checks whether we want to dispose the objects or not</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                _dbContext.Dispose();
            }
            disposed = true;
        }
    }
}
