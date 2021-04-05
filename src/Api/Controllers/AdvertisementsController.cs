using AluguelIdeal.Application.Interactors.Advertisements.Commands;
using AluguelIdeal.Application.Interactors.Advertisements.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class AdvertisementsController : ApiController
    {
        /// <summary>
        /// Create Advertisement
        /// </summary>
        /// <remarks> Create Advertisement </remarks>
        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertisementCommand command, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(command, cancellationToken));

        /// <summary>
        /// Get Advertisements
        /// </summary>
        /// <remarks> Get Advertisements </remarks>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAdvertisementsQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));

        /// <summary>
        /// Get Advertisement by id
        /// </summary>
        /// <remarks> Get Advertisement by id </remarks>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromQuery] GetAdvertisementsByIdQuery query, CancellationToken cancellationToken) =>
            new OkObjectResult(await Mediator.Send(query, cancellationToken));

        ///// <summary>
        ///// Put Advertisement
        ///// </summary>
        ///// <remarks> Put Advertisement </remarks>
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> Put(int id, AdvertisementModel model, CancellationToken cancellationToken)
        //{
        //    UpdateAdvertisementRequest request = new UpdateAdvertisementRequest()
        //    {
        //        Id = id,
        //        Title = model.Title,
        //    };

        //    UpdateAdvertisementResponse response = await Mediator.Send(request, cancellationToken).ConfigureAwait(false);

        //    if (response.Advertisement == null)
        //        return new NotFoundObjectResult(null);

        //    return new OkObjectResult(new{ response.Advertisement });
        //}

        ///// <summary>
        ///// Delete Advertisement
        ///// </summary>
        ///// <remarks> Delete Advertisement </remarks>
        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        //{
        //    DeleteAdvertisementRequest request = new DeleteAdvertisementRequest()
        //    {
        //        Id = id,
        //    };

        //    await Mediator.Send(request, cancellationToken).ConfigureAwait(false);

        //    return new NoContentResult();
        //}
    }
}
