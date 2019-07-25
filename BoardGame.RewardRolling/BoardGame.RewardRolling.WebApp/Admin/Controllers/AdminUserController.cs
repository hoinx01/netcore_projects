using AutoMapper;
using BoardGame.RewardRolling.Core.Auth;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Service.Common;
using BoardGame.RewardRolling.Service.User;
using BoardGame.RewardRolling.WebApp.Admin.Models.User;
using Hinox.Mvc.Controllers;
using Hinox.Mvc.Exceptions;
using Hinox.Static.Application;
using Hinox.Static.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/users")]
    public class AdminUserController : BaseRestController
    {
        private readonly LoginModel superAdmin = AppSettings.Get<LoginModel>("Authentication:SuperAdmin");
        private readonly string jwtSecretKey = AppSettings.Get<string>("Authentication:SecretKey");

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public AdminUserController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserService userService
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<LoginResultModel> Login([FromBody] LoginModel model)
        {
            var signInResult = await signInManager.PasswordSignInAsync(
                model.UserName, 
                model.Password, 
                model.RememberMe, 
                lockoutOnFailure: false
                );

            if (!signInResult.Succeeded)
                throw new AuthenticationException(ExceptionMessages.Authentication.InvalidLoginInfo);

            var userId = userManager.GetUserId(User);

            var token = GenerateJWT(userId);

            var result = new LoginResultModel()
            {
                Token = token
            };
            return result;

        }

        [HttpGet]
        [Route("test")]
        public async Task<string> TestToken()
        {
            var user = await userManager.GetUserAsync(User);
            return user.Name;
        }

        [HttpGet]
        [Route("me")]
        public async Task<UserModel> GetCurrentUser()
        {
            var user = await userManager.GetUserAsync(User);
            var model = Mapper.Map<UserModel>(user);
            return model;
        }

        [HttpPost]
        public async Task<UserModel> AddUser([FromBody] AddUserModel model)
        {
            var appUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Name = model.Name
            };
            var createResult = await userManager.CreateAsync(appUser, model.Password);
            var result = Mapper.Map<UserModel>(appUser);
            return result;
        }

        private string GenerateJWT(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        public async Task<List<UserModel>> FilterUser()
        {
            var userModels = await userService.FilterUser();
            return userModels;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteUser([FromRoute] string id)
        {
            await userService.DeleteUser(id);
            return;
        }
    }
}
