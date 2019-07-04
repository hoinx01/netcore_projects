using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Models.RollingCode;

namespace BoardGame.RewardRolling.WebApp.Services.Interfaces
{
    public interface IRollingCodeService
    {
        Task<RollResultModel> Roll(CustomerRollViewModel model);
    }
}
