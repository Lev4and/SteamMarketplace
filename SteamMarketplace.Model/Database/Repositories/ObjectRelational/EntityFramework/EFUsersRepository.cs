using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly SteamMarketplaceDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EFUsersRepository(SteamMarketplaceDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ApplicationUser GetUserById(Guid id, bool track = false)
        {
            IQueryable<ApplicationUser> users = _userManager.Users;

            if (!track)
            {
                users = users.AsNoTracking();
            }

            return users.SingleOrDefault(user => user.Id == id);
        }
    }
}
