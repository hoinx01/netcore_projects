using BoardGame.RewardRolling.WebApp.Admin.Models.Reward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces
{
    public interface IRewardQueryService
    {
        Task<RewardFilterResultModel> Filter(RewardFilterModel filterModel);
    }
}
