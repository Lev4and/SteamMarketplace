using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IItemsRepository
    {
        int GetCountGroupedItems(ItemsFilters filters);

        IQueryable<string> GetSearchSuggestions(string searchString);

        IQueryable<GroupedItem> GetGroupedItems(ItemsFilters filters);
    }
}
