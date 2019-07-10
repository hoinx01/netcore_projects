using System.Collections.Generic;

namespace BoardGame.RewardRolling.Core.Configurations.ExcelImportExport
{
    public class ExportCustomer
    {
        public Dictionary<string, ExportCustomerFileLayout> Templates { get; set; }
    }

    public class ExportCustomerFileLayout
    {
        public string TemplateFilePath { get; set; }
        public int SheetIndex { get; set; }
        public Dictionary<string, string> ColumnNameFieldNameMap { get; set; }
        public ExcelCellRange CodeHeaderRange { get; set; }
        public int FirstDataRow { get; set; }
    }
}
