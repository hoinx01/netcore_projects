using Hinox.Data.Mongo.Dal.Dao.Interfaces;
using Hinox.Data.Mongo.Dal.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Dal.Dao
{
    public abstract class BaseStringIdMongoDao<T> : BaseMongoDao<T, string>, IStringIdMongoDao<T> where T : BaseStringIdMongoEntity
    {
        public BaseStringIdMongoDao(IMongoDbFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
