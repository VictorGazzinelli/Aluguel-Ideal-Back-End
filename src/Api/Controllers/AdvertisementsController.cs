using AluguelIdeal.Api.Controllers.Models.Advertisement;
using AluguelIdeal.Api.Responses.Advertisements;
using AluguelIdeal.Application.Interactors.Advertisements.Requests;
using AluguelIdeal.Application.Interactors.Advertisements.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(AdvertisementIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(AdvertisementModel model, CancellationToken cancellationToken)
        {
            InsertAdvertisementRequest request = new InsertAdvertisementRequest()
            {
                Title = model.Title,
            };

            InsertAdvertisementResponse response = await Mediator.Send(request, cancellationToken);

            return new OkObjectResult(new AdvertisementIdResponse() { Id = response.Advertisement.Id });
        }

        /// <summary>
        /// Get Advertisement
        /// </summary>
        /// <remarks> Get Advertisement </remarks>
        [HttpGet]
        //[ProducesResponseType(typeof(), StatusCodes.Status200OK)]
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
        //[ProducesResponseType(typeof(), StatusCodes.Status200OK)]
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
        //[ProducesResponseType(typeof(), StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
