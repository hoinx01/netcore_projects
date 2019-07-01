using AutoMapper;
using BoardGame.RewardRolling.Core.Auth;
using BoardGame.RewardRolling.Core.Statics;
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

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/users")]
    public class AdminUserController : BaseRestController
    {
        private readonly LoginModel superAdmin = AppSettings.Get<LoginModel>("Authentication:SuperAdmin");
        private readonly string jwtSecretKey = AppSettings.Get<string>("Authentication:SecretKey");

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminUserController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
    }
}
