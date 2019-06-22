using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Filters;
using Hinox.Static.Utilities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Filters
{
    public class MdRewardFilter : BaseMdPagingFilter<MdReward>
    {
        public List<int> Statuses { get; set; }
        public override MdFilterSpecification<MdReward> GenerateFilterSpecification()
        {
            var mdFilterDefinition = new MdFilterSpecification<MdReward>();

            var filterDefinitionBuilder = Builders<MdReward>.Filter;

            var andFilterDefinitions = new List<FilterDefinition<MdReward>>();

            if(Statuses != null && Statuses.Count > 0)
                andFilterDefinitions.Add(Builders<MdReward>.Filter.In("Status", Statuses));

            if (andFilterDefinitions.Count > 0)
                mdFilterDefinition.Filter = filterDefinitionBuilder.And(andFilterDefinitions);
            else
                mdFilterDefinition.Filter = Builders<MdReward>.Filter.Empty;

            var pagination = GeneratePagination();
            mdFilterDefinition.Pagination = pagination;

            return mdFilterDefinition;
        }
    }
}
