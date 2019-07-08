using Hinox.Static.Application;
using Hinox.Static.Utilities;
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

        public static string GeneratePasswordSalt(string userName)
        {
            var inputText = string.Format(
                "{0}-{1}-{2}",
                userName, 
                DateTime.UtcNow.Ticks, 
                AppSettings.Get<string>("Authentication:CommonSalt")
                );
            var result = StringUtils.CalculateMD5Hash(inputText);
            return result;
        }

        public static ApplicationUser SuperAdmin = 
            AppSettings.Get<ApplicationUser>("Authentication:SuperAdmin");
    }
}
