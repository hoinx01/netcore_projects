using BoardGame.RewardRolling.Service.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Service.Services.Reward
{
    public interface IRewardService
    {
        Task AddReward(RewardDomain domain);
        Task UpdateReward(RewardDomain domain);
        Task DeleteReward(RewardDomain domain);
        Task<RewardDomain> GetById(string id);
    }
}
