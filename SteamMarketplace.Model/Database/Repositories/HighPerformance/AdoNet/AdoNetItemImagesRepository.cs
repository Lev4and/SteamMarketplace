using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetItemImagesRepository : IItemImagesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetItemImagesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(Guid itemId)
        {
            var query = $"SELECT * " +
                    $"FROM \"ItemImages\" " +
                    $"WHERE \"ItemImages\".\"ItemId\" = @ItemId " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetItemImageId(Guid itemId)
        {
            var query = $"SELECT \"Id\" " +
                    $"FROM \"ItemImages\" " +
                    $"WHERE \"ItemImages\".\"ItemId\" = @ItemId " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(ItemImage entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            if (!checkOnUnique || !Contains(entity.ItemId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"ItemImages\" (\"Id\", \"ItemId\", \"Image\", \"Image3d\", \"SteamImg\", \"Screenshot\") VALUES " +
                    $"(@Id, @ItemId, @Image, @Image3d, @SteamImg, @Screenshot)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.ItemId },
                    new NpgsqlParameter() { ParameterName = "@Image", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.Image.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@Image3d", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.Image3d.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@SteamImg", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.SteamImg.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@Screenshot", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.Screenshot.GetDbValue() },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = GetItemImageId(entity.ItemId);
            }

            return true;
        }
    }
}
