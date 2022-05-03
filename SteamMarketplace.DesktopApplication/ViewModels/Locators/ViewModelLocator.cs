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

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<ImportOnlineViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MenuViewModel>();

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

            services.AddSingleton<Hubs.ResourceAPI.ImportHubClient>();
            services.AddSingleton<Hubs.ResourceAPI.OnlineHubClient>();
            services.AddSingleton<Hubs.ResourceAPI.AutoImportHubClient>();
            services.AddSingleton<Hubs.ResourceAPI.ResourceAPIHubContext>();
            services.AddSingleton<HubContext>();

            services.AddSingleton<PageService>();
            services.AddSingleton<WindowService>();
            services.AddSingleton<MenuPageService>();
            services.AddSingleton<MyInventoryViewModel>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
    }
}
