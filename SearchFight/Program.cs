using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFight.Infraestructure;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = BuildConfiguration();
            EnableLogging(configuration);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            string query = Console.ReadLine();
            string[] queryArray = Regex.Matches(query, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value.Replace('\"', ' ').Trim())
                .ToArray();
            try
            {
                serviceProvider.GetService<SearchFight>().Run(queryArray);
            }
            catch (Exception exception)
            {
                var errorGuid = Guid.NewGuid();
                Log.Error("Error Id: {Id}, \r\n Error Information: {Error}", errorGuid, exception);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        #region Startup

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCustomApiClients(configuration)
                .AddCustomServices();
        }

        private static IConfiguration BuildConfiguration()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            IConfiguration config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            return config;
        }

        private static void EnableLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .WriteTo.Console()
                    .CreateLogger();
        }

        #endregion
    }


}
