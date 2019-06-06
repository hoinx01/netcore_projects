using AutoMapper;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Filters;
using BoardGame.RewardRolling.WebApp.Admin.Models.Reward;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Services
{
    public class RewardQueryService : IRewardQueryService
    {
        private readonly IMdRewardDao rewardDao;

        public RewardQueryService(
            IMdRewardDao rewardDao
            )
        {
            this.rewardDao = rewardDao;
        }
        public async Task<RewardFilterResultModel> Filter(RewardFilterModel filterModel)
        {
            var mdFilter = new MdRewardFilter()
            {
                Page = filterModel.Page,
                Limit = filterModel.Limit
            };
            var count = await rewardDao.Count(mdFilter);
            var mdRewards = await rewardDao.Filter(mdFilter);

            var rewardModels = mdRewards.Select(s => Mapper.Map<RewardModel>(s)).ToList();

            var result = new RewardFilterResultModel()
            {
                Rewards = rewardModels,
                Pagination = new PaginationModel()
                {
                    Count = count,
                    Page = filterModel.Page,
                    Limit = filterModel.Limit
                }
            };

            return result;
        }
    }
}
