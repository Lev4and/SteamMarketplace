using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IItemsRepository
    {
        int GetCountGroupedItems(ItemsFilters filters);

        Item GetItemByFullName(string fullName);

        IQueryable<string> GetSearchSuggestions(string searchString);

        IQueryable<GroupedItem> GetGroupedItems(ItemsFilters filters);
    }
}
