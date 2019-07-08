using BoardGame.RewardRolling.Domains.Campaign.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Models
{
    public class CampaignRewardDomain : SubAggregateRoot<CampaignDomain>
    {
        public string Id { get; set; }
        public string RewardId { get; set; }
        public int Rate { get; set; }
        public int Ordinal { get; set; }

        public CampaignRewardDomain(CampaignRewardDto dto)
        {
            Id = dto.Id;
            RewardId = dto.RewardId;
            Rate = dto.Rate;
            Ordinal = dto.Ordinal;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
        }
        private CampaignRewardDomain()
        {

        }
        internal static CampaignRewardDomain Reconstruct(CampaignRewardDto dto)
        {
            var domain = new CampaignRewardDomain()
            {
                Id = dto.Id,
                RewardId = dto.RewardId,
                Rate = dto.Rate,
                Ordinal = dto.Ordinal,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = dto.ModifiedAt
            };
            return domain;
        }
        public void Update(CampaignRewardDto dto)
        {
            SetRewardId(dto.RewardId);
            SetRate(dto.Rate);
            SetOrdinal(dto.Ordinal);
        }
        private void SetOrdinal(int ordinal)
        {
            if (Ordinal == ordinal)
                return;
            Ordinal = ordinal;
            Modify();
        }
        private void SetRate(int rate)
        {
            if (Rate == rate)
                return;
            Rate = rate;
            Modify();
        }
        private void SetRewardId(string rewardId)
        {
            if (Equals(RewardId, rewardId))
                return;
            RewardId = rewardId;
            Modify();
        }
    }
}
