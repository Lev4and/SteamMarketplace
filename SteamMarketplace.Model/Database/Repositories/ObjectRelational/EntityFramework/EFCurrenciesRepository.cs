using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFCurrenciesRepository : ICurrenciesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFCurrenciesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public IQueryable<Currency> GetAllCurrencies()
        {
            return _context.Currencies
                .OrderBy(currency => currency.Literal)
                .AsNoTracking();
        }
    }
}
