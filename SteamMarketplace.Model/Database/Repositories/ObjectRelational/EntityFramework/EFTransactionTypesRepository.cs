using Microsoft.EntityFrameworkCore;
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

        public IQueryable<TransactionType> GetAllTransactionTypes()
        {
            return _context.TransactionTypes
                .OrderBy(transactionType => transactionType.RuName)
                .AsNoTracking();
        }
    }
}
