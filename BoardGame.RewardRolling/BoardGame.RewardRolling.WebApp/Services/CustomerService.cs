using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Filters;
using BoardGame.RewardRolling.WebApp.Admin.Models.Customer;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Models;
using Hinox.Static.Enumerate;

namespace BoardGame.RewardRolling.WebApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMdCustomerDao customerDao;
        private readonly IMdRewardDao rewardDao;
        private readonly IMdCityDao cityDao;
        private readonly IMdDistrictDao districtDao;

        public CustomerService(
            IMdCustomerDao customerDao,
            IMdRewardDao rewardDao,
            IMdCityDao cityDao,
            IMdDistrictDao districtDao
        )
        {
            this.customerDao = customerDao;
            this.rewardDao = rewardDao;
            this.cityDao = cityDao;
            this.districtDao = districtDao;
        }
        public async Task<CustomerModel> GetById(string id)
        {
            var mdCustomer = await customerDao.GetByIdAsync(new MongoDB.Bson.ObjectId(id));
            if (mdCustomer == null)
                return null;
            var rewardId = mdCustomer.Rewards[0].RewardId;
            var mdReward = await rewardDao.GetByIdAsync(new MongoDB.Bson.ObjectId(rewardId));

            var customer = Mapper.Map<CustomerModel>(mdCustomer);
            var reward = new CustomerRewardModel()
            {
                Id = mdReward.Id.ToString(),
                Name = mdReward.Name,
                ImageUrl = StaticVariables.ImageBaseUrl + mdReward.ImageSrc
            };
            customer.Reward = reward;

            if(customer.Address != null)
            {
                var detail = customer.Address.Detail ?? customer.Address.Label;

                string provinceName = "";
                if(customer.Address.ProvinceId != null)
                {
                    var province = await cityDao.GetByIdAsync(customer.Address.ProvinceId);
                    if (province != null)
                        provinceName = province.Name;
                }

                string districtName = "";
                if(customer.Address.DistrictId != null)
                {
                    var district = await districtDao.GetByIdAsync(customer.Address.DistrictId);
                    if (district != null)
                        districtName = district.Name;
                }

                customer.DisplayedAddress = string.Format("{0} - {1} - {2}", detail, districtName, provinceName);
            }


            if(customer.GenderId > 0)
            {
                var gender = Enumeration.FromValue<Gender>(customer.GenderId);
                if (gender != null)
                    customer.GenderName = gender.DisplayedName;
            }

            return customer;
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

            var rewards = mdCustomers.Select(s => s.Rewards[0]).ToList();
            var rewardIds = rewards.Select(s => s.RewardId).ToHashSet().ToList();

            var mdRewardFilter = new MdRewardFilter()
            {
                Ids = rewardIds
            };
            var mdRewards = await rewardDao.Filter(mdRewardFilter);

            var customerRewards = mdRewards.Select(s => new CustomerRewardModel()
            {
                Id = s.Id.ToString(),
                Name = s.Name,
                ImageUrl = StaticVariables.ImageBaseUrl + s.ImageSrc
            }).ToList();


            var customerModels = new List<CustomerModel>();

            foreach(var mdCustomer in mdCustomers)
            {
                var customerModel = Mapper.Map<CustomerModel>(mdCustomer);
                var customerReward = customerRewards.FirstOrDefault(f => f.Id.Equals(mdCustomer.Rewards[0].RewardId));
                customerModel.Reward = customerReward;
                customerModels.Add(customerModel);
            }

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
