using BoardGame.RewardRolling.Domains.Campaign.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Models
{
    public class CampaignRewardDomain
    {
        public string Id { get; set; }
        public string RewardId { get; set; }
        public int Rate { get; set; }
        public int Ordinal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public CampaignRewardDomain(CampaignRewardDto dto)
        {
            Id = dto.Id;
            RewardId = dto.RewardId;
            Rate = dto.Rate;
            Ordinal = dto.Ordinal;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
        }
        public void Update(CampaignRewardDto dto)
        {

        }
    }
}
