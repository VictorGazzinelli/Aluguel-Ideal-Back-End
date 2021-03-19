using AluguelIdeal.Api.Controllers.Base;
using AluguelIdeal.Api.Interactors.Contact.Request;
using AluguelIdeal.Api.Interactors.Contact.Response;
using AluguelIdeal.Api.Models.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Controllers
{
    public sealed class ContactsController : ApiController
    {
        /// <summary>
        /// Post Contact
        /// </summary>
        /// <remarks> Post Contact </remarks>
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Post(PostContactModel model, CancellationToken cancellationToken)
        {
            InsertContactRequest request = new InsertContactRequest()
            {
                Name = model.Name,
            };

            InsertContactResponse response = await Mediator.Send(request, cancellationToken);

            return new OkObjectResult(new { response.Contact.Id });
        }

        /// <summary>
        /// Get Contact
        /// </summary>
        /// <remarks> Get Contact </remarks>
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            GetContactRequest request = new GetContactRequest();

            GetContactResponse response = await Mediator.Send(request, cancellationToken);

            return new OkObjectResult(new { response.Contacts });
        }

        /// <summary>
        /// Get Contact by id
        /// </summary>
        /// <remarks> Get Contact by id </remarks>
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            GetContactByIdRequest request = new GetContactByIdRequest()
            {
                Id = id,
            };

            GetContactByIdResponse response =
                await Mediator.Send(request, cancellationToken);

            return new OkObjectResult(new { response.Contact });
        }

        /// <summary>
        /// Put Contact
        /// </summary>
        /// <remarks> Put Contact </remarks>
        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> Put(int id, PutContactModel model, CancellationToken cancellationToken)
        {
            UpdateContactRequest request = new UpdateContactRequest()
            {
                Id = id,
                Name = model.Name,
            };

            UpdateContactResponse response = await Mediator.Send(request, cancellationToken);

            return new OkObjectResult(new { response.Contact });

        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <remarks> Delete Contact </remarks>
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            DeleteContactRequest request = new DeleteContactRequest()
            {
                Id = id,
            };

            await Mediator.Send(request, cancellationToken);

            return new NoContentResult();
        }
    }
}
