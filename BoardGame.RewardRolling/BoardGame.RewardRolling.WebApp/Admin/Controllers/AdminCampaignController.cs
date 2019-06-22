using BoardGame.RewardRolling.Domains.Campaign.Dtos;
using BoardGame.RewardRolling.Domains.Campaign.Models;
using BoardGame.RewardRolling.Service.Common;
using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/campaigns")]
    public class AdminCampaignController : BaseRestController
    {
        private readonly ICampaignQueryService campaignQueryService;
        private readonly ICampaignCommandService campaignCommandService;

        public AdminCampaignController(
            ICampaignQueryService campaignQueryService,
            ICampaignCommandService campaignCommandService
            )
        {
            this.campaignQueryService = campaignQueryService;
            this.campaignCommandService = campaignCommandService;
        }
        public async Task<CampaignFilterResultModel> Filter([FromQuery] CampaignFilterModel filterModel)
        {
            var result = await campaignQueryService.Filter(filterModel);
            return result;
        }

        [HttpPost]
        public async Task<CampaignModel> Add([FromBody] AddCampaignModel model)
        {
            var result = await campaignCommandService.Add(model);
            return result;
            
        }
    }
}
