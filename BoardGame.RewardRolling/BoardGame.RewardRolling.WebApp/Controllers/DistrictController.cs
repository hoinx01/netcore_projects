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
    [Route("api/districts")]
    public class DistrictController : BaseRestController
    {
        private readonly IAdministrativeUnitService administrativeUnitService;

        public DistrictController(
            IAdministrativeUnitService administrativeUnitService
        )
        {
            this.administrativeUnitService = administrativeUnitService;
        }
        public async Task<List<DistrictModel>> GetByCityId([FromQuery] string cityId)
        {
            var districts = await administrativeUnitService.GetDistrictByCityIdAsync(cityId);
            return districts;
        }
    }
}
