using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IItemsRepository : IFilterableRepository<Item, ItemsFilters>, ICRUDRepository<Item>
    {
        int GetCountItems();

        int GetCountItems(string fullName);

        int GetCountOwnersItems(string fullName);

        int GetCountGroupedItems(ItemsFilters filters);

        double GetRarityItem(string fullName);

        float? GetAverageFloatItem(string fullName);

        DateTime GetMinAddedAtItem(string fullName);

        Item GetItemByFullName(string fullName);

        IQueryable<string> GetSearchSuggestions(string searchString);

        IQueryable<GroupedItem> GetGroupedItems(ItemsFilters filters);

        IQueryable<AddedItemsDynamic> GetAddedItemsDynamics(string fullName);
    }
}
