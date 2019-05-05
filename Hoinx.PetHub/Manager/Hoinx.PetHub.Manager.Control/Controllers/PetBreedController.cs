using AutoMapper;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using Hinox.Mvc.Models;
using Hoinx.PetHub.Manager.Control.Models.Breed;
using Hoinx.PetHub.Manager.Data.Mongo.Dao.Interfaces;
using Hoinx.PetHub.Manager.Data.Mongo.Entities;
using Hoinx.PetHub.Manager.Data.Mongo.Filters;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hoinx.PetHub.Manager.Control.Controllers
{
    [ApiController]
    [Route("admin/pet_breeds")]
    public class PetBreedController : BaseRestController
    {
        private readonly IMdPetBreedDao petBreedDao;
        public PetBreedController(
            IMdPetBreedDao petBreedDao
            )
        {
            this.petBreedDao = petBreedDao;
        }

        [HttpPost]
        public async Task<BreedModel> Add([FromBody] AddBreedModel model)
        {
            var mdBreed = Mapper.Map<MdPetBreed>(model);
            mdBreed.CreatedAt = DateTime.UtcNow;
            mdBreed.ModifiedAt = DateTime.UtcNow;

            await petBreedDao.AddAsync(mdBreed);

            var result = Mapper.Map<BreedModel>(mdBreed);
            return result;
        }
        public async Task<BreedModel> GetById([FromRoute] long id)
        {
            var mdBreed = await petBreedDao.GetByIdAsync(id);
            if (mdBreed == null)
                throw new NotFoundException("Không tìm thấy đối tượng");

            var result = Mapper.Map<BreedModel>(mdBreed);
            return result;
        }

        public async Task<BreedFilterResult> Filter([FromQuery] BreedFilterModel model)
        {
            var mdFilter = Mapper.Map<MdBreedFilter>(model);
            var species = await petBreedDao.Filter(mdFilter);
            var speciesModels = species.Select(s => Mapper.Map<BreedModel>(s)).ToList();

            var count = await petBreedDao.Count(mdFilter);

            var result = new BreedFilterResult()
            {
                Breeds = speciesModels,
                Pagination = new PaginationModel()
                {
                    Count = count,
                    Page = model.Page,
                    Limit = model.Limit
                }
            };
            return result;
        }
    }
}
