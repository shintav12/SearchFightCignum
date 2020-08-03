using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFight.IServices;
using SearchFight.Models;
using SearchFight.Services;
using SearchFight.Services.Clients;
using SearchFight.Services.Interfaces;
using System;

namespace SearchFight.Infraestructure
{
    public static class ConfigureServices
    {
        private const string GOOGLE_SEARCH_ENGINE = "SearchEngines:0";
        private const string BING_SEARCH_ENGINE = "SearchEngines:1";

        public static IServiceCollection AddCustomApiClients(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton<ISearchApiClient<GoogleResponse>>(opt => new GoogleApiClient(configuration.GetSection($"{GOOGLE_SEARCH_ENGINE}:Host").Value, configuration[$"{GOOGLE_SEARCH_ENGINE}:Key"], configuration[$"{GOOGLE_SEARCH_ENGINE}:CX"]))
               .AddSingleton<ISearchApiClient<BingResponse>>(opt => new BingApiClient(configuration[$"{BING_SEARCH_ENGINE}:Host"], configuration[$"{BING_SEARCH_ENGINE}:Key"]));
            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services
                .AddTransient<ISearchEnginesServices, SearchEnginesServices>()
                .AddTransient<SearchFight>();
            return services;
        }
    }
}
