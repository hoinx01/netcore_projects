using Hinox.Mvc.Models;
using System;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Customer
{
    public class CustomerFilterRequest : BasePagingFilterRequest
    {
        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }
    }
}
