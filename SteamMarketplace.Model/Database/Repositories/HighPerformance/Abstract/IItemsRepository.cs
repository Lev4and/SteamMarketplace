using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IItemsRepository
    {
        bool Contains(long assetId);

        bool Save(Item entity, bool checkOnUnique = true);

        Guid GetItemId(long assetId);

        Guid GetItemIdByFullName(string fullName);
    }
}
