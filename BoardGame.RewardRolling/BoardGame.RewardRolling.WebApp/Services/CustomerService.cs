using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Filters;
using BoardGame.RewardRolling.WebApp.Admin.Models.Customer;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Models;

namespace BoardGame.RewardRolling.WebApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMdCustomerDao customerDao;

        public CustomerService(
            IMdCustomerDao customerDao
        )
        {
            this.customerDao = customerDao;
        }

        public async Task<CustomerFilterResponse> Filter(CustomerFilterRequest filterModel)
        {
            var mdFilter = new MdCustomerFilter()
            {
                Page = filterModel.Page,
                Limit = filterModel.Limit,
                CreatedAtMin = filterModel.CreatedAtMin,
                CreatedAtMax = filterModel.CreatedAtMax
            };
            var mdCustomers = await customerDao.Filter(mdFilter);

            var customerModels = mdCustomers.Select(s => Mapper.Map<CustomerModel>(s)).ToList();

            var count = await customerDao.Count(mdFilter);

            var result = new CustomerFilterResponse()
            {
                Customers = customerModels,
                Pagination = new PaginationModel()
                {
                    Count = count,
                    Limit = filterModel.Limit,
                    Page = filterModel.Page
                }
            };

            return result;
        }
    }
}
