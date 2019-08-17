using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;

namespace BoardGame.RewardRolling.Data.Mongo.Dao
{
    public class MdUserDao : BaseObjectIdMongoDao<MdUser>, IMdUserDao
    {
        public MdUserDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<MdUser> GetByUserNameAsync(string userName)
        {
            var filterDefinitionBuilder = Builders<MdUser>.Filter.Eq("UserName", userName);
            var filterResult = await(await Collection.FindAsync(filterDefinitionBuilder)).ToListAsync();

            return filterResult.FirstOrDefault();
        }
    }
}
