using PeoplePortalDomainLayer.Entities.Dto.DesignationDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Interfaces
{
    /// <summary>
    /// this interface is used to provide contract for designation service
    /// </summary>
    public interface IDesignationService 
    {
        /// <summary>
        /// this method is used to delete the designation
        /// </summary>
        /// <param name="designationId">designation id</param>
        /// <returns>string statting the status of the method</returns>
        Task<string> DeleteDesignationAsync(int designationId);

        /// <summary>
        /// this method is used to get the list of all designation
        /// </summary>
        /// <returns></returns>
        Task<List<DesignationListDto>> GetDesignationList();
    }
}
