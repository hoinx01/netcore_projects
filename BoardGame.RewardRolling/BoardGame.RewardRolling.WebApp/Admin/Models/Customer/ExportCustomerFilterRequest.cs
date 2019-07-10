using Hinox.Mvc.Models;
using System;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Customer
{
    public class ExportCustomerFilterRequest : BasePagingFilterRequest
    {
        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }
        public string TemplateName { get; set; }
    }
}
