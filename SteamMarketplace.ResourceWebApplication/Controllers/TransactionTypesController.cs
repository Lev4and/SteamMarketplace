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
    [Route("api/transactionTypes")]
    [EnableCors("CorsPolicy")]
    public class TransactionTypesController : Controller
    {
        private readonly ILogger<TransactionTypesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public TransactionTypesController(DefaultDataManager dataManager, ILogger<TransactionTypesController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTransactionTypes()
        {
            return Ok(new BaseResponseModel<List<TransactionType>>(await _dataManager.TransactionTypes.GetAllTransactionTypes()
                .ToListAsync(), Statuses.Success));
        }
    }
}
