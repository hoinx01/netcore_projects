using BoardGame.RewardRolling.Core.Auth;
using BoardGame.RewardRolling.Data.Mongo;
using BoardGame.RewardRolling.Data.Mongo.Dao;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Repositories;
using BoardGame.RewardRolling.Domains.Campaign.Repositories;
using BoardGame.RewardRolling.Service.Common;
using BoardGame.RewardRolling.Service.Services.Reward;
using BoardGame.RewardRolling.Service.User;
using BoardGame.RewardRolling.WebApp.Admin.Services;
using BoardGame.RewardRolling.WebApp.Admin.Services.Interfaces;
using BoardGame.RewardRolling.WebApp.Services;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Data.Mongo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGame.RewardRolling.WebApp.Registrations
{
    public class DependencyRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IMongoDbFactory, MongoDbFactory>();
            services.AddSingleton<IIdGenerator, IdGenerator>();

            services.AddSingleton<IPasswordHasher<ApplicationUser>, CustomPasswordHashser>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddSingleton<IMdRollingCodeDao, MdRollingCodeDao>();
            services.AddSingleton<IMdRewardDao, MdRewardDao>();
            services.AddSingleton<IMdCampaignDao, MdCampaignDao>();
            services.AddSingleton<IMdUserDao, MdUserDao>();
            services.AddSingleton<IMdCustomerDao, MdCustomerDao>();
            services.AddSingleton<IMdCityDao, MdCityDao>();
            services.AddSingleton<IMdDistrictDao, MdDistrictDao>();
            services.AddSingleton<IMdCommuneDao, MdCommuneDao>();
            services.AddSingleton<IMdNhanhCityDao, MdNhanhCityDao>();
            services.AddSingleton<IMdStandardToNhanhAdministrativeUnitMapDao, MdStandardToNhanhAdministrativeUnitMapDao>();

            services.AddSingleton<IRewardQueryService, RewardQueryService>();
            services.AddSingleton<IRewardService, RewardService>();
            services.AddSingleton<ICampaignRepository, CampaignRepository>();
            services.AddSingleton<ICampaignQueryService, CampaignQueryService>();
            services.AddSingleton<ICampaignCommandService, CampaignCommandService>();
            services.AddSingleton<IRollingCodeService, RollingCodeService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IUploadService, UploadService>();
            services.AddSingleton<IAdministrativeUnitService, AdministrativeUnitService>();
            services.AddSingleton<IUserService, UserService>();
        }
    }
}
