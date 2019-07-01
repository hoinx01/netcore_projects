using BoardGame.RewardRolling.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Models.User
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public List<ApplicationRole> AccountRoles { get; set; }

        public static UserModel SuperAdmin = new UserModel()
        {
            Id = "-1",
            Username = "sa",
            Name = "SuperAdmin",
            AccountRoles = new List<ApplicationRole>() { ApplicationRole.SuperAdmin }
        };
    }
}
