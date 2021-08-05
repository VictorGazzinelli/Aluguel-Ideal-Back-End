using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Interactors.Residences.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class ResidencesController : ApiController
    {
        /// <summary>
        /// Create Residence
        /// </summary>
        /// <remarks> Create Residence </remarks>
        [HttpPost]
        [Authorize(Roles = AdminOrLandlordRole)]
        [ProducesResponseType(typeof(IdResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateResidenceCommand command, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(command, cancellationToken));

        /// <summary>
        /// Get Residences
        /// </summary>
        /// <remarks> Get Residences </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResult<ResidenceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetResidencesQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));

        /// <summary>
        /// Get Residence by id
        /// </summary>
        /// <remarks> Get Residence by id </remarks>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ResidenceDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] GetResidenceByIdQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));

        /// <summary>
        /// Update Residence
        /// </summary>
        /// <remarks> Update Residence </remarks>
        [HttpPut("{id:Guid}")]
        [Authorize(Roles = AdminOrLandlordRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(UpdateResidenceCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Delete Residence
        /// </summary>
        /// <remarks> Delete Residence </remarks>
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = AdminOrLandlordRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] DeleteResidenceCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }
}
}
