using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SteamMarketplace.Hubs.ResourceAPI.ResponseModels;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Importers.HighPerformance;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;
using SteamMarketplace.ResourceWebApplication.Hubs;
using System.Security.Claims;

namespace SteamMarketplace.ResourceWebApplication.Areas.Import.Controllers
{
    [Authorize]
    [ApiController]
    [Area("Import")]
    [EnableCors("CorsPolicy")]
    [Route("api/import/item")]
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IHubContext<ImportHub> _hub;

        private readonly ItemImporter _importer;

        public ItemController(ItemImporter importer, ILogger<ItemController> logger, IHubContext<ImportHub> hub)
        {
            _hub = hub;
            _logger = logger;
            _importer = importer;
        }

        [HttpPost]
        [Route("import")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 401)]
        [ProducesResponseType(typeof(BaseResponseModel<Guid>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<Guid>), 400)]
        public async Task<IActionResult> Import([FromBody] Item item)
        {
            if (item == null)
            {
                _logger.LogWarning($"Validation failed. Invalid item param.");

                return BadRequest(new BaseResponseModel<Guid>(Guid.Empty, Statuses.InvalidData));
            }

            var result = new BaseResponseModel<Guid>(_importer.Import(item, Guid.Parse(User.Claims.First((claim) => 
                claim.Type == "id").Value), Guid.Parse(User.Claims.First((claim) => claim.Type == "currencyId").Value)),  
                Statuses.Success);

            if (result.Result != Guid.Empty)
            {
                await _hub.Clients.All.SendAsync("ItemImported", new ImportedItemInfo(DateTime.Now.ToUniversalTime(),
                    User.Claims.First((claim) => claim.Type == ClaimTypes.GivenName).Value, item));
            }

            return Ok(result);
        }
    }
}
