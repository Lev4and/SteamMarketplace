using SteamMarketplace.AuthorizationWebApplication.Services;

namespace SteamMarketplace.AuthorizationWebApplication.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
