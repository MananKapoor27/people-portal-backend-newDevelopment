using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto;
using PeoplePortalDomainLayer.Entities.Dto.ReportingManagerDto;
using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Interfaces
{
    public interface IReportingManagerService
    {
        /// <summary>
        /// this method is used to fetch  the Reporting Manager
        /// </summary>
        /// <param name="employeeId">EmployeeId of the project </param>
        /// <returns>returns the Reporting Manager</returns>
        Task<GetReportingManagerDto> GetReportingManager(Guid employeeId);

        /// <summary>
        /// this method returns all the Reportees of an employee
        /// </summary>
        /// <param name="employeeId">EmployeeId of the project</param>
        /// <returns></returns>
        Task<List<GetEmployeeListDto>> GetAllReporteeAsync(Guid employeeId);

        Task<List<ReportingManagerDto>> AddEmployeeReportingManagerAsync(Guid ReportingManagerId, List<Guid> listOfEmployeeId);
        Task<ReportingManager> ChangeEmployeeReportingManagerAsync(UpdateReportingManagerDto updateReportingManagerDto);

    }
}
