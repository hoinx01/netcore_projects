using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Configurations
{
    public class MongoConnection
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public bool Ssl { get; set; }
    }
}
