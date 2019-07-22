using Hinox.Mvc.Exceptions;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Hinox.Office.Utils
{
    public class PropertyColumnIndex
    {
        public PropertyInfo Property { get; set; }
        public int ColumnIndex { get; set; }
    }
    public class ExcelUtils
    {
        public static List<PropertyColumnIndex> GetFieldColumnIndex<T>(ISheet sheet, CellRangeAddress cellRange, Dictionary<string, string> columnNameFieldNameMap)
        {
            if (cellRange.FirstRow != cellRange.LastRow)
                throw new UnprocessableEntityException("Tiêu đề cho 1 kiểu dữ liệu phải nằm trên cùng 1 hàng");
            if(cellRange.NumberOfCells == 0)
                throw new UnprocessableEntityException("Cấu hình tiêu đề cột không hợp lệ: NumberOfCells = 0");
            if(columnNameFieldNameMap == null || columnNameFieldNameMap.Count == 0)
                throw new UnprocessableEntityException("Cấu hình tiêu đề cột không hợp lệ: Blank or Null value");

            int rowIndex = cellRange.FirstRow;
            var row = sheet.GetRow(rowIndex);

            var columnNameFieldNameLowerMap = new Dictionary<string, string>();
            foreach(var key in columnNameFieldNameMap.Keys)
            {
                var fieldName = columnNameFieldNameMap[key];
                if(string.IsNullOrWhiteSpace(fieldName))
                    continue;
                columnNameFieldNameLowerMap.Add(key.Trim().ToLower(), columnNameFieldNameMap[key].Trim().ToLower());
            }

            var properties = typeof(T).GetProperties();

            var result = new List<PropertyColumnIndex>();
            for (int i = cellRange.FirstColumn; i <= cellRange.LastColumn; i++)
            {
                var cell = row.GetCell(i);
                if (cell == null)
                    continue;

                var cellValue = cell.StringCellValue;
                var lowerCellValue = cellValue.ToLower().Trim();

                if (!columnNameFieldNameLowerMap.ContainsKey(lowerCellValue))
                    continue;

                var lowerFieldName = columnNameFieldNameLowerMap[lowerCellValue];
                var property = properties.FirstOrDefault(f => f.Name.ToLower() == lowerFieldName);
                if (property == null)
                    continue;

                var propertyColumnIndex = new PropertyColumnIndex()
                {
                    Property = property,
                    ColumnIndex = i
                };
                result.Add(propertyColumnIndex);
            }

            List<string> missingColumnNames = new List<string>();

            var detectedPropertyNames = result.Select(s => s.Property.Name.ToLower()).ToList();
            foreach(var key in columnNameFieldNameMap.Keys)
            {
                if(detectedPropertyNames.Contains(columnNameFieldNameMap[key].Trim().ToLower()))
                    continue;
                missingColumnNames.Add(key);
            }
            if(missingColumnNames.Count > 0)
                throw new UnprocessableEntityException(string.Format("Không tìm thấy các cột: {0}", string.Join(",", missingColumnNames)));

            return result;
        }

        public static T ReadObjectFromRow<T>(IRow row, List<PropertyColumnIndex> propertyColumnIndices, int keyColumnIndex)
        {
            if (row == null)
                return default;
            if (propertyColumnIndices == null || propertyColumnIndices.Count == 0)
                throw new UnprocessableEntityException("Cấu hình tiêu đề cột không hợp lệ: Blank or Null value");
            var keyCell = row.GetCell(keyColumnIndex);
            if (keyCell == null || keyCell.CellType == CellType.Blank)
                return default;

            var initialValue = Activator.CreateInstance<T>();

            foreach (var propertyColumnIndex in propertyColumnIndices)
            {
                var cell = row.GetCell(propertyColumnIndex.ColumnIndex);
                try
                {
                    SetObjectPropertyValueFromCell(initialValue, cell, propertyColumnIndex.Property);
                }
                catch(Exception e)
                {
                    throw;
                }
            }
            return initialValue;
        }

        public static void SetObjectPropertyValueFromCell<T>(T obj, ICell cell, PropertyInfo property)
        {
            if (cell == null)
                return;

            var propertyDataType = property.PropertyType;

            var cellType = cell.CellType;

            object cellValue = null;
            if(cellType == CellType.Boolean)
                cellValue = cell.BooleanCellValue;
            else if (cellType == CellType.Numeric)
                cellValue = cell.NumericCellValue;
            else if (cellType == CellType.String)
                cellValue = cell.StringCellValue;

            var fieldValue = ConvertData(cellValue, propertyDataType);
            property.SetValue(obj, fieldValue);
        }

        public static object ConvertData(object inputValue, Type outputType)
        {
            if (inputValue == null)
            {
                try
                {
                    return Activator.CreateInstance(outputType);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
                

            var inputType = inputValue.GetType();

            if (outputType == typeof(int))
                return Convert.ToInt32(inputValue);
            else if (outputType == typeof(int?))
                return new int?(Convert.ToInt32(inputValue));

            else if (outputType == typeof(long))
                return Convert.ToInt64(inputValue);
            else if (outputType == typeof(long?))
                return new long?(Convert.ToInt64(inputValue));

            else if (outputType == typeof(decimal))
                return Convert.ToDecimal(inputValue);
            else if (outputType == typeof(decimal?))
                return new decimal?(Convert.ToDecimal(inputValue));

            else if (outputType == typeof(double))
                return Convert.ToDouble(inputValue);
            else if (outputType == typeof(double?))
                return new double?(Convert.ToDouble(inputValue));

            else if (outputType == typeof(string))
                return Convert.ToString(inputValue);

            else if (outputType == typeof(bool))
                return Convert.ToBoolean(inputValue);
            else if (outputType == typeof(bool?))
                return new bool?(Convert.ToBoolean(inputValue));

            else if (outputType == typeof(DateTime))
                return Convert.ToDateTime(inputValue);
            else if (outputType == typeof(DateTime?))
                return new DateTime?(Convert.ToDateTime(inputValue));

            throw new Exception(string.Format("Missing converter to convert from {0} to {1}", inputType.Name, outputType.Name));
        }

        public static void WriteObjectToRow<T>(T obj, IRow row, List<PropertyColumnIndex> propertyColumnIndices)
        {
            foreach (var propertyColumnIndex in propertyColumnIndices)
            {
                var prop = propertyColumnIndex.Property;
                var columnIndex = propertyColumnIndex.ColumnIndex;
                var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

                var propValue = prop.GetValue(obj);

                SetCellValue(cell, propValue);
            }
        }

        public static void SetCellValue(ICell cell, object cellValue)
        {
            if (cellValue == null)
                return;

            var cellDataType = cellValue.GetType();
            var cellType = CellTypeDataType.List.FirstOrDefault(f => ReferenceEquals(cellDataType, f.DataType)).CellType;

            cell.SetCellType(cellType);

            if (cellDataType == typeof(int))
                cell.SetCellValue(Convert.ToInt32(cellValue));
            else if (cellDataType == typeof(int?))
                cell.SetCellValue(Convert.ToInt32(cellValue));

            else if (cellDataType == typeof(long))
                cell.SetCellValue(Convert.ToInt64(cellValue));
            else if (cellDataType == typeof(long?))
                cell.SetCellValue(Convert.ToInt64(cellValue));

            else if (cellDataType == typeof(decimal))
                cell.SetCellValue(Convert.ToDouble(cellValue));
            else if (cellDataType == typeof(decimal?))
                cell.SetCellValue(Convert.ToDouble(cellValue));

            else if (cellDataType == typeof(double))
                cell.SetCellValue(Convert.ToDouble(cellValue));
            else if (cellDataType == typeof(double?))
                cell.SetCellValue(Convert.ToDouble(cellValue));

            else if (cellDataType == typeof(string))
                cell.SetCellValue(Convert.ToString(cellValue));

            else if (cellDataType == typeof(bool))
                cell.SetCellValue(Convert.ToBoolean(cellValue));
            else if (cellDataType == typeof(bool?))
                cell.SetCellValue(Convert.ToBoolean(cellValue));

            else if (cellDataType == typeof(DateTime))
                cell.SetCellValue(Convert.ToDateTime(cellValue));
            else if (cellDataType == typeof(DateTime?))
                cell.SetCellValue(Convert.ToDateTime(cellValue));
        }
    }

    public class CellTypeDataType
    {
        public CellType CellType { get; set; }
        public Type DataType { get; set; }

        public CellTypeDataType(CellType cellType, Type dataType)
        {
            CellType = cellType;
            DataType = dataType;
        }

        public static List<CellTypeDataType> List = new List<CellTypeDataType>()
        {
            new CellTypeDataType(CellType.Numeric, typeof(int)),
            new CellTypeDataType(CellType.Numeric, typeof(int?)),
            new CellTypeDataType(CellType.Numeric, typeof(long)),
            new CellTypeDataType(CellType.Numeric, typeof(long?)),
            new CellTypeDataType(CellType.Numeric, typeof(byte)),
            new CellTypeDataType(CellType.Numeric, typeof(byte?)),
            new CellTypeDataType(CellType.Numeric, typeof(decimal)),
            new CellTypeDataType(CellType.Numeric, typeof(decimal?)),
            new CellTypeDataType(CellType.String, typeof(string)),
            new CellTypeDataType(CellType.Boolean, typeof(bool)),
            new CellTypeDataType(CellType.Boolean, typeof(bool?)),
            new CellTypeDataType(CellType.String, typeof(DateTime)),
            new CellTypeDataType(CellType.String, typeof(DateTime?)),
        };
    }
}
