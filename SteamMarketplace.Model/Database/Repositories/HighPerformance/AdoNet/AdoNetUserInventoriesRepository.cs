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

        public void DeleteItemFromUserInventory(Guid userId, Guid itemId)
        {
            var query = $"DELETE " +
                $"FROM UserInventories " +
                $"WHERE UserInventories.UserId = @UserId AND UserInventories.ItemId = @ItemId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId },
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId }
            };

            _context.ExecuteQuery(query, parameters);
        }

        public int GetCountItems(Guid userId)
        {
            var query = $"SELECT COUNT(*) AS [Count] " +
                $"FROM UserInventories " +
                $"WHERE UserInventories.UserId = @UserId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<int>("Count");
        }

        public Dictionary<Guid, decimal> GetRandomItems(Guid userId, int limit)
        {
            var query = $"SELECT TOP(@Limit) UserInventories.ItemId, (SELECT TOP(1) Sales.PriceUsd " +
                $"FROM Sales WHERE Sales.ItemId = UserInventories.ItemId AND NOT Sales.SoldAt IS NULL ORDER BY " +
                $"Sales.SoldAt DESC) AS PriceUsd " +
                $"FROM UserInventories " +
                $"WHERE UserInventories.UserId = @UserId " +
                $"ORDER BY NEWID()";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Limit", SqlDbType = SqlDbType.Int, Value = limit },
                new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId }
            };

            var result = new Dictionary<Guid, decimal>();

            foreach (DataRow item in _context.ExecuteQuery(query, parameters).Rows)
            {
                result.Add(item.Field<Guid>("ItemId"), item.Field<decimal>("PriceUsd"));
            }

            return result;
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

                var query = $"INSERT INTO [UserInventories] (Id, UserId, ItemId, AddedAt) VALUES (@Id, @UserId, @ItemId, " +
                    $"@AddedAt)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.UserId },
                    new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.ItemId },
                    new SqlParameter() { ParameterName = "@AddedAt", SqlDbType = SqlDbType.DateTime2, Value = entity.AddedAt }
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
