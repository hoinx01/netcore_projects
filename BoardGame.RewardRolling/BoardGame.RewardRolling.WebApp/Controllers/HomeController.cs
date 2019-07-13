using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoardGame.RewardRolling.WebApp.Models;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;

namespace BoardGame.RewardRolling.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICampaignQueryService campaignQueryService;
        public HomeController(
           ICampaignQueryService campaignQueryService
       )
        {
            this.campaignQueryService = campaignQueryService;
        }
        public async Task<ActionResult> Index()
        {
            var model = await campaignQueryService.GetCurrentCampaign();
            ViewData["Title"] = "Trang chủ";
            return View(model);
        }
        public async Task<ActionResult> Reward()
        {
            ViewData["Title"] = "Đăng nhập";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PostInfo()
        {
            ViewData["Title"] = "Đăng nhập";
            return View();
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
    }
}
