using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.HttpClients.CSMoney.ResponseModels;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;
using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.ResourceWebApplication.Areas.CSMoney.Controllers
{
    [Authorize]
    [ApiController]
    [Area("CSMoney")]
    [EnableCors("CorsPolicy")]
    [Route("api/csMoney/store")]
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> _logger;
        private readonly HttpClients.HttpContext _httpContext;

        public StoreController(HttpClients.HttpContext httpContext, ILogger<StoreController> logger)
        {
            _logger = logger;
            _httpContext = httpContext;
        }

        [HttpGet]
        [Route("inventory")]
        [ProducesResponseType(typeof(PagedResponseModel<Item>), 200)]
        [ProducesResponseType(typeof(PagedResponseModel<Item>), 400)]
        public async Task<IActionResult> GetInventory([Required][FromQuery(Name = "limit")] int limit, [Required][FromQuery(Name = "offset")] int offset, [FromQuery(Name = "minPrice")] decimal minPrice = 0, [FromQuery(Name = "maxPrice")] decimal maxPrice = 30000, [FromQuery(Name = "withStack")] bool withStack = true)
        {
            if (limit <= 0 || limit > 60)
            {
                _logger.LogWarning($"Validation failed. Invalid limit param.");

                return BadRequest(new PagedResponseModel<Item>(new List<Item>(), 1, 0, 0, Statuses.InvalidData));
            }

            if (offset < 0 || offset > 5000)
            {
                _logger.LogWarning($"Validation failed. Invalid offset param.");

                return BadRequest(new PagedResponseModel<Item>(new List<Item>(), 1, 0, 0, Statuses.InvalidData));
            }

            var botInventory = await _httpContext.CSMoney.Store.GetInventoryAsync(limit, offset, minPrice, 
                maxPrice, withStack);

            return Ok(new PagedResponseModel<Item>(botInventory.Items, offset / limit + 1, limit, 5000, 
                Statuses.Success));
        }
    }
}
