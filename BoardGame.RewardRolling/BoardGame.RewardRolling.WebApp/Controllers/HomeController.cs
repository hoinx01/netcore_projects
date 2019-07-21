using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoardGame.RewardRolling.WebApp.Models;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using BoardGame.RewardRolling.WebApp.Models.Customer;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Models.RollingCode;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using BoardGame.RewardRolling.WebApp.Admin.Models.Campaign;

namespace BoardGame.RewardRolling.WebApp.Controllers
{
    public class HomeController : BaseMvcController
    {
        private readonly ICampaignQueryService campaignQueryService;
        private readonly IAdministrativeUnitService administrativeUnitService;
        private readonly IRollingCodeService rollingCodeService;
        public HomeController(
           ICampaignQueryService campaignQueryService,
           IAdministrativeUnitService administrativeUnitService,
           IRollingCodeService rollingCodeService
       )
        {
            this.campaignQueryService = campaignQueryService;
            this.administrativeUnitService = administrativeUnitService;
            this.rollingCodeService = rollingCodeService;
        }
        public async Task<ActionResult> Index()
        {
            ViewData["Title"] = "Trang chủ";
            try
            {
                var model = await campaignQueryService.GetCurrentCampaign();
                return View(model);
            }
            catch (BaseCustomException e)
            {
                Response.StatusCode = (int)e.StatusCode;
                return View(new CampaignModel());
            }
        }
        public async Task<ActionResult> Reward()
        {
            ViewData["Title"] = "Đăng nhập";
            CustomerViewModel model = new CustomerViewModel();
            model.Cities = await administrativeUnitService.GetAllCityAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<RollModel> PostInfo([FromBody]CustomerRollViewModel model)
        {
            var result = new RollModel();
            try
            {
                var roll = await rollingCodeService.Roll(model);
                result.Roll = roll;
            }
            catch (BaseCustomException e)
            {
                Response.StatusCode = (int)e.StatusCode;
                result.Message = e.Messages != null ? e.Messages[0] : "";
            }
            return result;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<List<DistrictModel>> GetByCityId([FromQuery] string cityId)
        {
            var districts = await administrativeUnitService.GetDistrictByCityIdAsync(cityId);
            return districts;
        }
    }
}
