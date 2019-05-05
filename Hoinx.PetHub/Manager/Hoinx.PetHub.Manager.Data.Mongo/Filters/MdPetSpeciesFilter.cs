using Hinox.Data.Mongo.Filters;
using Hinox.Static.Extensions;
using Hoinx.PetHub.Manager.Data.Mongo.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Hoinx.PetHub.Manager.Data.Mongo.Filters
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
