using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Data.Mongo;
using BoardGame.RewardRolling.Data.Mongo.Dao;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.WebApp.Middlewares;
using Hinox.Data.Mongo;
using Hinox.Static.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGame.RewardRolling.WebApp
{
    public class Startup
    {
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

            services.AddSingleton<IMongoDbFactory, MongoDbFactory>();
            services.AddSingleton<IMdRollingCodeDao, MdRollingCodeDao>();
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

            app.Use(async (context, next) =>
            {
                await next();
                var path = context.Request.Path.Value;

                if (path.StartsWith("/admin"))
                {
                    if (context.Response.StatusCode == 404 && !Path.HasExtension(path) && !path.EndsWith(".json"))
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
