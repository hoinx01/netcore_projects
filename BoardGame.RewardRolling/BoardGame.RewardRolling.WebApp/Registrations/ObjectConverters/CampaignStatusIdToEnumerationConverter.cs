using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Registrations.ObjectConverters
{
    public class CampaignStatusIdToEnumerationConverter : ITypeConverter<int, CampaignStatus>
    {
        public CampaignStatus Convert(int statusId, CampaignStatus destination, ResolutionContext context)
        {
            return Enumeration.FromValue<CampaignStatus>(statusId);
        }
    }
}
