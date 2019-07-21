using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;
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
        [HttpGet]
        public async Task<List<CommuneModel>> GetByDistrictId([FromQuery] string districtId)
        {
            var communes = await administrativeUnitService.GetCommuneByDistrictId(districtId);
            return communes;
        }
        [HttpPost]
        public async Task<CommuneModel> Add([FromBody] AddCommuneRequest model)
        {
            var communeModel = await administrativeUnitService.AddCommune(model);
            return communeModel;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] string id)
        {
            await administrativeUnitService.DeleteCommuneAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<CommuneModel> Update([FromRoute] string id, [FromBody] UpdateCommuneRequest model)
        {
            var communeModel = await administrativeUnitService.UpdateCommune(id, model);
            return communeModel;
        }
    }
}
