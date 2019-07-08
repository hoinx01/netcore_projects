using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Models.Customer;

namespace BoardGame.RewardRolling.WebApp.Models.RollingCode
{
    public class CustomerRollViewModel
    {
        public string RollingCode { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
