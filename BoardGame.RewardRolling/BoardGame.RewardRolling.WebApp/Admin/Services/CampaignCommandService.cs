using AutoMapper;
using BoardGame.RewardRolling.Domains.Campaign.Dtos;
using BoardGame.RewardRolling.Domains.Campaign.Models;
using BoardGame.RewardRolling.Domains.Campaign.Repositories;
using BoardGame.RewardRolling.Service.Common;
using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Services
{
    public class CampaignCommandService : ICampaignCommandService
    {
        private readonly IIdGenerator idGenerator;
        private readonly ICampaignRepository campaignRepository;

        public CampaignCommandService(
            IIdGenerator idGenerator,
            ICampaignRepository campaignRepository
            )
        {
            this.idGenerator = idGenerator;
            this.campaignRepository = campaignRepository;
        }

        public async Task<CampaignModel> Add(AddCampaignModel model)
        {
            var id = await idGenerator.GenerateStringId();
            var dto = new CampainDto()
            {
                Id = id,
                Name = model.Name,
                StartedAt = model.StartedAt.Value,
                EndedAt = model.EndedAt.Value,
                Rewards = new List<CampaignRewardDto>()
            };
            foreach (var rewardModel in model.Rewards)
            {
                var campaignRewardId = await idGenerator.GenerateStringId();
                var rewardDto = new CampaignRewardDto()
                {
                    Id = campaignRewardId,
                    RewardId = rewardModel.RewardId,
                    Rate = rewardModel.Rate,
                    Ordinal = rewardModel.Ordinal
                };
                dto.Rewards.Add(rewardDto);
            }
            var domain = new CampaignDomain(dto);
            await campaignRepository.Save(domain);

            var result = Mapper.Map<CampaignModel>(domain);
            return result;
        }
    }
}
