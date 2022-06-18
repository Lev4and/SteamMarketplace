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
    [Route("api/itemTypes")]
    [EnableCors("CorsPolicy")]
    public class ItemTypesController : CRUDController<ItemType, ItemTypesFilters>
    {
        private readonly ILogger<ItemTypesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public ItemTypesController(DefaultDataManager dataManager, ILogger<ItemTypesController> logger)
            : base(dataManager.ItemTypes, dataManager.ItemTypes)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponseModel<List<ItemType>>), 200)]
        public async Task<IActionResult> GetAllItemTypes()
        {
            return Ok(new BaseResponseModel<List<ItemType>>(await _dataManager.ItemTypes.GetAllItemTypes()
                .ToListAsync(), Statuses.Success));
        }
    }
}
