using AutoMapper;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeTypeDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Services
{
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// this is the default constructor for the Skill service
        /// </summary>
        /// <param name="skillRepository">skillRepository interface object for dependency injection</param>
        public EmployeeTypeService(IEmployeeTypeRepository employeeTypeRepository, IMapper mapper)
        {
            _employeeTypeRepository = employeeTypeRepository;
            _mapper = mapper;
        }

        public async Task<string> AddEmployeeTypeAsync(string employeeType)
        {
            var existingEmployeeType = await _employeeTypeRepository.GetEmployeeTypeByNameAsync(employeeType);
            if (existingEmployeeType != null)
            {
                if(existingEmployeeType.IsDeleted == false)
                    throw new Exception("This Employee Type Already Exists");
                else
                {
                    existingEmployeeType.IsDeleted = false;
                    await _employeeTypeRepository.SaveChangesAsync();
                    return employeeType;
                }
            }
            else
            {
                var newEmployeeType = new EmployeeType
                {
                    EmployeeTypeName = employeeType,
                    IsDeleted = false
                };
                _employeeTypeRepository.AddEmployeeType(newEmployeeType);
                await _employeeTypeRepository.SaveChangesAsync();
                return employeeType;
            }
        }

        public async Task<List<EmployeeTypeDto>> GetAllEmployeeTypeAsync()
        {
            var employeeTypes = await _employeeTypeRepository.GetAllEmployeeTypeAsync();
            var result = new List<EmployeeTypeDto>();
            foreach(var employeeType in employeeTypes)
            {
                var empType = _mapper.Map<EmployeeTypeDto>(employeeType);
                result.Add(empType);
            }
            return result;
        }
        
        public async Task<EmployeeTypeDto> UpdateEmployeeTypeAsync(EmployeeTypeDto employeeType)
        {
            var result = await _employeeTypeRepository.GetEmployeeTypeAsync(employeeType.Id);
            result.EmployeeTypeName = employeeType.EmployeeTypeName;
            await _employeeTypeRepository.SaveChangesAsync();
            return employeeType;
        }

        public async Task<EmployeeTypeDto> DeleteEmployeeTypeAsync(Guid id)
        {
            var existingEmployeeType = await _employeeTypeRepository.GetEmployeeTypeAsync(id);
            if (existingEmployeeType == null)
            {
                throw new Exception("This Employee Type dosen't Exists");
            }
            else
            {
                existingEmployeeType.IsDeleted = true;
                await _employeeTypeRepository.SaveChangesAsync();
            }
            var result = _mapper.Map<EmployeeTypeDto>(existingEmployeeType);
            return result;
        }
    }
}
