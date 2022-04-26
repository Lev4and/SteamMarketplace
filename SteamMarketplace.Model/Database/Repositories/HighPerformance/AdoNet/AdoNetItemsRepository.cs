using Microsoft.Data.SqlClient;
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

        public bool Contains(long assetId)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM Items " +
                $"WHERE Items.AssetId = @AssetId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@AssetId", SqlDbType = SqlDbType.BigInt, Value = assetId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetItemId(long assetId)
        {
            var query = $"SELECT TOP(1) Id " +
                $"FROM Items " +
                $"WHERE Items.AssetId = @AssetId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@AssetId", SqlDbType = SqlDbType.BigInt, Value = assetId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public Guid GetItemIdByFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException("fullName", "The full name must not be empty.");
            }

            var query = $"SELECT TOP(1) Id " +
                $"FROM Items " +
                $"WHERE Items.FullName = @FullName";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@FullName", SqlDbType = SqlDbType.NVarChar, Value = fullName }
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

                var query = $"INSERT INTO [Items] (Id, ApplicationId, CollectionId, QualityId, RarityId, " +
                    $"TypeId, AssetId, Float, Name, SteamId, FullName, AddedAt) VALUES (@Id, @ApplicationId, " +
                    $"@CollectionId, @QualityId, @RarityId, @TypeId, @AssetId, @Float, @Name, @SteamId, @FullName, @AddedAt)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@ApplicationId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.ApplicationId },
                    new SqlParameter() { ParameterName = "@CollectionId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.CollectionId.GetDbValue() },
                    new SqlParameter() { ParameterName = "@QualityId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.QualityId.GetDbValue() },
                    new SqlParameter() { ParameterName = "@RarityId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.RarityId.GetDbValue() },
                    new SqlParameter() { ParameterName = "@TypeId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.TypeId },
                    new SqlParameter() { ParameterName = "@AssetId", SqlDbType = SqlDbType.BigInt, Value = entity.AssetId },
                    new SqlParameter() { ParameterName = "@Float", SqlDbType = SqlDbType.Float, Value = entity.Float.GetDbValue() },
                    new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = entity.Name.GetDbValue() },
                    new SqlParameter() { ParameterName = "@SteamId", SqlDbType = SqlDbType.NVarChar, Value = entity.SteamId },
                    new SqlParameter() { ParameterName = "@FullName", SqlDbType = SqlDbType.NVarChar, Value = entity.FullName },
                    new SqlParameter() { ParameterName = "@AddedAt", SqlDbType = SqlDbType.DateTime2, Value = entity.AddedAt },
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
