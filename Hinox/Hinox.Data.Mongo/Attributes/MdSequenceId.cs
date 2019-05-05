using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MdSequenceId : Attribute
    {
        public string Id { get; set; }
    }
}
