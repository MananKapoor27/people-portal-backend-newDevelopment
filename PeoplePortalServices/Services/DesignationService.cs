using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDomainLayer.Entities.Dto.DesignationDto;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;

namespace PeoplePortalServices.Services
{
    /// <summary>
    /// this interface is used to provide contract for designation service
    /// </summary>
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IDepartmentDesignationRepository _departmentDesignationRepository;
        /// <summary>
        /// this is the default constructor for the designation service class and used for repository and serivces dependency injection
        /// </summary>
        /// <param name="designationRepository">department repository object for dependency injection</param>
        /// <param name="departmentDesignationRepository">departmentdesignation repository object for dependency injection</param>
        public DesignationService(IDesignationRepository designationRepository, IDepartmentDesignationRepository departmentDesignationRepository)
        {
            _designationRepository = designationRepository;
            _departmentDesignationRepository = departmentDesignationRepository;
        }
        /// <summary>
        /// this method is used to delete the designation
        /// </summary>
        /// <param name="designationId">designation id</param>
        /// <returns>string statting the status of the method</returns>
        public async Task<string> DeleteDesignationAsync(int designationId)
        {
            var validDesg = await _designationRepository.GetDesignationDetailsAsync(designationId);
            if (validDesg == null)
            {
                throw new Exception("NotFound.Requested designation does not exist");
            }
            validDesg.IsDeleted = true;
            await _designationRepository.SaveChangesAsync();
            var validDeptDesig = await _departmentDesignationRepository.GetDepartmentDesignationDetailsAsync(designationId);
            validDeptDesig.IsDeleted = true;
            await _departmentDesignationRepository.SaveChangesAsync();
            return "Designation removed successfully";
        }

        /// <summary>
        /// this method is used to get the list of all designation
        /// </summary>
        /// <returns></returns>
        public async Task<List<DesignationListDto>> GetDesignationList()
        {
            var designations = await _designationRepository.GetDesignationList();
            var designationList = new List<DesignationListDto>();
            foreach(var d in designations)
            {
                var designation = new DesignationListDto
                {
                    Id = d.Id,
                    Title = d.Title
                };
                designationList.Add(designation);
            }

            return designationList;
        }
    }
}
