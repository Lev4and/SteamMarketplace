using Microsoft.AspNetCore.Identity;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IUserRolesRepository
    {
        bool Contains(Guid userId, Guid roleId);

        bool Save(IdentityUserRole<Guid> entity);
    }
}
