using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
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

        public void Add(Currency entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id != default)
            {
                throw new ArgumentException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public bool Contains(Currency entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return _context.Currencies.FirstOrDefault(currency => currency.Literal == entity.Literal && 
                currency.CultureInfoName == entity.CultureInfoName) != null;
        }

        public void DeleteById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            _context.Currencies.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IQueryable<Currency> GetAllByFilters(CurrenciesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Currencies
                .OrderBy(currency => currency.Literal)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public IQueryable<Currency> GetAllCurrencies()
        {
            return _context.Currencies
                .OrderBy(currency => currency.Literal)
                .AsNoTracking();
        }

        public Currency GetById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            return _context.Currencies
                .AsNoTracking()
                .FirstOrDefault(currency => currency.Id == id);
        }

        public int GetCount(CurrenciesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Currencies.Count();
        }

        public void Update(Currency entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id == default)
            {
                throw new ArgumentException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
