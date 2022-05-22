using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFExchangeRatesRepository : IExchangeRatesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFExchangeRatesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public IQueryable<ExchangeRate> GetExchangeRatesByCurrencyId(Guid currencyId)
        {
            return _context.ExchangeRates
                .Include(exchangeRate => exchangeRate.Currency)
                .Where(exchangeRate => exchangeRate.CurrencyId == currencyId)
                .OrderBy(exchangeRate => exchangeRate.DateTime)
                .AsNoTracking();
        }
    }
}
