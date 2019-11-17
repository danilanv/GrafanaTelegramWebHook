using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GrafanaTelegramWebHook.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GrafanaTelegramWebHook
{
    public class Startup
    {
        private readonly string _configFile;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _configFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\configuration.json";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (!File.Exists(_configFile))
                throw new Exception($"Missing configuration file \"{_configFile}\"");

            var config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(_configFile));

            services.AddSingleton(config.Proxy);

            services.AddSingleton(config.BotSettings);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
