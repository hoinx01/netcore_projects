using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Mongo.Filters
{
    public abstract class BaseMdPagingFilter<T> : BaseMdFilter<T>
    {
        public int Page { get; set; }
        public int Limit { get; set; }

        protected MdPagination GeneratePagination()
        {
            var pagination = new MdPagination();
            pagination.Skip = (Page - 1) * Limit;
            pagination.Limit = Limit;
            return pagination;
        }
    }
}
