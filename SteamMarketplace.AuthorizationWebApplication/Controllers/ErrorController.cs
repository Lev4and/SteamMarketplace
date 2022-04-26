using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.Model.Common;

namespace SteamMarketplace.AuthorizationWebApplication.Controllers
{
    [ApiController]
    [Route("api/error")]
    [EnableCors("CorsPolicy")]
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("badRequest")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 200)]
        public IActionResult BadRequest()
        {
            return Ok(new BaseResponseModel<object?>(null, Statuses.InvalidData));
        }

        [HttpGet]
        [Route("notFound")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 200)]
        public IActionResult NotFound()
        {
            return Ok(new BaseResponseModel<object?>(null, Statuses.NotFound));
        }

        [HttpGet]
        [Route("unauthorized")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 200)]
        public IActionResult Unauthorized()
        {
            return Ok(new BaseResponseModel<object?>(null, Statuses.Unauthorized));
        }
    }
}
