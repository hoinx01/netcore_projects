using BoardGame.RewardRolling.Domains.Campaign.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Domains.Campaign.Repositories
{
    public interface ICampaignRepository
    {
        Task Save(CampaignDomain domain);
        Task<CampaignDomain> Get(string id);
    }
}
