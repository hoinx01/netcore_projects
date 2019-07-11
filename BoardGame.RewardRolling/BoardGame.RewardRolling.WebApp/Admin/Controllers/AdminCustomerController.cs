using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGame.RewardRolling.Core.Configurations.ExcelImportExport;
using BoardGame.RewardRolling.WebApp.Admin.Models.Customer;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using Hinox.Office.Utils;
using Hinox.Static.Application;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/customers")]
    public class AdminCustomerController : BaseRestController
    {
        private readonly ICustomerService customerService;

        public AdminCustomerController(
            ICustomerService customerService
        )
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<CustomerFilterResponse> Filter([FromQuery] CustomerFilterRequest filterModel)
        {
            var result = await customerService.Filter(filterModel);
            return result;
        }

        [HttpGet]
        [Route("excel_files")]
        public async Task<object> Export([FromQuery] ExportCustomerFilterRequest exportFilterModel)
        {
            var customerFilterModel = Mapper.Map<CustomerFilterRequest>(exportFilterModel);
            customerFilterModel.Limit = 200;

            var excelCustomers = new List<ExcelCustomerModel>();

            var page = 1;
            int count;
            do
            {
                customerFilterModel.Page = page;
                var customerFilterResult = await customerService.Filter(customerFilterModel);
                var excelModels = ConvertToExcelCustomer(exportFilterModel.TemplateName, customerFilterResult.Customers);
                if(excelModels.Count > 0)
                    excelCustomers.AddRange(excelModels);

                page++;
                count = customerFilterResult.Pagination.Count;
            } while (count > 0);

            var exportConfigs = AppSettings.Get<ExportCustomer>("ExcelFileLayouts:ExportCustomer");

            if(!exportConfigs.Templates.ContainsKey(exportFilterModel.TemplateName))
                throw new NotFoundException("templateName không hợp lệ hoặc thiếu templateConfig");

            var templateConfig = exportConfigs.Templates[exportFilterModel.TemplateName];

            var templateFileUrl = templateConfig.TemplateFilePath;
            var templateWorkbook = new XSSFWorkbook(templateFileUrl);

            var filePath = GenerateExportedFilePath();

            var fileStream = System.IO.File.Create(filePath);
            templateWorkbook.Write(fileStream);

            var workbook = new XSSFWorkbook(filePath);
            var sheet = workbook.GetSheetAt(templateConfig.SheetIndex);

            var columnNameCodeFieldNameMap = templateConfig.ColumnNameFieldNameMap; //settings
            var codeColumnHeaderRange = new CellRangeAddress(
                templateConfig.CodeHeaderRange.FirstRow,
                templateConfig.CodeHeaderRange.LastRow,
                templateConfig.CodeHeaderRange.FirstColumn,
                templateConfig.CodeHeaderRange.LastColumn
            ); //settings

            var fieldColumnIndicates = ExcelUtils.GetFieldColumnIndex<ExcelCustomerModel>(sheet, codeColumnHeaderRange, columnNameCodeFieldNameMap);

            int currentRowIndex = templateConfig.FirstDataRow;
            foreach (var excelCustomer in excelCustomers)
            {
                var row = sheet.GetRow(currentRowIndex) ?? sheet.CreateRow(currentRowIndex);
                ExcelUtils.WriteObjectToRow<ExcelCustomerModel>(excelCustomer, row, fieldColumnIndicates);
                currentRowIndex++;
            }

            var writeStream = System.IO.File.OpenWrite(filePath);
            workbook.Write(writeStream);

            return new
            {
                url = filePath
            };
        }

        private string GenerateExportedFilePath()
        {
            return DateTime.Now.Ticks.ToString();
        }

        private List<ExcelCustomerModel> ConvertToExcelCustomer(string templateName, List<CustomerModel> customers)
        {
            return new List<ExcelCustomerModel>();
        }
    }
}
