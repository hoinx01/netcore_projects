using BoardGame.RewardRolling.Core.ValueObjects;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Models.Customer
{
    public class CustomerViewModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Birthday Birthday { get; set; }
        public int GenderId { get; set; }
        public Address Address { get; set; }
        public List<CityModel> Cities { get; set; }
        public List<DistrictModel> Districts { get; set; }
    }
}
