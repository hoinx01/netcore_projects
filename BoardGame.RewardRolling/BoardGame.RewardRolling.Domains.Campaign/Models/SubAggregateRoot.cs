using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.Models
{
    public class SubAggregateRoot<Aggregateroot> : Domain
    {
        public Aggregateroot Root { get; set; }
    }
}
