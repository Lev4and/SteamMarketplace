using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
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

            var query = $"SELECT \"Id\" " +
                $"FROM \"AspNetRoles\" " +
                $"WHERE \"AspNetRoles\".\"Name\" = @Name " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Name", NpgsqlDbType = NpgsqlDbType.Text, Value = name }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }
    }
}
