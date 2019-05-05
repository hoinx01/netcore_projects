using Hinox.Data.Mongo.Dal.Dao.Interfaces;
using Hinox.Data.Mongo.Dal.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Dal.Dao
{
    public abstract class BaseLongIdMongoDao<T> : BaseMongoDao<T, long>, ILongIdMongoDao<T> where T : BaseLongIdMongoEntity
    {
        public BaseLongIdMongoDao(IMongoDbFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
