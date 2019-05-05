using Hinox.Mvc.Behaviors;
using Hinox.Mvc.Middlewares;
using Hinox.Static.Application;
using Hoinx.PetHub.Manager.WebApp.Initiators;
using Hoinx.PetHub.Manager.WebApp.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Hoinx.PetHub.Manager.WebApp
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
            MapperInitiator.Init();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            DependencyRegistration.Register(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = CustomApiBehaviorOptions.InvalidModelStateResponseFactory;
            });

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
            app.UseMiddleware<ProcessExceptionMiddleware>();
            
            app.Use(async (context, next) =>
            {


                await next();
                var path = context.Request.Path.Value;
                if (context.Response.StatusCode == 404 && !Path.HasExtension(path) && !path.EndsWith(".json"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }

            });
            app.UseStaticFiles();
            app.UseMiddleware<RewriteUrlMiddleware>();
            app.UseCookiePolicy();

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
