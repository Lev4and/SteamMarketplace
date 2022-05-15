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
    public class MyPurchasesViewModel : PagedViewModel<PurchasesFilters>
    {
        public override PurchasesFilters Filters { get; set; }

        public ObservableCollection<Purchase> MyPurchases { get; set; }

        public MyPurchasesViewModel(HttpContext httpContext, HttpClientsServices.Authorization authorization)
            : base(httpContext, authorization)
        {
            Filters = new PurchasesFilters()
            {
                UserId = Guid.Empty,
                Pagination = new Pagination()
                {
                    Page = 1,
                    Limit = 50
                }
            };

            MyPurchases = new ObservableCollection<Purchase>();
        }

        private protected override async Task LoadedAsync()
        {
            Filters.UserId = _authorization.GetUserId();

            await base.LoadedAsync();
        }

        private protected override async Task SearchAsync()
        {
            await LoadPurchasesAsync();
        }

        private async Task LoadPurchasesAsync()
        {
            Loading = true;

            var response = await _httpContext.ResourceAPI.Purchases.GetMyPurchasesAsync(Filters);

            if (response.Status.Code == HttpStatusCode.OK)
            {
                var pagination = response.Result.PageInfo;

                Pagination = new Model.Common.Pagination(pagination.Page, pagination.Limit, pagination.TotalItems);

                MyPurchases.Clear();

                foreach (var item in response.Result.Items)
                {
                    MyPurchases.Add(item);
                }
            }

            Loading = false;
        }
    }
}
