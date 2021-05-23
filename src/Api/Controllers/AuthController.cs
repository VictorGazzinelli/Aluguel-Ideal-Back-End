using AluguelIdeal.Application.Interactors.Auth.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class AuthController : ApiController
    {
        /// <summary>
        /// Get Auth Token (Login)
        /// </summary>
        /// <remarks> Get Auth Token (Login) </remarks>
        [HttpPost]
        public async Task<IActionResult> GetAuthToken([FromQuery] GetAuthQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));
    }
}
