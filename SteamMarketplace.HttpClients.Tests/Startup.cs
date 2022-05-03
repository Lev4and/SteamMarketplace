using Microsoft.Extensions.DependencyInjection;

namespace SteamMarketplace.HttpClients.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClients.AuthorizationAPI.AuthorizationHttpClient>();
            services.AddSingleton<HttpClients.AuthorizationAPI.AuthorizationAPIHttpContext>();
            services.AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>();
            services.AddSingleton<HttpClients.CSMoney.StoreHttpClient>();
            services.AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>();
            services.AddSingleton<HttpClients.ResourceAPI.CSMoneyStoreHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ImportItemHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ResourceAPIHttpContext>();
            services.AddSingleton<HttpClients.ResourceAPI.UserInventoriesHttpClient>();
            services.AddSingleton<HttpContext>();
        }
    }
}
