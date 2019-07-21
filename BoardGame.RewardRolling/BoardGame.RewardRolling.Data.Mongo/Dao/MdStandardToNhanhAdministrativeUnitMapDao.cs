using System;
using System.Collections.Generic;
using System.Text;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;

namespace BoardGame.RewardRolling.Data.Mongo.Dao
{
    public class MdStandardToNhanhAdministrativeUnitMapDao : BaseObjectIdMongoDao<MdStandardToNhanhAdministrativeUnitMap>, IMdStandardToNhanhAdministrativeUnitMapDao
    {
        public MdStandardToNhanhAdministrativeUnitMapDao(
            IMongoDbFactory dbFactory
        ) : base(dbFactory)
        {

        }
    }
}
