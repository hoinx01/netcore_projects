using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Events
{
    public class UpdateRewardDomainEvent : DomainEvent
    {
        public UpdateRewardDomainEvent(string campaignRewardId) : base()
        {
            CampaignRewardId = campaignRewardId;
        }
        public string CampaignRewardId { get; set; }
    }
}
