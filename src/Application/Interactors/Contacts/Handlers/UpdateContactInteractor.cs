using AluguelIdeal.Application.Dtos.Contacts;
using AluguelIdeal.Application.Interactors.Contacts.Requests;
using AluguelIdeal.Application.Interactors.Contacts.Responses;
using AluguelIdeal.Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ContactEntity = AluguelIdeal.Domain.Entities.Contact;

namespace AluguelIdeal.Api.Interactors.Contacts.Handlers
{
    public class UpdateContactInteractor : IRequestHandler<UpdateContactRequest, UpdateContactResponse>
    {
        private readonly IContactRepository contactRepository;
        public UpdateContactInteractor(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<UpdateContactResponse> Handle(UpdateContactRequest request, CancellationToken cancellationToken)
        {
            if(await contactRepository.GetByIdAsync(request.Id, cancellationToken) == null)
                return new UpdateContactResponse()
                {
                    Contact = null
                };

            ContactEntity contact = new ContactEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            await contactRepository.UpdateAsync(contact, cancellationToken);

            return new UpdateContactResponse()
            {
                Contact = new ContactDto(contact)
            };
        }
    }
}
