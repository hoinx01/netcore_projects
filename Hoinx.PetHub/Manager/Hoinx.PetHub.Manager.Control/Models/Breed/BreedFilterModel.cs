using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoinx.PetHub.Manager.Control.Models.Breed
{
    public class BreedFilterModel : BasePagingFilterRequest
    {
        public List<long> Ids { get; set; }
        public List<long> SpeciesIds { get; set; }


    }
}
