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

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class MyInventoryViewModel : BindableBase
    {
        private readonly HttpContext _httpContext;

        public bool Loading { get; set; }

        public UserInventoriesFilters Filters { get; set; }

        public CommonModels.Pagination Pagination { get; set; }

        public ObservableCollection<UserInventory> MyInventory { get; set; }

        public ICommand Loaded => new AsyncCommand(() =>
        {
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

        public MyInventoryViewModel(HttpContext httpContext)
        {
            _httpContext = httpContext;

            Loading = false;
            Filters = new UserInventoriesFilters()
            {
                UserId = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB"),
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
