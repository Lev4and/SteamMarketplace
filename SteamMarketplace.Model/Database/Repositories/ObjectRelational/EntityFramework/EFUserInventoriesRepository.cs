using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFUserInventoriesRepository : IUserInventoriesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFUserInventoriesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public int GetCountItemsInUserInventory(Guid userId)
        {
            return _context.UserInventories
                .Where(userInventory => userInventory.UserId == userId)
                .Count();
        }

        public IQueryable<UserInventory> GetUserInventory(UserInventoriesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters", "The filters must not be empty.");
            }

            return _context.UserInventories
                .Include(userInventory => userInventory.User)
                    .ThenInclude(user => user.Currency)
                .Include(userInventory => userInventory.Item)
                    .ThenInclude(item => item.Rarity)
                .Include(userInventory => userInventory.Item)
                    .ThenInclude(item => item.Type)
                .Include(userInventory => userInventory.Item)
                    .ThenInclude(item => item.Quality)
                .Include(userInventory => userInventory.Item)
                    .ThenInclude(item => item.Image)
                .Include(userInventory => userInventory.Item)
                    .ThenInclude(item => item.Collection)
                .Include(userInventory => userInventory.Item)
                    .ThenInclude(item => item.Application)
                .Where(userInventory => userInventory.UserId == filters.UserId)
                .OrderByDescending(userInventory => userInventory.AddedAt)
                .Skip((filters.Pagination.Page - 1) * filters.Pagination.Limit)
                .Take(filters.Pagination.Limit)
                .AsNoTracking();
        }
    }
}
