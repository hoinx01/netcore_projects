using AutoMapper;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using Hinox.Mvc.Models;
using Hoinx.PetHub.Manager.Control.Models.Species;
using Hoinx.PetHub.Manager.Data.Mongo.Dao.Interfaces;
using Hoinx.PetHub.Manager.Data.Mongo.Entities;
using Hoinx.PetHub.Manager.Data.Mongo.Filters;
using Hoinx.PetHub.Manager.Static.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hoinx.PetHub.Manager.Control.Controllers
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

        [HttpGet]
        [Route("{id}")]
        public async Task<MdPetSpecies> GetById([FromRoute] long id)
        {
            var species = await petSpeciesDao.GetByIdAsync(id);
            if (species == null)
                throw new NotFoundException(ApiErrorMessages.NotFound);
            return species;
        }
    }
}
