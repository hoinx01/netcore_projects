using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Reward
{
    public class UpdateRewardRequest
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }
}
