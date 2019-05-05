using System;
using System.Collections.Generic;
using System.Text;
using Hinox.Data.Elasticsearch.Configurations;
using Nest;

namespace Hinox.Data.Elasticsearch
{
    public class EsClient : ElasticClient
    {
        public EsClient(EsConfig config) : base(new ConnectionSettings(new Uri(config.Address)))
        {

        }
    }

}
