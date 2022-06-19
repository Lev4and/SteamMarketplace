using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.ResourceWebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/itemNesteds")]
    [EnableCors("CorsPolicy")]
    public class ItemNestedsController : Controller
    {
        private readonly ILogger<ItemNestedsController> _logger;
        private readonly DefaultDataManager _dataManager;

        public ItemNestedsController(DefaultDataManager dataManager, ILogger<ItemNestedsController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpPost]
        [Route("byItemIds")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
        [ProducesResponseType(typeof(BaseResponseModel<List<ItemNested>>), 200)]
        public async Task<IActionResult> GetItemNesteds([FromBody] List<Guid> itemIds)
        {
            if (itemIds == null)
            {
                _logger.LogWarning($"Validation failed. Invalid filters param.");

                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            return Ok(new BaseResponseModel<List<ItemNested>>(await _dataManager.ItemNesteds.GetItemNesteds(itemIds)
                .ToListAsync(), Statuses.Success));
        }
    }
}
