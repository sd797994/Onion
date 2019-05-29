using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Oxygen;

namespace GeneralHost
{
    class Program
    {
        private static IConfiguration _configuration { get; set; }
        static async Task Main(string[] args)
        {
            await CreateDefaultHost(args).Build().RunAsync();
        }

        static IHostBuilder CreateDefaultHost(string[] args) => new HostBuilder()
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json");
                config.AddJsonFile("autofac.json");
                config.AddJsonFile("oxygen.json");
                _configuration = config.Build();
            })
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterOxygen();
                builder.RegisterModule(new ConfigurationModule(_configuration));
            })
            .ConfigureServices(services =>
            {
                services.AddOxygenServer(_configuration);
                services.AddLogging(configure =>
                {
                    configure.AddConfiguration(_configuration.GetSection("Logging"));
                    configure.AddConsole();
                });
                services.AddAutofac();
                services.AddCap(x =>
                {
                    x.UseDashboard();
                    x.UseEntityFramework<Infrastructure.EntityFrameworkDataAccess.Context>();
                    x.UseRabbitMQ(_configuration.GetSection("modules:3:properties:ConnectionString").Value);
                });
                services.AddMediatR();
                services.AddHostedService<CustomHostService>();
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseConsoleLifetime();
    }
}
