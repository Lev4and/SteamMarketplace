using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IFilterableRepository<T, F> where T : BaseEntity where F : Filters
    {
        int GetCount(F filters);

        IQueryable<T> GetAllByFilters(F filters);
    }
}
