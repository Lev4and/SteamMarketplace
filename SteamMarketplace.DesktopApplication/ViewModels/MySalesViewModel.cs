using SteamMarketplace.HttpClients;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using HttpClientsServices = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class MySalesViewModel : PagedViewModel<SalesFilters>
    {
        public override SalesFilters Filters { get; set; }

        public ObservableCollection<Sale> MySales { get; set; }

        public MySalesViewModel(HttpContext httpContext, HttpClientsServices.Authorization authorization) 
            : base(httpContext, authorization)
        {
            Filters = new SalesFilters()
            {
                UserId = Guid.Empty,
                Pagination = new Pagination()
                {
                    Page = 1,
                    Limit = 50
                }
            };

            MySales = new ObservableCollection<Sale>();
        }

        private protected override async Task LoadedAsync()
        {
            Filters.UserId = _authorization.GetUserId();

            await base.LoadedAsync();
        }

        private protected override async Task SearchAsync()
        {
            await LoadSalesAsync();
        }

        private async Task LoadSalesAsync()
        {
            Loading = true;

            var response = await _httpContext.ResourceAPI.Sales.GetMySalesAsync(Filters);

            if (response.Status.Code == HttpStatusCode.OK)
            {
                var pagination = response.Result.PageInfo;

                Pagination = new Model.Common.Pagination(pagination.Page, pagination.Limit, pagination.TotalItems);

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
