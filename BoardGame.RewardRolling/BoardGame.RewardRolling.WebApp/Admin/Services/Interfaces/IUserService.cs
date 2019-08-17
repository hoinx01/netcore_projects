using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.User;

namespace BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> FilterUser();
        Task DeleteUser(string id);
    }
}
