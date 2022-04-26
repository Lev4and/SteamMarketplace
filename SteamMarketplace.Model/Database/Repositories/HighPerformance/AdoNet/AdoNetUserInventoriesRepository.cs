using Microsoft.Data.SqlClient;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetUserInventoriesRepository : IUserInventoriesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetUserInventoriesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(Guid userId, Guid itemId)
        {
            var query = $"SELECT TOP(1) * " +
                    $"FROM UserInventories " +
                    $"WHERE UserInventories.UserId = @UserId AND UserInventories.ItemId = @ItemId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId },
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetUserInventoryId(Guid userId, Guid itemId)
        {
            var query = $"SELECT TOP(1) Id " +
                    $"FROM UserInventories " +
                    $"WHERE UserInventories.UserId = @UserId AND UserInventories.ItemId = @ItemId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId },
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(UserInventory entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            if (!checkOnUnique || !Contains(entity.UserId, entity.ItemId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO [UserInventories] (Id, UserId, ItemId) VALUES (@Id, @UserId, @ItemId)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.UserId },
                    new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.ItemId }
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = GetUserInventoryId(entity.UserId, entity.ItemId);
            }

            return true;
        }
    }
}
