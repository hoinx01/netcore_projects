using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hinox.Mvc.Models;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Customer
{
    public class CustomerFilterResponse : BasePagingFilterResponse
    {
        public List<CustomerModel> Customers { get; set; }  
    }
}
