using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto;
using PeoplePortalDomainLayer.Entities.Dto.ReportingManagerDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Services
{
    public class ReportingManagerService : IReportingManagerService
    { /// <summary>
      /// this class is used to handle operations related to Skills
      /// </summary>
        private readonly IReportingManagerRepository _reportingManagerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IDepartmentDesignationService _departmentDesignationService;
        /// <summary>
        /// this is the default constructor for the Skill service
        /// </summary>
        /// <param name="reportingManagerRepository">reportingManagerRepository interface object for dependency injection</param>
        /// <param name="employeeRepository">employeeRepository interface object for dependency injection</param>
        /// <param name="projectRepository">projectRepository interface object for dependency injection</param>
        /// <param name="departmentDesignationService">departmentDesignationService interface object for dependency injection</param>

        public ReportingManagerService(IReportingManagerRepository reportingManagerRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IDepartmentDesignationService departmentDesignationService)
        {
            _reportingManagerRepository = reportingManagerRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _departmentDesignationService = departmentDesignationService;

        }

        /// <summary>
        /// this method is used to fetch  the Reporting Manager
        /// </summary>
        /// <param name="employeeId">EmployeeId of the project </param>
        /// <returns>returns the Reporting Manager</returns>
        public async Task<GetReportingManagerDto> GetReportingManager(Guid employeeId)
        {
            var reportingManager = await _reportingManagerRepository.GetReportingManager(employeeId);
            if(reportingManager != null)
            {
                var empDetails = await _employeeRepository.GetEmployeeInformationAsync(reportingManager.ReportingManagerId);
                var result = new GetReportingManagerDto
                {
                    Name = string.Format("{0} {1} {2}", empDetails.FirstName, empDetails.MiddleName, empDetails.LastName),
                    Id = empDetails.Id
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
            var reportee = await _reportingManagerRepository.GetAllReporteeAsync(employeeId);
            List<GetEmployeeListDto> reporteeDetailsList = new List<GetEmployeeListDto>();
            foreach (var r in reportee)
            {
                var empDetails = await _employeeRepository.GetEmployeeInformationAsync(r);
                var reporteeDetails = new GetEmployeeListDto
                {
                    Name = string.Format("{0} {1} {2}", empDetails.FirstName, empDetails.MiddleName, empDetails.LastName),
                    Id = empDetails.Id
                };
                reporteeDetailsList.Add(reporteeDetails);
            }
            return reporteeDetailsList;

        }

        /// <summary>
        /// this method assigns reporting manager to employee
        /// </summary>
        /// <param name="ReportingManagerId"></param>
        /// <param name="listOfEmployeeId"></param> 
        /// <returns></returns>
        public async Task<List<ReportingManagerDto>> AddEmployeeReportingManagerAsync(Guid ReportingManagerId, List<Guid> listOfEmployeeId)
        {
            var reportees = await _reportingManagerRepository.GetAllReporteeAsync(ReportingManagerId);
            List<ReportingManagerDto> result = new List<ReportingManagerDto>();
            List<ReporteeDetailsDto> empSuccess = new List<ReporteeDetailsDto>();
            List<ReporteeDetailsDto> empFailure = new List<ReporteeDetailsDto>();

            //List<ReportingManagerDto> employeeAlreadyExistsList = new List<ReportingManagerDto>();
            foreach (var employee in listOfEmployeeId)
            {
                if (reportees.Contains(employee))
                {
                    var employeeExists = await _reportingManagerRepository.GetEmployeeAsync(employee);
                    if (employeeExists != null)
                    {
                        var empDetails = await _employeeRepository.GetEmployeeInformationAsync(employee);
                        var details = new ReporteeDetailsDto
                        {
                            EmployeeId = employee,
                            EmployeeName = string.Format("{0} {1} {2}", empDetails.FirstName, empDetails.MiddleName, empDetails.LastName)
                        };
                        empFailure.Add(details);
                        continue;

                    }
                }
                try
                {
                    var info = new ReportingManager
                    {
                        EmployeeId = employee,
                        ReportingManagerId = ReportingManagerId

                    };

                    _reportingManagerRepository.AddReportingManager(info);
                    await _reportingManagerRepository.SaveChangesAsync();
                    var empDetails = await _employeeRepository.GetEmployeeInformationAsync(employee);
                    var details = new ReporteeDetailsDto
                    {
                        EmployeeId = employee,
                        EmployeeName = string.Format("{0} {1} {2}", empDetails.FirstName, empDetails.MiddleName, empDetails.LastName)
                    };
                    empSuccess.Add(details);

                }
                catch (Exception ex)
                {
                    result.Add(new ReportingManagerDto { Message = ex.Message, Successful = false });
                }
                if (empFailure.Count != 0 && empSuccess.Count != 0)
                {
                    result.Add(new ReportingManagerDto { Count = empFailure.Count, ReportingManagerId = ReportingManagerId, EmployeeDetail = empFailure, Message = "Employee Already havee a Reporting manager !!", Successful = false });
                    result.Add(new ReportingManagerDto { Count = empSuccess.Count, ReportingManagerId = ReportingManagerId, EmployeeDetail = empSuccess, Message = "Employee is asigned with Reporting manager!!", Successful = true });
                }
                else if (empFailure.Count != 0)
                    result.Add(new ReportingManagerDto { Count = empFailure.Count, ReportingManagerId = ReportingManagerId, EmployeeDetail = empFailure, Message = "Employee Already havee a Reporting manager !!", Successful = false });
                else
                    result.Add(new ReportingManagerDto { Count = empSuccess.Count, ReportingManagerId = ReportingManagerId, EmployeeDetail = empSuccess, Message = "Employee is asigned with Reporting manager!!", Successful = true });

            }
            return result;
        }

        public async Task<ReportingManager> ChangeEmployeeReportingManagerAsync(UpdateReportingManagerDto updateReportingManagerDto)
        {
            var details = await _reportingManagerRepository.GetEmployeeAsync(updateReportingManagerDto.EmployeeId);
            if (details == null)
            {
                throw new Exception
                    ("Employee Doesn't Exist");
            }
            details.ReportingManagerId = updateReportingManagerDto.ReportingManagerId;
            await _reportingManagerRepository.SaveChangesAsync();
            return details;

        }
    }
}
