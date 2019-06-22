using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Domains.Campaign.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hinox.Mvc.Exceptions;

namespace BoardGame.RewardRolling.Domains.Campaign.Models
{
    public class CampaignDomain
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public CampaignStatus Status { get; set; }
        public List<CampaignRewardDomain> Rewards { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public bool Created { get; set; }
        public bool Modified { get; set; }

        public CampaignDomain(CampainDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            StartedAt = dto.StartedAt;
            EndedAt = dto.EndedAt;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
            Status = CampaignStatus.Active;
            Rewards = new List<CampaignRewardDomain>();
            foreach(var reward in dto.Rewards)
            {
                AddReweward(reward);
            }
        }
        public static CampaignDomain Reconstruct(CampainDto dto)
        {
            return null;
        }
        public void Update(CampainDto dto)
        {

        }
        public void AddReweward(CampaignRewardDto dto)
        {
            Rewards.Add(new CampaignRewardDomain(dto));
        }
        public void RemoveReward(string id)
        {
            Rewards.RemoveAll(r => r.Id == id);
        }
        public void UpdateReward(CampaignRewardDto dto)
        {
            var rewardDomain = Rewards.FirstOrDefault(f => f.Id == dto.Id);
            if (rewardDomain == null)
                throw new NotFoundException();
            rewardDomain.Update(dto);
        }
    }
}
