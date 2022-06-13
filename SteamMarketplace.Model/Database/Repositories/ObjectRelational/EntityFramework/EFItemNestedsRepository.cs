using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFItemNestedsRepository : IItemNestedsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFItemNestedsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public IQueryable<ItemNested> GetItemNesteds(List<Guid> itemIds)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            return _context.ItemNesteds
                .Include(itemNested => itemNested.Nested)
                    .ThenInclude(nested => nested.Image)
                .Where(itemNested => itemIds.Contains(itemNested.ItemId))
                .AsNoTracking();
        }
    }
}
