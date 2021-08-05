using AluguelIdeal.Application.Dtos.Users;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Interactors.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class UsersController : ApiController
    {
        /// <summary>
        /// Create User
        /// </summary>
        /// <remarks> Create User </remarks>
        [HttpPost]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType(typeof(IdResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(command, cancellationToken));

        /// <summary>
        /// Get Users
        /// </summary>
        /// <remarks> Get Users </remarks>
        [HttpGet]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType(typeof(QueryResult<InsensitiveUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetUsersQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));

        /// <summary>
        /// Get User by id
        /// </summary>
        /// <remarks> Get User by id </remarks>
        [HttpGet("{id:Guid}")]
        [Authorize(Roles = AdminOrLandlordRole)]
        [ProducesResponseType(typeof(InsensitiveUserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] GetUserByIdQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));

        /// <summary>
        /// Update User
        /// </summary>
        /// <remarks> Update User </remarks>
        [HttpPut("{id:Guid}")]
        [Authorize(Roles = AdminOrLandlordRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <remarks> Register User </remarks>
        [HttpPut("{id:Guid}/register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <remarks> Delete User </remarks>
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = AdminOrLandlordRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return new NoContentResult();
        }
    }
}
