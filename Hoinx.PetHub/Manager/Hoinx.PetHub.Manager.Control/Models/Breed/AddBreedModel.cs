using Hoinx.PetHub.Manager.Control.Models.Shared;
using System.Collections.Generic;

namespace Hoinx.PetHub.Manager.Control.Models.Breed
{
    public class AddBreedModel
    {
        public long SpeciesId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public ImageModel Avatar { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
