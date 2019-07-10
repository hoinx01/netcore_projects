using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    public class AdminHomeController : BaseMvcController
    {
        public ActionResult Index()
        {

            return View();
        }
    }
}
