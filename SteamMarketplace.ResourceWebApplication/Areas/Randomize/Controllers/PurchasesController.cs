using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Services.Randomizers;

namespace SteamMarketplace.ResourceWebApplication.Areas.Randomize.Controllers
{
    [Authorize]
    [ApiController]
    [Area("Randomize")]
    [EnableCors("CorsPolicy")]
    [Route("api/randomize/purchases")]
    public class PurchasesController : Controller
    {
        private readonly PurchaseRandomizer _purchaseRandomizer;

        public PurchasesController(PurchaseRandomizer purchaseRandomizer)
        {
            _purchaseRandomizer = purchaseRandomizer;
        }

        [HttpGet]
        [Route("buyItems")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 200)]
        public async Task<IActionResult> BuyItems()
        {
            await _purchaseRandomizer.BuyItemsAsync();

            return Ok(new BaseResponseModel<object?>(null, Statuses.Success));
        }
    }
}
