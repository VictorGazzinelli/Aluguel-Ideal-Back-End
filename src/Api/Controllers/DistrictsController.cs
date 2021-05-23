using AluguelIdeal.Application.Dtos.Districts;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Districts.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class DistrictsController : ApiController
    {
        /// <summary>
        /// Get Districts
        /// </summary>
        /// <remarks> Get Districts </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResult<DistrictDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetDistrictsQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));
    }
}
