using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IUserInventoriesRepository
    {
        bool Contains(Guid userId, Guid itemId);

        bool Save(UserInventory entity, bool checkOnUnique = true);

        int GetCountItems(Guid userId);

        Guid GetUserInventoryId(Guid userId, Guid itemId);

        List<RandomUserInventoryItem> GetRandomItems(Guid userId, int limit);

        void DeleteItemFromUserInventory(Guid userId, Guid itemId);
    }
}
