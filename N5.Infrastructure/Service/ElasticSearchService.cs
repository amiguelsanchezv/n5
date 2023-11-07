using Microsoft.Extensions.Configuration;
using N5.Application;
using N5.Domain;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5.Infrastructure
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IConfiguration _configuration;
        private readonly IElasticClient _client;
        private string indexName;

        public ElasticSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateInstance();
        }

        private ElasticClient CreateInstance()
        {
            string host = _configuration.GetSection("ElasticSearch:Host").Value;
            string port = _configuration.GetSection("ElasticSearch:Port").Value;
            string username = _configuration.GetSection("ElasticSearch:Username").Value;
            string password = _configuration.GetSection("ElasticSearch:Password").Value;
            indexName = _configuration.GetSection("ElasticSearch:Indexname").Value;
            var settings = new ConnectionSettings(new Uri(host + ":" + port));
            settings.EnableDebugMode();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                settings.BasicAuthentication(username, password);
            }
            return new ElasticClient(settings);
        }

        public async Task CheckIndex()
        {
            var anyy = await _client.Indices.ExistsAsync(indexName);
            if (anyy.Exists)
            {
                return;
            }
            var response = await _client.Indices.CreateAsync(indexName, ci => ci
                .Index(indexName)
                .PermissionsMapping()
                .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
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
            var response = await _client.GetAsync<Permission>(id, p => p.Index(indexName));
            return response.Source;
        }

        public async Task DeleteByIdDocument(Permission permissions)
        {
            var response = await _client.CreateAsync(permissions, p => p.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.DeleteAsync(DocumentPath<Permission>.Id(permissions.Id).Index(indexName));
            }
            return;
        }

        public async Task<List<Permission>> GetDocuments()
        {
            var response = await _client.SearchAsync<Permission>(p => p
                .From(0)
                .Take(10)
                .Index(indexName)
                .MatchAll()
            );
            return response.Documents.ToList();
        }

        public async Task InsertBulkDouments(ICollection<Permission> permissions)
        {
            var response = await _client.IndexManyAsync(permissions, index: indexName);
            return;
        }

        public async Task InsertDocument(Permission permissions)
        {
            var response = await _client.CreateAsync(permissions, p => p.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<Permission>(permissions, p => p.Index(indexName).Doc(permissions));
            }
        }
    }
}
