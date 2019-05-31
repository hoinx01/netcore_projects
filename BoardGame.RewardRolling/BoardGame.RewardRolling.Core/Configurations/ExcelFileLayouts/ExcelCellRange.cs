using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Configurations.ExcelFileLayouts
{
    public class ExcelCellRange
    {
        public int FirstRow { get; set; }
        public int LastRow { get; set; }
        public int FirstColumn { get; set; }
        public int LastColumn { get; set; }
    }
}
