using Hinox.Data.Mongo.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Dal.Entities
{
    public class MdRollingCode : BaseStringIdMongoEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? RolledAt { get; set; }
        public string Status { get; set; }
    }
}
