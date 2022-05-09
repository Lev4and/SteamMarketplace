namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IApplicatonRolesRepository
    {
        Guid GetRoleIdByName(string name);
    }
}
