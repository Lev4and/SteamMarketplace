using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework
{
    public class EFRolesRepository : IRolesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public EFRolesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public ApplicatonRole GetRoleByName(string name, bool track = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The name must not be empty.");
            }

            IQueryable<ApplicatonRole> roles = _context.Roles;

            if (!track)
            {
                roles = roles.AsNoTracking();
            }

            return roles.SingleOrDefault(role => role.Name == name);
        }

        public IQueryable<ApplicatonRole> GetRoles(bool track = false)
        {
            IQueryable<ApplicatonRole> roles = _context.Roles;

            if (!track)
            {
                roles = roles.AsNoTracking();
            }

            return roles;
        }
    }
}
