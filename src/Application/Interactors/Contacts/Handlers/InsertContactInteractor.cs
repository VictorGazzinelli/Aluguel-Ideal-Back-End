using AluguelIdeal.Application.Dto.Contacts;
using AluguelIdeal.Application.Interactors.Contacts.Requests;
using AluguelIdeal.Application.Interactors.Contacts.Responses;
using AluguelIdeal.Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ContactEntity = AluguelIdeal.Domain.Entities.Contact;

namespace AluguelIdeal.Api.Interactors.Contacts.Handlers
{
    public class InsertContactInteractor : IRequestHandler<InsertContactRequest, InsertContactResponse>
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
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            contact.Id = await contactRepository.CreateAsync(contact);

            return new InsertContactResponse()
            {
                Contact = new ContactDto(contact)
            };
        }
    }
}
