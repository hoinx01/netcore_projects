using Hinox.Data.Mongo.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Dal.Entities
{
    [MdCollection(Name = "LongSequenceIds")]
    public class MdLongSequenceId : BaseStringIdMongoEntity
    {
        public long CurrentValue { get; set; }
    }
}
