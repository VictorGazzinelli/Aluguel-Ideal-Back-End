using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Dto.Contact;
using AluguelIdeal.Api.Interactors.Contact.Request;
using AluguelIdeal.Api.Interactors.Contact.Response;
using AluguelIdeal.Api.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ContactEntity = AluguelIdeal.Api.Entities.Contact;

namespace AluguelIdeal.Api.Interactors.Contact
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
