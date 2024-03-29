﻿using Microsoft.Extensions.DependencyInjection;

namespace SteamMarketplace.HttpClients.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClients.Common.Services.Authorization>();
            services.AddSingleton<HttpClients.AuthorizationAPI.AuthorizationHttpClient>();
            services.AddSingleton<HttpClients.AuthorizationAPI.AuthorizationAPIHttpContext>();
            services.AddSingleton<HttpClients.CBR.DailyHttpClient>();
            services.AddSingleton<HttpClients.CBR.LatestHttpClient>();
            services.AddSingleton<HttpClients.CBR.CBRHttpContext>();
            services.AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>();
            services.AddSingleton<HttpClients.CSMoney.StoreHttpClient>();
            services.AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>();
            services.AddSingleton<HttpClients.ResourceAPI.CBRExchangeRatesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.CSMoneyStoreHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ImportExchangeRateHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ImportItemHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ItemsHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.PurchasesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.RandomizePurchasesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.RandomizeSalesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.RandomizeUsersHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.UserInventoriesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.SalesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ResourceAPIHttpContext>();
            services.AddSingleton<HttpContext>();
        }
    }
}
