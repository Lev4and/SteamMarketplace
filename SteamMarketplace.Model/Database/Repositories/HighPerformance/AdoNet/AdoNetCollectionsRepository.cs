using Microsoft.Data.SqlClient;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetCollectionsRepository : ICollectionsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetCollectionsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(string name, out Guid id)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The name must not be empty.");
            }

            var query = $"SELECT TOP(1) Id " +
                $"FROM Collections " +
                $"WHERE Collections.Name = @Name";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = name }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetCollectionId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The name must not be empty.");
            }

            var query = $"SELECT TOP(1) Id " +
                $"FROM Collections " +
                $"WHERE Collections.Name = @Name";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = name }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(Collection entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!checkOnUnique || !Contains(entity.Name, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO [Collections] (Id, Name, RuName) VALUES (@Id, @Name, @RuName)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = entity.Name },
                    new SqlParameter() { ParameterName = "@RuName", SqlDbType = SqlDbType.NVarChar, Value = entity.RuName.GetDbValue() },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetCollectionId(entity.Name) : foundId;
            }

            return true;
        }
    }
}
