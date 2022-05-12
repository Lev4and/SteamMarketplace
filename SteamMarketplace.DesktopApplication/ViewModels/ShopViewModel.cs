using DevExpress.Mvvm;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonModels = SteamMarketplace.Model.Common;

namespace SteamMarketplace.DesktopApplication.ViewModels
{
    public class ShopViewModel : BindableBase
    {
        private readonly HttpContext _httpContext;

        public bool Loading { get; set; }

        public ItemsFilters Filters { get; set; }

        public CommonModels.Pagination Pagination { get; set; }

        public ObservableCollection<string> SearchSuggestions { get; set; }

        public ObservableCollection<GroupedItem> GroupedItems { get; set; }

        public ICommand Reset => new AsyncCommand(() =>
        {
            Filters.Reset();

            return LoadedAsync();
        });

        public ICommand Search => new AsyncCommand(() =>
        {
            Filters.Pagination.Reset();

            return LoadedAsync();
        });

        public ICommand Loaded => new AsyncCommand(() =>
        {
            return LoadedAsync();
        });

        public ICommand PreviousPage => new AsyncCommand(() =>
        {
            Filters.Pagination.PreviousPage();

            return LoadGroupedItemsAsync();
        }, () => Filters.Pagination.Page > 1);

        public ICommand NextPage => new AsyncCommand(() =>
        {
            Filters.Pagination.NextPage();

            return LoadGroupedItemsAsync();
        }, () => Filters.Pagination.Page < Pagination.PagesCount);

        public ICommand SearchStringChanged => new AsyncCommand(() =>
        {
            return LoadSearchSuggestionsAsync();
        });

        public ShopViewModel(HttpContext httpContext)
        {
            _httpContext = httpContext;

            Loading = false;
            Filters = new ItemsFilters()
            {
                SearchString = "",
                Pagination = new Pagination()
                {
                    Page = 1,
                    Limit = 50
                }
            };
            Pagination = new CommonModels.Pagination(1, 50, 0);
            GroupedItems = new ObservableCollection<GroupedItem>();
            SearchSuggestions = new ObservableCollection<string>();
        }

        private async Task LoadedAsync()
        {
            await LoadGroupedItemsAsync();
        }

        private async Task LoadGroupedItemsAsync()
        {
            Loading = true;

            var response = await _httpContext.ResourceAPI.Items.GetGroupedItemsAsync(Filters);

            if (response.Status.Code == HttpStatusCode.OK)
            {
                Pagination = response.Result.PageInfo;

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
