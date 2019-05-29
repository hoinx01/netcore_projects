using AutoMapper;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using Hoinx.PetHub.Manager.Control.Models.Species;
using Hoinx.PetHub.Manager.Data.Mongo.Dao.Interfaces;
using Hoinx.PetHub.Manager.Data.Mongo.Entities;
using Hoinx.PetHub.Manager.Static.Constants;
using Hoinx.PetHub.Manager.Static.Enumerations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoinx.PetHub.Manager.Control.Controllers
{
    [ApiController]
    [Route("admin/pet_species/{speciesId}/breeds")]
    public class PetBreedController : BaseRestController
    {
        private readonly IMdPetSpeciesDao speciesDao;
        public PetBreedController(
            IMdPetSpeciesDao speciesDao
            )
        {
            this.speciesDao = speciesDao;
        }

        [HttpPost]
        public async Task<BreedModel> Add([FromRoute] long speciesId, [FromBody] AddBreedModel model)
        {
            var species = await speciesDao.GetByIdAsync(speciesId);
            if (species == null || species.Status.Equals(SpeciesStatus.Deleted.Name))
                throw new NotFoundException(ApiErrorMessages.NotFound);

            if (species.Breeds == null)
                species.Breeds = new List<MdPetBreed>();

            var nameDuplicated = species.Breeds.Count(c => c.Name.Equals(model.Name)) > 0;
            if (nameDuplicated)
                throw new UnprocessableEntityException("name: đã tồn tại");

            var aliasDuplicated = species.Breeds.Count(c => c.Alias.Equals(model.Alias)) > 0;
            if (aliasDuplicated)
                throw new UnprocessableEntityException("alias: đã tồn tại");

            var mdBreed = Mapper.Map<MdPetBreed>(model);
            mdBreed.CreatedAt = DateTime.UtcNow;
            mdBreed.ModifiedAt = DateTime.UtcNow;
            mdBreed.Status = BreedStatus.Active.Name;

            species.Breeds.Add(mdBreed);
            species.ModifiedAt = DateTime.UtcNow;

            await speciesDao.UpdateAsync(species);

            var result = Mapper.Map<BreedModel>(mdBreed);
            return result;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<BreedModel> Update([FromRoute] long speciesId, [FromRoute] long id, [FromBody] UpdateBreedModel model)
        {
            var species = await speciesDao.GetByIdAsync(speciesId);
            if (species == null || species.Status.Equals(SpeciesStatus.Deleted.Name))
                throw new NotFoundException(ApiErrorMessages.NotFound);

            if (species.Breeds == null)
                species.Breeds = new List<MdPetBreed>();

            var mdBreed = species.Breeds.FirstOrDefault(f => f.Id == id);
            if(mdBreed == null || mdBreed.Status.Equals(BreedStatus.Deleted.Name))
                throw new NotFoundException(ApiErrorMessages.NotFound);

            var nameDuplicated = species.Breeds.Count(c => c.Name.Equals(model.Name) && c.Id != id) > 0;
            if (nameDuplicated)
                throw new UnprocessableEntityException("name: đã tồn tại");

            var aliasDuplicated = species.Breeds.Count(c => c.Alias.Equals(model.Alias) && c.Id != id) > 0;
            if (aliasDuplicated)
                throw new UnprocessableEntityException("alias: đã tồn tại");

            mdBreed.Name = model.Name;
            mdBreed.Alias = model.Alias;
            mdBreed.ModifiedAt = DateTime.UtcNow;

            await speciesDao.UpdateAsync(species);

            var result = Mapper.Map<BreedModel>(mdBreed);
            return result;
        }


    }
}
