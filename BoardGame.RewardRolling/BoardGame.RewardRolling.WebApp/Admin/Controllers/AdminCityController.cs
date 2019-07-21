using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/cities")]
    public class AdminCityController : BaseRestController
    {
        private readonly IAdministrativeUnitService administrativeUnitService;

        public AdminCityController(
            IAdministrativeUnitService administrativeUnitService
        )
        {
            this.administrativeUnitService = administrativeUnitService;
        }
        [HttpGet]
        public async Task<List<CityModel>> GetAllCity()
        {
            var cities = await administrativeUnitService.GetAllCityAsync();
            return cities;
        }
        [HttpPost]
        public async Task<CityModel> Add([FromBody] AddCityRequest model)
        {
            var cityModel = await administrativeUnitService.AddCityAsync(model);
            return cityModel;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] string id)
        {
            await administrativeUnitService.DeleteCityAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<CityModel> UpdateCity([FromRoute] string id, [FromBody] UpdateCityRequest model)
        {
            var cityModel = await administrativeUnitService.UpdateCity(id, model);
            return cityModel;
        }
    }
}
