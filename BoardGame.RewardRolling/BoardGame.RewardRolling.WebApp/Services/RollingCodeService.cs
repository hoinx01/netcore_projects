using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Core.ValueObjects;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.Service.Common;
using BoardGame.RewardRolling.WebApp.Models.RollingCode;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Exceptions;
using Microsoft.EntityFrameworkCore.Internal;

namespace BoardGame.RewardRolling.WebApp.Services
{
    public class RollingCodeService : IRollingCodeService
    {
        private readonly IMdCampaignDao campaignDao;
        private readonly IMdRollingCodeDao rollingCodeDao;
        private readonly IIdGenerator idGenerator;
        private readonly IMdCustomerDao customerDao;

        public RollingCodeService(
            IMdCampaignDao campaignDao,
            IMdRollingCodeDao rollingCodeDao, 
            IIdGenerator idGenerator,
            IMdCustomerDao customerDao
        )
        {
            this.campaignDao = campaignDao;
            this.rollingCodeDao = rollingCodeDao;
            this.idGenerator = idGenerator;
            this.customerDao = customerDao;
        }

        public async Task<RollResultModel> Roll(CustomerRollViewModel model)
        {
            var code = await rollingCodeDao.GetBySerialAsync(model.RollingCode);

            if(code == null)
                throw new NotFoundException("Mã quà tặng không đúng");

            if(code.Status == RollingCodeStatus.Used.Id)
                throw new ForbiddenException("Mã quà tặng đã được sử dụng");

            var campaign = await campaignDao.GetCurrentCampaignAsync();
            if(campaign == null)
                throw new NotFoundException("Không có chương trình tặng quà nào đang hoạt động");

            var rewardIdContainer = new List<int>();
            foreach (var reward in campaign.Rewards)
            {
                for (int i = 0; i < reward.Rate; i++)
                    rewardIdContainer.Add(reward.Ordinal);
            }
            
            var rand = new Random();
            var rewardOrdinalIndex = rand.Next(0, rewardIdContainer.Count);
            var rewardOrdinal = rewardIdContainer[rewardOrdinalIndex]; 
            var rewardId = campaign.Rewards.FirstOrDefault(f => f.Ordinal == rewardOrdinal)
                .RewardId
                .ToString();
            var imgReward = campaign.Rewards.FirstOrDefault(f => f.Ordinal == rewardOrdinal).ImageSrc;

            var rolloutReward = new SpinReward()
            {
                RewardId = rewardId,
                CampaignId = campaign.Id.ToString(),
                RewardOrdinal = rewardOrdinal,
                RollingCodeId = code.Id.ToString(),
                RollingCodeSerial = code.Serial,
                CreatedAt = DateTime.UtcNow
            };
            var mdCustomer = new MdCustomer()
            {
                Rewards = new List<SpinReward>() { rolloutReward },
                FullName = model.Customer.FullName,
                PhoneNumber = model.Customer.PhoneNumber,
                Email = model.Customer.Email,
                Birthday = model.Customer.Birthday,
                GenderId = model.Customer.GenderId,
                Address = model.Customer.Address,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            code.RolledAt = DateTime.UtcNow;
            code.Status = RollingCodeStatus.Used.Id;

            await rollingCodeDao.UpdateAsync(code);
            await customerDao.AddAsync(mdCustomer);

            var result = new RollResultModel()
            {
                RewardId = rewardId,
                ImageReward = imgReward,
                CampaignId = campaign.Id.ToString(),
                RewardOrdinal = rewardOrdinal

            };
            return result;
        }
    }
}
