using BoardGame.RewardRolling.Domains.Campaign.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Campaign
{
    public class AddCampaignModel
    {
        public string Name { get; set; }
        [Required]
        public DateTime? StartedAt { get; set; }
        [Required]
        public DateTime? EndedAt { get; set; }
        public List<AddCampaignRewardModel> Rewards { get; set; }
        public LuckyWheel LuckyWheel { get; set; }
    }
    public class AddCampaignRewardModel
    {
        public string RewardId { get; set; }
        public int Rate { get; set; }
        public int Ordinal { get; set; }
    }
}
