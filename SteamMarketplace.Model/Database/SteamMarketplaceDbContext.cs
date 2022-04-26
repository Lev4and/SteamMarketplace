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

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemImage> ItemImages { get; set; }

        public DbSet<ItemNested> ItemNesteds { get; set; }

        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Quality> Qualities { get; set; }

        public DbSet<Rarity> Rarities { get; set; }

        public DbSet<Sale> Sales { get; set; }

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
                CultureInfoName = "en-US",
            });

            builder.Entity<ApplicatonRole>().HasData(new ApplicatonRole
            {
                Id = Guid.Parse("B867520A-92DB-4658-BE39-84DA53A601C0"),
                Name = "Администратор",
                NormalizedName = "АДМИНИСТРАТОР"
            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB"),
                CurrencyId = Guid.Parse("3DE91537-D302-4DD7-8803-B2BF6C973D26"),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "andrey.levchenko.2001@gmail.com",
                NormalizedEmail = "ANDREY.LEVCHENKO.2001@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("B867520A-92DB-4658-BE39-84DA53A601C0"),
                UserId = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB")
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

            builder.Entity<Collection>()
                .HasMany(collection => collection.Items)
                .WithOne(item => item.Collection)
                .HasForeignKey(item => item.CollectionId)
                .IsRequired(false);

            builder.Entity<Currency>()
                .HasMany(currency => currency.Users)
                .WithOne(user => user.Currency)
                .HasForeignKey(user => user.CurrencyId);

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
