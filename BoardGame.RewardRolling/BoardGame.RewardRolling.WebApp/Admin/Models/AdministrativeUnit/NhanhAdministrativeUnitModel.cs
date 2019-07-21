using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit
{
    public class NhanhCityModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<NhanhDistrictModel> Districts { get; set; }
    }

    public class NhanhDistrictModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
