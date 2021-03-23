using AluguelIdeal.Api.Interactors.Contact.Response;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Contact.Request
{
    public class GetContactByIdRequest : IRequest<GetContactByIdResponse>
    {
        public int Id { get; set; }
    }
}
