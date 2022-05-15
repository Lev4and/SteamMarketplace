using DevExpress.Mvvm;
using SteamMarketplace.HttpClients;
using System.Threading.Tasks;
using System.Windows.Input;
using HttpClientsServices = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        protected private readonly HttpContext _httpContext;
        protected private readonly HttpClientsServices.Authorization _authorization;

        public bool Loading { get; set; } = false;

        public ICommand Loaded => new AsyncCommand(() =>
        {
            return LoadedAsync();
        });

        public BaseViewModel(HttpContext httpContext, HttpClientsServices.Authorization authorization)
        {
            _httpContext = httpContext;
            _authorization = authorization;
        }

        protected private virtual async Task LoadedAsync()
        {
            
        }
    }
}
