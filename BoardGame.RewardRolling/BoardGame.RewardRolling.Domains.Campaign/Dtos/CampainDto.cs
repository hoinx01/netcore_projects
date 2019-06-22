using BoardGame.RewardRolling.Core.Statics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Dtos
{
    public class CampainDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public CampaignStatus Status { get; set; }
        public List<CampaignRewardDto> Rewards { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
