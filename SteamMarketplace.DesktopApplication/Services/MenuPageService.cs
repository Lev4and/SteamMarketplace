using System;
using System.Windows.Controls;

namespace SteamMarketplace.DesktopApplication.Services
{
    public class MenuPageService
    {
        public event Action<Page> OnPageChanged;

        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
