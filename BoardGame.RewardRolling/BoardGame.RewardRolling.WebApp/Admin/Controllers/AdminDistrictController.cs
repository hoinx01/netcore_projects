using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;
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
        [HttpGet]
        public async Task<List<DistrictModel>> GetByCityId([FromQuery] string cityId)
        {
            var districts = await administrativeUnitService.GetDistrictByCityIdAsync(cityId);
            return districts;
        }
        [HttpPost]
        public async Task<DistrictModel> Add([FromBody] AddDistrictRequest model)
        {
            var districtModel = await administrativeUnitService.AddDistrict(model);
            return districtModel;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] string id)
        {
            await administrativeUnitService.DeleteDistrictAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<DistrictModel> Update([FromRoute] string id, [FromBody] UpdateDistrictRequest model)
        {
            var districtModel = await administrativeUnitService.UpdateDistrict(id, model);
            return districtModel;
        }
    }
}
