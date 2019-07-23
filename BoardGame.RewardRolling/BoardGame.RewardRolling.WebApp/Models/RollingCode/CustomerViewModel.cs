using BoardGame.RewardRolling.Core.ValueObjects;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Models.Customer
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(255, ErrorMessage = "{0} tối đa {1} ký tự")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9\(\)\ \-\+]{10,25}$", ErrorMessage = "Số điện thoại không đúng")]
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Email không đúng")]
        public string Email { get; set; }
        public Birthday Birthday { get; set; }
        public int GenderId { get; set; }
        public Address Address { get; set; }
        public List<CityModel> Cities { get; set; }
        public List<DistrictModel> Districts { get; set; }
    }
}
