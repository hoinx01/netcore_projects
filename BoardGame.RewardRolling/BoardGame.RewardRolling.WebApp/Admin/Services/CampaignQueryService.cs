using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Filters;
using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Services
{
    public class CampaignQueryService : ICampaignQueryService
    {
        private readonly IMdCampaignDao campaignDao;

        public CampaignQueryService(
            IMdCampaignDao campaignDao
            )
        {
            this.campaignDao = campaignDao;
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
    }
}
