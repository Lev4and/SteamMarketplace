using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetApplicationUsersRepository : IApplicationUsersRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetApplicationUsersRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(string username, out Guid id)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username", "The username must not be empty.");
            }

            var query = $"SELECT \"Id\" " +
                $"FROM \"AspNetUsers\" " +
                $"WHERE \"AspNetUsers\".\"UserName\" = @UserName " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@UserName", NpgsqlDbType = NpgsqlDbType.Text, Value = username }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetCurrencyId(Guid userId)
        {
            var query = $"SELECT \"Id\", \"CurrencyId\" " +
                $"FROM \"AspNetUsers\" " +
                $"WHERE \"AspNetUsers\".\"Id\" = @Id " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("CurrencyId");
        }

        public ApplicationUser GetRandomUser()
        {
            var query = $"SELECT * " +
                $"FROM \"AspNetUsers\" " +
                $"ORDER BY random() " +
                $"LIMIT 1";

            var result = (_context.ExecuteQuery(query)).Rows;

            if (result.Count == 1)
            {
                return result[0].ToObject<ApplicationUser>();
            }

            return new ApplicationUser();
        }

        public Guid GetRandomUserId()
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"AspNetUsers\" " +
                $"ORDER BY random() " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>();

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public Guid GetUserId(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username", "The username must not be empty.");
            }

            var query = $"SELECT \"Id\" " +
                $"FROM \"AspNetUsers\" " +
                $"WHERE \"AspNetUsers\".\"UserName\" = @UserName " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@UserName", NpgsqlDbType = NpgsqlDbType.Text, Value = username }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public decimal GetWalletBalance(Guid userId)
        {
            var query = $"SELECT \"Id\", \"WalletBalance\" " +
                $"FROM \"AspNetUsers\" " +
                $"WHERE \"AspNetUsers\".\"Id\" = @Id " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<decimal>("WalletBalance");
        }

        public void ReduceWalletBalance(Guid userId, decimal value)
        {
            var query = $"UPDATE \"AspNetUsers\" " +
                $"SET \"WalletBalance\" = \"WalletBalance\" - @Value " +
                $"WHERE \"AspNetUsers\".\"Id\" = @Id";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Value", NpgsqlDbType = NpgsqlDbType.Numeric, Value = value },
                new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId }
            };

            _context.ExecuteQuery(query, parameters);
        }

        public bool Save(ApplicationUser entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!checkOnUnique || !Contains(entity.UserName, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"AspNetUsers\" (\"Id\", \"CurrencyId\", \"WalletBalance\", " +
                    $"\"RegisteredAt\", \"UserName\", \"NormalizedUserName\", \"Email\", \"NormalizedEmail\", \"EmailConfirmed\", " +
                    $"\"PasswordHash\", \"SecurityStamp\", \"ConcurrencyStamp\", \"PhoneNumber\", \"PhoneNumberConfirmed\", " +
                    $"\"TwoFactorEnabled\", \"LockoutEnd\", \"LockoutEnabled\", \"AccessFailedCount\") VALUES (@Id, @CurrencyId, " +
                    $"@WalletBalance, @RegisteredAt, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, " +
                    $"@EmailConfirmed, @PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumber, @PhoneNumberConfirmed, " +
                    $"@TwoFactorEnabled, @LockoutEnd, @LockoutEnabled, @AccessFailedCount)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@CurrencyId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.CurrencyId },
                    new NpgsqlParameter() { ParameterName = "@WalletBalance", NpgsqlDbType = NpgsqlDbType.Numeric, Value = entity.WalletBalance },
                    new NpgsqlParameter() { ParameterName = "@RegisteredAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.RegisteredAt },
                    new NpgsqlParameter() { ParameterName = "@UserName", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.UserName.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@NormalizedUserName", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.NormalizedUserName.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@Email", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.Email.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@NormalizedEmail", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.NormalizedEmail.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@EmailConfirmed", NpgsqlDbType = NpgsqlDbType.Boolean, Value = entity.EmailConfirmed },
                    new NpgsqlParameter() { ParameterName = "@PasswordHash", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.PasswordHash.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@SecurityStamp", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.SecurityStamp.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@ConcurrencyStamp", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.ConcurrencyStamp.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@PhoneNumber", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.PhoneNumber.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@PhoneNumberConfirmed", NpgsqlDbType = NpgsqlDbType.Boolean, Value = entity.PhoneNumberConfirmed },
                    new NpgsqlParameter() { ParameterName = "@TwoFactorEnabled", NpgsqlDbType = NpgsqlDbType.Boolean, Value = entity.TwoFactorEnabled },
                    new NpgsqlParameter() { ParameterName = "@LockoutEnd", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.LockoutEnd.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@LockoutEnabled", NpgsqlDbType = NpgsqlDbType.Boolean, Value = entity.LockoutEnabled },
                    new NpgsqlParameter() { ParameterName = "@AccessFailedCount", NpgsqlDbType = NpgsqlDbType.Integer, Value = entity.AccessFailedCount },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetUserId(entity.UserName) : foundId;
            }

            return true;
        }

        public void TopUpWalletBalance(Guid userId, decimal value)
        {
            var query = $"UPDATE \"AspNetUsers\" " +
                $"SET \"WalletBalance\" = \"WalletBalance\" + @Value " +
                $"WHERE \"AspNetUsers\".\"Id\" = @Id";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Value", NpgsqlDbType = NpgsqlDbType.Numeric, Value = value },
                new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = userId }
            };

            _context.ExecuteQuery(query, parameters);
        }
    }
}
