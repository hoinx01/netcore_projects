using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Statics
{
    public class UploadedFileGroup : Enumeration
    {
        public static UploadedFileGroup Unknown = new UploadedFileGroup(0, "unknown");
        public static UploadedFileGroup LuckyWheelPart = new UploadedFileGroup(1, "lucky-wheel-parts");
        
        public UploadedFileGroup(int id, string name) : base(id, name)
        {

        }
    }
}
