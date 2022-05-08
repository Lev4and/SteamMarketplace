using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IExchangeRatesRepository
    {
        bool Contains(DateTime dateTime, Guid currencyId, out Guid id);

        bool Save(ExchangeRate entity);

        decimal GetRateCurrency(Guid currencyId);

        Guid GetExchangeRateId(DateTime dateTime, Guid currencyId);
    }
}
