using Microsoft.Data.SqlClient;
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

            var query = $"SELECT TOP(1) Id " +
                $"FROM AspNetUsers " +
                $"WHERE AspNetUsers.UserName = @UserName";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@UserName", SqlDbType = SqlDbType.NVarChar, Value = username }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetCurrencyId(Guid userId)
        {
            var query = $"SELECT TOP(1) Id, CurrencyId " +
                $"FROM AspNetUsers " +
                $"WHERE AspNetUsers.Id = @Id";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("CurrencyId");
        }

        public Guid GetRandomUserId()
        {
            var query = $"SELECT TOP(1) Id " +
                $"FROM AspNetUsers " +
                $"ORDER BY NEWID()";

            var parameters = new List<SqlParameter>();

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public Guid GetUserId(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username", "The username must not be empty.");
            }

            var query = $"SELECT TOP(1) Id " +
                $"FROM AspNetUsers " +
                $"WHERE AspNetUsers.UserName = @UserName";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@UserName", SqlDbType = SqlDbType.NVarChar, Value = username }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public decimal GetWalletBalance(Guid userId)
        {
            var query = $"SELECT TOP(1) Id, WalletBalance " +
                $"FROM AspNetUsers " +
                $"WHERE AspNetUsers.Id = @Id";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<decimal>("WalletBalance");
        }

        public void ReduceWalletBalance(Guid userId, decimal value)
        {
            var query = $"UPDATE AspNetUsers " +
                $"SET WalletBalance = WalletBalance - @Value " +
                $"WHERE AspNetUsers.Id = @Id";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Value", SqlDbType = SqlDbType.Decimal, Value = value },
                new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId }
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

                var query = $"INSERT INTO [AspNetUsers] (Id, CurrencyId, WalletBalance, " +
                    $"RegisteredAt, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, " +
                    $"PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, " +
                    $"TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (@Id, @CurrencyId, " +
                    $"@WalletBalance, @RegisteredAt, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, " +
                    $"@EmailConfirmed, @PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumber, @PhoneNumberConfirmed, " +
                    $"@TwoFactorEnabled, @LockoutEnd, @LockoutEnabled, @AccessFailedCount)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@CurrencyId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.CurrencyId },
                    new SqlParameter() { ParameterName = "@WalletBalance", SqlDbType = SqlDbType.Decimal, Value = entity.WalletBalance },
                    new SqlParameter() { ParameterName = "@RegisteredAt", SqlDbType = SqlDbType.DateTime2, Value = entity.RegisteredAt },
                    new SqlParameter() { ParameterName = "@UserName", SqlDbType = SqlDbType.NVarChar, Value = entity.UserName.GetDbValue() },
                    new SqlParameter() { ParameterName = "@NormalizedUserName", SqlDbType = SqlDbType.NVarChar, Value = entity.NormalizedUserName.GetDbValue() },
                    new SqlParameter() { ParameterName = "@Email", SqlDbType = SqlDbType.NVarChar, Value = entity.Email.GetDbValue() },
                    new SqlParameter() { ParameterName = "@NormalizedEmail", SqlDbType = SqlDbType.NVarChar, Value = entity.NormalizedEmail.GetDbValue() },
                    new SqlParameter() { ParameterName = "@EmailConfirmed", SqlDbType = SqlDbType.Bit, Value = entity.EmailConfirmed },
                    new SqlParameter() { ParameterName = "@PasswordHash", SqlDbType = SqlDbType.NVarChar, Value = entity.PasswordHash.GetDbValue() },
                    new SqlParameter() { ParameterName = "@SecurityStamp", SqlDbType = SqlDbType.NVarChar, Value = entity.SecurityStamp.GetDbValue() },
                    new SqlParameter() { ParameterName = "@ConcurrencyStamp", SqlDbType = SqlDbType.NVarChar, Value = entity.ConcurrencyStamp.GetDbValue() },
                    new SqlParameter() { ParameterName = "@PhoneNumber", SqlDbType = SqlDbType.NVarChar, Value = entity.PhoneNumber.GetDbValue() },
                    new SqlParameter() { ParameterName = "@PhoneNumberConfirmed", SqlDbType = SqlDbType.Bit, Value = entity.PhoneNumberConfirmed },
                    new SqlParameter() { ParameterName = "@TwoFactorEnabled", SqlDbType = SqlDbType.Bit, Value = entity.TwoFactorEnabled },
                    new SqlParameter() { ParameterName = "@LockoutEnd", SqlDbType = SqlDbType.DateTimeOffset, Value = entity.LockoutEnd.GetDbValue() },
                    new SqlParameter() { ParameterName = "@LockoutEnabled", SqlDbType = SqlDbType.Bit, Value = entity.LockoutEnabled },
                    new SqlParameter() { ParameterName = "@AccessFailedCount", SqlDbType = SqlDbType.Int, Value = entity.AccessFailedCount },
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
            var query = $"UPDATE AspNetUsers " +
                $"SET WalletBalance = WalletBalance + @Value " +
                $"WHERE AspNetUsers.Id = @Id";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Value", SqlDbType = SqlDbType.Decimal, Value = value },
                new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = userId }
            };

            _context.ExecuteQuery(query, parameters);
        }
    }
}
