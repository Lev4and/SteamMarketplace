﻿using Microsoft.EntityFrameworkCore;
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

        public IQueryable<GroupedItem> GetGroupedItems(ItemsFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters", "The filters must not be empty.");
            }

            var exchangeRate = _context.ExchangeRates
                .Include(exchangeRate => exchangeRate.Currency)
                .Where(exchangeRate => exchangeRate.CurrencyId == filters.CurrencyId)
                .OrderByDescending(exchangeRate => exchangeRate.DateTime)
                .First();

            var cultureInfoName = exchangeRate?.Currency?.CultureInfoName ?? "us-US";
            var rate = exchangeRate?.Rate ?? 1;

            return _context.Sales
                .Include(sale => sale.Item)
                    .ThenInclude(item => item.Image)
                .Where(sale => sale.SoldAt == null && sale.CancelledAt == null
                    && EF.Functions.Like(sale.Item.FullName, $"%{filters.SearchString}%"))
                .GroupBy(sale => new { sale.Item.FullName })
                .Select(group => new GroupedItem
                {
                    Count = group.Count(),
                    MinPrice = group.Min(sale => sale.PriceUsd) * rate,
                    MinPriceUsd = group.Min(sale => sale.PriceUsd),
                    FullName = group.Key.FullName,
                    SteamImg = group.First().Item.Image.SteamImg,
                    CultureInfoName = cultureInfoName
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
