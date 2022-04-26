using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IRolesRepository
    {
        ApplicatonRole GetRoleByName(string name, bool track = false);

        IQueryable<ApplicatonRole> GetRoles(bool track = false);
    }
}
