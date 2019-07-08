using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Statics
{
    public class Gender : Enumeration
    {
        public static Gender Male = new Gender(1, nameof(Male));
        public static Gender Female = new Gender(2, nameof(Female));
        public static Gender Other = new Gender(3, nameof(Other));
        public Gender(int id, string name) : base(id, name)
        {

        }
    }
}
