using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Domains.Campaign.Models;
using BoardGame.RewardRolling.Domains.Campaign.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BoardGame.RewardRolling.Domains.Campaign.Dtos
{
    public class CampaignDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public CampaignStatus Status { get; set; }
        public List<CampaignRewardDto> Rewards { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public LuckyWheel LuckyWheel { get; set; }

        public CampaignDto()
        {

        }
        public static CampaignDto GetForUpdate(CampaignDomain domain)
        {
            var dto = new CampaignDto()
            {
                Id = domain.Id,
                Name = domain.Name,
                CreatedAt = domain.CreatedAt,
                StartedAt = domain.StartedAt,
                EndedAt = domain.EndedAt,
                Status = domain.Status,
                Rewards = domain.Rewards.Select(s => CampaignRewardDto.GetForUpdate(s)).ToList(),
                LuckyWheel = (LuckyWheel) domain.LuckyWheel.GetCopy()
            };
            return dto;
        }
    }
}
