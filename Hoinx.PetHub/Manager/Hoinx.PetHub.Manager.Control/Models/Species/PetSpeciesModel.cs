using System;
using System.Collections.Generic;
using System.Text;

namespace Hoinx.PetHub.Manager.Control.Models.Species
{
    public class PetSpeciesModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
