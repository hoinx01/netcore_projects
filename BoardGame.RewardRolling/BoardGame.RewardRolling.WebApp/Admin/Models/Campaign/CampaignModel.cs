using BoardGame.RewardRolling.Domains.Campaign.ValueObjects;
using BoardGame.RewardRolling.WebApp.Admin.Models.Reward;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Campaign
{
    public class CampaignModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int Status { get; set; }
        public List<CampaignRewardModel> Rewards { get; set; }
        public LuckyWheel LuckyWheel { get; set; }
        public List<CityModel> Cities { get; set; }
    }
    public class CampaignRewardModel
    {
        public string RewardId { get; set; }
        public int Rate { get; set; }
        public int Ordinal { get; set; }
        public RewardModel Reward { get; set; }
    }
}
