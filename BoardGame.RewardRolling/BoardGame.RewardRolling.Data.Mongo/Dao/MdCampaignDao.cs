using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace BoardGame.RewardRolling.Data.Mongo.Dao
{
    public class MdCampaignDao : BaseObjectIdMongoDao<MdCampaign>, IMdCampaignDao
    {
        public MdCampaignDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<MdCampaign> GetCurrentCampaignAsync()
        {
            var filterDefinition = Builders<MdCampaign>.Filter.And(
                Builders<MdCampaign>.Filter.Gte("StartedAt", DateTime.UtcNow),
                Builders<MdCampaign>.Filter.Lte("EndedAt", DateTime.UtcNow)
            );
            var filterResult = await (await Collection.FindAsync(filterDefinition)).ToListAsync();

            return filterResult.FirstOrDefault();
        }
    }
}
