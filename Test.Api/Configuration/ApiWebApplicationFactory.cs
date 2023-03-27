using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using Infrastructure;

namespace Test.Api.Configuration
{
    public class ApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public ServiceProvider ServiceProvider { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Is be called after the `ConfigureServices` from the Startup
            // which allows you to overwrite the DI with mocked instances
            //// use integrationsettings json file for test:
            
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "../../../Properties/integrationsettings.json");
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(configPath);
            });

            builder.ConfigureTestServices(services => { });

            //add DI of anothr layers
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder().AddJsonFile(configPath).Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddScoped<IDbConnection>((sp) => new SqlConnection(configuration.GetConnectionString("ReserveDb")));
            serviceCollection.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            //serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddInfrastructure(configuration);

            ServiceProvider = serviceCollection.BuildServiceProvider();

        }
    }
}
