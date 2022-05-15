using Microsoft.Extensions.DependencyInjection;
using SteamMarketplace.DesktopApplication.Services;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Hubs;

namespace SteamMarketplace.DesktopApplication.ViewModels.Locators
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public AuthorizationViewModel AuthorizationViewModel => _provider.GetService<AuthorizationViewModel>();

        public DashboardViewModel DashboardViewModel => _provider.GetService<DashboardViewModel>();

        public ImportOnlineViewModel ImportOnlineViewModel => _provider.GetService<ImportOnlineViewModel>();

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();

        public MenuViewModel MenuViewModel => _provider.GetRequiredService<MenuViewModel>();

        public MyInventoryViewModel MyInventoryViewModel => _provider.GetRequiredService<MyInventoryViewModel>();

        public MyPurchasesViewModel MyPurchasesViewModel => _provider.GetRequiredService<MyPurchasesViewModel>();

        public MySalesViewModel MySalesViewModel => _provider.GetRequiredService<MySalesViewModel>();

        public ShopViewModel ShopViewModel => _provider.GetRequiredService<ShopViewModel>();

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddSingleton<AuthorizationViewModel>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<ImportOnlineViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MenuViewModel>();
            services.AddSingleton<MyInventoryViewModel>();
            services.AddSingleton<MyPurchasesViewModel>();
            services.AddSingleton<MySalesViewModel>();
            services.AddSingleton<ShopViewModel>();

            services.AddSingleton<HttpClients.Common.Services.Authorization>();
            services.AddSingleton<HttpClients.AuthorizationAPI.AuthorizationHttpClient>();
            services.AddSingleton<HttpClients.AuthorizationAPI.AuthorizationAPIHttpContext>();
            services.AddSingleton<HttpClients.CBR.LatestHttpClient>();
            services.AddSingleton<HttpClients.CBR.CBRHttpContext>();
            services.AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>();
            services.AddSingleton<HttpClients.CSMoney.StoreHttpClient>();
            services.AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>();
            services.AddSingleton<HttpClients.ResourceAPI.CBRExchangeRatesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.CSMoneyStoreHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ImportExchangeRateHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ImportItemHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.RandomizePurchasesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.RandomizeSalesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.RandomizeUsersHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ItemsHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.PurchasesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.SalesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.UserInventoriesHttpClient>();
            services.AddSingleton<HttpClients.ResourceAPI.ResourceAPIHttpContext>();
            services.AddSingleton<HttpContext>();

            services.AddSingleton<Hubs.ResourceAPI.ImportHubClient>();
            services.AddSingleton<Hubs.ResourceAPI.OnlineHubClient>();
            services.AddSingleton<Hubs.ResourceAPI.AutoImportHubClient>();
            services.AddSingleton<Hubs.ResourceAPI.ResourceAPIHubContext>();
            services.AddSingleton<HubContext>();

            services.AddSingleton<PageService>();
            services.AddSingleton<WindowService>();
            services.AddSingleton<MenuPageService>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
    }
}
