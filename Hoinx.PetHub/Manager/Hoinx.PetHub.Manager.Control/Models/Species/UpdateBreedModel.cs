using Hoinx.PetHub.Manager.Control.Models.Shared;
using System.Collections.Generic;

namespace Hoinx.PetHub.Manager.Control.Models.Species
{
    public class UpdateBreedModel
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public ImageModel Avatar { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
