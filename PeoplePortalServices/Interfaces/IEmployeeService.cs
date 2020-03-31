using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeDto;
using PeoplePortalDomainLayer.Entities.DTO.SuccessStatusDTO;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.PaginationModels;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// This interface is used to provide contract for employee service
    /// </summary>
    public interface IEmployeeService 
    {
       
        /// <summary>
        /// this method is used to add new employee's official details in the system
        /// </summary>
        /// <param name="newEmployeeOfficialDetails">New Employee Official Details</param>
        Task<Guid> AddNewEmployeeOfficialInformationAsync(AddEmployeeOfficialInformationDto newEmployeeOfficialDetails);

        /// <summary>
        /// this method is used to edit the employee's official details
        /// </summary>
        /// <param name="employeeOfficialDetails">Employee Official Details</param>
        Task EditEmployeeOfficialDetailsAsync(EditEmployeeOfficialInformationDto employeeOfficialDetails);

        /// <summary>
        /// this method is used to add or edit the employee's personal details
        /// </summary>
        /// <param name="employeePersonalDetails">Employee Personal Details</param>
        Task EditEmployeePersonalDetailsAsync(EditEmployeePersonalInformationDto employeePersonalDetails);


        /// <summary>
        /// this method is used to add or edit employee education details in the system
        /// </summary>
        /// <param name="employeeEducationalDetails">Employee Educational Details</param>
        /// <returns></returns>
        Task EditEmployeeEducationalDetailsAsync(EditEmployeeEducationalInformationDto employeeEducationalDetails);


        /// <summary>
        /// this method is used to get all the official details of employee
        /// </summary>
        Task<EditEmployeeOfficialInformationDto> GetEmployeeOfficialDetails(Guid Id);


        /// <summary>
        /// this method is used to get employee personal details from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditEmployeePersonalInformationDto> GetEmployeePersonalDetails(Guid id);

        /// <summary>
        /// this method is used to get employee educational details from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditEmployeeEducationalInformationDto> GetEmployeeEducationalDetails(Guid id);


        Task<List<CsvResultDto>> AddEmployeesToDatabase(List<BulkAddEmployeesDto> employeesDto);
        /// <summary>
        /// this method is used to fetch list of gender for employees
        /// </summary>
        /// <returns>returns list of string that contains gender information</returns>
        List<string> GenderList();
        /// <summary>
        /// this method is used to fetch list of list of social handles titles for employees
        /// </summary>
        /// <returns>returns list of string that contains social handle titles</returns>
        List<string> SocialHandlesTitle();
        /// <summary>
        /// this method is used to soft delete an existing employee's data in the system
        /// </summary>
        /// <param name="getEmployeeId">all the details related to soft deleted employee</param>
        Task DeleteEmployeeAsync(Guid id);
        /// <summary>
        /// this method is used to upload the profile picture of the employee
        /// </summary>
        /// <param name="userId">guid of the employee</param>
        /// <param name="image">profile picture of the employee</param>
        /// <returns>string telling the status of the upload of the profile picture</returns>
        //Task<string> UploadProfileImageAsync(Guid userId, HttpPostedFile image);
        /// <summary>
        /// this method is used to delete the profile picture of the employee
        /// </summary>
        /// <param name="userId">guid of the employee</param>
        /// <returns>string telling the status of the deletion of the profile picture</returns>
        //Task<string> DeleteProfileImageAsync(Guid userId);

        /// <summary>
        /// this method is used to get employee name
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<string> GetEmployeeNameById(Guid employeeId);
        
        /// <summary>
        /// this method is used to get all the details of employee
        /// </summary>
        Task<EditEmployeeDto> GetEmployeeDetailsByEmail(string companyEmail);

        /// <summary>
        /// this method is used to get list of all employees details
        /// </summary>

        Task<PagedList<Employee>> GetAllEmployeeDetails(PagingParams pagingParams);

       
        IEnumerable<BulkAddEmployeesDto> GetListOfEmployees(IFormFile formFile);

        /// <summary>
        /// this method is used to fetch employeeList
        /// </summary>
        Task<List<GetEmployeeListDto>> GetEmployeeList();

        /// <summary>
        /// this method is used to search for searchString in employee's name
        /// </summary>
        Task<List<GetEmployeeListDto>> EmployeeSearch(string searchString);

        /// <summary>
        /// this method is used to search for employee for a perticular skill and designation.
        /// </summary>
        /// <param name="designationAndSkillEmployeeSearch"></param>
        /// <returns></returns>
        Task<List<GetAllEmployeeDetailsDto>> DesignationAndSkillEmployeeSearch(DesignationAndSkillEmployeeSearch designationAndSkillEmployeeSearch);

        List<BulkAddEmployeesDto> ProcessListOfEmployees(List<BulkAddEmployeesDto> resultTable);

    }
}
