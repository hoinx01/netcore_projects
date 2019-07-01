using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo.Entities
{
    [MdCollection(Name = "Users")]
    public class MdUser : BaseObjectIdMongoEntity
    {
        public string UserName { get; set; }
        public string PasswordSalt { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
