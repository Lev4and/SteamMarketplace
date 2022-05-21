using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFCollectionsRepository : ICollectionsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFCollectionsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public IQueryable<Collection> GetAllCollections()
        {
            return _context.Collections
                .OrderBy(collection => collection.Name)
                .AsNoTracking();
        }
    }
}
