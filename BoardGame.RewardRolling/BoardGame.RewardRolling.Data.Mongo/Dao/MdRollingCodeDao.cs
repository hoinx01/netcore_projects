using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BoardGame.RewardRolling.Data.Mongo.Dao
{
    public class MdRollingCodeDao : BaseObjectIdMongoDao<MdRollingCode>, IMdRollingCodeDao
    {
        public MdRollingCodeDao(
            IMongoDbFactory dbFactory
            ) : base(dbFactory)
        {

        }

        public async Task<MdRollingCode> GetBySerialAsync(string serial)
        {
            var filter = Builders<MdRollingCode>.Filter.Eq("Serial", serial);
            var rollingCode = (await Collection.FindAsync(filter)).FirstOrDefault();
            return rollingCode;
        }
    }
}
