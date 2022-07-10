using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
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
            var query = $"SELECT * " +
                    $"FROM \"UserInventories\" " +
                    $"WHERE \"UserInventories\".\"UserId\" = @UserId AND \"UserInventories\".\"ItemId\" = @ItemId " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId },
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public void DeleteItemFromUserInventory(Guid userId, Guid itemId)
        {
            var query = $"DELETE " +
                $"FROM \"UserInventories\" " +
                $"WHERE \"UserInventories\".\"UserId\" = @UserId AND \"UserInventories\".\"ItemId\" = @ItemId";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId },
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId }
            };

            _context.ExecuteQuery(query, parameters);
        }

        public int GetCountItems(Guid userId)
        {
            var query = $"SELECT COUNT(*) AS \"Count\" " +
                $"FROM \"UserInventories\" " +
                $"WHERE \"UserInventories\".\"UserId\" = @UserId";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<int>("Count");
        }

        public List<RandomUserInventoryItem> GetRandomItems(Guid userId, int limit)
        {
            var query = $"SELECT \"UserInventories\".\"ItemId\", \"Items\".\"FullName\" as \"ItemFullName\", \"ItemImages\".\"SteamImg\" AS \"ItemSteamImage\", " +
                $"(SELECT \"Sales\".\"PriceUsd\" FROM \"Sales\" WHERE \"Sales\".\"ItemId\" = \"UserInventories\".\"ItemId\" AND " +
                $"NOT \"Sales\".\"SoldAt\" IS NULL ORDER BY \"Sales\".\"SoldAt\" DESC LIMIT 1) AS PriceUsd " +
                $"FROM \"UserInventories\" INNER JOIN " +
                $"\"Items\" ON \"Items\".\"Id\" = \"UserInventories\".\"ItemId\" INNER JOIN " +
                $"\"ItemImages\" ON \"ItemImages\".\"ItemId\" = \"UserInventories\".\"ItemId\" " +
                $"WHERE \"UserInventories\".\"UserId\" = @UserId " +
                $"ORDER BY random() " +
                $"LIMIT @Limit";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Limit", NpgsqlDbType = NpgsqlDbType.Integer, Value = limit },
                new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId }
            };

            var result = new List<RandomUserInventoryItem>();

            foreach (DataRow item in _context.ExecuteQuery(query, parameters).Rows)
            {
                result.Add(item.ToObject<RandomUserInventoryItem>());
            }

            return result;
        }

        public Guid GetUserInventoryId(Guid userId, Guid itemId)
        {
            var query = $"SELECT \"Id\" " +
                    $"FROM \"UserInventories\" " +
                    $"WHERE \"UserInventories\".\"UserId\" = @UserId AND \"UserInventories\".\"ItemId\" = @ItemId " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId },
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId }
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

                var query = $"INSERT INTO \"UserInventories\" (\"Id\", \"UserId\", \"ItemId\", \"AddedAt\") VALUES (@Id, @UserId, @ItemId, " +
                    $"@AddedAt)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.UserId },
                    new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.ItemId },
                    new NpgsqlParameter() { ParameterName = "@AddedAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.AddedAt }
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
