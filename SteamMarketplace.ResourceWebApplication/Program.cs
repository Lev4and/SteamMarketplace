using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SteamMarketplace.Authorization.Common;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Importers;
using SteamMarketplace.Model.Importers.HighPerformance;
using SteamMarketplace.ResourceWebApplication.Hubs;
using SteamMarketplace.Services;
using System.Net;
using AdoNet = SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet;
using AdoNetAbstract = SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using HttpClients = SteamMarketplace.HttpClients;
using ObjectRelational = SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework;
using ObjectRelationalAbstract = SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HttpClients.Common.Services.Authorization>();
builder.Services.AddSingleton<HttpClients.AuthorizationAPI.AuthorizationHttpClient>();
builder.Services.AddSingleton<HttpClients.AuthorizationAPI.AuthorizationAPIHttpContext>();
builder.Services.AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>();
builder.Services.AddSingleton<HttpClients.CSMoney.StoreHttpClient>();
builder.Services.AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>();
builder.Services.AddSingleton<HttpClients.ResourceAPI.CSMoneyStoreHttpClient>();
builder.Services.AddSingleton<HttpClients.ResourceAPI.ImportItemHttpClient>();
builder.Services.AddSingleton<HttpClients.ResourceAPI.ResourceAPIHttpContext>();
builder.Services.AddSingleton<HttpClients.ResourceAPI.UserInventoriesHttpClient>();
builder.Services.AddSingleton<HttpClients.HttpContext>();

builder.Services.AddTransient<RoleManager<ApplicatonRole>>();
builder.Services.AddTransient<UserManager<ApplicationUser>>();

builder.Services.AddTransient<AdoNetAbstract.IApplicationsRepository, AdoNet.AdoNetApplicationsRepository>();
builder.Services.AddTransient<AdoNetAbstract.ICollectionsRepository, AdoNet.AdoNetCollectionsRepository>();
builder.Services.AddTransient<AdoNetAbstract.ICurrenciesRepository, AdoNet.AdoNetCurrenciesRepository>();
builder.Services.AddTransient<AdoNetAbstract.IItemImagesRepository, AdoNet.AdoNetItemImagesRepository>();
builder.Services.AddTransient<AdoNetAbstract.IItemNestedsRepository, AdoNet.AdoNetItemNestedsRepository>();
builder.Services.AddTransient<AdoNetAbstract.IItemsRepository, AdoNet.AdoNetItemsRepository>();
builder.Services.AddTransient<AdoNetAbstract.IItemTypesRepository, AdoNet.AdoNetItemTypesRepository>();
builder.Services.AddTransient<AdoNetAbstract.IPurchasesRepository, AdoNet.AdoNetPurchasesRepository>();
builder.Services.AddTransient<AdoNetAbstract.IQualitiesRepository, AdoNet.AdoNetQualitiesRepository>();
builder.Services.AddTransient<AdoNetAbstract.IRaritiesRepository, AdoNet.AdoNetRaritiesRepository>();
builder.Services.AddTransient<AdoNetAbstract.ISalesRepository, AdoNet.AdoNetSalesRepository>();
builder.Services.AddTransient<AdoNetAbstract.IUserInventoriesRepository, AdoNet.AdoNetUserInventoriesRepository>();
builder.Services.AddTransient<HighPerformanceDataManager>();

builder.Services.AddTransient<ObjectRelationalAbstract.IUserInventoriesRepository, ObjectRelational.EFUserInventoriesRepository>();
builder.Services.AddTransient<DefaultDataManager>();

builder.Services.AddDbContext<SteamMarketplaceDbContext>((options) =>
{
    options.UseSqlServer(DbConfig.ConnectionString);
});

builder.Services.AddTransient<ApplicationImporter>();
builder.Services.AddTransient<CollectionImporter>();
builder.Services.AddTransient<ItemImageImporter>();
builder.Services.AddTransient<ItemImporter>();
builder.Services.AddTransient<ItemNestedImporter>();
builder.Services.AddTransient<QualityImporter>();
builder.Services.AddTransient<RarityImporter>();
builder.Services.AddTransient<SaleImporter>();
builder.Services.AddTransient<TypeImporter>();
builder.Services.AddTransient<UserInventoryImporter>();
builder.Services.AddTransient<HighPerformanceImporterContext>();

builder.Services.AddTransient<AutoImport>();

builder.Services.AddIdentity<ApplicationUser, ApplicatonRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireUppercase = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<SteamMarketplaceDbContext>().AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthorizationOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthorizationOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = AuthorizationOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/error/unauthorized";
});

builder.Services.AddSignalR(options =>
{
    //options.ClientTimeoutInterval = TimeSpan.FromMinutes(30);
    //options.KeepAliveInterval = TimeSpan.FromMinutes(15);
    options.MaximumReceiveMessageSize = 8388608;
    options.EnableDetailedErrors = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:8080").AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials());
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages(context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.BadRequest)
    {
        response.Redirect("/api/error/bagRequest");
    }

    if (response.StatusCode == (int)HttpStatusCode.NotFound)
    {
        response.Redirect("/api/error/notFound");
    }

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        response.Redirect("/api/error/unauthorized");
    }

    return Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<OnlineHub>("/store/online"/*, options =>
    {
        options.TransportSendTimeout = TimeSpan.FromMinutes(30);
    }*/);
    endpoints.MapHub<ImportHub>("/store/items/import"/*, options =>
    {
        options.TransportSendTimeout = TimeSpan.FromMinutes(30);
    }*/);
    endpoints.MapHub<AutoImportHub>("/store/items/import/auto"/*, options =>
    {
        options.TransportSendTimeout = TimeSpan.FromMinutes(30);
    }*/);
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "api/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "csMoneyArea",
        areaName: "CSMoney",
        pattern: "api/csMoney/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "importArea",
        areaName: "Import",
        pattern: "api/import/{controller=Home}/{action=Index}/{id?}");
});

app.Run();

