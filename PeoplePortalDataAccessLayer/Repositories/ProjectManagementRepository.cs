using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// this class is used to do all ProjectManagement related database operations(feature, basic configuration and exceptions handled here)
    /// </summary>
    public class ProjectManagementRepository : IProjectManagementRepository
    {
        private readonly PeoplePortalDb _dbContext;
        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public ProjectManagementRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        /// <summary>
        /// This method will add details of project management. (Also this will be called while adding employee.)
        /// </summary>
        /// <param name="projectManagement"></param>
        public void AddEmployeeToProject(ProjectManagement projectManagement)
        {
            _dbContext.ProjectManagement.Add(projectManagement);
        }

        /// <summary>
        /// This method is used to find the Particular project of an Employee.
        /// </summary>
        /// <param name="projectManagement"></param>
        /// <returns> Object of project managment. </returns>
        public async Task<ProjectManagement> FindProjectEmployee(Guid ProjectId, Guid EmployeeId)
        {
            var result = await _dbContext.ProjectManagement.Where(p => p.EmployeeId == EmployeeId && p.ProjectId == ProjectId).SingleOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// this method will fetch all  projects in which the employee is  
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>returns the list of projects of employee</returns>
        public async Task<List<Guid?>> GetProjectOfEmployee(Guid employeeId)
        {
            var result = await _dbContext.ProjectManagement.Where(c => c.EmployeeId == employeeId && c.IsDeleted == false).Select(p => p.ProjectId).ToListAsync();
            return result;
        }

        /// <summary>
        /// this method will fetch all the members of the projects 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>returns the list of  members of the projects </returns>
        public async Task<List<ProjectManagement>> GetProjectMembers(Guid projectId)
        {
            var result = await _dbContext.ProjectManagement.Where(c => c.ProjectId == projectId && c.IsDeleted == false).ToListAsync();
            return result;
        }

        public async Task<List<Guid>> GetProjectManagers(Guid projectId)
        {
            var result = await _dbContext.ProjectManagement.Where(c => c.ProjectId == projectId && c.IsDeleted == false && c.IsManager == true).Select(p => p.EmployeeId).ToListAsync();
            return result;
        }
        /// <summary>
        /// this method is used to fetch all the projects (past and current) of an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>returns list of object of projectManagement</returns>
        public async Task<List<ProjectManagement>> GetAllProjectsOfEmployee(Guid employeeId)
        {
            var result = await _dbContext.ProjectManagement.Where(p => p.EmployeeId == employeeId && p.IsDeleted == false).ToListAsync();
            return result;
        }
        /// <summary>
        /// this method is used to fetch all the project manager of an employee
        /// </summary>
        /// <param name=employeeId></param>
        /// <returns>returns list  projectManager</returns>
        public async Task<List<Guid?>> GetProjectManagerAsync(Guid employeeId)
        {
            var result = await _dbContext.ProjectManagement.Where(c => c.EmployeeId == employeeId && c.IsDeleted == false).Select(p => p.ProjectManager).ToListAsync();
            return result;
        }

        public async Task<List<ProjectManagement>> GetEmployeesOfProjectManager(Guid projectManagerId)
        {
            var result = await _dbContext.ProjectManagement.Where(c => c.ProjectManager == projectManagerId && c.IsDeleted == false).ToListAsync();
            return result;
        }

        /// <summary>
        /// this method is used to retrieve Reporting manager of an employee in specific project.
        /// </summary>
        /// <param name="employeeId"> id of an employee </param>
        /// <returns>returns Reporting manager</returns>
        public async Task<ProjectManagement> GetReportingManager(Guid employeeId,Guid projectId)
        {

            var result = await _dbContext.ProjectManagement.Where(c => c.EmployeeId == employeeId && c.ProjectId == projectId).SingleOrDefaultAsync();

            return result;
        }
        /// <summary>
        /// this method is used to retrieve Reporting manager list of an employee in different project.
        /// </summary>
        /// <param name="employeeId"> id of an employee </param>
        /// <returns>returns List of Reporting manager</returns>
        public async Task<List<Guid?>> GetAllReportingManagers(Guid employeeId)
        {
            var result = await _dbContext.ProjectManagement.Where(c => c.EmployeeId == employeeId ).Select(r => r.ProjectReportingManager).Distinct().ToListAsync();
            return result;
        }

        /// <summary>`
        /// this method is used to retrieve reportees of Reporting manager 
        /// </summary>
        /// <param name="employeeId">  id of an employee </param>
        /// <returns>returns list of the reportees</returns>
        public async Task<List<ProjectManagement>> GetAllReporteeAsync(Guid employeeId)
        {
            try
            {
                var result = await _dbContext.ProjectManagement.Where(c => c.ProjectReportingManager == employeeId).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

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
