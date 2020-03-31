using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeSkillDto;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// this class is used to handle operations related to Employee Skills
    /// </summary>
    public interface IEmployeeSkillsService
    {
        /// <summary>
        /// this method is used to add Employee skill into database
        /// </summary>
        /// <param name="employeeSkill">new Employee skill object contains name only</param>
        /// <returns>return newly created Employee skill object</returns>
        Task<EmployeeSkillDto> AddEmployeeSkillAsync(EmployeeSkillDto employeeSkill);

        /// <summary>
        /// this method is used to update Employee skill into database
        /// </summary>
        /// <param name="employeeSkill">Employee skill object contains old id and new skill name</param>
        /// <returns>returns new Employee skill object</returns>
        Task<EmployeeSkillDto> UpdateEmployeeSkillAsync(EmployeeSkillDto employeeSkill);

        /// <summary>
        /// this method is used for deleting Employee skill into database
        /// </summary>
        /// <param name="employeeSkill">Employee skill object contains old id </param>
        /// <returns>returns new Employee skill object</returns>
        Task DeleteEmployeeSkillAsync(Guid employeeId);
    }
}
