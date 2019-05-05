using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Elasticsearch.Net;
using System.Linq;
using Hinox.Data.Elasticsearch.Configurations;
using Hinox.Data.Elasticsearch.Dao.Dto;

namespace Hinox.Data.Elasticsearch.Dal.Dao
{
    public abstract class EsBaseDao<T> : IEsDao<T> where T : EsBaseDto
    {
        protected ElasticClient client;
        protected string indexName;
        protected string typeName;
        public EsBaseDao(EsClient esClient, EsConfig esConfig)
        {
            client = esClient;
            indexName = esConfig.Mappings[typeof(T).Name].IndexName;
            typeName = esConfig.Mappings[typeof(T).Name].TypeName;

            //Kiểm tra index có tồn tại không, nếu không thì tạo mới
            if (!ExistIndex())
                CreateIndex();

            //Kiểm tra setting
            var currentSettings = GetIndexSetting();
            bool mustUpdateSettings = MustUpdateSettings(currentSettings);
            if (mustUpdateSettings)
                UpdateSettings();
        }

        private bool ExistIndex()
        {
            var indexExistsRequest = new IndexExistsRequest(indexName);
            var indexExistsResponse = client.IndexExists(indexExistsRequest);
            if (indexExistsResponse.Exists)
                return true;
            return false;
        }
        private bool CreateIndex()
        {
            var createIndexRequest = new CreateIndexRequest(indexName);
            var createIndexResponse = client.CreateIndex(createIndexRequest);
            if (createIndexResponse.Acknowledged)
                return true;
            else
            {
                throw new Exception("Có lỗi xảy ra khi tạo index" + indexName);
            }
        }
        private IIndexSettings GetIndexSetting()
        {
            var getIndexRequest = new GetIndexRequest(indexName);
            var getIndexResponse = client.GetIndex(getIndexRequest);
            var indexState = getIndexResponse.Indices[indexName];
            var indexSettings = indexState.Settings;
            return indexSettings;
        }
        private bool MustUpdateSettings(IIndexSettings currentSettings)
        {
            return false;
        }
        private void UpdateSettings()
        {

        }

        public string GetIndexName()
        {
            return indexName;
        }
        public string GetTypeName()
        {
            return typeName;
        }
        public ElasticClient GetClient()
        {
            return client;
        }

        public async Task IndexAsync(T document)
        {
            document.SetForEsIndex();
            var indexRequest = new IndexRequest<T>(indexName, typeName, document.Id)
            {
                Document = document
            };
            var indexResonse = await client.IndexAsync(indexRequest);
        }

        public async Task BulkIndexAsync(List<T> documents)
        {
            if (!documents.Any())
                return;
            foreach (var document in documents)
                document.SetForEsIndex();

            var bulkOperations = new List<IBulkOperation>();
            foreach (var document in documents)
                bulkOperations.Add(new BulkIndexOperation<T>(document));

            var bulkRequest = new BulkRequest(indexName, typeName)
            {
                Refresh = Refresh.True,
                Operations = bulkOperations
            };

            var bulkResponse = await client.BulkAsync(bulkRequest);
            if (!bulkResponse.IsValid)
                throw bulkResponse.OriginalException;

        }

        public async Task<T> GetById(string id)
        {
            var getRequest = new GetRequest<T>(indexName, typeName, id);
            var getResponse = await client.GetAsync<T>(getRequest);
            return getResponse.Source;
        }
    }
}
