using Microsoft.Extensions.Configuration;
using N5.Application;
using N5.Domain;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5.Infrastructure
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IConfiguration _configuration;
        private readonly ElasticsearchClient _client;
        private string indexName;

        public ElasticSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateInstance();
        }

        private ElasticsearchClient CreateInstance()
        {
            string host = _configuration.GetSection("ElasticSearch:Host").Value;
            string port = _configuration.GetSection("ElasticSearch:Port").Value;
            string username = _configuration.GetSection("ElasticSearch:Username").Value;
            string password = _configuration.GetSection("ElasticSearch:Password").Value;
            indexName = _configuration.GetSection("ElasticSearch:Indexname").Value;
            
            var uri = new Uri(host + ":" + port);
            var settings = new ElasticsearchClientSettings(uri);
            
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                settings.Authentication(new BasicAuthentication(username, password));
            }
            
            return new ElasticsearchClient(settings);
        }

        public async Task CheckIndex()
        {
            var existsResponse = await _client.Indices.ExistsAsync(indexName);
            if (existsResponse.Exists)
            {
                return;
            }
            
            var createResponse = await _client.Indices.CreateAsync(indexName, ci => ci
                .Mappings(m => m
                    .Properties<Permission>(p => p
                        .Keyword("Id")
                        .Text("EmployeeName")
                        .Text("EmployeeLastName")
                        .Date("PermissionDate")
                    )
                )
                .Settings(s => s
                    .NumberOfShards(3)
                    .NumberOfReplicas(1)
                )
            );
            return;
        }
        public async Task DeleteIndex()
        {
            await _client.Indices.DeleteAsync(indexName);
            return;
        }
        
        public async Task<Permission> GetDocument(string id)
        {
            var response = await _client.GetAsync<Permission>(id, idx => idx.Index(indexName));
            return response.Source;
        }

        public async Task DeleteByIdDocument(Permission permissions)
        {
            await _client.DeleteAsync<Permission>(permissions.Id.ToString(), idx => idx.Index(indexName));
            return;
        }

        public async Task<List<Permission>> GetDocuments()
        {
            var response = await _client.SearchAsync<Permission>(s => s
                .Indices(indexName)
                .From(0)
                .Size(10)
                .Query(q => q.MatchAll(new MatchAllQuery()))
            );
            return response.Documents.ToList();
        }

        public async Task InsertBulkDouments(ICollection<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                await _client.IndexAsync(permission, idx => idx.Index(indexName).Id(permission.Id.ToString()));
            }
            return;
        }

        public async Task InsertDocument(Permission permissions)
        {
            var response = await _client.IndexAsync(permissions, idx => idx.Index(indexName).Id(permissions.Id.ToString()));
            if (response.ApiCallDetails?.HttpStatusCode == 409)
            {
                await _client.IndexAsync(permissions, idx => idx.Index(indexName).Id(permissions.Id.ToString()));
            }
        }
    }
}
