using Data.Mongo.Entities;
using Hinox.Data.Mongo.Filters;
using Hinox.Static.Extensions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mongo.Filters
{
    public class MdBreedFilter : BaseMdPagingFilter<MdPetBreed>
    {
        public List<long> Ids { get; set; }
        public List<long> SpeciesIds { get; set; }
        public override MdFilterSpecification<MdPetBreed> GenerateFilterSpecification()
        {
            var mdFilterDefinition = new MdFilterSpecification<MdPetBreed>();

            var filterDefinitionBuilder = Builders<MdPetBreed>.Filter;
            var andFilterDefinitions = new List<FilterDefinition<MdPetBreed>>();
            if (!Ids.IsBlank())
                andFilterDefinitions.Add(Builders<MdPetBreed>.Filter.In("_id", Ids));

            if (!SpeciesIds.IsBlank())
                andFilterDefinitions.Add(Builders<MdPetBreed>.Filter.In("SpeciesId", SpeciesIds));

            if (andFilterDefinitions.Count > 0)
                mdFilterDefinition.Filter = filterDefinitionBuilder.And(andFilterDefinitions);
            else
                mdFilterDefinition.Filter = Builders<MdPetBreed>.Filter.Empty;

            var pagination = GeneratePagination();
            mdFilterDefinition.Pagination = pagination;

            return mdFilterDefinition;
        }
    }
}
