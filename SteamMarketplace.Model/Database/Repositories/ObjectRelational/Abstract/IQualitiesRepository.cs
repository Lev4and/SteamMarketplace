using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IQualitiesRepository : IFilterableRepository<Quality, QualitiesFilters>, ICRUDRepository<Quality>
    {
        IQueryable<Quality> GetAllQualities();
    }
}
