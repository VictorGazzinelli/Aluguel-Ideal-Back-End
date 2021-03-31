using MediatR;

namespace AluguelIdeal.Application.Interactors.Contacts.Requests
{
    public sealed class DeleteContactRequest : IRequest
    {
        public int Id { get; set; }
    }
}
