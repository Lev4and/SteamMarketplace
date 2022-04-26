using FluentAssertions;
using SteamMarketplace.Authorization.Models;
using System.Threading.Tasks;
using Xunit;

namespace SteamMarketplace.HttpClients.Tests.AuthorizationAPI
{
    public class AuthorizationHttpClientTests
    {
        private readonly HttpContext _httpContext;

        public AuthorizationHttpClientTests(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        [Fact]
        public async Task LoginAsync_WithParam_ReturnNotBeNullOrEmptyResponse()
        {
            var response = await _httpContext.AuthorizationAPI.Authorization.LoginAsync(new Login("Admin", "Admin"));

            response.Should().NotBeNull();
        }
    }
}
