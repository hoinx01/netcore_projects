using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGame.RewardRolling.Core.Configurations.ExcelImportExport;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Core.ValueObjects;
using BoardGame.RewardRolling.WebApp.Admin.Models.Customer;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using Hinox.Office.Utils;
using Hinox.Static.Application;
using Hinox.Static.Enumerate;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/customers")]
    public class AdminCustomerController : BaseRestController
    {
        private readonly ICustomerService customerService;
        private readonly IAdministrativeUnitService administrativeUnitService;

        public AdminCustomerController(
            ICustomerService customerService,
            IAdministrativeUnitService administrativeUnitService
        )
        {
            this.customerService = customerService;
            this.administrativeUnitService = administrativeUnitService;
        }

        [HttpGet]
        public async Task<CustomerFilterResponse> Filter([FromQuery] CustomerFilterRequest filterModel)
        {
            var result = await customerService.Filter(filterModel);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<CustomerModel> GetById([FromRoute] string id)
        {
            var customer = await customerService.GetById(id);
            if (customer == null)
                throw new NotFoundException();
            return customer;
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
                var excelModels = await ConvertToExcelCustomerAsync(exportFilterModel.TemplateName, customerFilterResult.Customers);
                if(excelModels.Count > 0)
                    excelCustomers.AddRange(excelModels);

                page++;
                count = customerFilterResult.Customers.Count;
            } while (count > 0);

            var exportConfigs = AppSettings.Get<ExportCustomer>("ExcelFileLayouts:ExportCustomer");

            if(!exportConfigs.Templates.ContainsKey(exportFilterModel.TemplateName))
                throw new NotFoundException("templateName không hợp lệ hoặc thiếu templateConfig");

            var templateConfig = exportConfigs.Templates[exportFilterModel.TemplateName];

            var templateFileUrl = templateConfig.TemplateFilePath;
            //var templateWorkbook = new XSSFWorkbook(templateFileUrl);

            //var filePath = GenerateExportedFilePath();

            //using (var fileStream = System.IO.File.Create(filePath))
            //{
            //    templateWorkbook.Write(fileStream);
            //    fileStream.Close();
            //}

            //var workbook = new XSSFWorkbook(filePath);

            var workbook = new XSSFWorkbook(templateFileUrl);
            var sheet = workbook.GetSheetAt(templateConfig.SheetIndex);

            var customerColumnNameCodeFieldNameMap = templateConfig.CustomerColumnNameFieldNameMap; //settings
            var customerColumnHeaderRange = new CellRangeAddress(
                templateConfig.CustomerCodeHeaderRange.FirstRow,
                templateConfig.CustomerCodeHeaderRange.LastRow,
                templateConfig.CustomerCodeHeaderRange.FirstColumn,
                templateConfig.CustomerCodeHeaderRange.LastColumn
            ); //settings

            var customerFieldColumnIndicates = ExcelUtils.GetFieldColumnIndex<ExcelCustomerModel>(
                sheet, 
                customerColumnHeaderRange, 
                customerColumnNameCodeFieldNameMap
                );

            var customerAddressColumnNameCodeFieldNameMap = templateConfig.CustomerAddressColumnNameFieldNameMap; //settings
            var customerAddressColumnHeaderRange = new CellRangeAddress(
                templateConfig.CustomerAddressCodeHeaderRange.FirstRow,
                templateConfig.CustomerAddressCodeHeaderRange.LastRow,
                templateConfig.CustomerAddressCodeHeaderRange.FirstColumn,
                templateConfig.CustomerAddressCodeHeaderRange.LastColumn
            ); //settings

            var customerAddressFieldColumnIndicates = ExcelUtils.GetFieldColumnIndex<ExcelCustomerAddress>(
                sheet, 
                customerAddressColumnHeaderRange, 
                customerAddressColumnNameCodeFieldNameMap
                );

            int currentRowIndex = templateConfig.FirstDataRow;
            foreach (var excelCustomer in excelCustomers)
            {
                var row = sheet.GetRow(currentRowIndex) ?? sheet.CreateRow(currentRowIndex);
                ExcelUtils.WriteObjectToRow<ExcelCustomerModel>(excelCustomer, row, customerFieldColumnIndicates);
                ExcelUtils.WriteObjectToRow<ExcelCustomerAddress>(excelCustomer.Address, row, customerAddressFieldColumnIndicates);
                currentRowIndex++;
            }


            var filePath = GenerateExportedFilePath();
            var writeStream = System.IO.File.Create(filePath);
            workbook.Write(writeStream);

            return new
            {
                url = Request.Scheme + "://" + Request.Host.Value + filePath.Replace("wwwroot", "")
            };
        }

        private string GenerateExportedFilePath()
        {
            return "wwwroot/iofiles/download/export_customer-" + DateTime.Now.Ticks.ToString() + ".xlsx";
        }

        private async Task<List<ExcelCustomerModel>> ConvertToExcelCustomerAsync(
            string templateName, 
            List<CustomerModel> customers
            )
        {
            var result = new List<ExcelCustomerModel>();
            if (customers == null || customers.Count == 0)
                return result;

            for (int i = 0; i < customers.Count; i++)
            {
                var customer = customers[i];
                var excelCustomer = new ExcelCustomerModel()
                {
                    Id = customer.Id,
                    FullName = customer.FullName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    Birthday = string.Format(
                        "{0}/{1}/{2}",
                        customer.Birthday.Day,
                        customer.Birthday.Month,
                        customer.Birthday.Year),
                    Gender = customer.GenderId <= 0 ? "" : Enumeration.FromValue<Gender>(customer.GenderId).Name,
                    CreatedAt = customer.CreatedAt,
                    ModifiedAt = customer.ModifiedAt

                };
                excelCustomer.Address = await administrativeUnitService.MapAddressToExcelAddress(customer.Address);
                result.Add(excelCustomer);
            }

            return result;
        }
    }
}
