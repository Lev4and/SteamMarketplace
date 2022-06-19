using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFCollectionsRepository : ICollectionsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFCollectionsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public void Add(Collection entity)
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

        public bool Contains(Collection entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return _context.Collections.FirstOrDefault(collection => collection.Name == entity.Name &&
                collection.RuName == entity.RuName) != null;
        }

        public void DeleteById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            _context.Collections.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IQueryable<Collection> GetAllByFilters(CollectionsFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Collections
                .OrderBy(collection => collection.RuName)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public IQueryable<Collection> GetAllCollections()
        {
            return _context.Collections
                .OrderBy(collection => collection.Name)
                .AsNoTracking();
        }

        public Collection GetById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            return _context.Collections
                .AsNoTracking()
                .FirstOrDefault(collection => collection.Id == id);
        }

        public int GetCount(CollectionsFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Collections.Count();
        }

        public void Update(Collection entity)
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
