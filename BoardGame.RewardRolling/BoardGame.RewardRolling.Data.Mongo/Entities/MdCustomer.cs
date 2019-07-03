using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Core.ValueObjects;
using Hinox.Data.Mongo.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Entities
{
    public class MdCustomer : BaseObjectIdMongoEntity
    {
        public List<RolloutReward> RolloutRewards { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Birthday Birthday { get; set; }
        public int GenderId { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
