using Microsoft.Data.SqlClient;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetItemTypesRepository : IItemTypesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetItemTypesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(int cSMoneyId, out Guid id)
        {
            var query = $"SELECT TOP(1) Id " +
                $"FROM ItemTypes " +
                $"WHERE ItemTypes.CSMoneyId = @CSMoneyId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@CSMoneyId", SqlDbType = SqlDbType.Int, Value = cSMoneyId }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetItemTypeId(int cSMoneyId)
        {
            var query = $"SELECT TOP(1) Id " +
                $"FROM ItemTypes " +
                $"WHERE ItemTypes.CSMoneyId = @CSMoneyId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@CSMoneyId", SqlDbType = SqlDbType.Int, Value = cSMoneyId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(ItemType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!Contains(entity.CSMoneyId, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO [ItemTypes] (Id, CSMoneyId, Name) VALUES (@Id, @CSMoneyId, @Name)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@CSMoneyId", SqlDbType = SqlDbType.Int, Value = entity.CSMoneyId },
                    new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = entity.Name.GetDbValue() },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetItemTypeId(entity.CSMoneyId) : foundId;
            }

            return true;
        }
    }
}
