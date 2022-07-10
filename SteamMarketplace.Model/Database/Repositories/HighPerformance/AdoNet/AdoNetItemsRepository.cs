using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetItemsRepository : IItemsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetItemsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(long cSMoneyId)
        {
            var query = $"SELECT * " +
                $"FROM \"Items\" " +
                $"WHERE \"Items\".\"CSMoneyId\" = @CSMoneyId " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@CSMoneyId", NpgsqlDbType = NpgsqlDbType.Bigint, Value = cSMoneyId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetItemId(long cSMoneyId)
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"Items\" " +
                $"WHERE \"Items\".\"CSMoneyId\" = @CSMoneyId " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@CSMoneyId", NpgsqlDbType = NpgsqlDbType.Bigint, Value = cSMoneyId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public Guid GetItemIdByFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException("fullName", "The full name must not be empty.");
            }

            var query = $"SELECT \"Id\" " +
                $"FROM \"Items\" " +
                $"WHERE \"Items\".\"FullName\" = @FullName " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@FullName", NpgsqlDbType = NpgsqlDbType.Text, Value = fullName }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            return result.Count > 0 ? result[0].Field<Guid>("Id") :Guid.Empty;
        }

        public bool Save(Item entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            if (!checkOnUnique || !Contains(entity.AssetId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"Items\" (\"Id\", \"ApplicationId\", \"CollectionId\", \"QualityId\", \"RarityId\", " +
                    $"\"TypeId\", \"AssetId\", \"CSMoneyId\", \"Float\", \"Name\", \"SteamId\", \"FullName\", \"AddedAt\") VALUES (@Id, @ApplicationId, " +
                    $"@CollectionId, @QualityId, @RarityId, @TypeId, @AssetId, @CSMoneyId, @Float, @Name, @SteamId, @FullName, @AddedAt)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@ApplicationId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.ApplicationId },
                    new NpgsqlParameter() { ParameterName = "@CollectionId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.CollectionId.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@QualityId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.QualityId.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@RarityId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.RarityId.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@TypeId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.TypeId },
                    new NpgsqlParameter() { ParameterName = "@AssetId", NpgsqlDbType = NpgsqlDbType.Bigint, Value = entity.AssetId },
                    new NpgsqlParameter() { ParameterName = "@CSMoneyId", NpgsqlDbType = NpgsqlDbType.Bigint, Value = entity.CSMoneyId },
                    new NpgsqlParameter() { ParameterName = "@Float", NpgsqlDbType = NpgsqlDbType.Real, Value = entity.Float.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@Name", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.Name.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@SteamId", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.SteamId },
                    new NpgsqlParameter() { ParameterName = "@FullName", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.FullName },
                    new NpgsqlParameter() { ParameterName = "@AddedAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.AddedAt },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = GetItemId(entity.AssetId);
            }

            return true;
        }
    }
}
