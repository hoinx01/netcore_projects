using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Filters;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Filters
{
    public class MdCampaignFilter : BaseMdPagingFilter<MdCampaign>
    {
        public List<int> Statuses { get; set; }
        public List<int> ExcludedStatuses { get; set; }
        public override MdFilterSpecification<MdCampaign> GenerateFilterSpecification()
        {
            var mdFilterDefinition = new MdFilterSpecification<MdCampaign>();

            var filterDefinitionBuilder = Builders<MdCampaign>.Filter;

            var andFilterDefinitions = new List<FilterDefinition<MdCampaign>>();

            if (Statuses != null && Statuses.Count > 0)
                andFilterDefinitions.Add(Builders<MdCampaign>.Filter.In("Status", Statuses));
            if (ExcludedStatuses != null && ExcludedStatuses.Count > 0)
                andFilterDefinitions.Add(Builders<MdCampaign>.Filter.Nin("Status", ExcludedStatuses));

            if (andFilterDefinitions.Count > 0)
                mdFilterDefinition.Filter = filterDefinitionBuilder.And(andFilterDefinitions);
            else
                mdFilterDefinition.Filter = Builders<MdCampaign>.Filter.Empty;

            var pagination = GeneratePagination();
            mdFilterDefinition.Pagination = pagination;

            return mdFilterDefinition;
        }
    }
}
