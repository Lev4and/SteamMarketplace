using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFPurchasesRepository : IPurchasesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFPurchasesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public int GetCountPurchases(Guid userId)
        {
            return _context.Purchases
                .Where(purchase => purchase.BuyerId == userId)
                .Count();
        }

        public IQueryable<Purchase> GetPurchases(PurchasesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Purchases
                .Include(purchase => purchase.Buyer)
                    .ThenInclude(buyer => buyer.Currency)
                .Include(purchase => purchase.Sale)
                    .ThenInclude(sale => sale.Seller)
                        .ThenInclude(seller => seller.Currency)
                .Include(purchase => purchase.Sale)
                    .ThenInclude(sale => sale.Item)
                        .ThenInclude(item => item.Image)
                .Where(purchase => purchase.BuyerId == filters.UserId)
                .OrderByDescending(purchase => purchase.PurchaseAt)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }
    }
}
