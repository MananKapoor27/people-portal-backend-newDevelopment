using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.DTO.SkillDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalServices.Interfaces;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// this class is used to handle operations related to Skills
    /// </summary>
    public class SkillsService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        /// <summary>
        /// this is the default constructor for the Skill service
        /// </summary>
        /// <param name="skillRepository">skillRepository interface object for dependency injection</param>
        public SkillsService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        /// <summary>
        /// this method is used to add skill into database
        /// </summary>
        /// <param name="skill">new skill object continas name only</param>
        /// <returns>return newly created skill object</returns>
        public async Task<Skill> AddSkillAsync(AddSkillDto skill)
        {
            var existingSkill = await _skillRepository.GetSkillByNameAsync(skill.Name);
            if (existingSkill != null)
            {
                throw new Exception("This skill Already Exists");
            }
            else
            {
                var aSkill = new Skill
                {
                    Name = skill.Name
                };
                _skillRepository.AddSkill(aSkill);
                await _skillRepository.SaveChangesAsync();
                return aSkill;
            }
        }
        /// <summary>
        /// this method is used to get skill from database
        /// </summary>
        /// <returns>return all skills from database</returns>
        public async Task<IEnumerable<Skill>> ShowSkillAsync()
        {
            var result = await _skillRepository.GetAllSkillAsync();

            return result;
        }
        /// <summary>
        /// this methid is used to update into database
        /// </summary>
        /// <param name="skill">skill object contains old id and new skill name</param>
        /// <returns>returns new skill object</returns>
        public async Task<Skill> UpdateSkillAsync(Skill skill)
        {
            var result = await _skillRepository.GetSkillAsync(skill.Id);
            result.Name = skill.Name;
            await _skillRepository.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// this method is used to add skill into database
        /// </summary>
        /// <param name="skill">new skill object continas name only</param>
        /// <returns>return newly created skill object</returns>
        public async Task<Skill> DeleteSkillAsync(int skillId)
        {
            var existingSkill = await _skillRepository.GetSkillAsync(skillId);
            if (existingSkill == null)
            {
                throw new Exception("This skill dosen't Exists");
            }
            else
            {              
                _skillRepository.DeleteSkill(existingSkill);
               await _skillRepository.SaveChangesAsync();              
            }
            return existingSkill;
        }
    }
}
