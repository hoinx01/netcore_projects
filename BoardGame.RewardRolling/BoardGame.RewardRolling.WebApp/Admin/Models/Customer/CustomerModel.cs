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
        public string RollingCode { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Birthday Birthday { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
