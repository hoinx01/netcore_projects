using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/communes")]
    public class AdminCommuneController : BaseRestController
    {
        private readonly IAdministrativeUnitService administrativeUnitService;

        public AdminCommuneController(
            IAdministrativeUnitService administrativeUnitService
        )
        {
            this.administrativeUnitService = administrativeUnitService;
        }

        public async Task<List<CommuneModel>> GetByDistrictId([FromQuery] string districtId)
        {
            var communes = await administrativeUnitService.GetCommuneByDistrictId(districtId);
            return communes;
        }
    }
}
