using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ASPNETCoreSample.Services;
using ASPNETCoreSample.MyControllers;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ASPNETCoreSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISampleService, SampleService>();
            services.AddTransient<HelloController>();
            Container = services.BuildServiceProvider();
        }

        public IServiceProvider Container { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole();                    

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.Map("/config", app1 =>
            {
                app1.Run(async context =>
                {
                    var s = Configuration.GetValue<string>("SampleGroup:AKey");
                    var s2 = Configuration.GetSection("SampleGroup").GetValue<string>("AKey");
                    var secret = Configuration.GetValue<string>("arealsecret");
                    await context.Response.WriteAsync($"<h1>{s}...{s2}, secret: {secret}</h1>");
                });
            });

            app.Map("/sample", app1 =>
            {
                app1.Run(async context =>
                {
                    await context.Response.WriteAsync("<h1>sample route</h1>");
                });
            });

            app.Map("/hello", app1 =>
            {
                app1.Run(async context =>
                {
                    var ctrl = Container.GetRequiredService<HelloController>();
                    await context.Response.WriteAsync(ctrl.GetList());
                });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
