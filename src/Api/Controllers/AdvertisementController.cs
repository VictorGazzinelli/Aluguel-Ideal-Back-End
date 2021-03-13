using AluguelIdeal.Api.Models.Advertisement;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public class AdvertisementController : ApiControllerBase
    {
        /// <summary>
        /// Get all advertisements
        /// </summary>
        /// <remarks> Gets all advertisements </remarks>
        //[HttpPost]
        //[Route("api/[controller]")]
        //[Consumes("application/json")]
        //public int InsertAdvertisement(CancellationToken cancellationToken)
        //{
            
        //}

        /// <summary>
        /// Get all advertisements
        /// </summary>
        /// <remarks> Gets all advertisements </remarks>
        [HttpGet]
        [Route("api/[controller]")]
        [Consumes("application/json")]
        public async Task<GetAllAdvertisementsOutput> GetAllAdvertisements(CancellationToken cancellationToken)
        {
            GetAllAdvertisementsResponse getAllAdvertisementsResponse =
                await Mediator.Send(new GetAllAdvertisementsRequest(), cancellationToken);

            return new GetAllAdvertisementsOutput()
            {
                Advertisements = getAllAdvertisementsResponse.Advertisements
            };
        }
    }
}
