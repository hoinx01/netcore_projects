using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Dal.Entities
{
    public abstract class BaseObjectIdMongoEntity : IMongoEntity<ObjectId>
    {
        [BsonId]
        public virtual ObjectId Id { get; set; }
    }
}
