using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class AuthorizationDataManager
    {
        public IRolesRepository Roles { get; }

        public IUsersRepository Users { get; }

        public AuthorizationDataManager(IRolesRepository roles, IUsersRepository users)
        {
            Roles = roles;
            Users = users;
        }
    }
}
