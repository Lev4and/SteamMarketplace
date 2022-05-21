using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFItemTypesRepository : IItemTypesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFItemTypesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public IQueryable<ItemType> GetAllItemTypes()
        {
            return _context.ItemTypes
                .OrderBy(itemType => itemType.CSMoneyId)
                .AsNoTracking();
        }
    }
}
