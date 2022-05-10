using DevExpress.Mvvm;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonModels = SteamMarketplace.Model.Common;
using HttpClientsServices = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class MySalesViewModel : BindableBase
    {
        private readonly HttpContext _httpContext;
        private HttpClientsServices.Authorization _authorization;

        public bool Loading { get; set; }

        public SalesFilters Filters { get; set; }

        public CommonModels.Pagination Pagination { get; set; }

        public ObservableCollection<Sale> MySales { get; set; }

        public ICommand Loaded => new AsyncCommand(() =>
        {
            Filters.UserId = _authorization.GetUserId();

            return LoadedAsync();
        });

        public ICommand PreviousPage => new AsyncCommand(() =>
        {
            Filters.Pagination.PreviousPage();

            return LoadSalesAsync();
        }, () => Filters.Pagination.Page > 1);

        public ICommand NextPage => new AsyncCommand(() =>
        {
            Filters.Pagination.NextPage();

            return LoadSalesAsync();
        }, () => Filters.Pagination.Page < Pagination.PagesCount);

        public MySalesViewModel(HttpContext httpContext, HttpClientsServices.Authorization authorization)
        {
            _httpContext = httpContext;
            _authorization = authorization;

            Loading = false;
            Filters = new SalesFilters()
            {
                UserId = Guid.Empty,
                Pagination = new Pagination()
                {
                    Page = 1,
                    Limit = 50
                }
            };
            Pagination = new CommonModels.Pagination(1, 50, 0);
            MySales = new ObservableCollection<Sale>();
        }

        private async Task LoadedAsync()
        {
            await LoadSalesAsync();
        }

        private async Task LoadSalesAsync()
        {
            Loading = true;

            var response = await _httpContext.ResourceAPI.Sales.GetMySalesAsync(Filters);

            if (response.Status.Code == HttpStatusCode.OK)
            {
                Pagination = response.Result.PageInfo;

                MySales.Clear();

                foreach (var item in response.Result.Items)
                {
                    MySales.Add(item);
                }
            }

            Loading = false;
        }
    }
}
