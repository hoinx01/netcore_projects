using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Dal.Dao.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces
{
    public interface IMdCampaignDao : IObjectIdMongoDao<MdCampaign>
    {
        Task<MdCampaign> GetCurrentCampaignAsync();
    }
}
