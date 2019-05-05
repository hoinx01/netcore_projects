using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MdCollectionAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
