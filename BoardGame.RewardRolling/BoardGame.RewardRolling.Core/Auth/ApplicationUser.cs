using Hinox.Static.Application;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace BoardGame.RewardRolling.Core.Auth
{
    public class ApplicationUser : IIdentity
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PasswordSalt { get; set; }
        public string HashedPassword { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string AuthenticationType { get; set; }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public static ApplicationUser SuperAdmin = 
            AppSettings.Get<ApplicationUser>("Authentication:SuperAdmin");
    }
}
