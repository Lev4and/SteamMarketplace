using DevExpress.Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class DashboardViewModel : BindableBase
    {
        public ICommand Loaded => new AsyncCommand(() =>
        {
            return LoadedAsync();
        });

        public DashboardViewModel()
        {

        }

        private async Task LoadedAsync()
        {

        }
    }
}
