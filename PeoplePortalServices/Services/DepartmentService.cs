using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using Amazon.Runtime.Internal;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// this service is used to handle all the operations related to the department
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentDesignationRepository _departmentDesignationRepository;
        private readonly IDesignationService _designationService;

        /// <summary>
        /// this is the default constructor for the department service class and used for repository and serivces dependency Injection
        /// </summary>
        /// <param name="departmentRepository">department repository object</param>
        /// <param name="departmentDesignationRepository">department designation repository object for dependency injection</param>
        public DepartmentService(IDepartmentRepository departmentRepository, IDepartmentDesignationRepository departmentDesignationRepository, IDesignationService designationService)
        {
            _departmentRepository = departmentRepository;
            _departmentDesignationRepository = departmentDesignationRepository;
            _designationService = designationService;
        }
        /// <summary>
        /// this method is used to add new department in the database
        /// </summary>
        /// <param name="departmentDetails">data of new department</param>
        /// <returns>returns newly added object</returns>
        public async Task<DepartmentUpdateDto> AddDepartmentsAsync(DepartmentCreateDto departmentDetails)
        {
            var getDepartment = await _departmentRepository.GetDepartmentByNameAsync(departmentDetails.Name);

            if(getDepartment == null)
            {
                var newDepartment = new Department
                {
                    Name = departmentDetails.Name,
                    IsDeleted = false
                };
                _departmentRepository.AddDepartments(newDepartment);
                await _departmentRepository.SaveChangesAsync();
                return new DepartmentUpdateDto { Name = newDepartment.Name, Id = newDepartment.Id};
            }
            else
            {
                if (getDepartment.IsDeleted == true)
                {
                    getDepartment.IsDeleted = false;
                    await _departmentRepository.SaveChangesAsync();
                    return new DepartmentUpdateDto { Name = getDepartment.Name, Id = getDepartment.Id };
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// this method is used to fetch list of all departments in the database
        /// </summary>
        /// <returns>returns list of departments</returns>
        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            return await _departmentRepository.GetAllDepartmentsAsync();
        }
        /// <summary>
        /// this method is used to fetch a departments in the database
        /// </summary>
        /// <returns>returns list of departments</returns>
        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await _departmentRepository.GetDepartmentByIdAsync(departmentId);
        }
        /// <summary>
        /// this method is used to fetch a departments in the database
        /// </summary>
        /// <returns>returns list of departments</returns>
        public async Task<Department> GetDepartmentByNameAsync(string departmentName)
        {
            return await _departmentRepository.GetDepartmentByNameAsync(departmentName);
        }
        /// <summary>
        /// this method is used to upfate the department
        /// </summary>
        /// <param name="department"> department details</param>
        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            var getDepartment = await _departmentRepository.GetDepartmentByIdAsync(department.Id);
            if (getDepartment == null)
            {
                return null;
            }

            var getDepartmentName = await _departmentRepository.GetDepartmentByNameAsync(department.Name);
            if (getDepartmentName != null)
            {
                return null;
                //throw new HttpRequestException(string.Format("Department already exists with name = {0}", department.Name));
            }
            getDepartment.Name = department.Name;
            await _departmentRepository.SaveChangesAsync();
            return getDepartment;
        }
        /// <summary>
        /// This method is used to delete the department
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <returns>returns department details</returns>
        public async Task<Department> DeleteDepartmentAsync(int id)
        {
            var getDepartment = await _departmentRepository.GetDepartmentDetailsAsync(id);
            if (getDepartment == null)
            {
                return null;
            }
            var getDesignationList = await _departmentDesignationRepository.GetDesignationList(id);
            if (getDesignationList != null)
            {
                foreach (var i in getDesignationList)
                {
                    await _designationService.DeleteDesignationAsync(i.DesignationId);
                }
            }
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                getDepartment.IsDeleted = true;
                await _departmentRepository.SaveChangesAsync();
                await _departmentDesignationRepository.SaveChangesAsync();
                ts.Complete();
            }
            return getDepartment;
        }
    }
}
