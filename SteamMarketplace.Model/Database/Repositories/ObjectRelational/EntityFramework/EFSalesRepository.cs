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

        public int GetCountSalesItem(string fullName)
        {
            return _context.Sales
                .Include(sale => sale.Item)
                .Where(sale => sale.SoldAt == null && sale.CancelledAt == null && sale.Item.FullName == fullName)
                .Count();
        }

        public IQueryable<Sale> GetSales(SalesFilters filters)
        {
            return _context.Sales
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Image)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Type)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Rarity)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Quality)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Collection)
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

        public IQueryable<Sale> GetSalesItem(SalesItemFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return _context.Sales
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Image)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Type)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Rarity)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Quality)
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Collection)
                .Include(sale => sale.Seller)
                    .ThenInclude(seller => seller.Currency)
                .Where(sale => sale.SoldAt == null && sale.CancelledAt == null && sale.Item.FullName == filters.FullName)
                .OrderBy(sale => sale.PriceUsd)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public IQueryable<PricesDynamic> GetPricesDynamicsItem(string fullName)
        {
            return _context.Sales
                .Include(sale => sale.Item)
                .Where(sale => sale.SoldAt != null && sale.Item.FullName == fullName)
                .GroupBy(sale => new { sale.SoldAt.Value.Year, sale.SoldAt.Value.Month, sale.SoldAt.Value.Day, sale.SoldAt.Value.Hour })
                .Select(group => new PricesDynamic
                {
                    Date = new DateTime(group.Key.Year, group.Key.Month, group.Key.Day, group.Key.Hour, 0, 0),
                    CountSold = group.Count(),
                    MinPriceUsd = group.Min(group => group.PriceUsd)
                })
                .AsNoTracking();
        }
    }
}
