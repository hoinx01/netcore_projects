using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Filters;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace BoardGame.RewardRolling.Data.Mongo.Filters
{
    public class MdCustomerFilter : BaseMdPagingFilter<MdCustomer>
    {
        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }

        public override MdFilterSpecification<MdCustomer> GenerateFilterSpecification()
        {
            var mdFilterDefinition = new MdFilterSpecification<MdCustomer>();

            var filterDefinitionBuilder = Builders<MdCustomer>.Filter;

            var andFilterDefinitions = new List<FilterDefinition<MdCustomer>>();

            if (CreatedAtMin != null)
                andFilterDefinitions.Add(Builders<MdCustomer>.Filter.Gte(nameof(MdCustomer.CreatedAt), CreatedAtMin));
            if (CreatedAtMax != null)
                andFilterDefinitions.Add(Builders<MdCustomer>.Filter.Lt(nameof(MdCustomer.CreatedAt), CreatedAtMax));

            if (andFilterDefinitions.Count > 0)
                mdFilterDefinition.Filter = filterDefinitionBuilder.And(andFilterDefinitions);
            else
                mdFilterDefinition.Filter = Builders<MdCustomer>.Filter.Empty;

            var sortDefinition = Builders<MdCustomer>.Sort.Ascending(nameof(MdCustomer.CreatedAt));

            mdFilterDefinition.Sort = sortDefinition;

            var pagination = GeneratePagination();
            mdFilterDefinition.Pagination = pagination;

            return mdFilterDefinition;
        }
    }
}
