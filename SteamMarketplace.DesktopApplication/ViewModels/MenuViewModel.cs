using DevExpress.Mvvm;
using SteamMarketplace.DesktopApplication.Services;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Hubs;
using SteamMarketplace.Hubs.HubEventArgs;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pages = SteamMarketplace.DesktopApplication.Views.Pages;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class MenuViewModel : BindableBase, IDisposable
    {
        private readonly HubContext _hubContext;
        private readonly HttpContext _httpContext;
        private readonly MenuPageService _menuPageService;

        public int Online { get; set; }

        public bool IsLeftDrawerOpen { get; set; }

        public Page PageSource { get; set; }

        public ICommand Loaded => new AsyncCommand(() =>
        {
            return LoadedAsync();
        });

        public ICommand OnUnchecked => new DelegateCommand(() =>
        {
            IsLeftDrawerOpen = false;
        });

        public ICommand ImportOnline => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Pages.ImportOnline.ImportOnline());
        });

        public ICommand MyInventory => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Pages.MyInventory.MyInventory());
        });

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });

        public MenuViewModel(HubContext hubContext, HttpContext httpContext, MenuPageService menuPageService)
        {
            Online = 0;
            PageSource = new Pages.Dashboard.Dashboard();

            _hubContext = hubContext;
            _httpContext = httpContext;

            _menuPageService = menuPageService;
            _menuPageService.OnPageChanged += (page) => { IsLeftDrawerOpen = false; PageSource = page; };
        }

        private void Online_OnlineChanged(OnlineHubEventArgs obj)
        {
            Online = obj.Online;
        }

        private async Task LoadedAsync()
        {
            _hubContext.ResourceAPI.Online.OnlineChanged += Online_OnlineChanged;
            
            var exchangeRates = await _httpContext.ResourceAPI.CBRExchangeRates.GetLatestExchangeRateAsync();

            await _httpContext.ResourceAPI.ImportExchangeRate.ImportAsync(exchangeRates.Result);
            await _hubContext.ResourceAPI.Online.Connect();
        }

        public void Dispose()
        {
            _hubContext.ResourceAPI.Online.OnlineChanged -= Online_OnlineChanged;
        }
    }
}
