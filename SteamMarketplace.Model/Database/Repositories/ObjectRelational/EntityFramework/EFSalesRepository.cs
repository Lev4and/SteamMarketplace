using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
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

        public IQueryable<dynamic> GetSales(SalesFilters filters)
        {
            return _context.Sales
                .Include(sale => sale.Seller)
                .Include(sale => sale.Purchase)
                    .ThenInclude(purchase => purchase.Buyer)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Image)
                .Where(sale => sale.SellerId == filters.UserId)
                .GroupBy(sale => sale.ExposedAt.Date)
                .Select(groupedSale => new
                {
                    Date = groupedSale.Key,
                    Sales = groupedSale.Select(sales => sales)
                })
                .OrderByDescending(groupedSale => groupedSale.Date)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }
    }
}
