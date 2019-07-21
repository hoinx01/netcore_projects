using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit
{
    public class AddDistrictRequest
    {
        [Required]
        public string CityId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? LevelId { get; set; }
    }
}
