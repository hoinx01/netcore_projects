using BoardGame.RewardRolling.Core.Configurations.ExcelFileLayouts;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.Data.Mongo.Filters;
using BoardGame.RewardRolling.WebApp.Admin.Models.RollingCode;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Models;
using Hinox.Office.Utils;
using Hinox.Static.Application;
using Hinox.Static.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NLog;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/rolling_codes")]
    public class AdminRollingCodeController : BaseRestController
    {
        private readonly Logger logger = LogManager.GetLogger("UploadFile");
        private readonly IMdRollingCodeDao mdRollingCodeDao;

        public AdminRollingCodeController(
            IMdRollingCodeDao mdRollingCodeDao
            )
        {
            this.mdRollingCodeDao = mdRollingCodeDao;
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

            var fileDirectory = "wwwroot/iofiles/upload/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}";
            var directory = string.Format(fileDirectory, DateTime.Now);

            var originalPath = string.Format("{0}/{1}", directory, file.FileName);

            string extension = Path.GetExtension(originalPath);

            var newFileName = file.FileName + "-" + DateTime.Now.Ticks.ToString();

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var fullPath = string.Format("{0}/{1}{2}", directory, newFileName, extension);

            using (FileStream bits = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }

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

            

            foreach(var excelCode in excelCodes)
            {
                var errors = ObjectUtils.ValidateObject(excelCode);
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
                }
                catch(Exception e)
                {

                }
            }

            return new UploadCodeResultModel();
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
