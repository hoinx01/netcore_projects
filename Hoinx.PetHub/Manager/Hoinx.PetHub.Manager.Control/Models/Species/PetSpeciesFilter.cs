using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoinx.PetHub.Manager.Control.Models.Species
{
    public class PetSpeciesFilter : BasePagingFilterRequest
    {
        public List<long> Ids { get; set; }
    }
}
