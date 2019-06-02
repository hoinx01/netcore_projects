using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.RollingCode
{
    public class RollingCodeFilterResult : BasePagingFilterResponse
    {
        public List<RollingCodeModel> RollingCodes { get; set; }
    }
}
