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
    public class MyInventoryViewModel : PagedViewModel<UserInventoriesFilters>
    {
        public override UserInventoriesFilters Filters { get; set; }

        public ObservableCollection<UserInventory> MyInventory { get; set; }

        public MyInventoryViewModel(HttpContext httpContext, HttpClientsServices.Authorization authorization) 
            : base(httpContext, authorization)
        {
            Filters = new UserInventoriesFilters()
            {
                UserId = Guid.Empty,
                Pagination = new Pagination()
                {
                    Page = 1,
                    Limit = 50
                }
            };
            MyInventory = new ObservableCollection<UserInventory>();
        }

        private protected override async Task LoadedAsync()
        {
            Filters.UserId = _authorization.GetUserId();

            await base.LoadedAsync();
        }

        private protected override async Task SearchAsync()
        {
            await LoadInventoryAsync();
        }

        private async Task LoadInventoryAsync()
        {
            Loading = true;

            var response = await _httpContext.ResourceAPI.UserInventories.GetUserInventoryAsync(Filters);

            if (response.Status.Code == HttpStatusCode.OK)
            {
                var pagination = response.Result.PageInfo;

                Pagination = new Model.Common.Pagination(pagination.Page, pagination.Limit, pagination.TotalItems);

                MyInventory.Clear();

                foreach (var item in response.Result.Items)
                {
                    MyInventory.Add(item);
                }
            }

            Loading = false;
        }
    }
}
