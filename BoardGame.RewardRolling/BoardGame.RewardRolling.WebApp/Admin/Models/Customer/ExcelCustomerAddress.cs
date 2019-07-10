using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Core.ValueObjects;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Customer
{
    public class ExcelCustomerAddress
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Detail { get; set; }
    }
}
