using AutoMapper;
using BoardGame.RewardRolling.Core.Auth;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Service.User
{
    public class ApplicationUserStore : IUserStore<ApplicationUser>
    {
        private readonly IMdUserDao userDao;

        public ApplicationUserStore(
            IMdUserDao userDao
            )
        {
            this.userDao = userDao;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var mdUser = Mapper.Map<MdUser>(user);
            try
            {
                await userDao.AddAsync(mdUser);
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                var error = new IdentityError()
                {
                    Code = e.GetType().Name,
                    Description = e.Message
                };
                var result = IdentityResult.Failed(error);
                return result;
            }
        }
        
        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (userId.Equals(ApplicationUser.SuperAdmin.Id))
                return ApplicationUser.SuperAdmin;

            var mdUser = await userDao.GetByIdAsync(new ObjectId(userId));
            if (mdUser == null)
                return null;

            var user = Mapper.Map<ApplicationUser>(mdUser);
            return user;
        }
        
        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(normalizedUserName))
                throw new ArgumentNullException(nameof(normalizedUserName));

            if (normalizedUserName.ToLower().Equals(ApplicationUser.SuperAdmin.UserName))
                return ApplicationUser.SuperAdmin;

            var mdUser = await userDao.GetByUserNameAsync(normalizedUserName.ToLower());
            if (mdUser == null)
                return null;

            var user = Mapper.Map<ApplicationUser>(mdUser);
            return user;

        }
        
        public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }
        
        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.Id;
        }
        
        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }
        
        public async Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return;
        }
        
        public async Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            return;
        }
        
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var mdUser = Mapper.Map<MdUser>(user);
            try
            {
                await userDao.UpdateAsync(mdUser);
                return IdentityResult.Success;
            }
            catch(Exception e)
            {
                var error = new IdentityError()
                {
                    Code = e.GetType().Name,
                    Description = e.Message
                };
                var result = IdentityResult.Failed(error);
                return result;
            }
            
        }

        public void Dispose()
        {
        }
    }
}
