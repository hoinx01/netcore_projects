using System;
using System.Collections.Generic;
using System.Text;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;

namespace BoardGame.RewardRolling.Data.Mongo.Dao
{
    public class MdCustomerDao : BaseObjectIdMongoDao<MdCustomer>, IMdCustomerDao
    {
        public MdCustomerDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
