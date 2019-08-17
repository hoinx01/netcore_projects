using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.WebApp.Admin.Models.User;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using Hinox.Mvc.Exceptions;
using MongoDB.Bson;

namespace BoardGame.RewardRolling.WebApp.Admin.Services
{
    public class UserService : IUserService
    {
        private readonly IMdUserDao userDao;
        public UserService(
            IMdUserDao userDao
        )
        {
            this.userDao = userDao;
        }
        public async Task<List<UserModel>> FilterUser()
        {
            var users = await userDao.GetAllAsync();
            var userModels = users.Select(s => Mapper.Map<UserModel>(s)).ToList();
            return userModels;
        }

        public async Task DeleteUser(string id)
        {
            var user = await userDao.GetByIdAsync(new ObjectId(id));

            if(user == null)
                throw new NotFoundException();

            await userDao.DeleteAsync(user);
        }
    }
}
