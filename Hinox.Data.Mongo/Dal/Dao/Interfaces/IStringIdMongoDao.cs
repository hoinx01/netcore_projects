using Hinox.Data.Mongo.Dal.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Dal.Dao.Interfaces
{
    public interface IStringIdMongoDao<T> : IMongoDao<T, string> where T : BaseStringIdMongoEntity
    {

    }
}
