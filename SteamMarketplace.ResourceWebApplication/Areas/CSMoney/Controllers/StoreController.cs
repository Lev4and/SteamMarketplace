using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.HttpClients.CSMoney.ResponseModels;
using SteamMarketplace.Model.Common;
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
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 401)]
        [ProducesResponseType(typeof(BaseResponseModel<BotInventory>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<BotInventory>), 400)]
        public async Task<IActionResult> GetInventory([Required][FromQuery(Name = "limit")] int limit, [Required][FromQuery(Name = "offset")] int offset, [FromQuery(Name = "withStack")] bool withStack = true)
        {
            if (limit <= 0)
            {
                _logger.LogWarning($"Validation failed. Invalid limit param.");

                return BadRequest(new BaseResponseModel<BotInventory>(new BotInventory(), Statuses.InvalidData));
            }

            if (offset < 0)
            {
                _logger.LogWarning($"Validation failed. Invalid offset param.");

                return BadRequest(new BaseResponseModel<BotInventory>(new BotInventory(), Statuses.InvalidData));
            }

            return Ok(new BaseResponseModel<BotInventory>(await _httpContext.CSMoney.Store.GetInventoryAsync(limit, offset, withStack),
                Statuses.Success));
        }
    }
}
