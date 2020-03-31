using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    public interface IReportingManagerRepository
    {
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// this method is used to retrieve Reporting manager of an employee
        /// </summary>
        /// <param name="employeeId"> id of an employee </param>
        /// <returns>returns Reporting manager</returns>
        Task<ReportingManager> GetReportingManager(Guid employeeId);

        Task<List<Guid>> GetAllReporteeAsync(Guid employeeId);

        /// <summary>
        /// this method is used to add new exception in database
        /// </summary>
        /// <param name="reportingManager">reportingManager table object</param>
        void AddReportingManager(ReportingManager reportingManager);
        /// <summary>
        /// this method is used to retrieve employee ID
        /// </summary>
        /// <param name="employeeId"> employee of the project </param>
        /// <returns>returns employee ID</returns>
        Task<ReportingManager> GetEmployeeAsync(Guid employeeId);
        void DeleteRecord(ReportingManager reportingManager);
    }
}
