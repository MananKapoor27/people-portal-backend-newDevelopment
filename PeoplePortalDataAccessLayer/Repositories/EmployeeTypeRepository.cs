using Microsoft.EntityFrameworkCore;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalDataAccessLayer.Repositories
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
        private readonly PeoplePortalDb _dbContext;
        private bool _disposed = false;

        public EmployeeTypeRepository(PeoplePortalDb peoplePortalDb)
        {
            _dbContext = peoplePortalDb;
        }

        public void AddEmployeeType(EmployeeType employeeType)
        {
            _dbContext.EmployeeType.Add(employeeType);
        }

        public async Task<EmployeeType> GetEmployeeTypeAsync(Guid id)
        {
            var result = await _dbContext.EmployeeType.SingleOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<EmployeeType> GetEmployeeTypeByNameAsync(string name)
        {
            var result = await _dbContext.EmployeeType.SingleOrDefaultAsync(c => c.EmployeeTypeName == name);
            return result;
        }

        public async Task<List<EmployeeType>> GetAllEmployeeTypeAsync()
        {
            var result = await _dbContext.EmployeeType.Where(x => x.IsDeleted == false).ToListAsync();
            return result;
        }

        public void DeleteEmployeeType(EmployeeType employeeType)
        {
            _dbContext.EmployeeType.Remove(employeeType);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
