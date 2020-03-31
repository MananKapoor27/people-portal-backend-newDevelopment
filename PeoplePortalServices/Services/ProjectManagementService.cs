using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto;
using AutoMapper;
using Newtonsoft.Json;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using System.Transactions;

namespace PeoplePortalServices.Services
{
    public class ProjectManagementService : IProjectManagementService
    {
        /// <summary>
        /// this class is used to handle operations related to Skills
        /// </summary>
        private readonly IProjectManagementRepository _projectManagementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IDepartmentDesignationService _departmentDesignationService;
        private readonly IEmployeeService _employeeService;
        private readonly IProjectRequirementsRepository _projectRequirementsRepository;
        private readonly IReportingManagerService _reportingManagerService;


        /// <summary>
        /// this is the default constructor for the Skill service
        /// </summary>
        /// <param name="projectManagementRepository">projectManagementRepository interface object for dependency injection</param>
        /// <param name="employeeRepository">employeeRepository interface object for dependency injection</param>
        /// <param name="projectRepository">employeeRepository interface object for dependency injection</param>
        /// <param name="reportingManagerService">reportingManagerService interface object for dependency injection</param>


        public ProjectManagementService(IProjectManagementRepository projectManagementRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IMapper mapper,
            IDepartmentDesignationService departmentDesignationService, IEmployeeService employeeService, IProjectRequirementsRepository projectRequirementsRepository, IReportingManagerService reportingManagerService)
        {
            _projectManagementRepository = projectManagementRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _departmentDesignationService = departmentDesignationService;
            _employeeService = employeeService;
            _projectRequirementsRepository = projectRequirementsRepository;
            _reportingManagerService = reportingManagerService;
        }

