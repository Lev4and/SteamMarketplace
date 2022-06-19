using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFTransactionTypesRepository : ITransactionTypesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFTransactionTypesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public void Add(TransactionType entity)
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

        public bool Contains(TransactionType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return _context.TransactionTypes.FirstOrDefault(transactionType => transactionType.Name == entity.Name &&
                transactionType.RuName == entity.RuName) != null;
        }

        public void DeleteById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            _context.TransactionTypes.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IQueryable<TransactionType> GetAllByFilters(TransactionTypesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.TransactionTypes
                .OrderBy(transactionType => transactionType.RuName)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public IQueryable<TransactionType> GetAllTransactionTypes()
        {
            return _context.TransactionTypes
                .OrderBy(transactionType => transactionType.RuName)
                .AsNoTracking();
        }

        public TransactionType GetById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            return _context.TransactionTypes
                .AsNoTracking()
                .FirstOrDefault(transactionType => transactionType.Id == id);
        }

        public int GetCount(TransactionTypesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.TransactionTypes.Count();
        }

        public void Update(TransactionType entity)
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
