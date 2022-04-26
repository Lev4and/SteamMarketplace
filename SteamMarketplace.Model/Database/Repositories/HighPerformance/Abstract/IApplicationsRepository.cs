using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IApplicationsRepository
    {
        bool Contains(int steamId, out Guid id);

        bool Save(Application entity, bool checkOnUnique = true);

        Guid GetApplicationId(int steamId);
    }
}
