using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetUserRolesRepository : IUserRolesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetUserRolesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(Guid userId, Guid roleId)
        {
            var query = $"SELECT \"UserId\", \"RoleId\" " +
                $"FROM \"AspNetUserRoles\" " +
                $"WHERE \"AspNetUserRoles\".\"UserId\" = @UserId AND \"AspNetUserRoles\".\"RoleId\" = @RoleId " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId },
                new NpgsqlParameter() { ParameterName = "@RoleId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = roleId }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            return result.Count > 0;
        }

        public bool Save(IdentityUserRole<Guid> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            if (!Contains(entity.UserId, entity.RoleId))
            {
                var query = $"INSERT INTO \"AspNetUserRoles\" (\"UserId\", \"RoleId\") VALUES (@UserId, @RoleId)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.UserId },
                    new NpgsqlParameter() { ParameterName = "@RoleId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.RoleId }
                };

                _context.ExecuteQuery(query, parameters);
            }

            return true;
        }
    }
}
