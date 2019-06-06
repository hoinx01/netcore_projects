using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Filters;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Filters
{
    public class MdRewardFilter : BaseMdPagingFilter<MdReward>
    {
        public override MdFilterSpecification<MdReward> GenerateFilterSpecification()
        {
            var mdFilterDefinition = new MdFilterSpecification<MdReward>();

            var filterDefinitionBuilder = Builders<MdReward>.Filter;

            var andFilterDefinitions = new List<FilterDefinition<MdReward>>();

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
