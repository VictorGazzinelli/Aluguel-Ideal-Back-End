using AluguelIdeal.Application.Interactors.Profiles.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class ProfilesController : ApiController
    {
        /// <summary>
        /// Bind Profile
        /// </summary>
        /// <remarks> Bind Profile </remarks>
        [HttpPost]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Bind(BindProfileCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Unbind Profile
        /// </summary>
        /// <remarks> Unbind Profile </remarks>
        [HttpDelete]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Unbind(UnbindProfileCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }
    }
}
