using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Elasticsearch.Dao.Dto
{
    public abstract class EsBaseDto
    {
        public string Id { get; set; }

        public abstract void SetForEsIndex();
    }
}
