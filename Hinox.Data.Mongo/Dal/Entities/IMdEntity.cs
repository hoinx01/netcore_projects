using System;
using Hinox.Data.Mongo.IdGenerators;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Hinox.Data.Mongo.Dal.Entities
{
    public interface IMongoEntity<T>
    {
        T Id { get; set; }
    }
    
    
    
}
