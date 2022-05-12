using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFItemsRepository : IItemsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFItemsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public int GetCountGroupedItems(ItemsFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters", "The filters must not be empty.");
            }

            return _context.Sales
                .Include(sale => sale.Item)
                .Where(sale => sale.SoldAt == null && sale.CancelledAt == null)
                .GroupBy(sale => new { sale.Item.FullName })
                .Where(group => EF.Functions.Like(group.Key.FullName, $"%{filters.SearchString}%"))
                .Count();
        }

        public IQueryable<GroupedItem> GetGroupedItems(ItemsFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters", "The filters must not be empty.");
            }

            return _context.Sales
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Image)
                .Where(sale => sale.SoldAt == null && sale.CancelledAt == null)
                .GroupBy(sale => new { sale.Item.FullName, sale.Item.Image.SteamImg })
                .Select(group => new GroupedItem
                {
                    Count = group.Count(),
                    MinPriceUsd = group.Min(sale => sale.PriceUsd),
                    FullName = group.Key.FullName,
                    SteamImg = group.Key.SteamImg
                })
                .Where(group => EF.Functions.Like(group.FullName, $"%{filters.SearchString}%"))
                .OrderByDescending(group => group.Count)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public IQueryable<string> GetSearchSuggestions(string searchString)
        {
            if (searchString == null)
            {
                throw new ArgumentNullException(nameof(searchString));
            }

            return _context.Items
                .GroupBy(item => new { item.FullName })
                .Where(group => EF.Functions.Like(group.Key.FullName, $"%{searchString}%"))
                .Select(group => group.Key.FullName)
                .OrderBy(group => group)
                .Take(7)
                .AsNoTracking();
        }
    }
}
