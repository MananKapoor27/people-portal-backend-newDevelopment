using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// this class is used for skills(related to an employee) related database operations
    /// </summary>
    public class EmployeeSkillsRepository : IEmployeeSkillsRepository
    {
        private readonly PeoplePortalDb _dbContext;
        private bool _disposed = false;
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public EmployeeSkillsRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }


        /// <summary>
        /// this method is used to add new skill in database
        /// </summary>
        /// <param name="employeeSkill">this object contains new skill information</param>
        public void AddEmployeeSkill(EmployeeSkill employeeSkill)
        {
            _dbContext.EmployeeSkills.Add(employeeSkill);
        }
        /// <summary>
        /// this method is used to fetch all employee skills 
        /// </summary>
        /// <param name="employeesId">employee id</param>
        /// <returns>returns list of employee skills</returns>
        public async Task<EmployeeSkill> GetEmployeeSkills(Guid employeesId)
        {
            var result = await _dbContext.EmployeeSkills.SingleOrDefaultAsync(c => c.EmployeeId == employeesId);
            return result;
        }
        /// <summary>
        /// this method is used to give primary skill of employee
        /// </summary>
        /// <param name="employeeId">Id</param>
        public async Task<string> PrimarySkillOfEmployee(Guid employeeId)
        {
            var result = await _dbContext.EmployeeSkills.SingleOrDefaultAsync(c => c.EmployeeId == employeeId);
            return result.PrimarySkills;
        }
        /// <summary>
        /// this method is used to give secondary skill of employee
        /// </summary>
        /// <param name="employeeId">Id</param>
        public async Task<string> SecondarySkillofEmployee(Guid employeeId)
        {
            var result = await _dbContext.EmployeeSkills.SingleOrDefaultAsync(c => c.EmployeeId == employeeId);
            return result.SecondarySkills;
        }
        
        /// <summary>
        /// this method is used to search the searchString in the primary and secondary skills of an employee
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<List<Guid>> SearchForEmployeeSkill(string searchString)
        {
            var result = await _dbContext.EmployeeSkills.Where(s => (s.PrimarySkills.Contains(searchString) || s.SecondarySkills.Contains(searchString)) && s.IsDeleted == false).Select(e => e.EmployeeId).ToListAsync();
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
