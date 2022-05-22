using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IExchangeRatesRepository
    {
        IQueryable<ExchangeRate> GetExchangeRatesByCurrencyId(Guid currencyId);
    }
}
