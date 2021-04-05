using AluguelIdeal.Application.Interactors.Auth.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class AuthController : ApiController
    {
        /// <summary>
        /// Get Auth Token
        /// </summary>
        /// <remarks> Get Auth Token </remarks>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAuthQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));
    }
}
