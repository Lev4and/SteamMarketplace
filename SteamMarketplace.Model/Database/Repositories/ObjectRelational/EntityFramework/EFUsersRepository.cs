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

        public ApplicationUser GetUserById(Guid id)
        {
            return _context.Users
                .Include(user => user.Currency)
                    .ThenInclude(currency => currency.Rates.OrderByDescending(rate => rate.DateTime).Take(1))
                .FirstOrDefault(user => user.Id == id);
        }
    }
}
