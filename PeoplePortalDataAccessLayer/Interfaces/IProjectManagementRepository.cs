using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// This interface is used to provide contract for ProjectManagement repository
    /// </summary>
    public interface IProjectManagementRepository
    {
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// This method will add details of project management. (Also this will be called while adding employee.)
        /// </summary>
        /// <param name="projectManagement"></param>
        void AddEmployeeToProject(ProjectManagement projectManagement);

        /// <summary>
        /// This method is used to find the project managment object.
        /// </summary>
        /// <param name="projectManagement"></param>
        /// <returns> Object of project managment. </returns>
        Task<ProjectManagement> FindProjectEmployee(Guid ProjectId, Guid EmployeeId);


        Task<List<ProjectManagement>> GetEmployeesOfProjectManager(Guid projectManagerId);

        /// <summary>
        /// this method will fetch all  projects in which the employee is  
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>returns the list of projects of employee</returns>
        Task<List<Guid?>> GetProjectOfEmployee(Guid employeeId);
        /// <summary>
        /// this method will fetch all the members of the projects 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>returns the list of  members of the projects </returns>
        Task<List<ProjectManagement>> GetProjectMembers(Guid projectId);

        /// <summary>
        /// this method is used to fetch all the projects (past and current) of an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>returns list of object of projectManagement</returns>
        Task<List<ProjectManagement>> GetAllProjectsOfEmployee(Guid employeeId);
        Task<List<Guid?>> GetProjectManagerAsync(Guid employeeId);
        Task<List<Guid>> GetProjectManagers(Guid projectId);
        Task<ProjectManagement> GetReportingManager(Guid employeeId, Guid projectId);
        Task<List<Guid?>> GetAllReportingManagers(Guid employeeId);
        Task<List<ProjectManagement>> GetAllReporteeAsync(Guid employeeId);
    }
}
