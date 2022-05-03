using DevExpress.Mvvm;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class MyInventoryViewModel : BindableBase
    {
        private readonly HttpContext _httpContext;

        public bool Loading { get; set; }

        public UserInventoriesFilters Filters { get; set; }

        public ObservableCollection<UserInventory> MyInventory { get; set; }

        public ICommand Loaded => new AsyncCommand(() =>
        {
            return LoadedAsync();
        });

        public ICommand ScrollViewerChanged => new AsyncCommand<object>((obj) =>
        {
            return LoadAsync(obj as ScrollChangedEventArgs);
        });

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
                foreach (var item in response.Result.Items)
                {
                    MyInventory.Add(item);
                }
            }

            Loading = false;
        }

        private async Task LoadAsync(ScrollChangedEventArgs eventArgs)
        {
            if ((int)(eventArgs.VerticalOffset / (eventArgs.ExtentHeight - eventArgs.ViewportHeight) * 100) >= 80)
            {
                Filters.Pagination.NextPage();

                await LoadInventoryAsync();
            }
        }
    }
}
