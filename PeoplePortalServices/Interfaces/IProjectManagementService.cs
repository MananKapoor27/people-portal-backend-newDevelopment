using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// This interface is used to provide contract for ProjectManagement service
    /// </summary>
    public interface IProjectManagementService
    {
        Task<EmployeesInProjectDto> AddEmployeeToProject(EmployeesInProjectDto addEmployeeToProject);
        /// <summary>
        /// this method will fetch all  projects in which the employee is/was
        /// </summary>
        /// <param name="employeeId">EmployeeId of the project</param>
        /// <returns>returns the list of projects of employee</returns>
        Task<List<ProjectManagementViewDto>> GetAllProjectsOfEmployeeAsync(Guid employeeId);

        /// <summary>
        /// this method will fetch all the members of the project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<EmployeeBasicDetailsDto>> GetProjectMembers(Guid projectId);

        /// <summary>
        /// this method is used to get the employee count for an project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<int> GetProjectMembersCount(Guid projectId);

        /// <summary>
        /// this method is used to get all project members name and id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<GetEmployeeListDto>> GetProjectMembersList(Guid projectId);

        /// <summary>
        /// this method is used to soft delete an employee form a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<ProjectManagement> RemoveEmployeeFromProject(Guid projectId, Guid employeeId);


        Task<ProjectManagement> UpdateEmployeesInProject(EmployeesInProjectDto employeesInProject);

        Task<List<Guid?>> GetEmployeeProjectAsync(Guid employeeId);

        /// <summary>
        /// this method will fetch all the project manager of employee
        /// </summary>
        /// <param name="employeeId">EmployeeId of the project</param>
        /// <returns>returns the list of project managers</returns>
        Task<List<EmployeeBasicDetailsDto>> GetProjectManagerOfEmployee(Guid employeeId);
        Task<List<GetEmployeeListDto>> GetProjectManagersOfProject(Guid projectId);
        Task<List<GetEmployeeListDto>> GetAllReporteeAsync(Guid employeeId);
        Task<List<GetReportingManagerDto>> GetAllReportingManagers(Guid employeeId);
        Task<GetReportingManagerDto> GetReportingManager(Guid employeeId, Guid projectId);
        Task RemoveReportingManagerForEmployee(Guid reportingManagerId);
        Task RemoveProjectManagerForEmployee(Guid projectManagerId);
    }
}
