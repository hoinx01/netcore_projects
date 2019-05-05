using Hinox.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Control.Models.Species
{
    public class PetSpeciesFilterResult : BasePagingFilterResponse
    {
        public List<PetSpeciesModel> PetSpecies { get; set; }
    }
}
