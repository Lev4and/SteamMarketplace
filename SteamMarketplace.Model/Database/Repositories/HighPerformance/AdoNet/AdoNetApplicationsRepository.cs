using Microsoft.Data.SqlClient;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetApplicationsRepository : IApplicationsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetApplicationsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(int steamId, out Guid id)
        {
            var query = $"SELECT TOP(1) Id " +
                $"FROM Applications " +
                $"WHERE Applications.SteamId = @SteamId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@SteamId", SqlDbType = SqlDbType.Int, Value = steamId }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetApplicationId(int steamId)
        {
            var query = $"SELECT TOP(1) Id " +
                $"FROM Applications " +
                $"WHERE Applications.SteamId = @SteamId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@SteamId", SqlDbType = SqlDbType.Int, Value = steamId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(Application entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!checkOnUnique || !Contains(entity.SteamId, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO [Applications] (Id, SteamId, Name) VALUES (@Id, @SteamId, @Name)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@SteamId", SqlDbType = SqlDbType.Int, Value = entity.SteamId },
                    new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = entity.Name.GetDbValue() },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetApplicationId(entity.SteamId) : foundId;
            }

            return true;
        }
    }
}
