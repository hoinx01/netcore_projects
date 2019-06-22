using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Statics
{
    public class CampaignStatus : Enumeration
    {
        public static CampaignStatus Active = new CampaignStatus(1, "active");
        public static CampaignStatus Inactive = new CampaignStatus(2, "inactive");
        public static CampaignStatus Deleted = new CampaignStatus(3, "deleted");
        public CampaignStatus(int id, string name) : base(id, name)
        {

        }
    }
}
