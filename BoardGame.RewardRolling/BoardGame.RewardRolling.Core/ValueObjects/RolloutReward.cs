using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.ValueObjects
{
    public class RolloutReward
    {
        public string RollingCodeId { get; set; }
        public string RollingCodeSerial { get; set; }
        public string CampaignId { get; set; }
        public string RewardId { get; set; }
        public int RewardOrdinal { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
