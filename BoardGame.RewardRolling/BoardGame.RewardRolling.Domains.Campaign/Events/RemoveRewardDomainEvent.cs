using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Events
{
    public class RemoveRewardDomainEvent : DomainEvent
    {
        public RemoveRewardDomainEvent(string campaignRewardId) : base()
        {
            CampaignRewardId = campaignRewardId;
        }
        public string CampaignRewardId { get; set; }
    }
}
