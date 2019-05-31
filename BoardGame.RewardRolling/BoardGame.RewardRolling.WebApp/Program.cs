using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BoardGame.RewardRolling.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunWebHost(args);
        }

        public static void RunWebHost(string[] args)
        {
            Dictionary<string, string> runVariables = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                var argPaths = arg.Split("=");
                if (argPaths.Length == 2)
                    runVariables.Add(argPaths[0], argPaths[1]);
            }
            string profile = runVariables.GetValueOrDefault("profile", "dev");
            Console.WriteLine("Running with profile: " + profile);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile("excel-settings.json", true)
                .Build();

            IWebHost webHost = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseKestrel()
                .Build();
            webHost.Run();
        }
    }
}
