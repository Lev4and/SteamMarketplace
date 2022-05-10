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
    [Route("api/sales")]
    [EnableCors("CorsPolicy")]
    public class SalesController : Controller
    {
        private readonly ILogger<SalesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public SalesController(DefaultDataManager dataManager, ILogger<SalesController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpPost]
        [Route("mySales")]
        public async Task<IActionResult> GetInventory([FromBody] SalesFilters filters)
        {
            if (filters == null)
            {
                _logger.LogWarning($"Validation failed. Invalid filters param.");

                return BadRequest(new BaseResponseModel<List<Sale>>(new List<Sale>(), Statuses.InvalidData));
            }

            var count = _dataManager.Sales.GetCountSales(filters.UserId);
            var result = await _dataManager.Sales.GetSales(filters).ToListAsync();

            return Ok(new PagedResponseModel<Sale>(result, filters.Pagination.Page,
                filters.Pagination.Limit, count, Statuses.Success));
        }
    }
}
