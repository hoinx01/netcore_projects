using BoardGame.RewardRolling.Core.Auth;
using BoardGame.RewardRolling.Service.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Service.User
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IIdGenerator idGenerator;
        public ApplicationUserManager(IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger,
            IIdGenerator idGenerator
            )
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.idGenerator = idGenerator;
        }

        public override async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            ThrowIfDisposed();
            if (user == null)
                return false;

            if (user.UserName.Equals(ApplicationUser.SuperAdmin.UserName)
                && user.HashedPassword.Equals(password))
                return true;

            var result = PasswordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

            var success = result != PasswordVerificationResult.Failed;
            if (!success)
            {
                Logger.LogWarning(0, "Invalid password for user {userId}.", await GetUserIdAsync(user));
            }
            return success;
        }

        public override string GetUserId(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal.FindFirstValue("Id");
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            user.Id = await idGenerator.GenerateStringId();
            user.PasswordSalt = ApplicationUser.GeneratePasswordSalt(user.UserName);
            user.CreatedAt = DateTime.UtcNow;
            user.ModifiedAt = DateTime.UtcNow;

            await UpdatePasswordHash(user, password, false);

            return await CreateAsync(user);
        }

        protected override async Task<IdentityResult> UpdatePasswordHash(ApplicationUser user, string newPassword, bool validatePassword)
        {
            var hashedPassword = PasswordHasher.HashPassword(user, newPassword);
            user.HashedPassword = hashedPassword;
            return IdentityResult.Success;
        }
    }
}
