using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFRaritiesRepository : IRaritiesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFRaritiesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public void Add(Rarity entity)
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

        public bool Contains(Rarity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return _context.Rarities.FirstOrDefault(rarity => rarity.Name == entity.Name &&
                rarity.RuName == entity.RuName) != null;
        }

        public void DeleteById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            _context.Rarities.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IQueryable<Rarity> GetAllByFilters(RaritiesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Rarities
                .OrderBy(rarity => rarity.RuName)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public IQueryable<Rarity> GetAllRarities()
        {
            return _context.Rarities
                .OrderBy(rarity => rarity.Name)
                .AsNoTracking();
        }

        public Rarity GetById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            return _context.Rarities
                .AsNoTracking()
                .FirstOrDefault(rarity => rarity.Id == id);
        }

        public int GetCount(RaritiesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Rarities.Count();
        }

        public void Update(Rarity entity)
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
