using AutoMapper;
using BoardGame.RewardRolling.Data.Mongo.Dao;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.Domains.Campaign.Dtos;
using BoardGame.RewardRolling.Domains.Campaign.Models;
using BoardGame.RewardRolling.Domains.Campaign.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Data.Mongo.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly IMdCampaignDao campaignDao;

        public CampaignRepository(
            IMdCampaignDao campaignDao
            )
        {
            this.campaignDao = campaignDao;
        }
        public async Task Save(CampaignDomain domain)
        {
            var entity = Mapper.Map<MdCampaign>(domain);

            if (domain.Created)
                await campaignDao.AddAsync(entity);
            else if (domain.Modified)
                await campaignDao.UpdateAsync(entity);
        }
        public async Task<CampaignDomain> Get(string id)
        {
            var entity = await campaignDao.GetByIdAsync(new ObjectId(id));
            if (entity == null)
                return null;

            var domainDto = Mapper.Map<CampaignDto>(entity);
            var domain = CampaignDomain.Reconstruct(domainDto);
            return domain;
        }
    }
}
