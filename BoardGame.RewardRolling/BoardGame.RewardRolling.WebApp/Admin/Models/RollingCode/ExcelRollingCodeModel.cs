using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.RollingCode
{
    public class ExcelRollingCodeModel
    {
        public int? Index { get; set; }
        [Required]
        public string Serial { get; set; }
        public string RewardCode { get; set; }
        public decimal? RewardRate { get; set; }
        public int RowIndex { get; set; }
        public int DisplayedRowIndex { get; set; }
    }
}
