using Microsoft.Extensions.Configuration;
using Nest;
using PeoplePortalDomainLayer.HelperModels.ElasticSearchModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Shared.ElasticSearch
{
    public class ElasticSearchService : IElasticSearchService
    {
        public readonly Uri _elasticSearchUri;
        public readonly string _indexName;
        private readonly ConnectionSettings _settings;
        private readonly ElasticClient _client;
        private readonly IConfiguration _configuration;

        public ElasticSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _elasticSearchUri = new Uri(_configuration.GetSection("ElasticSearch").GetSection("Uri").Value);
            _indexName = _configuration.GetSection("ElasticSearch").GetSection("IndexName").Value;
            _settings = new ConnectionSettings(_elasticSearchUri).DefaultIndex(_indexName);
            _client = new ElasticClient(_settings);
        }

        private async Task<bool> CreateIndex()
        {
            var result = await _client.Indices.CreateAsync(_indexName, c => c.Map(m => m.AutoMap()));
            if (result.Acknowledged)
                return true;
            return false;
        }

        public async Task<bool> AddUserToElasticSearch(ElasticEmployeeDetails employee)
        {
            await CreateIndex();
            var result = await _client.IndexDocumentAsync(employee);
            if (result.IsValid)
                return true;
            return false;
        }

        public async Task<ISearchResponse<ElasticEmployeeDetails>> Search(string searchString)
        {
            var searchResult = await _client.SearchAsync<ElasticEmployeeDetails>(s => s.Query(
                q => q.MultiMatch(m => m.Fields(fs => fs
                .Field(f => f.Name)
                .Field(f => f.DepartmentName)
                .Field(f => f.DesignationName)
                .Field(f => f.CompanyEmail)
                .Field(f => f.CompanyId)
                .Field(f => f.Skills)
                )
                .Query(searchString))));
            //var searchResult = await _client.SearchAsync<Employee>(s => s.Query(q => q.Match(m => m.Field(f => f.FirstName).Field(f => f.LastName) .Name(searchString)/* .Field("").Query(searchString)*/)));

            if (searchResult.IsValid)
                return searchResult;
            return null;
        }
    }
}
