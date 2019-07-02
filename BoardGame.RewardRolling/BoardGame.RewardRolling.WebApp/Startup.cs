using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Core.Auth;
using BoardGame.RewardRolling.Data.Mongo;
using BoardGame.RewardRolling.Data.Mongo.Dao;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Service.User;
using BoardGame.RewardRolling.WebApp.Middlewares;
using BoardGame.RewardRolling.WebApp.Registrations;
using Hinox.Data.Mongo;
using Hinox.Mvc.Middlewares;
using Hinox.Static.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLog;

namespace BoardGame.RewardRolling.WebApp
{
    public class Startup
    {
        private readonly Logger routerLog = LogManager.GetLogger("router-log");

        private List<string> availabeUnauthenticatedPaths = new List<string>()
        {
            "/admin/api/users/login.json"
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings.SetConfig(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentityCore<ApplicationUser>()
                .AddUserManager<ApplicationUserManager>()
                .AddSignInManager<ApplicationUserSignInManager>()
                .AddUserStore<ApplicationUserStore>()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

            var jwtSecretKey = AppSettings.Get<string>("Authentication:SecretKey");
            var key = Encoding.ASCII.GetBytes(jwtSecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });






            DependencyRegistration.Register(services);
            MapperInitiator.Init();

            DependencyManager.SetServiceProvider(services.BuildServiceProvider());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseMiddleware<ProcessExceptionMiddleware>();

            app.UseAuthentication();

            app.Use(async (context, next) =>
            {

                var path = context.Request.Path.Value;

                routerLog.Info(path);

                if (context.User.Identity.IsAuthenticated || !path.Contains(".json"))
                {
                    routerLog.Info("next luon");
                    await next();
                }
                else if (availabeUnauthenticatedPaths.Contains(path))
                {
                    routerLog.Info("next luon");
                    await next();
                }
                else
                {
                    routerLog.Info("tra ra json loi 401");
                    context.Response.StatusCode = 200;
                    if (context.Response.Headers != null)
                        context.Response.Headers.Add("Content-Type", "application/json; charset=utf-8");
                    var obj = new
                    {
                        statusCode = 401,
                        messages = new List<string>()
                            {
                                "Cần đăng nhập để thực hiện hành động này"
                            }
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(obj));

                }
            });

            app.Use(async (context, next) =>
            {
                await next();
                var path = context.Request.Path.Value;

                if (path.StartsWith("/admin"))
                {
                    if (
                        context.Response.StatusCode == 404 
                        && !Path.HasExtension(path) 
                        && !path.EndsWith(".json")
                    )
                    {
                        context.Request.Path = "/AdminHome/Index";
                        await next();
                    }
                }
            });

            app.UseStaticFiles();
            app.UseMiddleware<RewriteUrlMiddleware>();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
