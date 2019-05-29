using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hoinx.PetHub.Manager.Control.Models.Species
{
    public class UpdatePetSpeciesModel
    {
        [Required(AllowEmptyStrings=false)]
        public string Name { get; set; }
        public string Alias { get; set; }
    }
}
