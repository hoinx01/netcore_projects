using AutoMapper;
using Control.Models.Breed;
using Data.Mongo.Dao;
using Data.Mongo.Dao.Interfaces;
using Data.Mongo.Entities;
using Data.Mongo.Filters;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
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