        /// <summary>
        /// this method will add employee to the project
        /// </summary>
        /// <param name="addEmployeeToProject">EmployeesInProjectDto object</param>
        /// <returns>returns the employee added to the project</returns>
        public async Task<EmployeesInProjectDto> AddEmployeeToProject(EmployeesInProjectDto addEmployeeToProject)
        {
            var employeeExists = await _projectManagementRepository.FindProjectEmployee(addEmployeeToProject.ProjectId, addEmployeeToProject.EmployeeDetails.EmployeeId);
            var requirement = await _projectRequirementsRepository.GetRequirement((Guid)addEmployeeToProject.RequirementId);
            if (employeeExists == null && requirement != null)
            {
                var employee = _mapper.Map<ProjectManagement>(addEmployeeToProject.EmployeeDetails);
                employee.ProjectId = addEmployeeToProject.ProjectId;
                employee.RequirementId = (Guid)addEmployeeToProject.RequirementId;
                requirement.ResourceAllocated = addEmployeeToProject.EmployeeDetails.EmployeeId;
                requirement.LastModifiedAt = DateTime.UtcNow;
                //requirement.ModifiedBy = id; TODO
                requirement.IsFullfilled = true;

                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _projectManagementRepository.AddEmployeeToProject(employee);
                    await _projectManagementRepository.SaveChangesAsync();

                    await _projectRequirementsRepository.SaveChangesAsync();
                    
                    ts.Complete();
                }
                return addEmployeeToProject;
            }
            else if (employeeExists.IsDeleted == true && requirement != null)
            {
                employeeExists.IsDeleted = false;
                employeeExists.ProjectManager = addEmployeeToProject.EmployeeDetails.ProjectManager;
                employeeExists.ProjectReportingManager = addEmployeeToProject.EmployeeDetails.ProjectReportingManager;
                employeeExists.Role = addEmployeeToProject.EmployeeDetails.Role;
                employeeExists.PrimaryStatus = addEmployeeToProject.EmployeeDetails.PrimaryStatus;
                employeeExists.SecondaryStatus = addEmployeeToProject.EmployeeDetails.SecondaryStatus;
                employeeExists.AllocationStartDate = addEmployeeToProject.EmployeeDetails.AllocationStartDate;
                employeeExists.AllocationEndDate = addEmployeeToProject.EmployeeDetails.AllocationEndDate;
                employeeExists.IsManager = addEmployeeToProject.EmployeeDetails.IsManager;
                employeeExists.RequirementId = (Guid)addEmployeeToProject.RequirementId;

                requirement.ResourceAllocated = addEmployeeToProject.EmployeeDetails.EmployeeId;
                requirement.LastModifiedAt = DateTime.UtcNow;
                //requirement.ModifiedBy = id; TODO
                requirement.IsFullfilled = true;

                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _projectManagementRepository.SaveChangesAsync();
                    await _projectRequirementsRepository.SaveChangesAsync();
                    ts.Complete();
                }
                return addEmployeeToProject;
            }
            throw new Exception("Employee already exists !");
        }

        /// <summary>
        /// this method will fetch all  projects in which the employee is/was
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>returns the list of projects of employee</returns>
        public async Task<List<Guid?>> GetEmployeeProjectAsync(Guid employeeId)
        {
            return await _projectManagementRepository.GetProjectOfEmployee(employeeId);
        }

        /// <summary>
        /// this method will fetch all  projects in which the employee is/was
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>returns the list of projects of employee</returns>
        public async Task<List<ProjectManagementViewDto>> GetAllProjectsOfEmployeeAsync(Guid employeeId)
        {
            var result = await _projectManagementRepository.GetAllProjectsOfEmployee(employeeId);
            var projectList = new List<ProjectManagementViewDto>();
            foreach (var p in result)
            {
                var projectDetails = await _projectRepository.GetProjectByIdAsync((Guid)p.ProjectId);
                var project = _mapper.Map<ProjectManagementViewDto>(p);
                project.ProjectName = projectDetails.Name;

                if (p.ProjectManager != null)
                {
                    var projectManagerDetails = new GetEmployeeListDto
                    {
                        Id = (Guid)p.ProjectManager,
                        Name = await _employeeService.GetEmployeeNameById((Guid)p.ProjectManager)
                    };

                    project.ProjectManagerDetails = projectManagerDetails;
                }
                if (p.ProjectReportingManager != null)
                {
                    var reportingManagerDetails = new GetEmployeeListDto
                    {
                        Id = (Guid)p.ProjectReportingManager,
                        Name = await _employeeService.GetEmployeeNameById((Guid)p.ProjectReportingManager)
                    };
                    project.ReportingManagerDetails = reportingManagerDetails;
                }

                projectList.Add(project);
            }
            return projectList;
        }

        /// <summary>
        /// this method will fetch all the members of the project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<EmployeeBasicDetailsDto>> GetProjectMembers(Guid projectId)
        {
            var projectMembersList = await _projectManagementRepository.GetProjectMembers(projectId);
            var projectMemberDetailsList = new List<EmployeeBasicDetailsDto>();
            foreach (var projectMember in projectMembersList)
            {
                var projectMemberDetails = _mapper.Map<EmployeeBasicDetailsDto>(projectMember);
                projectMemberDetails.Id = projectMember.EmployeeId;
                if (projectMember.ProjectManager != null)
                {
                    var projectManagerDetails = new GetEmployeeListDto
                    {
                        Id = (Guid)projectMember.ProjectManager,
                        Name = await _employeeService.GetEmployeeNameById((Guid)projectMember.ProjectManager)
                    };

                    projectMemberDetails.ProjectManagerDetails = projectManagerDetails;
                }
                if (projectMember.ProjectReportingManager != null)
                {
                    var reportingManagerDetails = new GetEmployeeListDto
                    {
                        Id = (Guid)projectMember.ProjectReportingManager,
                        Name = await _employeeService.GetEmployeeNameById((Guid)projectMember.ProjectReportingManager)
                    };
                    projectMemberDetails.ReportingManagerDetails = reportingManagerDetails;
                }
                projectMemberDetails.Name = await _employeeService.GetEmployeeNameById(projectMember.EmployeeId);
                var employeeDetails = await _employeeRepository.GetEmployeeInformationAsync(projectMember.EmployeeId);
                if(employeeDetails != null)
                {
                    projectMemberDetails.CompanyId = employeeDetails.CompanyId;
                    var department = await _departmentDesignationService.GetDepartment(employeeDetails.DepartmentDesignationId);
                    if (department != null)
                        projectMemberDetails.Department = department.Name;
                    var designation = await _departmentDesignationService.GetDesignation(employeeDetails.DepartmentDesignationId);
                    if (designation != null)
                        projectMemberDetails.Designation = designation.Title;
                }
                
                projectMemberDetailsList.Add(projectMemberDetails);
            }
            return projectMemberDetailsList;
        }

        /// <summary>
        /// this method is used to get the employee count for an project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<int> GetProjectMembersCount(Guid projectId)
        {
            var projectMembersList = await _projectManagementRepository.GetProjectMembers(projectId);
            return projectMembersList.Count();
        }

        /// <summary>
        /// this method is used to get all project members name and id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<GetEmployeeListDto>> GetProjectMembersList(Guid projectId)
        {
            var projectMembersList = await _projectManagementRepository.GetProjectMembers(projectId);
            var projectMemberDetailsList = new List<GetEmployeeListDto>();
            foreach (var projectMember in projectMembersList)
            {
                var projectMemberDetails = new GetEmployeeListDto
                {
                    Id = (Guid)projectMember.EmployeeId,
                    Name = await _employeeService.GetEmployeeNameById((Guid)projectMember.EmployeeId)
                };
                projectMemberDetailsList.Add(projectMemberDetails);
            }
            return projectMemberDetailsList;
        }

        public async Task<List<GetEmployeeListDto>> GetProjectManagersOfProject(Guid projectId)
        {
            var result = new List<GetEmployeeListDto>();

            var listOfProjectManagers = await _projectManagementRepository.GetProjectManagers(projectId);
            foreach (var projectManager in listOfProjectManagers)
            {
                var employeeDetails = new GetEmployeeListDto()
                {
                    Name = await _employeeService.GetEmployeeNameById(projectManager),
                    Id = projectManager
                };
                result.Add(employeeDetails);
            }
            return result;
        }

        public async Task RemoveProjectManagerForEmployee(Guid projectManagerId)
        {
            var listOfEmployees = await _projectManagementRepository.GetEmployeesOfProjectManager(projectManagerId);

            foreach (var employee in listOfEmployees)
            {
                employee.ProjectManager = null;
                await _projectManagementRepository.SaveChangesAsync();
            }
             
        }

        public async Task RemoveReportingManagerForEmployee(Guid reportingManagerId)
        {
            var listOfReportees = await _projectManagementRepository.GetAllReporteeAsync(reportingManagerId);

            foreach (var employee in listOfReportees)
            {
                employee.ProjectReportingManager = null;
                await _projectManagementRepository.SaveChangesAsync();
            }

        }
        /// <summary>
        /// this method is used to soft delete an employee form a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<ProjectManagement> RemoveEmployeeFromProject(Guid projectId, Guid employeeId)
        {
            var projectManagement = await _projectManagementRepository.FindProjectEmployee(projectId, employeeId);
            var projectRequirement = await _projectRequirementsRepository.GetRequirement(projectManagement.RequirementId);
            
            if (projectManagement.IsManager)
            {
                await RemoveProjectManagerForEmployee(employeeId);
            }
            await RemoveReportingManagerForEmployee(employeeId);
            projectManagement.IsDeleted = true;
            projectManagement.AllocationEndDate = DateTime.UtcNow;

            if(projectRequirement != null)
                projectRequirement.IsFullfilled = false;

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _projectManagementRepository.SaveChangesAsync();
                await _projectRequirementsRepository.SaveChangesAsync();
                ts.Complete();
            }

            return projectManagement;
        }

        public async Task<ProjectManagement> UpdateEmployeesInProject(EmployeesInProjectDto employeesInProjectDto)
        {
            var employeeDetails = await _projectManagementRepository.FindProjectEmployee(employeesInProjectDto.ProjectId, employeesInProjectDto.EmployeeDetails.EmployeeId);
            //var employeeUpdatedDetails = employeeDetails;
            if (employeeDetails != null)
            {
                //employeeUpdatedDetails = _mapper.Map<ProjectManagement>(employeesInProjectDto.EmployeeDetail);
                //employeeUpdatedDetails.ProjectId = employeesInProjectDto.ProjectId;
                //employeeUpdatedDetails.ProjectManagementId = employeeDetails.ProjectManagementId;
                //employeeDetails = employeeUpdatedDetails;
                _mapper.Map(employeesInProjectDto.EmployeeDetails, employeeDetails);

                employeeDetails.ProjectId = employeesInProjectDto.ProjectId;
                //employeeDetails.RequirementId = (Guid)employeesInProjectDto.RequirementId;

                if(employeeDetails.IsManager && !employeesInProjectDto.EmployeeDetails.IsManager)
                {
                    await RemoveProjectManagerForEmployee(employeeDetails.EmployeeId);
                }

                await _projectManagementRepository.SaveChangesAsync();
                return employeeDetails;
            }
            throw new Exception("Employee does not exists.");
        }

        /// <summary>
        /// this method is used to fetch all the Project Manager
        /// </summary>
        /// <param name="projectId">EmployeeId of the project </param>
        /// <returns>returns list of all the Project Manager</returns>
        public async Task<List<EmployeeBasicDetailsDto>> GetProjectManagerOfEmployee(Guid employeeId)
        {
            List<EmployeeBasicDetailsDto> results = new List<EmployeeBasicDetailsDto>();
            var projectManager = await _projectManagementRepository.GetProjectManagerAsync(employeeId);
            foreach (var employee in projectManager)
            {
                string departmentName = string.Empty;
                string designationName = string.Empty;
                var empDetails = await _employeeRepository.GetEmployeeInformationAsync(employee);
                var department = await _departmentDesignationService.GetDepartment(empDetails.DepartmentDesignationId);
                if (department != null)
                    departmentName = department.Name;
                var designation = await _departmentDesignationService.GetDesignation(empDetails.DepartmentDesignationId);
                if (designation != null)
                    designationName = designation.Title;
                var result = new EmployeeBasicDetailsDto
                {
                    Name = string.Format("{0} {1} {2}", empDetails.FirstName, empDetails.MiddleName, empDetails.LastName),
                    Id = empDetails.Id,
                    Designation = designationName,
                    Department = departmentName
                };
                results.Add(result);
            }
            return results;
        }


        /// <summary>
        /// this method is used to fetch  the Reporting Manager
        /// </summary>
        /// <param name="employeeId">EmployeeId of the project </param>
        /// <returns>returns the Reporting Manager</returns>
        public async Task<List<GetReportingManagerDto>> GetAllReportingManagers(Guid employeeId)
        {
            var reportingManager = await _reportingManagerService.GetReportingManager(employeeId);

            List<GetReportingManagerDto> resultList = new List<GetReportingManagerDto>();
            var reportingManagerList = await _projectManagementRepository.GetAllProjectsOfEmployee(employeeId);
            if (reportingManagerList != null)
            {
                foreach (var reportingManger in reportingManagerList)
                {
                    var projectDetails = await _projectRepository.GetProjectByIdAsync((Guid)reportingManger.ProjectId);

                    var empDetails = await _employeeRepository.GetEmployeeInformationAsync(reportingManger.EmployeeId);
                    var result = new GetReportingManagerDto
                    {
                        Name = string.Format("{0} {1} {2}", empDetails.FirstName, empDetails.MiddleName, empDetails.LastName),
                        Id = empDetails.Id,
                        ProjectId = (Guid)reportingManger.ProjectId,
                        ProjectName = projectDetails.Name
                    };
                    resultList.Add(result);
                }

                resultList.Add(reportingManager);
                return resultList;
            }
            return null;
        }

        public async Task<GetReportingManagerDto> GetReportingManager(Guid employeeId, Guid projectId)
        {
            var reportingManager = await _projectManagementRepository.GetReportingManager(employeeId, projectId);
            var projectName = await _projectRepository.GetProjectByIdAsync(projectId);
            if (reportingManager != null)
            {
                var reportingManagerDetails = await _employeeRepository.GetEmployeeInformationAsync(reportingManager.ProjectReportingManager);
                var result = new GetReportingManagerDto
                {
                    Name = string.Format("{0} {1} {2}", reportingManagerDetails.FirstName, reportingManagerDetails.MiddleName, reportingManagerDetails.LastName),
                    Id = reportingManagerDetails.Id,
                    ProjectId = reportingManager.ProjectId,
                    ProjectName = projectName.Name

                };
                return result;
            }
            return null;
        }
        /// <summary>
        /// this method returns all the Reportees of an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<List<GetEmployeeListDto>> GetAllReporteeAsync(Guid employeeId)
        {

            var reportee = await _projectManagementRepository.GetAllReporteeAsync(employeeId);
            List<GetEmployeeListDto> reporteeDetailsList = new List<GetEmployeeListDto>();
            foreach (var r in reportee)
            {
                var empDetails = await _employeeRepository.GetEmployeeInformationAsync(r.EmployeeId);
                var reporteeDetails = new GetEmployeeListDto
                {
                    Name = string.Format("{0} {1} {2}", empDetails.FirstName, empDetails.MiddleName, empDetails.LastName),
                    Id = empDetails.Id
                };
                reporteeDetailsList.Add(reporteeDetails);
            }
            return reporteeDetailsList;

        }
    }
}
