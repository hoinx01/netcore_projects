using System;
using System.Collections.Generic;
using System.Text;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Dal.Dao.Interfaces;

namespace BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces
{
    public interface IMdNhanhCityDao : IStringIdMongoDao<MdNhanhCity>
    {
    }
}
