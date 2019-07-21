using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Models.RollingCode
{
    public class RollResultModel
    {
        public string CampaignId { get; set; }      
        public string RewardId { get; set; }      
        public int RewardOrdinal { get; set; }
        public RollRewardModel Reward { get; set; }
    }

    public class RollRewardModel
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string ImageSrc { get; set; }
    }
}
