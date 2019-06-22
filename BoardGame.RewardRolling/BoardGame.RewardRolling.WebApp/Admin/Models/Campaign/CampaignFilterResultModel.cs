using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Campaign
{
    public class CampaignFilterResultModel : BasePagingFilterResponse
    {
        public List<CampaignModel> Campaigns { get; set; }
    }
}
