using MediatR;

namespace AluguelIdeal.Application.Interactors.Contacts.Request
{
    public class GetContactByIdRequest : IRequest<GetContactByIdResponse>
    {
        public int Id { get; set; }
    }
}
