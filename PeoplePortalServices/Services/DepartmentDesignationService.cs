using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Shared.Helpers;
using AutoMapper;
using PeoplePortalDomainLayer.Entities.Dto.DesignationDto;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// this service is used to handle all the operations related to mapping of department and designations 
    /// </summary>
    public class DepartmentDesignationService : IDepartmentDesignationService
    {
        private readonly IDepartmentDesignationRepository _departmentDesignationRepository;
        private readonly IDesignationRepository _designationRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// this is the default constructor for the DepartmentDesignation service class
        /// </summary>
        /// <param name="departmentDesignationRepository">departmentDesignation repository object for dependency injection</param>
        /// <param name="designationRepository">designation repository object for dependency injection</param>
        public DepartmentDesignationService(IDepartmentDesignationRepository departmentDesignationRepository, IDesignationRepository designationRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentDesignationRepository = departmentDesignationRepository;
            _designationRepository = designationRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// this method is used to fetch all designations of a department
        /// </summary>
        /// <param name="departmentId">integer to identity department</param>
        /// <returns>returns list of designation of that department</returns>
        public async Task<List<DesignationListDto>> GetAllDesignationAsync(int departmentId)
        {
            var designationIdsList = await _departmentDesignationRepository.GetAllDesignationsAsync(departmentId);
            if (designationIdsList.Count == 0)
            {
                return null;
                throw new Exception("No designation department mapping found");
            }
            var designationList = await _designationRepository.GetDesignation(designationIdsList);
            var designationListDto = new List<DesignationListDto>();
            foreach (var d in designationList)
            {
                var designation = new DesignationListDto
                {
                    Id = d.Id,
                    Title = d.Title
                };
                designationListDto.Add(designation);
            }

            return designationListDto;
        }
        /// <summary>
        /// this method is used to add designations to the department
        /// </summary>
        /// <param name="departmentId">department id</param>
        /// <param name="designations">list of designations</param>
        /// <returns>string stating the status of the method</returns>
        public async Task<string> AddDesignationToDepartmentAsync(int departmentId, List<string> designations)
        {
            if (designations == null)
            {
                throw new Exception("Not Found. No designations added");
            }
            var validDeptartment = await _designationRepository.GetDepartementDetailsAsync(departmentId);
            if (validDeptartment == null)
            {
                throw new Exception("NotFound.Requested department does not exist");
            }
            var designationList = new List<Designation>();
            foreach (var item in designations)
            {
                designationList.Add(new Designation
                {
                    Title = item,
                    IsDeleted = false
                });
            }
            _designationRepository.AddDesignations(designationList);
            await _designationRepository.SaveChangesAsync();
            var designationIds = _designationRepository.GetDesignationIds(designations);
            var departmentDesignationList = new List<DepartmentDesignation>();
            foreach (var item in designationIds)
            {
                departmentDesignationList.Add(new DepartmentDesignation
                {
                    DepartmentId = departmentId,
                    DesignationId = item,
                    IsDeleted = false
                });
            }
            _departmentDesignationRepository.AddDepartmentDesignation(departmentDesignationList);
            await _departmentDesignationRepository.SaveChangesAsync();
            return "Designations added successfully in the department";
        }

        public async Task<Department> GetDepartment(int departmentDesignationId)
        {
            var departmentDetails = await _departmentDesignationRepository.GetDepartmentByDepartmentDesignation(departmentDesignationId);
            var department = await _departmentRepository.GetDepartmentDetailsAsync(departmentDetails);
            return department;
        }

        public async Task<Designation> GetDesignation(int departmentDesignationId)
        {
            var designationDetails = await _departmentDesignationRepository.GetDesignationByDepartmentDesignation(departmentDesignationId);
            var designation = await _designationRepository.GetDesignationDetailsAsync(designationDetails);
            return designation;
        }

        public async Task<int> GetDepartmentDesignationId(int departmentId, int designationId)
        {
            var departmentDesignation = await _departmentDesignationRepository.GetDepartmentDesignationAsync(departmentId, designationId);
            return departmentDesignation.Id;
        }

        /// <summary>
        /// this method is used to get the designation title in list
        /// </summary>
        /// <param name="Id">departmentdesignation Id</param>
        public async Task<string> GetDesignationByDepartmentDesignationId(int id)
        {
            var result = await _departmentDesignationRepository.GetDesignationByDepartmentDesignationId(id);
            return result;
        }
    }
}
