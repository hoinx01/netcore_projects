using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace Hinox.Data.Mongo
{
    public interface IMongoDbFactory
    {
        IMongoDatabase GetMongoDatabase<T>();
        string GetCollectionName<T>();
    }
}
