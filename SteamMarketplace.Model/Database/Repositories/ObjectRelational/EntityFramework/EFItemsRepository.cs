using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
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

        public IQueryable<AddedItemsDynamic> GetAddedItemsDynamics(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return _context.Items
                .Where(item => item.FullName == fullName)
                .GroupBy(item => new { item.AddedAt.Year, item.AddedAt.Month, item.AddedAt.Day, item.AddedAt.Hour })
                .Select(group => new AddedItemsDynamic
                {
                    Date = new DateTime(group.Key.Year, group.Key.Month, group.Key.Day, group.Key.Hour, 0, 0),
                    CountAdded = group.Count(),
                })
                .AsNoTracking();
        }

        public float? GetAverageFloatItem(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return _context.Items
                .Where(item => item.FullName == fullName)
                .Average(item => item.Float);
        }

        public int GetCountGroupedItems(ItemsFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters", "The filters must not be empty.");
            }

            return _context.Sales
                .Include(sale => sale.Item)
                .Where(sale => sale.SoldAt == null && sale.CancelledAt == null 
                    && EF.Functions.Like(sale.Item.FullName, $"%{filters.SearchString}%"))
                .GroupBy(sale => new { sale.Item.FullName })
                .Count();
        }

        public int GetCountItems()
        {
            return _context.Items.Count();
        }

        public int GetCountItems(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return _context.Items
                .Where(item => item.FullName == fullName)
                .Count();
        }

        public int GetCountOwnersItems(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return _context.UserInventories
                .Include(userInventory => userInventory.Item)
                .Where(userInventory => userInventory.Item.FullName == fullName)
                .GroupBy(userInventory => userInventory.UserId)
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
                .Where(sale => sale.SoldAt == null && sale.CancelledAt == null
                    && EF.Functions.Like(sale.Item.FullName, $"%{filters.SearchString}%"))
                .GroupBy(sale => new { sale.Item.FullName })
                .Select(group => new GroupedItem
                {
                    Count = group.Count(),
                    MinPriceUsd = group.Min(sale => sale.PriceUsd),
                    FullName = group.Key.FullName,
                    SteamImg = group.First().Item.Image.SteamImg,
                })
                .OrderByDescending(group => group.Count)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }

        public Item GetItemByFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return _context.Items
                .Include(item => item.Type)
                .Include(item => item.Image)
                .Include(item => item.Rarity)
                .Include(item => item.Quality)
                .Include(item => item.Collection)
                .Include(item => item.Application)
                .OrderBy(item => item.AddedAt)
                .FirstOrDefault(item => item.FullName == fullName);
        }

        public DateTime GetMinAddedAtItem(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return _context.Items
                .Where(item => item.FullName == fullName)
                .Min(item => item.AddedAt);
        }

        public double GetRarityItem(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return (double)GetCountItems(fullName) / (double)GetCountItems();
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
