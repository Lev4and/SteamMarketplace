using Microsoft.Data.SqlClient;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetTransactionTypesRepository : ITransactionTypesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetTransactionTypesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public Guid GetTransactionTypeIdByRuName(string ruName)
        {
            if (string.IsNullOrEmpty(ruName))
            {
                throw new ArgumentNullException("ruName", "The ruName must not be empty.");
            }

            var query = $"SELECT TOP(1) Id " +
                $"FROM TransactionTypes " +
                $"WHERE TransactionTypes.RuName = @RuName";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@RuName", SqlDbType = SqlDbType.NVarChar, Value = ruName }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }
    }
}
