using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    /// <summary>
    /// this class is used to do all skills related database operations
    /// </summary>
    public interface ISkillRepository : IDisposable
    {
        /// <summary>
        /// this method is used to add new skill in database
        /// </summary>
        /// <param name="skills">this object contains new skill information</param>
        void AddSkill(Skill skills);
        /// <summary>
        /// this method is used to get skill from database
        /// </summary>
        /// <param name="id">skill id </param>
        Task<Skill> GetSkillAsync(int id);
        /// <summary>
        /// this method is used to get all skills from database
        /// </summary> 
        Task<Skill> GetSkillByNameAsync(string name);
        /// <summary>
        /// this method is used to get skill from database
        /// </summary>
        /// <param name="name">skill name </param>
        Task<IEnumerable<Skill>> GetAllSkillAsync();
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// this method is used to get the skill of employee
        /// </summary>
        /// <param name="skillId">skill id</param>
        Task<List<Skill>> GetSkillInfoOfEmployeeBySkillId(List<int> skillId);
        /// <summary>
        /// this method is used to deletes the skill in database
        /// </summary>
        /// <param name="skills">this object contains skill information which is to be deleted</param>
        void DeleteSkill(Skill skills);
    }
}
