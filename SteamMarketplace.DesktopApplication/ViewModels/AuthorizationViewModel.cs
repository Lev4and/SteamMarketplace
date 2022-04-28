using DevExpress.Mvvm;
using SteamMarketplace.Authorization.Models;
using SteamMarketplace.DesktopApplication.Commands;
using SteamMarketplace.DesktopApplication.Services;
using SteamMarketplace.HttpClients;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pages = SteamMarketplace.DesktopApplication.Views.Pages;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class AuthorizationViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly HttpContext _httpContext;

        public string Login { get; set; }

        public string Password { get; set; }

        public AuthorizationViewModel(PageService pageService, HttpContext httpContext)
        {
            _pageService = pageService;
            _httpContext = httpContext;
        }

        public ICommand PasswordChanged => new Command((obj) =>
        {
            if (obj != null) Password = (obj as PasswordBox).Password;
        });

        public ICommand Authorize => new AsyncCommand(() =>
        {
            return AuthorizeAsync();
        }, () => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password));

        private async Task AuthorizeAsync()
        {
            var response = await _httpContext.AuthorizationAPI.Authorization.LoginAsync(new Login(Login, Password));

            if (response.Status.Code == System.Net.HttpStatusCode.OK)
            {
                _pageService.ChangePage(new Pages.Menu.Menu());
            }
            else
            {
                MessageBox.Show(response.Status.Message);
            }
        }
    }
}
