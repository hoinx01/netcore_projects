using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces
{
    public interface ICampaignQueryService
    {
        Task<CampaignFilterResultModel> Filter(CampaignFilterModel filterModel);
        Task<CampaignModel> GetByIdAsync(string id);
        Task<CampaignModel> GetCurrentCampaign();
    }
}
