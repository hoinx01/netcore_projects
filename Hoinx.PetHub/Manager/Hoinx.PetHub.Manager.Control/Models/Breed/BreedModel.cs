using Hoinx.PetHub.Manager.Control.Models.Shared;
using System;
using System.Collections.Generic;

namespace Hoinx.PetHub.Manager.Control.Models.Breed
{
    public class BreedModel
    {
        public long Id { get; set; }
        public long SpeciesId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ImageModel Avatar { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
