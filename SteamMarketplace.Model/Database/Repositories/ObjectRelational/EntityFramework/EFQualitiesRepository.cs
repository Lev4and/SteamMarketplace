using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFQualitiesRepository : IQualitiesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFQualitiesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public void Add(Quality entity)
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

        public bool Contains(Quality entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return _context.Qualities.FirstOrDefault(quality => quality.Name == entity.Name && 
                quality.RuName == entity.RuName) != null;
        }

        public void DeleteById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            _context.Qualities.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IQueryable<Quality> GetAllByFilters(QualitiesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Qualities
                .OrderBy(quality => quality.RuName)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public IQueryable<Quality> GetAllQualities()
        {
            return _context.Qualities
                .OrderBy(quality => quality.Name)
                .AsNoTracking();
        }

        public Quality GetById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            return _context.Qualities
                .AsNoTracking()
                .FirstOrDefault(quality => quality.Id == id);
        }

        public int GetCount(QualitiesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Qualities.Count();
        }

        public void Update(Quality entity)
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
