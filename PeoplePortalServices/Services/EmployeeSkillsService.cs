using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeSkillDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// this class is used to handle operations related to Skills
    /// </summary>
    public class EmployeeSkillsService : IEmployeeSkillsService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeSkillsRepository _employeeSkillsRepositories;
        /// <summary>
        /// this is the default constructor for the Employee Skill service
        /// </summary>
        /// <param name="employeeSkillsRepositories">Employee skillRepository interface object for dependency injection</param>
        /// <param name="employeeRepository"> Employee Repository interface object for dependency injection</param>
        /// <param name="skillRepository"> Skill Repository interface object for dependency injection</param>
        public EmployeeSkillsService(IEmployeeSkillsRepository employeeSkillsRepositories, IEmployeeRepository employeeRepository, ISkillRepository skillRepository, IMapper mapper)
        {
            _employeeSkillsRepositories = employeeSkillsRepositories;
            _employeeRepository = employeeRepository;
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// this method is used to fetch all the Features
        /// </summary>
        /// <returns>returns list of all the feature</returns>
        public async Task<EmployeeSkillDto> GetAllEmployeeSkill(Guid employeesId)
        {
            var employeeSkill = await _employeeSkillsRepositories.GetEmployeeSkills(employeesId);
            //return _mapper.Map<EmployeeSkillDto>(result);
            var result = new EmployeeSkillDto
            {
                EmployeeId = employeeSkill.EmployeeId,
                PrimarySkills = JsonConvert.DeserializeObject<List<string>>(employeeSkill.PrimarySkills),
                SecondarySkills = JsonConvert.DeserializeObject<List<string>>(employeeSkill.SecondarySkills),
            };
            return result;
        }
        /// <summary>
        /// this method is used to add skill into database
        /// </summary>
        /// <param name="employeeSkill">new skill object contains name only</param>
        /// <returns>return newly created skill object</returns>
        public async Task<EmployeeSkillDto> AddEmployeeSkillAsync(EmployeeSkillDto employeeSkill)
        {
            //check if employee present or not
            var existingEmp = await _employeeRepository.GetEmployeeByIdAsync(employeeSkill.EmployeeId);
            if (existingEmp == null)
            {
                throw new Exception("Not Found.Employee does not exist");
            }
            //var newEmployeeSkill = _mapper.Map<EmployeeSkill>(employeeSkill);
            var newEmployeeSkill = new EmployeeSkill
            {
                EmployeeId = employeeSkill.EmployeeId,
                PrimarySkills = JsonConvert.SerializeObject(employeeSkill.PrimarySkills),
                SecondarySkills = JsonConvert.SerializeObject(employeeSkill.SecondarySkills),
                IsDeleted = false,                
            };
            _employeeSkillsRepositories.AddEmployeeSkill(newEmployeeSkill);
            await _employeeSkillsRepositories.SaveChangesAsync();
            return employeeSkill;
        }
        /// <summary>
        /// this method is used for updating Employee Skill into database
        /// </summary>
        /// <param name="employeeSkill">skill object contains id </param>
        /// <returns>updates skill object</returns>
        public async Task<EmployeeSkillDto> UpdateEmployeeSkillAsync(EmployeeSkillDto employeeSkill)
        {
            var sameEmployeeSkills = await _employeeSkillsRepositories.GetEmployeeSkills(employeeSkill.EmployeeId);
            if (sameEmployeeSkills == null)
            {
                throw new Exception("Bad Request.Employee does not have this skill" );
            }

            sameEmployeeSkills.PrimarySkills = JsonConvert.SerializeObject(employeeSkill.PrimarySkills);
            sameEmployeeSkills.SecondarySkills = JsonConvert.SerializeObject(employeeSkill.SecondarySkills);
            await _employeeSkillsRepositories.SaveChangesAsync();
            return employeeSkill;
        }
        /// <summary>
        /// this method is used to Delete skill into database
        /// </summary>
        /// <param name="employeeSkill">new skill object contains name only</param>
        /// <returns>return newly created skill object</returns>
        public async Task DeleteEmployeeSkillAsync(Guid employeeId)
        {
            var sameEmployeeSkills = await _employeeSkillsRepositories.GetEmployeeSkills(employeeId);
            if (sameEmployeeSkills == null)
            {
                throw new Exception("Bad Request.Employee does not have this skill");
            }
            sameEmployeeSkills.IsDeleted = true;
            await _employeeSkillsRepositories.SaveChangesAsync();
        }
    }
}
