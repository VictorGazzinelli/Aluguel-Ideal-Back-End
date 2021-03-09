using AluguelIdeal.Api.Models.Client;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class ClientsController : ApiControllerBase
    {
        [HttpGet]
        [Route("api/[controller]/{name}")]
        public async Task<IActionResult> GetClientsByNameAsync(CancellationToken cancellationToken, string name = null)
        {
            GetClientsResponse getClientsResponse =
                await Mediator.Send(
                    new GetClientsRequest() { 
                        Name = name
                    }, cancellationToken);

            return new OkObjectResult(new { getClientsResponse.Clients } );
        }

        [HttpGet]
        [Route("api/[controller]/{email}")]
        public async Task<IActionResult> GetClientsByEmailAsync(CancellationToken cancellationToken, string email = null)
        {
            GetClientsResponse getClientsResponse =
                await Mediator.Send(
                    new GetClientsRequest()
                    {
                        Email = email
                    }, cancellationToken);

            return new OkObjectResult(new { getClientsResponse.Clients });
        }
    }
}
