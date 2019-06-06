using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Dao
{
    public class MdRewardDao : BaseObjectIdMongoDao<MdReward>, IMdRewardDao
    {
        public MdRewardDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
