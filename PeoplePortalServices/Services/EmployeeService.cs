using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Newtonsoft.Json;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeDto;
using PeoplePortalDomainLayer.Entities.DTO.SuccessStatusDTO;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Shared.AWS;
using PeoplePortalServices.Shared.Helpers;
using CsvHelper;
using System.Globalization;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDto;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDesignationDto;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;
using PeoplePortalServices.Shared.ElasticSearch;
using PeoplePortalDomainLayer.HelperModels.ElasticSearchModels;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// This EmployeeService class contains all the Employee related functions
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentDesignationRepository _departmentDesignationRepository;
        private readonly IEmployeeSkillsRepository _employeeSkillRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IProjectManagementRepository _projectManagementRepository;
        private readonly IProjectRequirementsRepository _projectRequirementsRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        //private readonly IAWSServices _aWSServices;
        private readonly IReportingManagerRepository _reportingManagerRepository;
        private readonly IElasticSearchService _elasticSearchService;

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
        /// this is the default constructor for the Employee service class
        /// </summary> 
        /// <param name="unityContainer">object of unity container interface for dependency injection </param>    
        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentDesignationRepository departmentDesignationRepository,
            IEmployeeSkillsRepository employeeSkillRepository, ISkillRepository skillRepository /*IAWSServices aWSServices*/, IProjectManagementRepository projectManagementRepository, IProjectRequirementsRepository projectRequirementsRepository,
            IProjectRepository projectRepository, IConfiguration configuration, IMapper mapper, IReportingManagerRepository reportingManagerRepository, IElasticSearchService elasticSearchService)

        {
            _employeeRepository = employeeRepository;
            _departmentDesignationRepository = departmentDesignationRepository;
            _employeeSkillRepository = employeeSkillRepository;
            _skillRepository = skillRepository;
            _projectManagementRepository = projectManagementRepository;
            _projectRequirementsRepository = projectRequirementsRepository;
            _projectRepository = projectRepository;
            _configuration = configuration;
            _mapper = mapper;
            _reportingManagerRepository = reportingManagerRepository;
            _elasticSearchService = elasticSearchService;
            totalRecords = 0;
            returnedRecords = 0;
            currentPageNumber = 0;
        }


        /// <summary>
        /// this method is used to add new employee's official details in the system
        /// </summary>
        /// <param name="newEmployeeOfficialDetails">New Employee Official Details</param>
        public async Task<Guid> AddNewEmployeeOfficialInformationAsync(AddEmployeeOfficialInformationDto newEmployeeOfficialDetails)
        {
            var employeeId = Guid.NewGuid();
            //check if same employee id and employee email already exists
            var sameEmployeeDetails = await _employeeRepository.GetEmployeeByCompanyIdAsync(newEmployeeOfficialDetails.CompanyId);
            if (sameEmployeeDetails != null)
            {
                throw new Exception("Employee already exist");
            }
            //get department designation mapping id from departmentdesignation table
            var departmentDesignation = await _departmentDesignationRepository.GetDepartmentDesignationAsync(newEmployeeOfficialDetails.DepartmentId, newEmployeeOfficialDetails.DesignationId);
            if (departmentDesignation == null)
            {
                throw new Exception("Department designation Not Mapped");
            }

            var employee = _mapper.Map<Employee>(newEmployeeOfficialDetails);

            employee.Id = employeeId;
            employee.DepartmentDesignationId = departmentDesignation.Id;
            employee.IsDeleted = false;
            employee.CreatedAt = DateTime.UtcNow;
            employee.ModifiedAt = DateTime.UtcNow;
            employee.Languages = JsonConvert.SerializeObject(newEmployeeOfficialDetails.Languages);
            employee.Gender = employee.Gender.ToLower();

            var employeeSkill = new EmployeeSkill
            {
                EmployeeId = employee.Id,
                PrimarySkills = JsonConvert.SerializeObject(newEmployeeOfficialDetails.PrimarySkill),
                SecondarySkills = JsonConvert.SerializeObject(newEmployeeOfficialDetails.SecondarySkill),
                IsDeleted = false,
            };

            var reportingManager = new ReportingManager
            {
                EmployeeId = employeeId,
                ReportingManagerId = newEmployeeOfficialDetails.ReportingManager

            };


            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _employeeRepository.AddNewEmployee(employee);
                await _employeeRepository.SaveChangesAsync();

                if (newEmployeeOfficialDetails.ReportingManager != null)
                {
                    _reportingManagerRepository.AddReportingManager(reportingManager);
                    await _reportingManagerRepository.SaveChangesAsync();
                }

             
                _employeeSkillRepository.AddEmployeeSkill(employeeSkill);
                await _employeeSkillRepository.SaveChangesAsync();
                ts.Complete();
            }

            //var elasticEmployeeDetailsDto = _mapper.Map<ElasticEmployeeDetailsDto>(newEmployeeOfficialDetails);
            //elasticEmployeeDetailsDto.Id = employeeId;
            //await AddEmployeeToElasticSearch(elasticEmployeeDetailsDto);

            return employee.Id;
        }

        /// <summary>
        /// this method is used to get all the official details of employee
        /// </summary>
        /// <param name="employeeId">emloyee id </param>
        public async Task<EditEmployeeOfficialInformationDto> GetEmployeeOfficialDetails(Guid employeeId)
        {
            var employeeDetails = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            var empDepartmentDesignationId = employeeDetails.DepartmentDesignationId;
            if (empDepartmentDesignationId == 0)
            {
                throw new Exception("NotFound.Employee not found");
            }
            var departmentId = await _departmentDesignationRepository.GetDepartmentByDepartmentDesignation(empDepartmentDesignationId);
            var department = await _departmentDesignationRepository.GetDepartmentById(departmentId);
            var departmentName = department.Name;
            var designationId = await _departmentDesignationRepository.GetDesignationByDepartmentDesignation(empDepartmentDesignationId);
            var designation = await _departmentDesignationRepository.GetDesignationById(designationId);
            var designationName = designation.Title;
            var employeeSkills = await _employeeSkillRepository.GetEmployeeSkills(employeeId);
            var reportingManager = await _reportingManagerRepository.GetReportingManager(employeeId);

            var result = _mapper.Map<EditEmployeeOfficialInformationDto>(employeeDetails);
            result.Designation = new DesignationUpdateDto { Id = designationId, Title = designationName };
            result.Department = new DepartmentUpdateDto { Id = departmentId, Name = departmentName };
            if (result.Languages.Count > 0)
                result.Languages = JsonConvert.DeserializeObject<List<string>>(employeeDetails.Languages);
            if (employeeSkills != null)
            {
                result.PrimarySkill = JsonConvert.DeserializeObject<List<string>>(employeeSkills.PrimarySkills);
                result.SecondarySkill = JsonConvert.DeserializeObject<List<string>>(employeeSkills.SecondarySkills);
            }
            result.ReportingManager = new GetEmployeeListDto();
            if (reportingManager != null)
            {
                var reportingManagerDetails = new GetEmployeeListDto();
                var reportingManagerName = await GetEmployeeNameById((Guid)reportingManager.ReportingManagerId);
                if (reportingManagerName != null)
                {
                    reportingManagerDetails = new GetEmployeeListDto
                    {
                        Id = (Guid)reportingManager.ReportingManagerId,
                        Name = reportingManagerName
                    };
                }
                result.ReportingManager = reportingManagerDetails;
            }
            return result;
        }

        /// <summary>
        /// this method is used to get all the personal details of employee
        /// </summary>
        /// <param name="employeeId">employee id </param>
        public async Task<EditEmployeePersonalInformationDto> GetEmployeePersonalDetails(Guid employeeId)
        {
            List<ChildrenData> childrenDataList = new List<ChildrenData>();
            List<DateTime> childrenBirthday = new List<DateTime>();
            var employeeDetails = await _employeeRepository.GetEmployeeInformationAsync(employeeId);
            if (employeeDetails == null)
                throw new Exception("Employee does not exist in the system");
            var result = _mapper.Map<EditEmployeePersonalInformationDto>(employeeDetails);
          
            if (result.NumberOfChildren != 0)
            {
                var namesOfChildren = JsonConvert.DeserializeObject<List<string>>(employeeDetails.NamesOfChildren);
                var childrenDateOfBirth = JsonConvert.DeserializeObject<List<string>>(employeeDetails.DateOfBirthOfChildren);
                foreach (var childDateOfBirth in childrenDateOfBirth)
                {
                    DateTime childDob = Convert.ToDateTime(childDateOfBirth);
                    childrenBirthday.Add(childDob);
                }

                for (int i = 0; i<result.NumberOfChildren ; i++)
                {
                    ChildrenData childrenData = new ChildrenData();
                    childrenData.Name = namesOfChildren[i];
                    childrenData.DateOfBirth = childrenBirthday[i];
                    childrenDataList.Add(childrenData);
                }
                result.childrenData = childrenDataList;
            }
            return result;
        }


        /// <summary>
        /// this method is used to get all the educational details of employee
        /// </summary>
        /// <param name="employeeId">used to identify the required employee</param>
        public async Task<EditEmployeeEducationalInformationDto> GetEmployeeEducationalDetails(Guid employeeId)
        {
            var employeeDetails = await _employeeRepository.GetEmployeeInformationAsync(employeeId);
            if (employeeDetails == null)
                throw new Exception("Employee does not exist in the system");
            EditEmployeeEducationalInformationDto employeeEducationalDetails = new EditEmployeeEducationalInformationDto();
            _mapper.Map(employeeDetails, employeeEducationalDetails);

            return employeeEducationalDetails;
        }

        /// <summary>
        /// this method is used to edit the employees's official details
        /// </summary>
        /// <param name="employeeOfficialDetails">Employee Official Details</param>
        public async Task EditEmployeeOfficialDetailsAsync(EditEmployeeOfficialInformationDto employeeOfficialDetails)
        {
            if (employeeOfficialDetails == null)
            {
                throw new Exception(" Employee details to be edited are not mentioned");
            }

            var oldEmployeeData = await _employeeRepository.GetEmployeeInformationAsync(employeeOfficialDetails.Id);
            _mapper.Map(employeeOfficialDetails, oldEmployeeData);
            oldEmployeeData.ModifiedAt = DateTime.UtcNow;
            oldEmployeeData.Languages = JsonConvert.SerializeObject(employeeOfficialDetails.Languages);
            oldEmployeeData.Gender = employeeOfficialDetails.Gender.ToLower();

            var department = await _departmentDesignationRepository.GetDepartmentById(employeeOfficialDetails.Department.Id);
            var designation = await _departmentDesignationRepository.GetDesignationById(employeeOfficialDetails.Designation.Id);
            var departmentDesignation = await _departmentDesignationRepository.GetDepartmentDesignationAsync(department.Id, designation.Id);
            oldEmployeeData.DepartmentDesignationId = departmentDesignation.Id;
            await _employeeRepository.SaveChangesAsync();

            var skills = await _employeeSkillRepository.GetEmployeeSkills(employeeOfficialDetails.Id);
            skills.PrimarySkills = JsonConvert.SerializeObject(employeeOfficialDetails.PrimarySkill);
            skills.SecondarySkills = JsonConvert.SerializeObject(employeeOfficialDetails.SecondarySkill);
            await _employeeSkillRepository.SaveChangesAsync();

            var oldReportingManager = await _reportingManagerRepository.GetReportingManager(employeeOfficialDetails.Id);

            if (employeeOfficialDetails.ReportingManager.Id != null)
            {
                var newReportingManager = new ReportingManager
                {
                    EmployeeId = employeeOfficialDetails.Id,
                    ReportingManagerId = employeeOfficialDetails.ReportingManager.Id
                };

                if (oldReportingManager != null)
                {
                    oldReportingManager.ReportingManagerId = employeeOfficialDetails.ReportingManager.Id;
                }
                else
                {
                    _reportingManagerRepository.AddReportingManager(newReportingManager);
                }

                await _reportingManagerRepository.SaveChangesAsync();
            }

        }

        /// <summary>
        /// this method is used to edit the employees's personal details
        /// </summary>
        /// <param name="employeePersonalDetails">Employee Personal Details</param>
        public async Task EditEmployeePersonalDetailsAsync(EditEmployeePersonalInformationDto employeePersonalDetails)
        {
            List<string> namesOfChildren = new List<string>();
            List<DateTime> childrenDateOFBirth = new List<DateTime>();
            if (employeePersonalDetails == null)
            {
                throw new Exception(" Employee details to be edited are not mentioned");
            }
            var oldEmployeeData = await _employeeRepository.GetEmployeeInformationAsync(employeePersonalDetails.Id);
            if (oldEmployeeData == null)
                throw new Exception("Employee does not exist in the system");

            _mapper.Map(employeePersonalDetails, oldEmployeeData);
            if (employeePersonalDetails.NumberOfChildren != 0)
            {
                foreach (var childData in employeePersonalDetails.childrenData)
                {
                    string name = childData.Name;
                    DateTime dob = childData.DateOfBirth;
                    namesOfChildren.Add(name);
                    childrenDateOFBirth.Add(dob);
                }

                oldEmployeeData.NamesOfChildren = JsonConvert.SerializeObject(namesOfChildren);
                oldEmployeeData.DateOfBirthOfChildren = JsonConvert.SerializeObject(childrenDateOFBirth);
            }
          
            oldEmployeeData.ModifiedAt = DateTime.UtcNow;
            await _employeeRepository.SaveChangesAsync();

        }

        /// <summary>
        /// this method is used to edit the employees's educational details
        /// </summary>
        /// <param name="employeeEducationalDetails">Employee Educational Details</param>
        public async Task EditEmployeeEducationalDetailsAsync(EditEmployeeEducationalInformationDto employeeEducationalDetails)
        {
            var oldEmployeeData = await _employeeRepository.GetEmployeeInformationAsync(employeeEducationalDetails.Id);
            if (oldEmployeeData == null)
                throw new Exception("Employee does not exist in the system");

            _mapper.Map(employeeEducationalDetails, oldEmployeeData);
            oldEmployeeData.ModifiedAt = DateTime.UtcNow;
            await _employeeRepository.SaveChangesAsync();

        }

        /// <summary>
        /// this method is used to get all the details of employee
        /// </summary>
        /// <param name="companyEmail"></param>
        /// <returns>Employee Details</returns>
        public async Task<EditEmployeeDto> GetEmployeeDetailsByEmail(string companyEmail)
        {
            var empDetails = await _employeeRepository.GetEmployeeByCompanyEmailAsync(companyEmail);
            var empDepartmentDesignationId = empDetails.DepartmentDesignationId;
            if (empDepartmentDesignationId == 0)
            {
                throw new Exception("NotFound.Employee not found");
            }
            var departmentId = await _departmentDesignationRepository.GetDepartmentByDepartmentDesignation(empDepartmentDesignationId);
            var department = await _departmentDesignationRepository.GetDepartmentById(departmentId);
            var designationId = await _departmentDesignationRepository.GetDesignationByDepartmentDesignation(empDepartmentDesignationId);
            var designation = await _departmentDesignationRepository.GetDesignationById(designationId);
            var employeeSkills = await _employeeSkillRepository.GetEmployeeSkills(empDetails.Id);
            var reportingManager = await _reportingManagerRepository.GetReportingManager(empDetails.Id);

            var result = _mapper.Map<EditEmployeeDto>(empDetails);
            result.Designation = new DesignationUpdateDto { Id = designationId, Title = designation.Title };
            result.Department = new DepartmentUpdateDto { Id = departmentId, Name = department.Name };
            if (result.Languages.Count > 0)
                result.Languages = JsonConvert.DeserializeObject<List<string>>(empDetails.Languages);
            if (employeeSkills != null)
            {
                result.PrimarySkill = JsonConvert.DeserializeObject<List<string>>(employeeSkills.PrimarySkills);
                result.SecondarySkill = JsonConvert.DeserializeObject<List<string>>(employeeSkills.SecondarySkills);
            }
            result.ReportingManager = null;
             if (reportingManager != null)
            {
                var reportingManagerName = await GetEmployeeNameById((Guid)reportingManager.ReportingManagerId);
                var reportingManagerDetails = new GetEmployeeListDto
                {
                    Id = (Guid)reportingManager.ReportingManagerId,
                    Name = reportingManagerName
                };
                result.ReportingManager = reportingManagerDetails;
            }
            return result;
        }



        public async Task<List<CsvResultDto>> AddEmployeesToDatabase(List<BulkAddEmployeesDto> listOfEmployees)
        {
            List<CsvResultDto> erroredOperations = new List<CsvResultDto>();
            List<CsvResultDto> completedOperations = new List<CsvResultDto>();

            var departmentDesignationTable = await _employeeRepository.GetDepartmentDesignationTable();

            foreach (var employee in listOfEmployees)
            {
                //check if same employee id and employee email already exists
                var employeeExists = await _employeeRepository.GetEmployeeByCompanyIdAsync(employee.CompanyId);

                if (employeeExists != null)
                {
                    erroredOperations.Add(new CsvResultDto { companyEmail = employee.CompanyEmail, errorMessage = "This CompanyID already exists in our database !!", Successful = false });
                    continue;
                }

                try
                {
                    var employeeDetails = _mapper.Map<Employee>(employee);
                    var employeeId = Guid.NewGuid();
                    employeeDetails.Id = employeeId;
                    employeeDetails.Languages = JsonConvert.SerializeObject(employee.Languages);
                    employee.Gender = employee.Gender.ToLower();
                    var departmentDesignation = departmentDesignationTable.AsEnumerable().SingleOrDefault(x => x.DepartmentId == employee.Department && x.DesignationId == employee.Designation);
                    if (departmentDesignation != null)
                    {
                        employeeDetails.DepartmentDesignationId = departmentDesignation.Id;
                    }



                    var employeeSkill = new EmployeeSkill
                    {
                        EmployeeId = employeeId,
                        PrimarySkills = JsonConvert.SerializeObject(employee.PrimarySkills),
                        SecondarySkills = JsonConvert.SerializeObject(employee.SecondarySkills),
                        IsDeleted = false,
                    };


                    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        _employeeRepository.AddNewEmployee(employeeDetails);
                        await _employeeRepository.SaveChangesAsync();

                        _employeeSkillRepository.AddEmployeeSkill(employeeSkill);
                        await _employeeSkillRepository.SaveChangesAsync();

                        completedOperations.Add(new CsvResultDto { companyEmail = employee.CompanyEmail, Successful = true, errorMessage = null });
                        ts.Complete();
                    }

                    var elasticEmployeeDetailsDto = _mapper.Map<ElasticEmployeeDetailsDto>(employee);
                    elasticEmployeeDetailsDto.Id = employeeId;
                    await AddEmployeeToElasticSearch(elasticEmployeeDetailsDto);

                }
                catch (Exception ex)
                {
                    erroredOperations.Add(new CsvResultDto { companyEmail = employee.CompanyEmail, Successful = true, errorMessage = "Unknown Error !!" });
                }
            }

            return erroredOperations;
        }

        /// <summary>
        /// this method is used to fetch list of gender for employees
        /// </summary>
        /// <returns>returns list of string that contains gender information</returns>
        public List<string> GenderList()
        {
            return new List<string>
            {
                "Female",
                "Male",
                "Prefer not to say"
            };
        }

        /// <summary>
        /// this method is used to fetch list of social handle titles for employees
        /// </summary>
        /// <returns>returns list of string that contains social handle titles</returns>
        public List<string> SocialHandlesTitle()
        {
            return new List<string>
            {
                "Github",
                "Twitter",
                "LinkedIn",
                "Facebook"
            };
        }

        /// <summary>
        /// this method is used to soft delete an existing employee's data in the system
        /// </summary>
        /// <param name="employeeId">id to indentify the employee to be deleted</param>
        public async Task DeleteEmployeeAsync(Guid employeeId)
        {
            var empId = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (empId == null)
            {
                throw new Exception("Employee not found");
            }
            empId.IsDeleted = true;

            await _employeeRepository.SaveChangesAsync();

            var empSkills = await _employeeSkillRepository.GetEmployeeSkills(empId.Id);
            if (empSkills == null)
            {
                throw new Exception("Employee skills not found");
            }
            empSkills.IsDeleted = true;
            await _employeeSkillRepository.SaveChangesAsync();

            //* These methods of Project management Service (RemoveProjectManagerForEmployee,RemoveReportingManagerForEmployee,RemoveEmployeeFromProject) should have been used but we could not call the interface because of cyclic dependency*//
            // this will set ProjectReportingManager to null for employee who had this Employee as an ProjectReportingManager
            var reportee = await _reportingManagerRepository.GetAllReporteeAsync(empId.Id);
            foreach (var employee in reportee)
            {
               var empDetails= await _reportingManagerRepository.GetReportingManager(employee);
                _reportingManagerRepository.DeleteRecord(empDetails);
                await _reportingManagerRepository.SaveChangesAsync();

            }
            var listOfReportees = await _projectManagementRepository.GetAllReporteeAsync(empId.Id);
            foreach (var employee in listOfReportees)
            {
                employee.ProjectReportingManager = null;
                await _projectManagementRepository.SaveChangesAsync();
            }
            var empProject = await _projectManagementRepository.GetAllProjectsOfEmployee(empId.Id);

            foreach (var project in empProject)
            {
                if (project.IsManager == true)
                {
                    var listOfEmployees = await _projectManagementRepository.GetEmployeesOfProjectManager(empId.Id);

                    foreach (var employee in listOfEmployees)
                    {
                        employee.ProjectManager = null;
                        await _projectManagementRepository.SaveChangesAsync();
                    }

                    var projectManagement = await _projectManagementRepository.FindProjectEmployee((Guid)project.ProjectId, empId.Id);
                    var projectRequirement = await _projectRequirementsRepository.GetRequirement(projectManagement.RequirementId);

                    projectManagement.IsDeleted = true;
                    projectManagement.AllocationEndDate = DateTime.UtcNow;

                    if (projectRequirement != null)
                        projectRequirement.IsFullfilled = false;

                    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _projectManagementRepository.SaveChangesAsync();
                        await _projectRequirementsRepository.SaveChangesAsync();
                        ts.Complete();
                    }
                }
            }

        }

        public IEnumerable<BulkAddEmployeesDto> GetListOfEmployees(IFormFile formFile)
        {
            //TextReader reader = new StreamReader(formFile.FileName);

            using (TextReader textReader = new System.IO.StreamReader(formFile.OpenReadStream()))
            using (var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.HasHeaderRecord = true;
                csvReader.Configuration.Delimiter = ",";
                csvReader.Configuration.RegisterClassMap<BulkAddEmployeesDtoMapper>();
                csvReader.Configuration.MissingFieldFound = null;
                try
                {
                    var records = csvReader.GetRecords<BulkAddEmployeesDto>().ToList();
                    return records;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }


            }

        }

        public List<BulkAddEmployeesDto> ProcessListOfEmployees(List<BulkAddEmployeesDto> resultTable)
        {
            foreach (var employeeEntry in resultTable)
            {
                if (employeeEntry.Languages.Count > 0)
                    employeeEntry.Languages = employeeEntry.Languages[0].Split(',').Select(x => x.Trim()).ToList();

                if (employeeEntry.PrimarySkills.Count > 0)
                    employeeEntry.PrimarySkills = employeeEntry.PrimarySkills[0].Split(',').Select(x => x.Trim()).ToList();

                if (employeeEntry.SecondarySkills.Count > 0)
                    employeeEntry.SecondarySkills = employeeEntry.SecondarySkills[0].Split(',').Select(x => x.Trim()).ToList();

            }
            return resultTable;
        }

        /// <summary>
        /// this method is used to get employee name
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<string> GetEmployeeNameById(Guid employeeId)
        {
            string employeeName = null;
            var employeeDetails = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employeeDetails != null)
            {
                employeeName = string.Format("{0} {1} {2}", employeeDetails.FirstName, employeeDetails.MiddleName, employeeDetails.LastName);
                employeeName = string.Join(" ", employeeName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            }
            return employeeName;
        }

       

     
        /// <summary>
        /// this method is used to get list of all employees details
        /// </summary>
        /// <param name="pageNumber">pagenumber</param>
        public async Task<PagedList<Employee>> GetAllEmployeeDetails(PagingParams pagingParams)
        {
            var empDetails = await _employeeRepository.GetListOfEmployeeAsync(pagingParams);
            return empDetails;
        }

        /// <summary>
        /// this method is used to fetch employeeList
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetEmployeeListDto>> GetEmployeeList()
        {
            var employeeDetailsList = await _employeeRepository.GetListOfEmployeeAsync();
            var employeeList = new List<GetEmployeeListDto>();

            foreach (var emp in employeeDetailsList)
            {
                var employee = new GetEmployeeListDto
                {
                    Id = emp.Id,
                    Name = string.Format("{0} {1} {2}", emp.FirstName, emp.MiddleName, emp.LastName)
                };
                employeeList.Add(employee);
            }
            return employeeList;
        }

        /// <summary>
        /// this method is used to search for searchString in employee's name
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>List of Employee</returns>
        public async Task<List<GetEmployeeListDto>> EmployeeSearch(string searchString)
        {
            var employeeDetailsList = await _employeeRepository.EmployeeSearch(searchString);
            var employeeList = new List<GetEmployeeListDto>();

            foreach (var emp in employeeDetailsList)
            {
                var employeeName = string.Format("{0} {1} {2}", emp.FirstName, emp.MiddleName, emp.LastName);
                employeeName = String.Join(" ", employeeName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

                var employee = new GetEmployeeListDto
                {
                    Id = emp.Id,
                    Name = employeeName
                };
                employeeList.Add(employee);
            }
            return employeeList;
        }

        /// <summary>
        /// this method is used to search for employee for a perticular skill and designation.
        /// skill name will be empty string instead of null
        /// </summary>
        /// <param name="designationAndSkillEmployeeSearch"></param>
        /// <returns></returns>
        public async Task<List<GetAllEmployeeDetailsDto>> DesignationAndSkillEmployeeSearch(DesignationAndSkillEmployeeSearch designationAndSkillEmployeeSearch)
        {
            string searchString = string.Empty;
            var employeeList = new List<GetAllEmployeeDetailsDto>();
            var employeeWithSkills = await _employeeSkillRepository.SearchForEmployeeSkill(designationAndSkillEmployeeSearch.SkillName);
            if (designationAndSkillEmployeeSearch.searchString != null)
                searchString = designationAndSkillEmployeeSearch.searchString.ToLower();

            if (designationAndSkillEmployeeSearch.DesignationId != null)
            {
                var departmentDesignation = await _departmentDesignationRepository.GetDepartmentDesignationForDesignation((int)designationAndSkillEmployeeSearch.DesignationId);
                var designationDetails = await _departmentDesignationRepository.GetDesignationByDepartmentDesignationId(departmentDesignation.Id);

                foreach (var employee in employeeWithSkills)
                {
                    var employeeDetails = await _employeeRepository.GetEmployeeInformationAsync(employee);
                    if (employeeDetails.DepartmentDesignationId == departmentDesignation.Id)
                    {
                        var emp = _mapper.Map<GetAllEmployeeDetailsDto>(employeeDetails);
                        emp.DesignationTitle = designationDetails;

                        if (designationAndSkillEmployeeSearch.searchString == null)
                        {
                            employeeList.Add(emp);
                            continue;
                        }
                        var employeeName = string.Format("{0} {1} {2}", employeeDetails.FirstName, employeeDetails.MiddleName, employeeDetails.LastName);
                        employeeName = String.Join(" ", employeeName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                        emp.Name = employeeName;
                        if (employeeName.ToLower().Contains(searchString))
                            employeeList.Add(emp);
                    }
                }
            }
            else
            {
                foreach (var employee in employeeWithSkills)
                {
                    var employeeDetails = await _employeeRepository.GetEmployeeInformationAsync(employee);
                    var designationDetails = await _departmentDesignationRepository.GetDesignationByDepartmentDesignationId(employeeDetails.DepartmentDesignationId);
                    var emp = _mapper.Map<GetAllEmployeeDetailsDto>(employeeDetails);
                    emp.DesignationTitle = designationDetails;
                    if (designationAndSkillEmployeeSearch.searchString == null)
                    {
                        employeeList.Add(emp);
                        continue;
                    }
                    var employeeName = string.Format("{0} {1} {2}", employeeDetails.FirstName, employeeDetails.MiddleName, employeeDetails.LastName);
                    employeeName = String.Join(" ", employeeName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                    emp.Name = employeeName;
                    if (employeeName.ToLower().Contains(searchString))
                        employeeList.Add(emp);
                }
            }
            return employeeList;
        }

        private async Task<ElasticEmployeeDetails> AddEmployeeToElasticSearch(ElasticEmployeeDetailsDto elasticEmployeeDetailsDto)
        {
            var employeeName = string.Format("{0} {1} {2}", elasticEmployeeDetailsDto.FirstName, elasticEmployeeDetailsDto.MiddleName, elasticEmployeeDetailsDto.LastName);
            employeeName = String.Join(" ", employeeName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

            var department = await _departmentDesignationRepository.GetDepartmentById(elasticEmployeeDetailsDto.DepartmentId);
            var designation = await _departmentDesignationRepository.GetDesignationById(elasticEmployeeDetailsDto.DesignationId);

            var skills = new List<string>();
            skills.Union(elasticEmployeeDetailsDto.PrimarySkill);
            skills.Union(elasticEmployeeDetailsDto.SecondarySkill);

            var elasticEmployeeDetails = new ElasticEmployeeDetails
            {
                Id = elasticEmployeeDetailsDto.Id,
                Name = employeeName,
                DepartmentName = department.Name,
                DesignationName = designation.Title,
                CompanyId = elasticEmployeeDetailsDto.CompanyId,
                CompanyEmail = elasticEmployeeDetailsDto.CompanyEmail,
                Skills = skills
            };

            await _elasticSearchService.AddUserToElasticSearch(elasticEmployeeDetails);

            return elasticEmployeeDetails;
        }
    }
}
