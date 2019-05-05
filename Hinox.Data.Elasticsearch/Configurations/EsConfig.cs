using System;
using System.Collections.Generic;

namespace Hinox.Data.Elasticsearch.Configurations
{
    public class EsConfig
    {
        public string Address { get; set; }
        public Dictionary<string, MappingConfig> Mappings { get; set; }
    }

}
