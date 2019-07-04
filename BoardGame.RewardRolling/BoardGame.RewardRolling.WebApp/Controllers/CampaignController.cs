using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Controllers
{
    [Route("api/campaigns")]
    public class CampaignController : BaseRestController
    {
        private readonly ICampaignQueryService campaignQueryService;

        public CampaignController(
            ICampaignQueryService campaignQueryService
        )
        {
            this.campaignQueryService = campaignQueryService;
        }

        [HttpGet]
        public async Task<CampaignModel> Get()
        {
            var model = await campaignQueryService.GetCurrentCampaign();
            if(model == null)
                throw new NotFoundException("Hiện tại không có đợt tặng quà nào đang hoạt đông");

            return model;
        }
    }
}
