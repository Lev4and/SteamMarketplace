using System;
using System.Windows.Controls;

namespace SteamMarketplace.DesktopApplication.Services
{
    public class PageService
    {
        public event Action<Page> OnPageChanged;

        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
