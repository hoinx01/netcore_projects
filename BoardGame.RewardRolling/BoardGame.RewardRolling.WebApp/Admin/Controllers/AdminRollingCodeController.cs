using BoardGame.RewardRolling.Core.Configurations.ExcelImportExport;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.Data.Mongo.Filters;
using BoardGame.RewardRolling.WebApp.Admin.Models.RollingCode;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Models;
using Hinox.Office.Utils;
using Hinox.Static.Application;
using Hinox.Static.Extensions;
using Hinox.Static.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NLog;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/rolling_codes")]
    public class AdminRollingCodeController : BaseRestController
    {
        private readonly Logger logger = LogManager.GetLogger("UploadFile");
        private readonly IMdRollingCodeDao mdRollingCodeDao;
        private readonly IUploadService uploadService;

        public AdminRollingCodeController(
            IMdRollingCodeDao mdRollingCodeDao,
            IUploadService uploadService
            )
        {
            this.mdRollingCodeDao = mdRollingCodeDao;
            this.uploadService = uploadService;
        }

        [HttpPost]
        [Route("excel_files")]
        public async Task<UploadCodeResultModel> UploadFile([FromForm] IFormFile file, [FromForm] string type)
        {
            try
            {
                logger.Info(string.Format("fileName:{0}, contentType:{1}, name: {2}, length: {3}", file.FileName, file.ContentType, file.Name, file.Length));
            }
            catch (Exception e)
            {
                logger.Error(e);
            }

            var fullPath = await uploadService.StoreUploadedFile(file);

            var importedFileLayout = AppSettings.Get<ImportRollingCodeFileLayout>("ExcelFileLayouts:ImportRollingCode");

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
            var codePropertyColumnIndicates = ExcelUtils.GetFieldColumnIndex<ExcelRollingCodeModel>(sheet, codeColumnHeaderRange, columnNameCodeFieldNameMap);
            var keyColumnIndex = codePropertyColumnIndicates.FirstOrDefault(f => f.Property.Name.ToLower() == columnNameCodeFieldNameMap[keyColumnName].ToLower()).ColumnIndex;
            var firstRow = importedFileLayout.FirstDataRow; //settings
            var maxBlankRow = importedFileLayout.MaxBlankRow; //settings

            var excelCodes = new List<ExcelRollingCodeModel>();

            int blankRow = 0;
            for (int i = firstRow; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var code = ExcelUtils.ReadObjectFromRow<ExcelRollingCodeModel>(row, codePropertyColumnIndicates, keyColumnIndex);

                if(code == null)
                {
                    blankRow++;
                    if (blankRow == maxBlankRow)
                        break;
                }
                else
                {
                    blankRow = 0;
                    code.RowIndex = i;
                    code.DisplayedRowIndex = i + 1;
                    excelCodes.Add(code);
                }
            }

            var rewardCodes = excelCodes
                .Where(w => !string.IsNullOrWhiteSpace(w.RewardCode))
                .Select(s => s.RewardCode)
                .ToList();

            var result = new UploadCodeResultModel()
            {
                FailedRecordCount = 0,
                SuccessRecordCount = 0,
                TotalRecords = 0,
                Errors = new List<ErrorRollingCode>()
            };

            foreach (var excelCode in excelCodes)
            {
                var errors = ObjectUtils.ValidateObject(excelCode);

                if (errors.Count > 0)
                {
                    result.FailedRecordCount += 1;
                    result.Errors.Add(new ErrorRollingCode()
                    {
                        RowIndex = excelCode.DisplayedRowIndex,
                        ErrorMessage = string.Join(",", errors.Values.ToList())
                    });
                    continue;
                }

                var mdCode = new MdRollingCode()
                {
                    Id = ObjectId.GenerateNewId(),
                    Serial = excelCode.Serial,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    ActivatedAt = DateTime.UtcNow,
                    Status = RollingCodeStatus.Active.Id
                };
                try
                {
                    await mdRollingCodeDao.AddAsync(mdCode);
                    result.SuccessRecordCount += 1;
                }
                catch(Exception e)
                {
                    result.FailedRecordCount += 1;
                    result.Errors.Add(new ErrorRollingCode()
                    {
                        RowIndex = excelCode.DisplayedRowIndex,
                        ErrorMessage = e.Message
                    });
                }
            }

            return result;
        }

        public async Task<RollingCodeFilterResult> Filter([FromQuery] RollingCodeFilterRequest filterModel)
        {
            var mdCodeFilter = new MdRollingCodeFilter()
            {
                Page = filterModel.Page,
                Limit = filterModel.Limit
            };
            var count = await mdRollingCodeDao.Count(mdCodeFilter);
            if (count == 0)
                return new RollingCodeFilterResult()
                {
                    RollingCodes = new List<RollingCodeModel>(),
                    Pagination = new PaginationModel()
                    {
                        Count = 0,
                        Limit = filterModel.Limit,
                        Page = filterModel.Page
                    }
                };

            var codes = await mdRollingCodeDao.Filter(mdCodeFilter);
            var codeModes = codes.Select(s => new RollingCodeModel(s)).ToList();

            var result = new RollingCodeFilterResult()
            {
                RollingCodes = codeModes,
                Pagination = new PaginationModel()
                {
                    Count = count,
                    Limit = filterModel.Limit,
                    Page = filterModel.Page
                }
            };
            return result;
        }
    }
}
