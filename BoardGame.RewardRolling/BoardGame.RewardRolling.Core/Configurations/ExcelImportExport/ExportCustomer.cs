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
        public Dictionary<string, string> CustomerColumnNameFieldNameMap { get; set; }
        public Dictionary<string, string> CustomerAddressColumnNameFieldNameMap { get; set; }
        public ExcelCellRange CustomerCodeHeaderRange { get; set; }
        public ExcelCellRange CustomerAddressCodeHeaderRange { get; set; }
        public int FirstDataRow { get; set; }
    }
}
