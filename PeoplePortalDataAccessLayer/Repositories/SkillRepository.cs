using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// this class is used to do all skills related database operations
    /// </summary>
    public class SkillRepository : ISkillRepository
    {
        private readonly PeoplePortalDb _dbContext;
        private bool _disposed = false;

        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public SkillRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }
        /// <summary>
        /// this method is used to add new skill in database
        /// </summary>
        /// <param name="skills">this object contains new skill information</param>
        public void AddSkill(Skill skills)
        {
            _dbContext.Skills.Add(skills);
        }

        /// <summary>
        /// this method is used to get skill from database
        /// </summary>
        /// <param name="id">skill id </param>
        public async Task<Skill> GetSkillAsync(int id)
        {
            var result = await _dbContext.Skills.SingleOrDefaultAsync(c => c.Id == id);
            return result;
        }

        /// <summary>
        /// this method is used to get skill from database
        /// </summary>
        /// <param name="name">skill name </param>
        public async Task<Skill> GetSkillByNameAsync(string name)
        {
            var result = await _dbContext.Skills.SingleOrDefaultAsync(c => c.Name == name);
            return result;
        }
        /// <summary>
        /// this method is used to get the skill of employee
        /// </summary>
        /// <param name="skillId">skill id</param>
        public async Task<List<Skill>> GetSkillInfoOfEmployeeBySkillId(List<int> skillId)
        {
            var requiredSkill = await _dbContext.Skills.Where(c => skillId.Contains(c.Id)).ToListAsync();
            return requiredSkill;
        }
        /// <summary>
        /// this method is used to get all skills from database
        /// </summary>   
        public async Task<IEnumerable<Skill>> GetAllSkillAsync()
        {
            var result = await _dbContext.Skills.ToListAsync();
            return result;
        }
        /// <summary>
        /// this method is used to deletes the skill in database
        /// </summary>
        /// <param name="skills">this object contains skill information which is to be deleted</param>
        public void DeleteSkill(Skill skills)
        {
            _dbContext.Skills.Remove(skills);
        }
        /// <summary>
        /// this method is used to save the changes done in database
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// this function is used to implement dispose pattern callable by consumers
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// this function is used to free all managed objects.
        /// </summary>
        /// <param name="disposing"> bool value which tells whether to dispose or not </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }
    }
}
