using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Gateways.Interfaces;
using AluguelIdeal.Api.Interactors.Contact.Request;
using AluguelIdeal.Api.Interactors.Contact.Response;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactEntity = AluguelIdeal.Api.Entities.Contact;

namespace AluguelIdeal.Api.Interactors.Contact
{
    public sealed class GetContactInteractor : IRequestHandler<GetContactRequest, GetContactResponse>
    {
        private readonly IContactRepository contactRepository;
        public GetContactInteractor(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<GetContactResponse> Handle(GetContactRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<ContactEntity> contacts = await contactRepository.ReadAsync(cancellationToken);

            return new GetContactResponse()
            {
                Contacts = contacts.Select(a => new ContactDto(a)).ToList()
            };
        }
    }
}
