using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IItemImagesRepository
    {
        bool Contains(Guid itemId);

        bool Save(ItemImage entity, bool checkOnUnique = true);

        Guid GetItemImageId(Guid itemId);
    }
}
