using BoardGame.RewardRolling.Core.Auth;
using Hinox.Static.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Service.User
{
    public class CustomPasswordHashser : IPasswordHasher<ApplicationUser>
    {
        
        public string HashPassword(ApplicationUser user, string password)
        {
            var inputText = string.Format("{0}-{1}-{2}", user.UserName, user.PasswordSalt, user.HashedPassword);
            var result = StringUtils.CalculateMD5Hash(inputText);
            return result;
        }
        
        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            var proviedHasedPassword = HashPassword(user, providedPassword);
            if (proviedHasedPassword.Equals(hashedPassword))
                return PasswordVerificationResult.Success;
            return PasswordVerificationResult.Failed;
        }
    }
}
