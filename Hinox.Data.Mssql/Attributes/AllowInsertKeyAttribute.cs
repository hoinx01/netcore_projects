using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Data.Mssql.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowInsertKeyAttribute : Attribute
    {
        public bool Value { get; set; }
    }
}
