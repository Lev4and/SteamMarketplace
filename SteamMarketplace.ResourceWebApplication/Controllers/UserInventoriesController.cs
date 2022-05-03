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
    [EnableCors("CorsPolicy")]
    [Route("api/userInventories")]
    public class UserInventoriesController : Controller
    {
        private readonly ILogger<UserInventoriesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public UserInventoriesController(DefaultDataManager dataManager, ILogger<UserInventoriesController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpPost]
        [Route("inventory")]
        public async Task<IActionResult> GetInventory([FromBody] UserInventoriesFilters filters)
        {
            if (filters == null)
            {
                _logger.LogWarning($"Validation failed. Invalid filters param.");

                return BadRequest(new BaseResponseModel<List<UserInventory>>(new List<UserInventory>(), 
                    Statuses.InvalidData));
            }

            var count = _dataManager.UserInventories.GetCountItemsInUserInventory(filters.UserId);
            var result = await _dataManager.UserInventories.GetUserInventory(filters).ToListAsync();

            return Ok(new PagedResponseModel<UserInventory>(result, filters.Pagination.Page, 
                filters.Pagination.Limit, count, Statuses.Success));
        }
    }
}
