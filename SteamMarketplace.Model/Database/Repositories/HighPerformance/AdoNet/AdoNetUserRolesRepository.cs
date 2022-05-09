using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
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
            var query = $"SELECT TOP(1) UserId, RoleId " +
                $"FROM AspNetUserRoles " +
                $"WHERE AspNetUserRoles.UserId = @UserId AND AspNetUserRoles.RoleId = @RoleId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId },
                new SqlParameter() { ParameterName = "@RoleId", SqlDbType = SqlDbType.UniqueIdentifier, Value = roleId }
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
                var query = $"INSERT INTO [AspNetUserRoles] (UserId, RoleId) VALUES (@UserId, @RoleId)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.UserId },
                    new SqlParameter() { ParameterName = "@RoleId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.RoleId }
                };

                _context.ExecuteQuery(query, parameters);
            }

            return true;
        }
    }
}
