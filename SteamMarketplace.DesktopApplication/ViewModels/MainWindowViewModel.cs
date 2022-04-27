using DevExpress.Mvvm;
using SteamMarketplace.DesktopApplication.Services;
using System.Windows.Controls;
using Pages = SteamMarketplace.DesktopApplication.Views.Pages;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly PageService _pageService;

        public Page PageSource { get; set; }

        public MainWindowViewModel(PageService pageService)
        {
            _pageService = pageService;

            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new Pages.Menu.Menu());
        }
    }
}
