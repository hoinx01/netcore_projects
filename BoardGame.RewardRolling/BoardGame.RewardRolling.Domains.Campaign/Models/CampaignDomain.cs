using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Domains.Campaign.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hinox.Mvc.Exceptions;
using BoardGame.RewardRolling.Domains.Campaign.ValueObjects;
using BoardGame.RewardRolling.Domains.Campaign.Events;

namespace BoardGame.RewardRolling.Domains.Campaign.Models
{
    public class CampaignDomain : AggregateRoot
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public CampaignStatus Status { get; set; }
        public List<CampaignRewardDomain> Rewards { get; set; }
        public LuckyWheel LuckyWheel { get; set; }

        public CampaignDomain(CampaignDto dto)
        {
            Created = true;
            Id = dto.Id;
            Name = dto.Name;
            StartedAt = dto.StartedAt;
            EndedAt = dto.EndedAt;
            LuckyWheel = dto.LuckyWheel;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
            Status = CampaignStatus.Active;
            Rewards = new List<CampaignRewardDomain>();
            LuckyWheel = dto.LuckyWheel;
            SetRewards(dto.Rewards);
        }
        
        private CampaignDomain()
        {

        }

        public static CampaignDomain Reconstruct(CampaignDto dto)
        {
            var domain = new CampaignDomain()
            {
                Id = dto.Id,
                Name = dto.Name,
                StartedAt = dto.StartedAt,
                EndedAt = dto.EndedAt,
                LuckyWheel = (LuckyWheel)dto.LuckyWheel.GetCopy(),
                CreatedAt = dto.CreatedAt,
                ModifiedAt = dto.ModifiedAt,
                Status = dto.Status,
                Rewards = dto.Rewards.Select(s => CampaignRewardDomain.Reconstruct(s)).OrderBy(s => s.Ordinal).ToList()
               
            };
            return domain;
        }
        public void Update(CampaignDto dto)
        {
            SetName(dto.Name);
            SetStartedAt(dto.StartedAt);
            SetEndedAt(dto.EndedAt);
            SetRewards(dto.Rewards);
            SetLuckyWheel(dto.LuckyWheel);
        }
        private void SetLuckyWheel(LuckyWheel luckyWheel)
        {
            if (LuckyWheel.Equals(luckyWheel))
                return;
            LuckyWheel = (LuckyWheel)luckyWheel.GetCopy();
            Modify();
        }
        private void SetRewards(List<CampaignRewardDto> dtoes)
        {
            var currentRewardIds = Rewards.Select(s => s.Id).ToList();

            for (int i = 0; i < dtoes.Count; i++)
            {
                var dto = dtoes[i];
                if (!currentRewardIds.Contains(dto.Id))
                    AddReweward(dto);
                else
                    UpdateReward(dto.Id, dto);
            }

            var targetRewardIds = dtoes.Select(s => s.Id).ToList();
            var removedCampaignRewardIds = currentRewardIds.Except(targetRewardIds).ToList();
            foreach(var removedCampaignRewardId in removedCampaignRewardIds)
                RemoveReward(removedCampaignRewardId);

            var reOrderedRewards = Rewards.OrderBy(o => o.Ordinal).ToList();
            Rewards = reOrderedRewards;
        }

        
        public void RemoveReward(string campaignRewardId)
        {
            var removeResult = Rewards.RemoveAll(f => f.Id == campaignRewardId);
            if(removeResult > 0)
            {
                AddEvent(new RemoveRewardDomainEvent(campaignRewardId));
                Modify();
            }
        }
        public void AddReweward(CampaignRewardDto dto)
        {
            Rewards.Add(new CampaignRewardDomain(dto));
            AddEvent(new AddRewardDomainEvent(dto.Id));
            Modify();
        }
        private void UpdateReward(string campaignRewardId, CampaignRewardDto dto)
        {
            var campaignRewardDomain = Rewards.FirstOrDefault(f => f.Id == dto.Id);
            campaignRewardDomain.Update(dto);
            if (campaignRewardDomain.Modified)
            {
                AddEvent(new UpdateRewardDomainEvent(dto.Id));
                Modify();
            }
        }
        private void SetEndedAt(DateTime endedAt)
        {
            if (Equals(EndedAt, endedAt))
                return;
            EndedAt = endedAt;
            Modify();
        }
        private void SetStartedAt(DateTime startedAt)
        {
            if (Equals(StartedAt, startedAt))
                return;
            StartedAt = startedAt;
            Modify();
        }
        private void SetName(string name)
        {
            if (Equals(Name, name))
                return;
            Name = name;
            Modify();
        }
        
        public void UpdateReward(CampaignRewardDto dto)
        {
            var rewardDomain = Rewards.FirstOrDefault(f => f.Id == dto.Id);
            if (rewardDomain == null)
                throw new NotFoundException();
            rewardDomain.Update(dto);
        }

        protected override void Create()
        {
            AddEvent(new CreateDomainEvent());
            base.Create();
        }
        protected override void Modify()
        {
            AddEvent(new ModifyDomainEvent());
            base.Modify();
        }
    }
}
