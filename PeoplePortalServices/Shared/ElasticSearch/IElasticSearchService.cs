using Nest;
using PeoplePortalDomainLayer.HelperModels.ElasticSearchModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Shared.ElasticSearch
{
    public interface IElasticSearchService
    {
        Task<bool> AddUserToElasticSearch(ElasticEmployeeDetails employee);

        Task<ISearchResponse<ElasticEmployeeDetails>> Search(string searchString);
    }
}
