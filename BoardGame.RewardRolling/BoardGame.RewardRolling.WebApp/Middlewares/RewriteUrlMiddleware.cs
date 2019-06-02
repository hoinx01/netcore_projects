using Hinox.Mvc.Middlewares;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Middlewares
{
    public class RewriteUrlMiddleware : BaseCustomMiddleware
    {
        public RewriteUrlMiddleware(RequestDelegate next) : base(next)
        {
        }
        public override async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.Value.StartsWith("/admin"))
                await next(context);
            else
            {
                if (!context.Request.Path.Value.EndsWith(".json"))
                {
                    await next(context);
                }
                else
                {
                    context.Request.Path = context.Request.Path.Value.Replace(".json", "");
                    await next(context);
                }
            }
            

        }
    }
}
