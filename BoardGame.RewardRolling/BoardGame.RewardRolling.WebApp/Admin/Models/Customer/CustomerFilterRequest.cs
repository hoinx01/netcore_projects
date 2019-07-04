using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Customer
{
    public class CustomerFilterRequest : BasePagingFilterRequest
    {
        [FromQuery(Name = "created_at_min")]
        public DateTime? CreatedAtMin { get; set; }
        [FromQuery(Name = "created_at_max")]
        public DateTime? CreatedAtMax { get; set; }
    }
}
