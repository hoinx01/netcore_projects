using BoardGame.RewardRolling.Core.Statics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Service.Domains
{
    public class RewardDomain
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public RewardStatus Status { get; set; }

    }
}
