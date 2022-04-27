using SteamMarketplace.DesktopApplication.ViewModels.Locators;
using System.Windows;

namespace SteamMarketplace.DesktopApplication
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModelLocator.Init();

            base.OnStartup(e);
        }
    }
}
