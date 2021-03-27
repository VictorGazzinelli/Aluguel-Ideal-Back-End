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
