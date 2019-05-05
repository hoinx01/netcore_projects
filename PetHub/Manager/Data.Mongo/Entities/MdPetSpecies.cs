using Data.Mongo.ValueObjects;
using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Dal.Entities;
using Hinox.Data.Mongo.IdGenerators;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Data.Mongo.Entities
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
