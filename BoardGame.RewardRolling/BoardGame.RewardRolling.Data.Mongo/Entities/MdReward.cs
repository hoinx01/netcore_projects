using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Entities
{
    [MdCollection(Name = "Rewards")]
    public class MdReward : BaseObjectIdMongoEntity
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int Status { get; set; }
    }
}
