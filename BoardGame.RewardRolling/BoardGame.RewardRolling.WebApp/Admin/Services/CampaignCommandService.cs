using AutoMapper;
using BoardGame.RewardRolling.Domains.Campaign.Dtos;
using BoardGame.RewardRolling.Domains.Campaign.Models;
using BoardGame.RewardRolling.Domains.Campaign.Repositories;
using BoardGame.RewardRolling.Service.Common;
using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using Hinox.Mvc.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var dto = new CampaignDto()
            {
                Id = id,
                Name = model.Name,
                StartedAt = model.StartedAt.Value,
                EndedAt = model.EndedAt.Value,
                Rewards = new List<CampaignRewardDto>(),
                LuckyWheel = model.LuckyWheel
            };

            for (int i = 0; i < model.Rewards.Count; i++)
            {
                var rewardModel = model.Rewards[i];
                rewardModel.Ordinal = i + 1;

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

        public async Task<CampaignModel> Update(string id, UpdateCampaignModel model, string inputJson)
        {
            var domain = await campaignRepository.Get(id);

            if (domain == null)
                throw new NotFoundException();

            var dto = CampaignDto.GetForUpdate(domain);

            dto.Name = model.Name;
            dto.StartedAt = model.StartedAt.Value;
            dto.EndedAt = model.EndedAt.Value;
            dto.Rewards = new List<CampaignRewardDto>();

            for(int i = 0; i < model.Rewards.Count; i++)
            {
                var rewardModel = model.Rewards[i];
                rewardModel.Ordinal = i + 1;

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

            dto.LuckyWheel = model.LuckyWheel;

            domain.Update(dto);

            await campaignRepository.Save(domain);

            var result = Mapper.Map<CampaignModel>(domain);
            return result;
        }
    }
}
