using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.Customer
{
    public class CustomerModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Birthday Birthday { get; set; }
        public int GenderId { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public CustomerRewardModel Reward { get; set; }
        public string DisplayedAddress { get; set; }
        public string GenderName { get; set; }
    }
    public class CustomerRewardModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
