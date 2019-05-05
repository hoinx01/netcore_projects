using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Dal.Entities
{
    public abstract class BaseStringIdMongoEntity : IMongoEntity<string>
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public virtual string Id { get; set; }
    }
}
