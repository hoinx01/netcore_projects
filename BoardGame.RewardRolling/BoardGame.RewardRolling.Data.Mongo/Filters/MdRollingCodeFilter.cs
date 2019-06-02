using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Filters;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Filters
{
    public class MdRollingCodeFilter : BaseMdPagingFilter<MdRollingCode>
    {
        public override MdFilterSpecification<MdRollingCode> GenerateFilterSpecification()
        {
            var mdFilterDefinition = new MdFilterSpecification<MdRollingCode>();

            var filterDefinitionBuilder = Builders<MdRollingCode>.Filter;

            var andFilterDefinitions = new List<FilterDefinition<MdRollingCode>>();

            if (andFilterDefinitions.Count > 0)
                mdFilterDefinition.Filter = filterDefinitionBuilder.And(andFilterDefinitions);
            else
                mdFilterDefinition.Filter = Builders<MdRollingCode>.Filter.Empty;

            var pagination = GeneratePagination();
            mdFilterDefinition.Pagination = pagination;

            return mdFilterDefinition;
        }
    }
}
