﻿using Hinox.Mvc.Models;
using Hinox.Static.Constants;
using Microsoft.AspNetCore.Http;
using NetCore.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hinox.Mvc.Middlewares
{
    public abstract class BaseCustomMiddleware
    {
        protected readonly RequestDelegate next;
        public BaseCustomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public abstract Task InvokeAsync(HttpContext context);
        public async Task WriteExceptionResponse(HttpContext context, ErrorModel errorModel)
        {
            await WriteJsonResult(context, (HttpStatusCode)errorModel.StatusCode, errorModel);
        }
        public async Task WriteJsonResult(HttpContext context, HttpStatusCode statusCode, object obj)
        {
            context.Response.StatusCode = (int)statusCode;
            if (context.Response.Headers != null)
                context.Response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            await context.Response.WriteAsync(JsonConvert.SerializeObject(obj, NewtonJsonSerializerSettings.CAMEL));

        }
        public async Task WriteImageResult(HttpContext context, string file)
        {
            await context.Response.SendFileAsync(file, 0, null);

        }
    }
}
