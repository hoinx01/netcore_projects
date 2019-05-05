using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Mvc.Models
{
    public class BasePagingFilterRequest
    {
        private int page;
        public int Page
        {
            get
            {
                if (page < 1)
                    return 1;
                return page;
            }
            set
            {
                page = value;
            }
        }
        private int limit;
        public int Limit
        {
            get
            {
                if (limit < 1)
                    return 20;
                if (limit > 100)
                    return 100;
                return limit;
            }
            set
            {
                limit = value;
            }
        }
    }
}
