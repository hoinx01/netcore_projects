using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Dal.Entities;
using Hoinx.PetHub.Manager.Data.Mongo.ValueObjects;
using System;
using System.Collections.Generic;

namespace Hoinx.PetHub.Manager.Data.Mongo.Entities
{
    [MdSequenceId(Id = "PetBreedSequenceId")]
    public class MdPetBreed : BaseLongIdMongoEntity
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public MdImage Avatar { get; set; }
        public List<MdImage> Images { get; set; }
        public string Status { get; set; }
    }
}
