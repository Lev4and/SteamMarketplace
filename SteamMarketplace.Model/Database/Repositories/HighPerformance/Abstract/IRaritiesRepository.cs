using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IRaritiesRepository
    {
        bool Contains(string name, out Guid id);

        bool Save(Rarity entity, bool checkOnUnique = true);

        Guid GetRarityId(string name);
    }
}
