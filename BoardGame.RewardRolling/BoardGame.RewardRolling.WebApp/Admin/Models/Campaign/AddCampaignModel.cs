﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Campaign
{
    public class AddCampaignModel
    {
        public string Name { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int Status { get; set; }
        public List<AddCampaignRewardModel> Rewards { get; set; }
    }
    public class AddCampaignRewardModel
    {
        public string RewardId { get; set; }
        public int Rate { get; set; }
        public int Ordinal { get; set; }
    }
}
