using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ContactEntity = AluguelIdeal.Domain.Entities.Contact;

namespace AluguelIdeal.Api.Interactors.Contacts.Handlers
{
    public class GetContactByIdInteractor : IRequestHandler<GetContactByIdRequest, GetContactByIdResponse>
    {
        private readonly IContactRepository contactRepository;
        public GetContactByIdInteractor(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<GetContactByIdResponse> Handle(GetContactByIdRequest request, CancellationToken cancellationToken)
        {
            ContactEntity contact = await contactRepository.GetByIdAsync(request.Id, cancellationToken);

            return new GetContactByIdResponse()
            {
                Contact = contact != null ? new ContactDto(contact) : null
            };
        }
    }
}
