using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit
{
    public class ExcelStandardAdministrativeUnitModel
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string CommuneId { get; set; }
        public string CommuneName { get; set; }
    }
    public class ExcelStandardProvinceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LevelName { get; set; }
    }

    public class ExcelStandardDistrictModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LevelName { get; set; }
    }

    public class ExcelStandardCommuneModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LevelName { get; set; }
    }
}
