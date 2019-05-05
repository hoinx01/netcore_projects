using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Control.Models.Species
{
    public class AddPetSpeciesModel
    {
        [Required(AllowEmptyStrings=false)]
        public string Name { get; set; }
        public string Alias { get; set; }
    }
}
