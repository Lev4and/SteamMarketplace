using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IQualitiesRepository
    {
        bool Contains(string name, out Guid id);

        bool Save(Quality entity, bool checkOnUnique = true);

        Guid GetQualityId(string name);
    }
}
