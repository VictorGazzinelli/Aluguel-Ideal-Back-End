using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AluguelIdeal.Api.Controllers.Base
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
