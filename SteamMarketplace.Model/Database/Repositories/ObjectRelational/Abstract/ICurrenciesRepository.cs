using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface ICurrenciesRepository : IFilterableRepository<Currency, CurrenciesFilters>, ICRUDRepository<Currency>
    {
        IQueryable<Currency> GetAllCurrencies();
    }
}
