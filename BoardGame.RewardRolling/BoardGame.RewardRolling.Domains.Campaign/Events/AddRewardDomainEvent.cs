using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Events
{
    public class AddRewardDomainEvent : DomainEvent
    {
        public AddRewardDomainEvent(string campaignRewardId) : base()
        {
            CampaignRewardId = campaignRewardId;
        }
        public string CampaignRewardId { get; set; }

    }
}
