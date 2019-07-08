using BoardGame.RewardRolling.Domains.Campaign.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Dtos
{
    public class CampaignRewardDto
    {
        [Required]
        public string Id { get; set; }
        public string RewardId { get; set; }
        public int Rate { get; set; }
        public int Ordinal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public static CampaignRewardDto GetForUpdate(CampaignRewardDomain domain)
        {
            var dto = new CampaignRewardDto()
            {
                Id = domain.Id,
                RewardId = domain.RewardId,
                Rate = domain.Rate,
                Ordinal = domain.Ordinal
            };
            return dto;
        }
    }
}
