using Data.Mongo.Entities;
using Hinox.Data.Mongo.Filters;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hinox.Static.Extensions;

namespace Data.Mongo.Filters
{
    public class MdPetSpeciesFilter : BaseMdPagingFilter<MdPetSpecies>
    {
        public List<long> Ids { get; set; }
        public override MdFilterSpecification<MdPetSpecies> GenerateFilterSpecification()
        {
            var mdFilterDefinition = new MdFilterSpecification<MdPetSpecies>();

            var filterDefinitionBuilder = Builders<MdPetSpecies>.Filter;
            var andFilterDefinitions = new List<FilterDefinition<MdPetSpecies>>();
            if (!Ids.IsBlank())
                andFilterDefinitions.Add(Builders<MdPetSpecies>.Filter.AnyIn("_id", Ids));

            if (andFilterDefinitions.Count > 0)
                mdFilterDefinition.Filter = filterDefinitionBuilder.And(andFilterDefinitions);
            else
                mdFilterDefinition.Filter = Builders<MdPetSpecies>.Filter.Empty;

            var pagination = GeneratePagination();
            mdFilterDefinition.Pagination = pagination;

            return mdFilterDefinition;
        }
    }
}
