using Microsoft.Data.SqlClient;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetApplicatonRolesRepository : IApplicatonRolesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetApplicatonRolesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public Guid GetRoleIdByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The name must not be empty.");
            }

            var query = $"SELECT TOP(1) Id " +
                $"FROM AspNetRoles " +
                $"WHERE AspNetRoles.Name = @Name";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = name }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }
    }
}
