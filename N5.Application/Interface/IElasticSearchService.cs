using N5.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5.Application
{
    public interface IElasticSearchService
    {
        Task CheckIndex();
        Task InsertDocument(Permission permissions);
        Task DeleteIndex();
        Task DeleteByIdDocument(Permission permissions);
        Task InsertBulkDouments(ICollection<Permission> permissions);
        Task<Permission> GetDocument(string id);
        Task<List<Permission>> GetDocuments();

    }
}
