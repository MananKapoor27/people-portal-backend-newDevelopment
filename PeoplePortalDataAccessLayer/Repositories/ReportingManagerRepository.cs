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
    public class ReportingManagerRepository : IReportingManagerRepository
    {

        private readonly PeoplePortalDb _dbContext;
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public ReportingManagerRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        /// <summary>
        /// this method is used to retrieve Reporting manager of an employee
        /// </summary>
        /// <param name="employeeId"> id of an employee </param>
        /// <returns>returns Reporting manager</returns>
        public async Task<ReportingManager> GetReportingManager(Guid employeeId)
        {
            var result = await _dbContext.ReportingManager.Where(c => c.EmployeeId == employeeId).SingleOrDefaultAsync();
            return result;
        }


        /// <summary>`
        /// this method is used to retrieve reportees of Reporting manager 
        /// </summary>
        /// <param name="employeeId">  id of an employee </param>
        /// <returns>returns list of the reportees</returns>
        public async Task<List<Guid>> GetAllReporteeAsync(Guid employeeId)
        {
            try
            {
                var result = await _dbContext.ReportingManager.Where(c => c.ReportingManagerId == employeeId).Select(e => e.EmployeeId).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        /// <summary>
        /// this method is used to add new exception in database
        /// </summary>
        /// <param name="reportingManager">reportingManager table object</param>
        public void AddReportingManager(ReportingManager reportingManager)
        {
            _dbContext.ReportingManager.Add(reportingManager);
        }

        /// <summary>
        /// this method is used to retrieve employee ID
        /// </summary>
        /// <param name="employeeId"> employee of the project </param>
        /// <returns>returns employee ID</returns>
        public async Task<ReportingManager> GetEmployeeAsync(Guid employeeId)
        {
            try
            {
                var result = await _dbContext.ReportingManager.FirstOrDefaultAsync(c => c.EmployeeId == employeeId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteRecord(ReportingManager reportingManager)
        {
            _dbContext.ReportingManager.Remove(reportingManager);

        }
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
