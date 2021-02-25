using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AluguelIdeal.Api.Controllers
{
    [ApiController]
    [RequireHttps]
    [Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
