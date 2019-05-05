using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Control.Models.Breed
{
    public class BreedFilterResult : BasePagingFilterResponse
    {
        public List<BreedModel> Breeds { get; set; }
    }
}
