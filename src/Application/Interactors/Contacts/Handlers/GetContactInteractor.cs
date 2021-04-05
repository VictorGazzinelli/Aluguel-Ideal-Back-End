using AluguelIdeal.Application.Dtos.Contacts;
using AluguelIdeal.Application.Interactors.Contacts.Requests;
using AluguelIdeal.Application.Interactors.Contacts.Responses;
using AluguelIdeal.Application.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactEntity = AluguelIdeal.Domain.Entities.Contact;

namespace AluguelIdeal.Api.Interactors.Contacts.Handlers
{
    public class GetContactInteractor : IRequestHandler<GetContactRequest, GetContactResponse>
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
                Contacts = contacts.Select(c => new ContactDto(c)).ToList()
            };
        }
    }
}
