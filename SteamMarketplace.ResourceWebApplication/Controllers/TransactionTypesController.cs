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
    [Route("api/transactionTypes")]
    [EnableCors("CorsPolicy")]
    public class TransactionTypesController : CRUDController<TransactionType, TransactionTypesFilters>
    {
        private readonly ILogger<TransactionTypesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public TransactionTypesController(DefaultDataManager dataManager, ILogger<TransactionTypesController> logger)
            : base(dataManager.TransactionTypes, dataManager.TransactionTypes)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponseModel<List<TransactionType>>), 200)]
        public async Task<IActionResult> GetAllTransactionTypes()
        {
            return Ok(new BaseResponseModel<List<TransactionType>>(await _dataManager.TransactionTypes.GetAllTransactionTypes()
                .ToListAsync(), Statuses.Success));
        }
    }
}
