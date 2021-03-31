using AluguelIdeal.Application.Interactors.Contacts.Responses;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Contacts.Requests
{
    public class GetContactByIdRequest : IRequest<GetContactByIdResponse>
    {
        public int Id { get; set; }
    }
}
