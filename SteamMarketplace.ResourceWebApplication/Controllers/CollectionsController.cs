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
    [Route("api/collections")]
    [EnableCors("CorsPolicy")]
    public class CollectionsController : Controller
    {
        private readonly ILogger<CollectionsController> _logger;
        private readonly DefaultDataManager _dataManager;

        public CollectionsController(DefaultDataManager dataManager, ILogger<CollectionsController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCollections()
        {
            return Ok(new BaseResponseModel<List<Collection>>(await _dataManager.Collections.GetAllCollections()
                .ToListAsync(), Statuses.Success));
        }
    }
}
