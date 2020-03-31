using PeoplePortalDomainLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalDataAccessLayer.Interfaces
{
    public interface IDesignationLevelRepository
    {
        Task<List<int>> ListOfLevelsAssociatedWithListOfDepartmentDesignationId(List<int> listOfDepartmentDesignationId);
    }
}
