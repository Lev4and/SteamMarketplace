using SteamMarketplace.ResourceWebApplication.Services;

namespace SteamMarketplace.ResourceWebApplication.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
