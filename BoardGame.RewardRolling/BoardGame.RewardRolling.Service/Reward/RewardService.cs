using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.Service.Domains;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Service.Services.Reward
{
    public class RewardService : IRewardService
    {
        private readonly IMdRewardDao rewardDao;

        public RewardService(
            IMdRewardDao rewardDao
            )
        {
            this.rewardDao = rewardDao;
        }
        public async Task AddReward(RewardDomain domain)
        {
            var mdReward = Mapper.Map<MdReward>(domain);
            await rewardDao.AddAsync(mdReward);

            domain.Id = mdReward.Id.ToString();
        }
        public async Task UpdateReward(RewardDomain domain)
        {
            var mdReward = Mapper.Map<MdReward>(domain);
            await rewardDao.UpdateAsync(mdReward);
        }
        public async Task DeleteReward(RewardDomain domain)
        {
            domain.ModifiedAt = DateTime.UtcNow;
            domain.Status = RewardStatus.Deleted;

            var mdReward = Mapper.Map<MdReward>(domain);
            await rewardDao.UpdateAsync(mdReward);
        }
        public async Task<RewardDomain> GetById(string id)
        {
            var mdReward = await rewardDao.GetByIdAsync(new ObjectId(id));
            var domain = Mapper.Map<RewardDomain>(mdReward);
            return domain;
        }
    }
}
