using MediatR;

namespace AluguelIdeal.Application.Interactors.Contacts.Request
{
    public sealed class DeleteContactRequest : IRequest
    {
        public int Id { get; set; }
    }
}
