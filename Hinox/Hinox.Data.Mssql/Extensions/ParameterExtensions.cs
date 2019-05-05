using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Hinox.Static.Utilities;

namespace Hinox.Data.Mssql.Extensions
{
    public static class ParameterExtensions
    {
        public static List<Type> SimpleTypes = new List<Type>()
        {
            typeof(int),
            typeof(int?),
            typeof(string),
            typeof(bool),
            typeof(bool?),
            typeof(decimal),
            typeof(decimal?)
        };
        public static DataTable ToTVP<T>(this List<T> input)
        {
            var elementType = input.GetElementType();

            var table = new DataTable();
            table.Columns.Add("Idx", typeof(int));

            if (SimpleTypes.Contains(elementType))
            {

                table.Columns.Add("Value");
                if (input == null || input.Count() == 0)
                    return table;

                for (int i = 1; i <= input.Count(); i++)
                {
                    var element = input.ElementAt(i - 1);
                    var row = table.NewRow();
                    row[0] = i;
                    row[1] = element;
                    table.Rows.Add(row);
                }
                return table;
            }

            var propertyNames = ObjectUtils.GetProperyNames(elementType);
            foreach (var propertyName in propertyNames)
                table.Columns.Add(propertyName);

            if (input == null || input.Count() == 0)
                return table;

            for (int i = 1; i <= input.Count(); i++)
            {
                var element = input.ElementAt(i - 1);
                var propertyValues = ObjectUtils.GetPropertyValues(element);
                var rowValues = new List<object>() { i };
                rowValues.AddRange(propertyValues);

                var row = table.NewRow();
                row.ItemArray = rowValues.ToArray();
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
