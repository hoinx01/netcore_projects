using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Customer
{
    public class CustomerFilterRequest : BasePagingFilterRequest
    {
        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }
    }
}
