using DevExpress.Mvvm;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonModels = SteamMarketplace.Model.Common;
using HttpClientsServices = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public abstract class PagedViewModel<T> : BaseViewModel, IDisposable where T : Filters
    {
        public abstract T Filters { get; set; }

        public CommonModels.Pagination Pagination { get; set; }

        public ICommand Reset => new AsyncCommand(() =>
        {
            Filters.Reset();

            return ResetAsync();
        });

        public ICommand Search => new AsyncCommand(() =>
        {
            Filters.Pagination.Reset();

            return SearchAsync();
        });

        public PagedViewModel(HttpContext httpContext, HttpClientsServices.Authorization authorization) 
            : base(httpContext, authorization)
        {
            Pagination = new CommonModels.Pagination(1, 1, 0);
        }

        protected private virtual async Task ResetAsync()
        {
            await SearchAsync();
        }

        protected private virtual async Task SearchAsync()
        {

        }

        protected private virtual async Task OnPageChnaged()
        {
            if (Filters.Pagination.Page != Pagination.Page)
            {
                await SearchAsync();
            }
        }

        private protected override async Task LoadedAsync()
        {
            Filters.Pagination.PageChanged += OnPageChnaged;

            await SearchAsync();
        }

        public void Dispose()
        {
            Filters.Pagination.PageChanged -= OnPageChnaged;
        }
    }
}
