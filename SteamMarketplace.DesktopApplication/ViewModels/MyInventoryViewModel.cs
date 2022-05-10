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
    public class MyInventoryViewModel : BindableBase
    {
        private readonly HttpContext _httpContext;
        private HttpClientsServices.Authorization _authorization;

        public bool Loading { get; set; }

        public UserInventoriesFilters Filters { get; set; }

        public CommonModels.Pagination Pagination { get; set; }

        public ObservableCollection<UserInventory> MyInventory { get; set; }

        public ICommand Loaded => new AsyncCommand(() =>
        {
            Filters.UserId = _authorization.GetUserId();

            return LoadedAsync();
        });

        public ICommand PreviousPage => new AsyncCommand(() =>
        {
            Filters.Pagination.PreviousPage();

            return LoadInventoryAsync();
        }, () => Filters.Pagination.Page > 1);

        public ICommand NextPage => new AsyncCommand(() =>
        {
            Filters.Pagination.NextPage();

            return LoadInventoryAsync();
        }, () => Filters.Pagination.Page < Pagination.PagesCount);

        public MyInventoryViewModel(HttpContext httpContext, HttpClientsServices.Authorization authorization)
        {
            _httpContext = httpContext;
            _authorization = authorization;

            Loading = false;
            Filters = new UserInventoriesFilters()
            {
                UserId = Guid.Empty,
                Pagination = new Pagination()
                {
                    Page = 1,
                    Limit = 50
                }
            };
            Pagination = new CommonModels.Pagination(1, 50, 0);
            MyInventory = new ObservableCollection<UserInventory>();
        }

        private async Task LoadedAsync()
        {
            await LoadInventoryAsync();
        }

        private async Task LoadInventoryAsync()
        {
            Loading = true;

            var response = await _httpContext.ResourceAPI.UserInventories.GetUserInventoryAsync(Filters);

            if (response.Status.Code == HttpStatusCode.OK)
            {
                Pagination = response.Result.PageInfo;

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
