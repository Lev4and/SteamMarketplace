using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SteamMarketplace.Model.Database.Entities;
using System.Data;
using System.Diagnostics;

namespace SteamMarketplace.Model.Database
{
    public class SteamMarketplaceDbContext : IdentityDbContext<ApplicationUser, ApplicatonRole, Guid>
    {
        private readonly ILogger _logger;

        private readonly Stopwatch _watch;
        private readonly SqlConnection _connection;

        public DbSet<Application> Applications { get; set; }

        public DbSet<Collection> Collections { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemImage> ItemImages { get; set; }

        public DbSet<ItemNested> ItemNesteds { get; set; }

        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Quality> Qualities { get; set; }

        public DbSet<Rarity> Rarities { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }

        public DbSet<UserInventory> UserInventories { get; set; }

        public SteamMarketplaceDbContext(DbContextOptions<SteamMarketplaceDbContext> options,
            ILogger<SteamMarketplaceDbContext> logger) : base(options)
        {
            _logger = logger;

            _watch = new Stopwatch();
            _connection = new SqlConnection(Database.GetConnectionString());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=DESKTOP-9CDGA5B;Database=SteamMarketplace;User ID=sa;Password=sa;" +
                        "Trusted_Connection=True;", b => b.MigrationsAssembly("SteamMarketplace.ResourceWebApplication"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("3DE91537-D302-4DD7-8803-B2BF6C973D26"),
                Literal = "USD",
                CultureInfoName = "us-US",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("CF7B0C49-42A1-483D-97F8-B88711F8546C"),
                Literal = "RUB",
                CultureInfoName = "ru-RU",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("2B1BA08D-97EA-427D-B356-D3AD65E09905"),
                Literal = "UAH",
                CultureInfoName = "uk-UA",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("B8B74EE6-D9A5-4DDE-B7B6-21E4A04A6F7D"),
                Literal = "KZT",
                CultureInfoName = "kk-KZ",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("F340E01A-72E6-40C5-94BC-B7D407F54BD0"),
                Literal = "CNY",
                CultureInfoName = "zh-CN",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("A58D5766-4FBD-4F59-9DEA-2D1BACEDA710"),
                Literal = "EUR",
                CultureInfoName = "eu-EU",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("72616E1E-E63F-4262-863C-72140C5EF912"),
                Literal = "GBP",
                CultureInfoName = "en-GB",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("ECCBB5A2-0FE6-4C9F-930A-313EE8F2D2BB"),
                Literal = "AUD",
                CultureInfoName = "au-AU",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("D8BDAC45-92C3-4183-85EE-90B335D9F500"),
                Literal = "AZN",
                CultureInfoName = "az-AZ",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("C76D4F7E-4F1D-47D8-A339-89F0AC7E7096"),
                Literal = "AMD",
                CultureInfoName = "am-AM",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("4F4CED7B-0623-4BF3-8FA2-4297F9779024"),
                Literal = "BYN",
                CultureInfoName = "by-BY",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("68AE2EBB-B92B-46B7-9032-3CB80A22842C"),
                Literal = "BGN",
                CultureInfoName = "bg-BG",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("6C75D227-BD1F-42C5-9FDC-17DEF5240E4A"),
                Literal = "BRL",
                CultureInfoName = "br-BR",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("E272FF65-2A3F-448D-B0A2-553B22BC8FF1"),
                Literal = "HUF",
                CultureInfoName = "hu-HU",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("3B157395-D7D3-46E1-BFC9-365A9A44D153"),
                Literal = "HKD",
                CultureInfoName = "hk-HK",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("3F54754B-E73C-48D7-A746-ABFF0E31D5EB"),
                Literal = "DKK",
                CultureInfoName = "dk-DK",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("71DF1BA6-67A8-4E9E-9947-9136AA3F1079"),
                Literal = "INR",
                CultureInfoName = "in-IN",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("0C6D22BC-CEE2-47F8-8D29-444FD726E3E4"),
                Literal = "CAD",
                CultureInfoName = "ca-CA",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("D8B6A3E8-69E1-4905-AEA1-808E4B25DD29"),
                Literal = "KGS",
                CultureInfoName = "kg-KG",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("6E0379A4-EA9C-423D-9590-0BDCC0BAC7DD"),
                Literal = "MDL",
                CultureInfoName = "md-MD",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("AC20BE74-4C1A-4EAF-9196-DF50879B7A44"),
                Literal = "NOK",
                CultureInfoName = "no-NO",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("D64B4B93-77DD-4B39-A82F-9C012FD61924"),
                Literal = "PLN",
                CultureInfoName = "pl-PL",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("B4E37CF0-9BF7-471E-A68F-E6F0E4C4C98F"),
                Literal = "RON",
                CultureInfoName = "ro-RO",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("FECBDEAE-EAD2-42E2-B19E-5AD2599FA6F4"),
                Literal = "XDR",
                CultureInfoName = "???",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("6E1C5E6E-925F-421B-8C3F-8F4551E1AD35"),
                Literal = "SGD",
                CultureInfoName = "sg-SG",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("84376203-FB2F-4871-B2FC-C462FAF6CC78"),
                Literal = "TJS",
                CultureInfoName = "tj-TJ",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("E1DB9424-CF1D-442B-A236-46F461BACE48"),
                Literal = "TRY",
                CultureInfoName = "tr-TR",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("8EFCA627-8746-49CE-9689-F0C195661CCD"),
                Literal = "TMT",
                CultureInfoName = "tm-TM",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("314A66A0-EF8D-4529-8DD7-04342FE0C7CF"),
                Literal = "UZS",
                CultureInfoName = "uz-UZ",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("85E94C52-6B83-4ED0-8AE9-3975DAA38AF0"),
                Literal = "CZK",
                CultureInfoName = "cz-CZ",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("3BC13B81-2AC1-4149-9EE0-769D1F420BF8"),
                Literal = "SEK",
                CultureInfoName = "se-SE",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("6F1063D5-A35D-41E7-AF1B-95E1DB2FDCA1"),
                Literal = "CHF",
                CultureInfoName = "ch-CH",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("8C46157C-ADAE-46E1-AFAC-B65FF275F60D"),
                Literal = "ZAR",
                CultureInfoName = "za-ZA",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("7E6FDB58-2B26-4CF8-B89F-FDC3972CE9DC"),
                Literal = "KRW",
                CultureInfoName = "kr-KR",
            });

            builder.Entity<Currency>().HasData(new Currency
            {
                Id = Guid.Parse("C2D2EEAB-7AB9-4D56-969A-192D9AE9538C"),
                Literal = "JPY",
                CultureInfoName = "jp-JP",
            });

            builder.Entity<TransactionType>().HasData(new TransactionType
            {
                Id = Guid.Parse("0FDD5521-90FE-4709-9CCE-D4A7D4FFB2E1"),
                Name = "Adding funds to your account",
                RuName = "Пополнение",
            });

            builder.Entity<TransactionType>().HasData(new TransactionType
            {
                Id = Guid.Parse("2EDBC026-1AAA-4F61-9F71-E3AB94FA6252"),
                Name = "Withdrawal of funds",
                RuName = "Вывод средств",
            });

            builder.Entity<TransactionType>().HasData(new TransactionType
            {
                Id = Guid.Parse("E98B7E58-8F0E-4662-B5A7-5358E8107EF8"),
                Name = "Purchase",
                RuName = "Покупка",
            });

            builder.Entity<TransactionType>().HasData(new TransactionType
            {
                Id = Guid.Parse("1F70A7DC-9DEE-4E44-946D-4B82783BF77B"),
                Name = "Sale",
                RuName = "Продажа",
            });

            builder.Entity<ApplicatonRole>().HasData(new ApplicatonRole
            {
                Id = Guid.Parse("B867520A-92DB-4658-BE39-84DA53A601C0"),
                Name = "Администратор",
                NormalizedName = "АДМИНИСТРАТОР"
            });

            builder.Entity<ApplicatonRole>().HasData(new ApplicatonRole
            {
                Id = Guid.Parse("21E8CC7E-8DF5-4113-B9F9-20498B651581"),
                Name = "Игрок",
                NormalizedName = "Игрок"
            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB"),
                CurrencyId = Guid.Parse("3DE91537-D302-4DD7-8803-B2BF6C973D26"),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "andrey.levchenko.2001@gmail.com",
                NormalizedEmail = "ANDREY.LEVCHENKO.2001@GMAIL.COM",
                RegisteredAt = new DateTime(2022, 5, 1, 0, 0, 0),
                WalletBalance = 1000000,
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("B867520A-92DB-4658-BE39-84DA53A601C0"),
                UserId = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB")
            });

            builder.Entity<Transaction>().HasData(new Transaction
            {
                Id = Guid.Parse("756CD385-85A6-4EB8-A2D8-E9DFDDBC98EF"),
                UserId = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB"),
                TypeId = Guid.Parse("0FDD5521-90FE-4709-9CCE-D4A7D4FFB2E1"),
                PurchaseId = null,
                Value = 1000000,
                HappenedAt = new DateTime(2022, 5, 1, 0, 5, 0)
            });

            builder.Entity<Application>()
                .HasMany(application => application.Items)
                .WithOne(item => item.Application)
                .HasForeignKey(item => item.ApplicationId);

            builder.Entity<ApplicationUser>()
                .HasMany(user => user.Inventory)
                .WithOne(inventory => inventory.User)
                .HasForeignKey(inventory => inventory.UserId);

            builder.Entity<ApplicationUser>()
                .HasMany(user => user.Sales)
                .WithOne(sale => sale.Seller)
                .HasForeignKey(sale => sale.SellerId);

            builder.Entity<ApplicationUser>()
                .HasMany(user => user.Purchases)
                .WithOne(purchase => purchase.Buyer)
                .HasForeignKey(purchase => purchase.BuyerId);

            builder.Entity<ApplicationUser>()
                .HasMany(user => user.Transactions)
                .WithOne(transaction => transaction.User)
                .HasForeignKey(transaction => transaction.UserId);

            builder.Entity<Collection>()
                .HasMany(collection => collection.Items)
                .WithOne(item => item.Collection)
                .HasForeignKey(item => item.CollectionId)
                .IsRequired(false);

            builder.Entity<Currency>()
                .HasMany(currency => currency.Users)
                .WithOne(user => user.Currency)
                .HasForeignKey(user => user.CurrencyId);

            builder.Entity<Currency>()
                .HasMany(currency => currency.Rates)
                .WithOne(rate => rate.Currency)
                .HasForeignKey(rate => rate.CurrencyId);

            builder.Entity<Item>()
                .HasOne(item => item.Image)
                .WithOne(image => image.Item)
                .HasForeignKey<ItemImage>(image => image.ItemId);

            builder.Entity<Item>()
                .HasMany(item => item.Sales)
                .WithOne(sale => sale.Item)
                .HasForeignKey(sale => sale.ItemId);

            builder.Entity<Item>()
                .HasMany(item => item.Items)
                .WithOne(itemNested => itemNested.Item)
                .HasForeignKey(itemNested => itemNested.ItemId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Item>()
                .HasMany(item => item.ItemNesteds)
                .WithOne(itemNested => itemNested.Nested)
                .HasForeignKey(itemNested => itemNested.ItemNestedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Item>()
                .HasMany(item => item.UserInventories)
                .WithOne(userInventory => userInventory.Item)
                .HasForeignKey(userInventory => userInventory.ItemId);

            builder.Entity<ItemType>()
                .HasMany(itemType => itemType.Items)
                .WithOne(item => item.Type)
                .HasForeignKey(item => item.TypeId);

            builder.Entity<Purchase>()
                .HasMany(purchase => purchase.Transactions)
                .WithOne(transaction => transaction.Purchase)
                .HasForeignKey(transaction => transaction.PurchaseId)
                .IsRequired(false);

            builder.Entity<Quality>()
                .HasMany(quality => quality.Items)
                .WithOne(item => item.Quality)
                .HasForeignKey(item => item.QualityId)
                .IsRequired(false);

            builder.Entity<Rarity>()
                .HasMany(rarity => rarity.Items)
                .WithOne(item => item.Rarity)
                .HasForeignKey(item => item.RarityId)
                .IsRequired(false);

            builder.Entity<Sale>()
                .HasOne(sale => sale.Purchase)
                .WithOne(purchase => purchase.Sale)
                .HasForeignKey<Purchase>(purchase => purchase.SaleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<TransactionType>()
                .HasMany(transactionType => transactionType.Transactions)
                .WithOne(transaction => transaction.Type)
                .HasForeignKey(transaction => transaction.TypeId);
        }

        public object ExecuteScalar(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("query", "The query must not be empty.");
            }

            var sqlCommand = new SqlCommand(query, _connection);

            _connection.Open();

            var id = sqlCommand.ExecuteScalar();

            _connection.Close();

            return id;
        }

        public DataTable ExecuteQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("query", "The query must not be empty.");
            }

            var result = new DataTable();

            _connection.Open();

            var dataAdapter = new SqlDataAdapter(query, _connection);
            dataAdapter.Fill(result);

            _connection.Close();

            return result;
        }

        public DataTable ExecuteQuery(string query, List<SqlParameter> sqlParameters)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("query", "The query must not be empty.");
            }

            if (sqlParameters == null)
            {
                throw new ArgumentNullException("sqlParameters", "The sqlParameters must not be empty.");
            }

            var dataTable = new DataTable();
            var sqlCommand = new SqlCommand(query, _connection);

            _watch.Restart();
            _connection.Open();

            sqlCommand.Parameters.AddRange(sqlParameters.ToArray());
            dataTable.Load(sqlCommand.ExecuteReader());

            _connection.Close();
            _watch.Stop();

            _logger.LogInformation($"Microsoft.Data.SqlClient Executed DbCommand ({_watch.ElapsedMilliseconds}ms) {sqlCommand.CommandText} " +
                $"{string.Join(", ", sqlParameters.Select((parameter) => parameter.ParameterName + " = " + parameter.Value.ToString()))}");

            return dataTable;
        }
    }
}
