using DevExpress.Mvvm;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using HttpClientsServices = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class ShopViewModel : PagedViewModel<ItemsFilters>
    {
        public string FullName { get; set; }

        public override ItemsFilters Filters { get; set; }

        public ObservableCollection<string> SearchSuggestions { get; set; }

        public ObservableCollection<GroupedItem> GroupedItems { get; set; }

        public ICommand Info => new AsyncCommand(() =>
        {
            return Task.Delay(1000);
        }, () => !string.IsNullOrEmpty(FullName));

        public ICommand SearchStringChanged => new AsyncCommand(() =>
        {
            return LoadSearchSuggestionsAsync();
        });

        public ShopViewModel(HttpContext httpContext, HttpClientsServices.Authorization authorization)
            : base(httpContext, authorization)
        {
            Filters = new ItemsFilters()
            {
                SearchString = "",
                Pagination = new Pagination()
                {
                    Page = 1,
                    Limit = 50
                }
            };
            GroupedItems = new ObservableCollection<GroupedItem>();
            SearchSuggestions = new ObservableCollection<string>();
        }

        private protected override async Task LoadedAsync()
        {
            await base.LoadedAsync();
        }

        private protected override async Task SearchAsync()
        {
            await LoadGroupedItemsAsync();
        }

        private async Task LoadGroupedItemsAsync()
        {
            Loading = true;

            var response = await _httpContext.ResourceAPI.Items.GetGroupedItemsAsync(Filters);

            if (response.Status.Code == HttpStatusCode.OK)
            {
                var pagination = response.Result.PageInfo;

                Pagination = new Model.Common.Pagination(pagination.Page, pagination.Limit, pagination.TotalItems);

                GroupedItems.Clear();

                foreach (var item in response.Result.Items)
                {
                    GroupedItems.Add(item);
                }
            }

            Loading = false;
        }

        private async Task LoadSearchSuggestionsAsync()
        {
            if (!string.IsNullOrEmpty(Filters.SearchString))
            {
                var response = await _httpContext.ResourceAPI.Items.GetSearchSuggestionsAsync(Filters.SearchString);

                if (response.Status.Code == HttpStatusCode.OK)
                {
                    SearchSuggestions.Clear();

                    foreach (var item in response.Result)
                    {
                        SearchSuggestions.Add(item);
                    }
                }
            }
            else
            {
                SearchSuggestions.Clear();
            }
        }
    }
}
