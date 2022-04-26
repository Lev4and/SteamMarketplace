using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface ICollectionsRepository
    {
        bool Contains(string name, out Guid id);

        bool Save(Collection entity, bool checkOnUnique = true);

        Guid GetCollectionId(string name);
    }
}
