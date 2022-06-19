using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IRaritiesRepository : IFilterableRepository<Rarity, RaritiesFilters>, ICRUDRepository<Rarity>
    {
        IQueryable<Rarity> GetAllRarities();
    }
}
