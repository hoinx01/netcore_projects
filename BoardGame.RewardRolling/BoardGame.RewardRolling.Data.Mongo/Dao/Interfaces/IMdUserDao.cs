using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Dal.Dao.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces
{
    public interface IMdUserDao : IObjectIdMongoDao<MdUser>
    {
        Task<MdUser> GetByUserNameAsync(string userName);
    }
}
