using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Statics
{
    public class RollingCodeStatus : Enumeration
    {
        public static RollingCodeStatus Pending = new RollingCodeStatus(0, "pending");
        public static RollingCodeStatus Active = new RollingCodeStatus(1, "active");
        public static RollingCodeStatus Disabled = new RollingCodeStatus(2, "disabled");
        public static RollingCodeStatus Expired = new RollingCodeStatus(3, "expired");
        public static RollingCodeStatus Deleted = new RollingCodeStatus(4, "deleted");
        public RollingCodeStatus(int id, string name) : base(id, name)
        {

        }
    }
}
