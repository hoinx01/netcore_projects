using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.Customer;

namespace BoardGame.RewardRolling.WebApp.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerFilterResponse> Filter(CustomerFilterRequest filterModel);
        Task<CustomerModel> GetById(string id);
    }
}
