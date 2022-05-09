using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IUserInventoriesRepository
    {
        bool Contains(Guid userId, Guid itemId);

        bool Save(UserInventory entity, bool checkOnUnique = true);

        Guid GetUserInventoryId(Guid userId, Guid itemId);

        void DeleteItemFromUserInventory(Guid userId, Guid itemId);
    }
}
