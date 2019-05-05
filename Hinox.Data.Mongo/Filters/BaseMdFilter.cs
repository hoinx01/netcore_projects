using Hinox.Data.Mongo.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Filters
{
    public abstract class BaseMdFilter<T>
    {
        public abstract MdFilterSpecification<T> GenerateFilterSpecification();
    }
}
