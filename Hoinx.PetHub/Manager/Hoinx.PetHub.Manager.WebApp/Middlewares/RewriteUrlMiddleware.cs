using Hinox.Mvc.Exceptions;
using Hinox.Mvc.Middlewares;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hoinx.PetHub.Manager.WebApp.Middlewares
{
    public class RewriteUrlMiddleware : BaseCustomMiddleware
    {
        public RewriteUrlMiddleware(RequestDelegate next) : base(next)
        {
        }
        public override async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.Value.EndsWith(".json"))
            {
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return;
            }
            else
            {
                context.Request.Path = context.Request.Path.Value.Replace(".json", "");
                await next(context);
            }
            
        }
    }
}
