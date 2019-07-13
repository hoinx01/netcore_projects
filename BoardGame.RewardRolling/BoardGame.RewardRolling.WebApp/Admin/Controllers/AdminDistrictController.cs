using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/districts")]
    public class AdminDistrictController : BaseRestController
    {
        private readonly IAdministrativeUnitService administrativeUnitService;

        public AdminDistrictController(
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
