using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Reward
{
    public class RewardFilterResultModel : BasePagingFilterResponse
    {
        public List<RewardModel> Rewards { get; set; }
    }
}
