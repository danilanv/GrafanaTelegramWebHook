using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GrafanaTelegramWebHook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "GRAFANA TELEGRAM WEBHOOK";
            Console.WriteLine("Grafana Telegram Webhook adds proxy support to Grafana's telegram alerting channel.");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
