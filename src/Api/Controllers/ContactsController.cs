﻿using AluguelIdeal.Api.Controllers.Models.Contact;
using AluguelIdeal.Application.Interactors.Contacts.Requests;
using AluguelIdeal.Application.Interactors.Contacts.Responses;
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
    public class ContactsController : ApiController
    {
        /// <summary>
        /// Post Contact
        /// </summary>
        /// <remarks> Post Contact </remarks>
        [HttpPost]
        public async Task<IActionResult> Post(ContactModel model, CancellationToken cancellationToken)
        {
            InsertContactRequest request = new InsertContactRequest()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone
            };

            InsertContactResponse response = await Mediator.Send(request, cancellationToken);

            return new OkObjectResult(new { response.Contact.Id });
        }

        /// <summary>
        /// Get Contact
        /// </summary>
        /// <remarks> Get Contact </remarks>
        [HttpGet]
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
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            GetContactByIdRequest request = new GetContactByIdRequest()
            {
                Id = id,
            };

            GetContactByIdResponse response = await Mediator.Send(request, cancellationToken);

            if (response.Contact == null)
                return new NotFoundObjectResult(null);
            return new OkObjectResult(new { response.Contact });
        }

        /// <summary>
        /// Put Contact
        /// </summary>
        /// <remarks> Put Contact </remarks>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ContactModel model, CancellationToken cancellationToken)
        {
            UpdateContactRequest request = new UpdateContactRequest()
            {
                Id = id,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone
            };

            UpdateContactResponse response = await Mediator.Send(request, cancellationToken);

            return new OkObjectResult(new { response.Contact });

        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <remarks> Delete Contact </remarks>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
