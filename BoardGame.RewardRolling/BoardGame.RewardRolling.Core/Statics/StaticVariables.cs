using Hinox.Static.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Statics
{
    public static class StaticVariables
    {
        public static string ImageBaseUrl = AppSettings.Get<string>("Media:StaticBaseUrl");
    }
}
