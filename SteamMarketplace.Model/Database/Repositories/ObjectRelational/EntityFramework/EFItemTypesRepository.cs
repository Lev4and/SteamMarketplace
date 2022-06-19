using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFItemTypesRepository : IItemTypesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFItemTypesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public void Add(ItemType entity)
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

        public bool Contains(ItemType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return _context.ItemTypes.FirstOrDefault(itemType => itemType.CSMoneyId == entity.CSMoneyId) != null;
        }

        public void DeleteById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            _context.ItemTypes.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IQueryable<ItemType> GetAllByFilters(ItemTypesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.ItemTypes
                .OrderBy(itemType => itemType.Name)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public IQueryable<ItemType> GetAllItemTypes()
        {
            return _context.ItemTypes
                .OrderBy(itemType => itemType.Name)
                .AsNoTracking();
        }

        public ItemType GetById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            return _context.ItemTypes
                .AsNoTracking()
                .FirstOrDefault(itemType => itemType.Id == id);
        }

        public int GetCount(ItemTypesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.ItemTypes.Count();
        }

        public void Update(ItemType entity)
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
