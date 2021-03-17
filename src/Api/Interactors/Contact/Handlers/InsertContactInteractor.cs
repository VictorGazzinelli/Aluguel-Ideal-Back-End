using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Repositories.Interfaces;
using AluguelIdeal.Api.Interactors.Contact.Request;
using AluguelIdeal.Api.Interactors.Contact.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ContactEntity = AluguelIdeal.Api.Entities.Contact;

namespace AluguelIdeal.Api.Interactors.Contact
{
    public sealed class InsertContactInteractor : IRequestHandler<InsertContactRequest, InsertContactResponse>
    {
        private readonly IContactRepository contactRepository;
        public InsertContactInteractor(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<InsertContactResponse> Handle(InsertContactRequest request, CancellationToken cancellationToken)
        {
            ContactEntity contact = new ContactEntity()
            {
                Name = request.Name
            };

            contact.Id = await contactRepository.CreateAsync(contact);

            return new InsertContactResponse()
            {
                Contact = new ContactDto(contact)
            };
        }
    }
}
