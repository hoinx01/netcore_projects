using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Hinox.Static.Application
{
    public class AppSettings
    {
        private static IConfiguration config;
        public static void SetConfig(IConfiguration configuration)
        {
            config = configuration;
        }
        public static T Get<T>(string key = null, T defaultValue = default(T))
        {
            if (string.IsNullOrWhiteSpace(key))
                return config.Get<T>();
            else
                return config.GetSection(key).Get<T>();
        }
        public static T Get<T>(string key = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                return config.Get<T>();
            else
                return config.GetSection(key).Get<T>();
        }
    }
}
