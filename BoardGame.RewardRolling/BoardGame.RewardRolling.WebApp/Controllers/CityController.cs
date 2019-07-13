using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Controllers
{
    [Route("api/cities")]
    public class CityController : BaseRestController
    {
        private readonly IAdministrativeUnitService administrativeUnitService;

        public CityController(
            IAdministrativeUnitService administrativeUnitService
        )
        {
            this.administrativeUnitService = administrativeUnitService;
        }
        public async Task<List<CityModel>> GetAllCity()
        {
            var cities = await administrativeUnitService.GetAllCityAsync();
            return cities;
        }
    }
}
