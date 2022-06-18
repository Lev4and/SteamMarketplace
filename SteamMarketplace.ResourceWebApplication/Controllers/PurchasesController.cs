using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.ResourceWebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/purchases")]
    [EnableCors("CorsPolicy")]
    public class PurchasesController : Controller
    {
        private readonly ILogger<PurchasesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public PurchasesController(DefaultDataManager dataManager, ILogger<PurchasesController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpPost]
        [Route("myPurchases")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
        [ProducesResponseType(typeof(PagedResponseModel<Purchase>), 200)]
        public async Task<IActionResult> GetMyPurchases([FromBody] PurchasesFilters filters)
        {
            if (filters == null)
            {
                _logger.LogWarning($"Validation failed. Invalid filters param.");

                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            var count = _dataManager.Purchases.GetCountPurchases(filters.UserId);
            var result = await _dataManager.Purchases.GetPurchases(filters).ToListAsync();

            return Ok(new PagedResponseModel<Purchase>(result, filters.Pagination.Page,
                filters.Pagination.Limit, count, Statuses.Success));
        }
    }
}
