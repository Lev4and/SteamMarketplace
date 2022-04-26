using Microsoft.Data.SqlClient;
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
            var query = $"SELECT TOP(1) * " +
                    $"FROM ItemImages " +
                    $"WHERE ItemImages.ItemId = @ItemId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetItemImageId(Guid itemId)
        {
            var query = $"SELECT TOP(1) Id " +
                    $"FROM ItemImages " +
                    $"WHERE ItemImages.ItemId = @ItemId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId }
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

                var query = $"INSERT INTO [ItemImages] (Id, ItemId, Image, Image3d, SteamImg, Screenshot) VALUES " +
                    $"(@Id, @ItemId, @Image, @Image3d, @SteamImg, @Screenshot)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.ItemId },
                    new SqlParameter() { ParameterName = "@Image", SqlDbType = SqlDbType.NVarChar, Value = entity.Image.GetDbValue() },
                    new SqlParameter() { ParameterName = "@Image3d", SqlDbType = SqlDbType.NVarChar, Value = entity.Image3d.GetDbValue() },
                    new SqlParameter() { ParameterName = "@SteamImg", SqlDbType = SqlDbType.NVarChar, Value = entity.SteamImg.GetDbValue() },
                    new SqlParameter() { ParameterName = "@Screenshot", SqlDbType = SqlDbType.NVarChar, Value = entity.Screenshot.GetDbValue() },
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
