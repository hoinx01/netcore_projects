using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Filters;
using BoardGame.RewardRolling.Domains.Campaign.Repositories;
using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;
using BoardGame.RewardRolling.WebApp.Admin.Models.Reward;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using Hinox.Mvc.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Services
{
    public class CampaignQueryService : ICampaignQueryService
    {
        private readonly IMdCampaignDao campaignDao;
        private readonly IMdRewardDao rewardDao;
        private readonly ICampaignRepository campaignRepository;

        public CampaignQueryService(
            IMdCampaignDao campaignDao,
            IMdRewardDao rewardDao,
            ICampaignRepository campaignRepository
            )
        {
            this.campaignDao = campaignDao;
            this.rewardDao = rewardDao;
            this.campaignRepository = campaignRepository;
        }
        public async Task<CampaignFilterResultModel> Filter(CampaignFilterModel filterModel)
        {
            var mdFilter = new MdCampaignFilter()
            {
                Page = filterModel.Page,
                Limit = filterModel.Limit,
                ExcludedStatuses = new List<int>() { CampaignStatus.Deleted.Id }
            };
            var count = await campaignDao.Count(mdFilter);
            var entities = await campaignDao.Filter(mdFilter);

            var models = entities.Select(s => Mapper.Map<CampaignModel>(s)).ToList();

            var result = new CampaignFilterResultModel()
            {
                Campaigns = models,
                Pagination = new PaginationModel()
                {
                    Count = count,
                    Page = filterModel.Page,
                    Limit = filterModel.Limit
                }
            };

            return result;
        }

        public async Task<CampaignModel> GetByIdAsync(string id)
        {
            //var entity = await campaignDao.GetByIdAsync(new ObjectId(id));
            //var model = Mapper.Map<CampaignModel>(entity);
            var domain = await campaignRepository.Get(id);
            var model = Mapper.Map<CampaignModel>(domain);

            var rewardIds = model.Rewards.Select(s => s.RewardId).ToList();

            var mdRewardFilter = new MdRewardFilter()
            {
                Page = 1,
                Limit = 200,
                Ids = rewardIds
            };

            var rewardEntities = await rewardDao.Filter(mdRewardFilter);
            var rewardModels = rewardEntities.Select(s => Mapper.Map<RewardModel>(s)).ToList();

            model.Rewards.ForEach(f =>
            {
                var rewardModel = rewardModels.FirstOrDefault(reward => reward.Id == f.RewardId);
                if (rewardModel != null)
                    f.Reward = rewardModel;
            });

            return model;

        }
        
    }
}
