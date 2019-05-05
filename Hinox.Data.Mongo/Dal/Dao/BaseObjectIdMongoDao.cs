using Hinox.Data.Mongo.Dal.Dao.Interfaces;
using Hinox.Data.Mongo.Dal.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Dal.Dao
{
    public abstract class BaseObjectIdMongoDao<T> : BaseMongoDao<T, ObjectId>, IObjectIdMongoDao<T> where T : BaseObjectIdMongoEntity
    {
        public BaseObjectIdMongoDao(IMongoDbFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
