using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Models.RollingCode;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Controllers
{
    [Route("api/rolling_codes")]
    public class RollingCodeController : BaseRestController
    {
        private readonly IRollingCodeService rollingCodeService;

        public RollingCodeController(
            IRollingCodeService rollingCodeService
        )
        {
            this.rollingCodeService = rollingCodeService;
        }

        [HttpPost]
        [Route("spins")]
        public async Task<RollResultModel> Roll([FromBody] CustomerRollViewModel model)
        {
            var result = await rollingCodeService.Roll(model);
            return result;
        }
    }
}
