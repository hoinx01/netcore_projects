using Hinox.Data.Mongo.IdGenerators;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Dal.Entities
{
    public abstract class BaseLongIdMongoEntity : IMongoEntity<long>
    {
        [BsonId(IdGenerator = typeof(LongObjectIdGenerator))]
        public virtual long Id { get; set; }
    }
}
