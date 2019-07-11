using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Configurations.ExcelImportExport
{
    public class ImportStandardAdministrativeUnitFileLayout
    {
        public int SheetIndex { get; set; }
        public string KeyColumnName { get; set; }
        public Dictionary<string, string> ColumnNameFieldNameMap { get; set; }
        public ExcelCellRange CodeHeaderRange { get; set; }
        public int FirstDataRow { get; set; }
        public int MaxBlankRow { get; set; }
    }
}
