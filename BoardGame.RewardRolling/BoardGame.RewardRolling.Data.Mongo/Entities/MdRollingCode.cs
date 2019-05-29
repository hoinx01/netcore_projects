using Hinox.Data.Mongo.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Entities
{
    public class MdRollingCode : BaseObjectIdMongoEntity
    {
        public string Serial { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ActivatedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public DateTime? RolledAt { get; set; }
        public int Status { get; set; }
    }
}
