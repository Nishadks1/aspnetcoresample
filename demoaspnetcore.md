# Demo ASP.NET Core

## Intro

1. Create a new ASP.NET Core empty project
2. Add hello.html and return it (Microsoft.AspnetCore.StaticFiles, UseStaticFiles)
3. define a route

```csharp
            app.Map("/sample", app1 =>
            {
                app1.Run(async context =>
                {
                    await context.Response.WriteAsync("<h1>sample route</h1>");
                });
            });
```

## DI

4. add a service ISampleService, returns IEnumerable<string>, GetSampleStrings
5. add a SampleService implementing ISampleService
6. add HelloController in the Ctrls directory, GetList returns ul

```csharp
        private readonly ISampleService _sampleService;
        public HelloController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public string GetList() =>
            $"<ul>{GetListItems()}</ul>";

        private string GetListItems() =>
            string.Join("", _sampleService.GetSampleStrings().Select(s => $"<li>{s}</li>"));
```

7. Map HelloController

```csharp
            app.Map("/hello", app1 =>
            {
                app1.Run(async context =>
                {
                    var ctrl = Container.GetRequiredService<HelloController>();
                    await context.Response.WriteAsync(ctrl.GetList());
                });
            });
```

## Configuration

8. Add Microsoft.Extensions.Logging.Debug package

9. Load config files

```csharp
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
```

10. define json

```javascript
{
  "SampleGroup": {
    "AKey": "AValue"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=_CHANGE_ME;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

11. read json

```csharp
            app.Map("/config", app1 =>
            {
                app1.Run(async context =>
                {
                    var s = Configuration.GetValue<string>("SampleGroup:AKey");
                    var s2 = Configuration.GetSection("SampleGroup").GetValue<string>("AKey");
                    await context.Response.WriteAsync($"<h1>{s}...{s2}</h1>");
                });
            });
```

12. add package Microsoft.Extensions.Configuration.UserSecrets
13. load user secrets

```csharp
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }
```



## Logging

14. Add Microsoft.Extensions.Logging.Debug package
15. AddDebug to the ILoggerFactory

```csharp
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole();                    

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
            }
            //...
```

16. Add logging to the controller

```csharp
    public class HelloController
    {
        private readonly ISampleService _sampleService;
        private readonly ILogger<HelloController> _logger;
        public HelloController(ISampleService sampleService, ILogger<HelloController> logger)
        {
            _sampleService = sampleService;
            _logger = logger;
        }

        public string GetList() =>
            Log("GetList", GetHtmlList);

        private string GetHtmlList() =>
            $"<ul>{GetListItems()}</ul>";

        private string GetListItems() =>
            string.Join("", _sampleService.GetSampleStrings().Select(s => $"<li>{s}</li>"));

        private string Log(string message, Func<string> action)
        {
            _logger.LogInformation($"{message} started");
            string result = action();
            _logger.LogInformation($"{message} completed");
            return result;
        }
    }
```
