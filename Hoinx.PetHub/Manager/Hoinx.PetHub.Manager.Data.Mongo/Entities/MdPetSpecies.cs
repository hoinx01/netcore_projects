using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Dal.Entities;
using System;

namespace Hoinx.PetHub.Manager.Data.Mongo.Entities
{
    [MdCollection(Name = "PetSpecies")]
    [MdSequenceId(Id = "PetSpeciesSequenceId")]
    public class MdPetSpecies : BaseLongIdMongoEntity
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
