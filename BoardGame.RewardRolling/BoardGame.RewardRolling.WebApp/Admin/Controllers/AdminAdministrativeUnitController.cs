using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Core.Configurations.ExcelImportExport;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Admin.Models.RollingCode;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Hinox.Office.Utils;
using Hinox.Static.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/administrative_units")]
    public class AdminAdministrativeUnitController : BaseRestController
    {
        private readonly IUploadService uploadService;
        private readonly IAdministrativeUnitService administrativeUnitService;

        public AdminAdministrativeUnitController(
            IUploadService uploadService,
            IAdministrativeUnitService administrativeUnitService
        )
        {
            this.uploadService = uploadService;
            this.administrativeUnitService = administrativeUnitService;
        }

        [HttpPost]
        [Route("excel_files")]
        public async Task<UploadAdministrativeUnitResult> Upload([FromForm] IFormFile file)
        {
            var fullPath = await uploadService.StoreUploadedFile(file);

            var importedFileLayout = AppSettings.Get<ImportStandardAdministrativeUnitFileLayout>("ExcelFileLayouts:ImportStandardAdministrativeUnit");

            var workbook = new XSSFWorkbook(fullPath);

            var sheet = workbook.GetSheetAt(importedFileLayout.SheetIndex);

            Dictionary<string, string> columnNameCodeFieldNameMap = importedFileLayout.ColumnNameFieldNameMap; //settings
            var codeColumnHeaderRange = new CellRangeAddress(
                importedFileLayout.CodeHeaderRange.FirstRow,
                importedFileLayout.CodeHeaderRange.LastRow,
                importedFileLayout.CodeHeaderRange.FirstColumn,
                importedFileLayout.CodeHeaderRange.LastColumn
            ); //settings
            var keyColumnName = importedFileLayout.KeyColumnName; //settings
            var codePropertyColumnIndicates = ExcelUtils.GetFieldColumnIndex<ExcelStandardAdministrativeUnitModel>(sheet, codeColumnHeaderRange, columnNameCodeFieldNameMap);
            var keyColumnIndex = codePropertyColumnIndicates.FirstOrDefault(f => f.Property.Name.ToLower() == columnNameCodeFieldNameMap[keyColumnName].ToLower()).ColumnIndex;
            var firstRow = importedFileLayout.FirstDataRow; //settings
            var maxBlankRow = importedFileLayout.MaxBlankRow; //settings

            var excelModels = new List<ExcelStandardAdministrativeUnitModel>();

            int blankRow = 0;
            for (int i = firstRow; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var excelModel = ExcelUtils.ReadObjectFromRow<ExcelStandardAdministrativeUnitModel>(row, codePropertyColumnIndicates, keyColumnIndex);

                if (excelModel == null)
                {
                    blankRow++;
                    if (blankRow == maxBlankRow)
                        break;
                }
                else
                {
                    blankRow = 0;
                    excelModels.Add(excelModel);
                }
            }

            await administrativeUnitService.ImportExcelAdministrativeUnit(excelModels);

            var result = new UploadAdministrativeUnitResult()
            {
                TotalRecords = excelModels.Count,
                SuccessRecordCount = excelModels.Count,
                FailedRecordCount = 0,
                ErrorMessages = new List<string>()
            };
            return result;

        }
    }
}
