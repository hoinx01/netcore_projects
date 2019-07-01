using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Auth
{
    public class ApplicationRole : Enumeration
    {
        public static ApplicationRole SuperAdmin = new ApplicationRole(0, "super_admin");
        public static ApplicationRole Admin = new ApplicationRole(1, "admin");
        public ApplicationRole(int id, string name) : base(id, name)
        {

        }
    }
}
