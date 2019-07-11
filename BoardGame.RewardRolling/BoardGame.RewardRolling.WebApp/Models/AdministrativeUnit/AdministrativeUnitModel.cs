using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Core.Statics;

namespace BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit
{
    public class CityModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CityLevel Level { get; set; }
        public List<DistrictModel> Districts { get; set; }
    }

    public class DistrictModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DistrictLevel Level { get; set; }
        public List<CommuneModel> Communes { get; set; }
    }

    public class CommuneModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CommuneLevel Level { get; set; }
    }
}
