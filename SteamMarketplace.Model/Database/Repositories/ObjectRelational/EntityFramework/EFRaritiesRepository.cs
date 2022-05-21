using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFRaritiesRepository : IRaritiesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFRaritiesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public IQueryable<Rarity> GetAllRarities()
        {
            return _context.Rarities
                .OrderBy(rarity => rarity.Name)
                .AsNoTracking();
        }
    }
}
