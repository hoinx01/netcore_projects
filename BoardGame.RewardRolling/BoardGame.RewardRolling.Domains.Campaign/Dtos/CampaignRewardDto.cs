using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Dtos
{
    public class CampaignRewardDto
    {
        public string Id { get; set; }
        public string RewardId { get; set; }
        public int Rate { get; set; }
        public int Ordinal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
