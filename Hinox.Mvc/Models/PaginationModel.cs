using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Mvc.Models
{
    public class PaginationModel
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
