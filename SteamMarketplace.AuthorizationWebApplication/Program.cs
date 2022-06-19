using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.AuthorizationWebApplication.Extensions;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.Entities;
using System.Net;
using EntityFramework = SteamMarketplace.Model.Database.Repositories.ObjectRelational.EntityFramework;
using EntityFrameworkAbstract = SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;
using Services = SteamMarketplace.AuthorizationWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<RoleManager<ApplicatonRole>>();
builder.Services.AddTransient<UserManager<ApplicationUser>>();
builder.Services.AddTransient<Services.Authorization>();

builder.Services.AddTransient<EntityFrameworkAbstract.IRolesRepository, EntityFramework.EFRolesRepository>();
builder.Services.AddTransient<EntityFrameworkAbstract.IUsersRepository, EntityFramework.EFUsersRepository>();
builder.Services.AddTransient<AuthorizationDataManager>();

builder.Services.AddDbContext<SteamMarketplaceDbContext>((options) =>
{
    options.UseSqlServer(DbConfig.ConnectionString);
});

builder.Services.AddIdentity<ApplicationUser, ApplicatonRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireUppercase = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<SteamMarketplaceDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/error/unauthorized";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:8080").AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials());
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

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
app.MapControllers();
app.UseRouting();
app.UseCors("CorsPolicy");
app.Run();
