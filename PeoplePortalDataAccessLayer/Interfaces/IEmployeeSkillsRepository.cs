using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// this class is used for providing contract for skills(related to an employee) repository
    /// </summary>
    public interface IEmployeeSkillsRepository : IDisposable
    {

        /// <summary>
        /// this method is used to add new Employee skill in database
        /// </summary>
        /// <param name="employeeSkill">this object contains new skill information</param>
        void AddEmployeeSkill(EmployeeSkill employeeSkill);

        /// <summary>
        /// this method is used to get list of all employee skills
        /// </summary>
        /// <param name="employeesId">employee skills corresponding to employee id</param>
        /// <returns>list of employee skills</returns>
        Task<EmployeeSkill> GetEmployeeSkills(Guid employeesId);

        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// this method is used to give primary skill of employee
        /// </summary>
        /// <param name="employeeId">Id</param>
        Task<string> PrimarySkillOfEmployee(Guid employeeId);

        /// <summary>
        /// this method is used to give secondary skill of employee
        /// </summary>
        /// <param name="employeeId">Id</param>
        Task<string> SecondarySkillofEmployee(Guid employeeId);

        /// <summary>
        /// this method is used to search the searchString in the primary and secondary skills of an employee
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        Task<List<Guid>> SearchForEmployeeSkill(string searchString);
    }
}
