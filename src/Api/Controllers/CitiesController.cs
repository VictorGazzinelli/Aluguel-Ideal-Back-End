using AluguelIdeal.Application.Dtos.Cities;
using AluguelIdeal.Application.Interactors.Cities.Queries;
using AluguelIdeal.Application.Interactors.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class CitiesController : ApiController
    {
        /// <summary>
        /// Get Cities
        /// </summary>
        /// <remarks> Get Cities </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResult<CityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(new GetCitiesQuery(), cancellationToken));
    }
}
