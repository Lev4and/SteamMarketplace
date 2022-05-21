using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFQualitiesRepository : IQualitiesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFQualitiesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public IQueryable<Quality> GetAllQualities()
        {
            return _context.Qualities
                .OrderBy(quality => quality.Name)
                .AsNoTracking();
        }
    }
}
