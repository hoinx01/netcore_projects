using BoardGame.RewardRolling.Data.Mongo;
using BoardGame.RewardRolling.Data.Mongo.Dao;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Service.Services.Reward;
using BoardGame.RewardRolling.WebApp.Admin.Services;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using Hinox.Data.Mongo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Registrations
{
    public class DependencyRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IMongoDbFactory, MongoDbFactory>();

            services.AddSingleton<IMdRollingCodeDao, MdRollingCodeDao>();
            services.AddSingleton<IMdRewardDao, MdRewardDao>();

            services.AddSingleton<IRewardQueryService, RewardQueryService>();
            services.AddSingleton<IRewardService, RewardService>();
        }
    }
}
