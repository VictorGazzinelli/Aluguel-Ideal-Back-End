using AluguelIdeal.Application.Dtos.Roles;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Roles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class RolesController : ApiController
    {
        /// <summary>
        /// Get Roles
        /// </summary>
        /// <remarks> Get Roles </remarks>
        [HttpGet]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType(typeof(QueryResult<RoleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetRolesQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));
    }
}
