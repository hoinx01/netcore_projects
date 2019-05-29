using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.RollingCode
{
    public class UploadCodeResultModel
    {
        public int TotalRecords { get; set; }
        public int SuccessRecordCount { get; set; }
        public int FailedRecordCount { get; set; }
        public List<ErrorRollingCode> Errors { get; set; }
    }
    public class ErrorRollingCode
    {
        public int RowIndex { get; set; }
        public string ErrorMessage { get; set; }
    }
}
