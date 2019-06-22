using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces
{
    public interface ICampaignCommandService
    {
        Task<CampaignModel> Add(AddCampaignModel model);
    }
}
