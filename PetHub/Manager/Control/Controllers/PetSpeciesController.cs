using AutoMapper;
using Control.Models.Species;
using Data.Mongo.Dao;
using Data.Mongo.Dao.Interfaces;
using Data.Mongo.Entities;
using Data.Mongo.Filters;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Hinox.Mvc.Models;

namespace Control.Controllers
{
    [Route("admin/pet_species")]
    [ApiController]
    public class PetSpeciesController : BaseRestController
    {
        private readonly IMdPetSpeciesDao petSpeciesDao;
        public PetSpeciesController(
            IMdPetSpeciesDao petSpeciesDao
            )
        {
            this.petSpeciesDao = petSpeciesDao;
        }
        [HttpGet]
        public async Task<PetSpeciesFilterResult> Filter([FromQuery] PetSpeciesFilter filterModel)
        {
            var mdFilter = Mapper.Map<MdPetSpeciesFilter>(filterModel);
            var species = await petSpeciesDao.Filter(mdFilter);
            var speciesModels = species.Select(s => Mapper.Map<PetSpeciesModel>(s)).ToList();

            var count = await petSpeciesDao.Count(mdFilter);

            var result = new PetSpeciesFilterResult()
            {
                PetSpecies = speciesModels,
                Pagination = new PaginationModel()
                {
                    Count = count,
                    Page = filterModel.Page,
                    Limit = filterModel.Limit
                }
            };
            return result;
        }

        [HttpPost]
        public async Task<MdPetSpecies> Add([FromBody] AddPetSpeciesModel model)
        {
            var species = new MdPetSpecies()
            {
                Name = model.Name,
                Alias = model.Alias,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            await petSpeciesDao.AddAsync(species);
            return species;
        }
    }
}
