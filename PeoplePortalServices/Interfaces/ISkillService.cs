using PeoplePortalDomainLayer.Entities.DTO.SkillDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// this class is used to handle operations related to Skills
    /// </summary>
    public interface ISkillService
    {
        /// <summary>
        /// this method is used to add skill into database
        /// </summary>
        /// <param name="skill">new skill object contains name only</param>
        /// <returns>return newly created skill object</returns>
        Task<Skill> AddSkillAsync(AddSkillDto skill);
        /// <summary>
        /// this methid is used to update into database
        /// </summary>
        /// <param name="skill">skill object contains old id and new skill name</param>
        /// <returns>returns new skill object</returns>
        Task<Skill> UpdateSkillAsync(Skill skill);
        /// <summary>
        /// this method is used to delete skill into database
        /// </summary>
        /// <param name="skill"> skill object contains name only</param>
        Task<Skill> DeleteSkillAsync(int skillId);
        /// <summary>
        /// this method is used to get skill from database
        /// </summary>
        /// <returns>return all skills from database</returns>
        Task<IEnumerable<Skill>> ShowSkillAsync();
    }
}
