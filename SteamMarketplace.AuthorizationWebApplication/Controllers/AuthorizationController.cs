using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.Authorization.Models;
using SteamMarketplace.Model.Common;

namespace SteamMarketplace.AuthorizationWebApplication.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("api/authorization")]
    public class AuthorizationController : Controller
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly Services.Authorization _authorization;

        public AuthorizationController(Services.Authorization authorization, ILogger<AuthorizationController> logger)
        {
            _logger = logger;
            _authorization = authorization;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(BaseResponseModel<JWT>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<JWT>), 400)]
        [ProducesResponseType(typeof(BaseResponseModel<JWT>), 401)]
        public async Task<IActionResult> Login([FromBody] Login viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _authorization.AuthenticateUserAsync(viewModel.EmailOrLogin, viewModel.Password);

                if (user != null)
                {
                    _logger.LogInformation($"User {viewModel.EmailOrLogin} has been successfully logged in.");

                    return Ok(new BaseResponseModel<JWT>(new JWT(await _authorization.GenerateJWTAsync(user)),
                        Statuses.Success));
                }

                _logger.LogWarning($"Somebody under the guise of {viewModel.EmailOrLogin} failed authorization.");

                return Unauthorized(new BaseResponseModel<JWT>(new JWT(), Statuses.FatalUnauthorized));
            }

            _logger.LogWarning($"Validation failed.");

            return BadRequest(new BaseResponseModel<JWT>(new JWT(), Statuses.InvalidData));
        }
    }
}
