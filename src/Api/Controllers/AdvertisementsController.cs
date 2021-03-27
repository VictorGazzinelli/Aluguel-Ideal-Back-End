using AluguelIdeal.Api.Controllers.Base;
using AluguelIdeal.Api.Interactors.Advertisement.Request;
using AluguelIdeal.Api.Interactors.Advertisement.Response;
using AluguelIdeal.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class AdvertisementsController : ApiController
    {
        /// <summary>
        /// Post Advertisement
        /// </summary>
        /// <remarks> Post Advertisement </remarks>
        [HttpPost]
        public async Task<IActionResult> Post(AdvertisementModel model, CancellationToken cancellationToken)
        {
            InsertAdvertisementRequest request = new InsertAdvertisementRequest()
            {
                Title = "",
            };

            InsertAdvertisementResponse response = await Mediator.Send(request, cancellationToken);

            return new OkObjectResult(new { response.Advertisement.Id });
        }

        /// <summary>
        /// Get Advertisement
        /// </summary>
        /// <remarks> Get Advertisement </remarks>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            GetAdvertisementRequest request = new GetAdvertisementRequest();

            GetAdvertisementResponse response = await Mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return new OkObjectResult(new { response.Advertisements });
        }

        /// <summary>
        /// Get Advertisement by id
        /// </summary>
        /// <remarks> Get Advertisement by id </remarks>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            GetAdvertisementByIdRequest request = new GetAdvertisementByIdRequest()
            {
                Id = id,
            };

            GetAdvertisementByIdResponse response = await Mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return new OkObjectResult(new { response.Advertisement });
        }

        /// <summary>
        /// Put Advertisement
        /// </summary>
        /// <remarks> Put Advertisement </remarks>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, AdvertisementModel model, CancellationToken cancellationToken)
        {
            UpdateAdvertisementRequest request = new UpdateAdvertisementRequest()
            {
                Id = id,
                Title = model.Title,
            };

            UpdateAdvertisementResponse response = await Mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return new OkObjectResult(new{ response.Advertisement });
        }

        /// <summary>
        /// Delete Advertisement
        /// </summary>
        /// <remarks> Delete Advertisement </remarks>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            DeleteAdvertisementRequest request = new DeleteAdvertisementRequest()
            {
                Id = id,
            };

            await Mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return new NoContentResult();
        }
    }
}
