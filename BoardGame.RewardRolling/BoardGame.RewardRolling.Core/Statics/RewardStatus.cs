using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Statics
{
    public class RewardStatus : Enumeration
    {
        public static RewardStatus Active = new RewardStatus(1, "active");
        public static RewardStatus Deleted = new RewardStatus(2, "deleted");
        public RewardStatus(int id, string name) : base(id, name)
        {

        }
    }
}
