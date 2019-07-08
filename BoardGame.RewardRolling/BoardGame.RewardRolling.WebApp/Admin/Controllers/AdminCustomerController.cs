using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.Customer;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/customers")]
    public class AdminCustomerController : BaseRestController
    {
        private readonly ICustomerService customerService;

        public AdminCustomerController(
            ICustomerService customerService
        )
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<CustomerFilterResponse> Filter([FromQuery] CustomerFilterRequest filterModel)
        {
            var result = await customerService.Filter(filterModel);
            return result;
        }
    }
}
