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
    [Route("api/itemTypes")]
    [EnableCors("CorsPolicy")]
    public class ItemTypesController : Controller
    {
        private readonly ILogger<ItemTypesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public ItemTypesController(DefaultDataManager dataManager, ILogger<ItemTypesController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllItemTypes()
        {
            return Ok(new BaseResponseModel<List<ItemType>>(await _dataManager.ItemTypes.GetAllItemTypes()
                .ToListAsync(), Statuses.Success));
        }
    }
}
