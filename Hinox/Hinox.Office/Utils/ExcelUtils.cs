using Hinox.Mvc.Exceptions;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
                if(detectedPropertyNames.Contains(key.Trim().ToLower()))
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
                SetObjectPropertyValueFromCell(initialValue, cell, propertyColumnIndex.Property);
            }
            return initialValue;
        }

        public static void SetObjectPropertyValueFromCell<T>(T obj, ICell cell, PropertyInfo property)
        {
            var propertyDataType = property.DeclaringType;
            var cellTypeDataType = CellTypeDataType.List.FirstOrDefault(f => f.DataType.Equals(propertyDataType));
            if (cellTypeDataType == null)
                throw new Exception("Missing celltype when convert from " + propertyDataType.Name);

            var cellType = cellTypeDataType.CellType;

            if (propertyDataType == typeof(int))
                property.SetValue(obj, cellType == CellType.Blank ? default : (int)cell.NumericCellValue);
            if (propertyDataType == typeof(int?))
                property.SetValue(obj, cellType == CellType.Blank ? default : (int?) cell.NumericCellValue);
            if (propertyDataType == typeof(long))
                property.SetValue(obj, cellType == CellType.Blank ? default : (long)cell.NumericCellValue);
            if (propertyDataType == typeof(long?))
                property.SetValue(obj, cellType == CellType.Blank ? default : (long?)cell.NumericCellValue);
            if (propertyDataType == typeof(byte))
                property.SetValue(obj, cellType == CellType.Blank ? default : (byte)cell.NumericCellValue);
            if (propertyDataType == typeof(byte?))
                property.SetValue(obj, cellType == CellType.Blank ? default : (byte?)cell.NumericCellValue);
            if (propertyDataType == typeof(decimal))
                property.SetValue(obj, cellType == CellType.Blank ? default : (decimal)cell.NumericCellValue);
            if (propertyDataType == typeof(decimal?))
                property.SetValue(obj, cellType == CellType.Blank ? default : (decimal?)cell.NumericCellValue);
            if (propertyDataType == typeof(bool))
                property.SetValue(obj, cellType == CellType.Blank ? default : (bool)cell.BooleanCellValue);
            if (propertyDataType == typeof(bool?))
                property.SetValue(obj, cellType == CellType.Blank ? default : (bool?)cell.BooleanCellValue);
            if (propertyDataType == typeof(string))
                property.SetValue(obj, cellType == CellType.Blank ? default : cell.StringCellValue);
            if (propertyDataType == typeof(DateTime))
                property.SetValue(obj, cellType == CellType.Blank ? default : (DateTime) cell.DateCellValue);
            if (propertyDataType == typeof(DateTime?))
                property.SetValue(obj, cellType == CellType.Blank ? default : (DateTime?)cell.DateCellValue);
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
