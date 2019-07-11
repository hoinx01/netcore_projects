using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit
{
    public class UploadAdministrativeUnitResult
    {
        public int TotalRecords { get; set; }
        public int SuccessRecordCount { get; set; }
        public int FailedRecordCount { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
