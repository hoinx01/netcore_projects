using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Service.Domains;
using BoardGame.RewardRolling.Service.Services.Reward;
using BoardGame.RewardRolling.WebApp.Admin.Models.Reward;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/rewards")]
    public class AdminRewardController : BaseRestController
    {
        private readonly IRewardQueryService rewardQueryService;
        private readonly IRewardService rewardService;

        public AdminRewardController(
            IRewardQueryService rewardQueryService,
            IRewardService rewardService
            )
        {
            this.rewardQueryService = rewardQueryService;
            this.rewardService = rewardService;
        }

        [HttpGet]
        public async Task<RewardFilterResultModel> Filter([FromQuery] RewardFilterModel filterModel)
        {
            var result = await rewardQueryService.Filter(filterModel);
            return result;
        }

        [HttpPost]
        public async Task<RewardModel> Add([FromBody] AddRewardRequest model)
        {
            var domain = new RewardDomain()
            {
                Name = model.Name,
                Cost = model.Cost,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                Status = RewardStatus.Active
            };
            await rewardService.AddReward(domain);

            var result = Mapper.Map<RewardModel>(domain);
            return result;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<RewardModel> Update([FromRoute] string id, [FromBody] UpdateRewardRequest model)
        {
            var domain = await rewardService.GetById(id);
            if (domain == null || domain.Status.Equals(RewardStatus.Deleted))
                throw new NotFoundException(ExceptionMessages.Reward.NotFound);

            domain.Name = model.Name;
            domain.Cost = model.Cost;
            domain.ModifiedAt = DateTime.UtcNow;

            await rewardService.UpdateReward(domain);

            var result = Mapper.Map<RewardModel>(domain);
            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] string id)
        {
            var domain = await rewardService.GetById(id);
            if (domain == null || domain.Status.Equals(RewardStatus.Deleted))
                throw new NotFoundException(ExceptionMessages.Reward.NotFound);

            domain.Status = RewardStatus.Deleted;
            domain.ModifiedAt = DateTime.UtcNow;

            await rewardService.DeleteReward(domain);

            return;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<RewardModel> GetById([FromRoute] string id)
        {
            var domain = await rewardService.GetById(id);
            if (domain == null || domain.Status.Equals(RewardStatus.Deleted))
                throw new NotFoundException(ExceptionMessages.Reward.NotFound);

            var result = Mapper.Map<RewardModel>(domain);
            return result;
        }

    }
}
