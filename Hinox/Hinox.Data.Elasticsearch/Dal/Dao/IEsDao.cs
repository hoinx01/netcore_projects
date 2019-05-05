using Hinox.Data.Elasticsearch.Dao.Dto;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hinox.Data.Elasticsearch.Dal.Dao
{
    public interface IEsDao<T> where T : EsBaseDto
    {
        string GetIndexName();
        string GetTypeName();
        ElasticClient GetClient();
        Task IndexAsync(T document);
        Task BulkIndexAsync(List<T> documents);
        Task<T> GetById(string id);
    }
}
