using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.RollingCode
{
    public class RollingCodeModel
    {
        public string Id { get; set; }
        public string Serial { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? RolledAt { get; set; }

        public RollingCodeModel(MdRollingCode mdCode)
        {
            Id = mdCode.Id.ToString();
            Serial = mdCode.Serial;
            Status = Enumeration.FromValue<RollingCodeStatus>(mdCode.Status).Name;
            CreatedAt = mdCode.CreatedAt;
            ModifiedAt = mdCode.ModifiedAt;
            RolledAt = mdCode.RolledAt;
        }
    }
}
