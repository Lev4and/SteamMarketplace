using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFSalesRepository : ISalesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFSalesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public int GetCountSales(Guid userId)
        {
            return _context.Sales
                .Where(sale => sale.SellerId == userId)
                .Count();
        }

        public IQueryable<Sale> GetSales(SalesFilters filters)
        {
            return _context.Sales
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Image)
                .Include(sale => sale.Seller)
                    .ThenInclude(seller => seller.Currency)
                .Include(sale => sale.Purchase)
                    .ThenInclude(purchase => purchase.Buyer)
                        .ThenInclude(buyer => buyer.Currency)
                .Where(sale => sale.SellerId == filters.UserId)
                .OrderByDescending(sale => sale.ExposedAt)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }
    }
}
