using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.Repositories
{
    /// <summary>
    /// This class is used to interact with the database for login related queries
    /// </summary>
    public class LoginRepository : ILoginRepository
    {
        private readonly PeoplePortalDb _dbContext;

        /// <summary>
        /// this constructor is used to initialize database object
        /// </summary>
        public LoginRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        public async Task<Login> checkUser(string companyEmail)
        {
            var emailExists = await _dbContext.Login.FirstOrDefaultAsync(email =>
               email.CompanyEmail == companyEmail && email.IsDeleted == false);

            return emailExists;
        }

        public async Task<bool> UpdatePassword(Login userDetails,string newPassword)
        {
            try
            {
                userDetails.Password = newPassword;
                await SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
           
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}

