using Control.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Control.Models.Breed
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
