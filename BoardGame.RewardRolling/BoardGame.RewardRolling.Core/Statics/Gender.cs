using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Statics
{
    public class Gender : Enumeration
    {
        public string DisplayedName { get; set; }

        public static Gender Male = new Gender(1, nameof(Male), "Nam");
        public static Gender Female = new Gender(2, nameof(Female), "Nữ");
        public static Gender Other = new Gender(3, nameof(Other), "Khác");
        public Gender(int id, string name, string displayedName) : base(id, name)
        {
            this.DisplayedName = displayedName;
        }
    }
}
