using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// this class is used to handle operations related to Skills
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentDesignationService _departmentDesignationService;
        private readonly IProjectManagementRepository _projectManagementRepository;
        private readonly IMapper _mapper;
        private readonly IProjectManagementService _projectManagementService;

        /// <summary>
        /// this member is used to store total records of a table
        /// </summary>
        public int totalRecords;
        /// <summary>
        /// this member is used to store total number of records being returned according to given page number
        /// </summary>
        public int returnedRecords;
        /// <summary>
        /// this member is used to store currently requested page number
        /// </summary>
        public int currentPageNumber;

        /// <summary>
        /// this is the default constructor for the Skill servicew
        /// </summary>
        /// <param name="projectRepository">projectRepository interface object for dependency injection</param>
        /// <param name="employeeRepository">employeeRepository interface object for dependency injection</param>
        /// <param name="departmentDesignationService">employeeRepository interface object for dependency injection</param>

        public ProjectService(IProjectRepository projectRepository, IEmployeeRepository employeeRepository, IDepartmentDesignationService departmentDesignationService, IProjectManagementRepository projectManagementRepository, IMapper mapper,
            IProjectManagementService projectManagementService)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _departmentDesignationService = departmentDesignationService;
            _projectManagementRepository = projectManagementRepository;
            _mapper = mapper;
            _projectManagementService = projectManagementService;
            totalRecords = 0;
            returnedRecords = 0;
            currentPageNumber = 0;
        }

        /// <summary>
        /// this method is used to add skill into database
        /// </summary>
        /// <param name="project">new project object contains name only</param>
        /// <returns>return newly created skill object</returns>
        public async Task<Project> AddProjectAsync(ProjectDetailsDto project)
        {
            //check if project exists or not

            var existingProject = await _projectRepository.GetProjectByNameAsync(project.Name);
            if (existingProject != null)
            {
                throw new Exception( "Project already exists!!");
            }
            var newProject = _mapper.Map<Project>(project);
            newProject.CreatedAt = DateTime.UtcNow;
            _projectRepository.AddProject(newProject);
            await _projectRepository.SaveChangesAsync();
            return newProject;
        }

        /// <summary>
        /// this method is used to Delete Project into database
        /// </summary>
        /// <param name="id">Id of the project</param>
        /// <returns>Soft deletes the project </returns>
        public async Task<Project> DeleteProjectAsync(Guid projectId)
        {
            var sameProject = await _projectRepository.GetProjectByIdAsync(projectId);
            if (sameProject == null)
                throw new Exception("Project does not exist.");
            sameProject.IsDeleted = true;
            sameProject.EndDate = DateTime.UtcNow;

            var employeeList = await _projectManagementService.GetProjectMembers(projectId);
            foreach (var employee in employeeList)
            {
                await _projectManagementService.RemoveEmployeeFromProject(projectId, employee.Id);
            }

            await _projectRepository.SaveChangesAsync();

            return sameProject;
        }

        /// <summary>
        /// this method is used for updating Project details into database
        /// </summary>
        /// <param name="project">Id of the project </param>
        /// <returns>updates project object</returns>
        public async Task<Project> UpdateProjectDetailsAsync(UpdateProjectDto project)
        {
            var sameProject = await _projectRepository.GetProjectByIdAsync(project.Id);

            if (sameProject == null)
            {
                throw new Exception("Project does not exist."); ;
            }
            sameProject.Name = project.Name;
            sameProject.EndDate = project.EndDate;
            sameProject.StartDate = project.StartDate;
            sameProject.ProjectClient = project.ProjectClient;            
            sameProject.ProjectDescription = project.ProjectDescription;

            await _projectRepository.SaveChangesAsync();
            return sameProject;
        }

        /// <summary>
        /// this method is used to fetch all the Projects
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="project">Object of the project </param>
        /// <returns>returns list of all the Projects</returns>
        public async Task<PagedList<Project>> GetAllProjects(PagingParams pagingParams)
        {
            var projectDetailsList = await _projectRepository.GetListOfProjectAsync(pagingParams);
            return projectDetailsList;
        }

        /// <summary>
        /// this method is used to fetch list all project
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetProjectsDetailsDto>> GetProjectList()
        {
            var projectDetailsList = await _projectRepository.GetListOfProjectAsync();
            var listOfProject = new List<GetProjectsDetailsDto>();

            foreach (var p in projectDetailsList)
            {
                var listOfManager = new List<GetEmployeeListDto>();                             
                var projectDetail = _mapper.Map<GetProjectsDetailsDto>(p);
                var projectMembers = await _projectManagementRepository.GetProjectMembers(p.Id);
                projectDetail.ProjectMembersCount = projectMembers.Count();
                listOfProject.Add(projectDetail);
            }
            return listOfProject;
        }

        /// <summary>
        /// this method is used to fetch the details of Project
        /// </summary>
        /// <param name="id">id of the project </param>
        /// <returns>returns details of  the Project</returns>
        public async Task<GetProjectsDetailsDto> GetProjectById(Guid id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            var projectDetail = _mapper.Map<GetProjectsDetailsDto>(project);
            var listOfManager = new List<GetEmployeeListDto>();           
            var projectMembers = await _projectManagementRepository.GetProjectMembers(project.Id);
            projectDetail.ProjectMembersCount = projectMembers.Count();
            return projectDetail;
        }
        /// <summary>
        /// this method is used to fetch the details of Project
        /// </summary>
        /// <param name="name">id of the project </param>
        /// <returns>returns details of  the Project</returns>
        public async Task<Project> GetProjectByName(String name)
        {
            return await _projectRepository.GetProjectByNameAsync(name);
        }
    }
}
