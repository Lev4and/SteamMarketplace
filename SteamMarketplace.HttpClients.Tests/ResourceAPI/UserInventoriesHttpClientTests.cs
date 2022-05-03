using FluentAssertions;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SteamMarketplace.HttpClients.Tests.ResourceAPI
{
    public class UserInventoriesHttpClientTests
    {
        private readonly HttpContext _httpContext;

        public UserInventoriesHttpClientTests(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        [Fact]
        public async Task GetUserInventoryAsync_WithParams_ReturnNotBeNullOrEmptyResponse()
        {
            _httpContext.ResourceAPI.UserInventories.Login("Admin", "Admin");

            var response = await _httpContext.ResourceAPI.UserInventories
                .GetUserInventoryAsync(new UserInventoriesFilters()
                {
                    UserId = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB"),
                    Pagination = new Pagination()
                    {
                        Page = 1,
                        Limit = 100,
                    }
                });

            response.Should().NotBeNull();
        }
    }
}
