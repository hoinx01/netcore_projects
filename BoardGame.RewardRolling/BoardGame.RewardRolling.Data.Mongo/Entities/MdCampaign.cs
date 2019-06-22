using Hinox.Data.Mongo.Dal.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Entities
{
    public class MdCampaign : BaseObjectIdMongoEntity
    {
        public string Name { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int Status { get; set; }
        public List<MdCampaignReward> Rewards { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
    public class MdCampaignReward
    {
        public ObjectId RewardId { get; set; }
        public int Rate { get; set; }
        public int Ordinal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
