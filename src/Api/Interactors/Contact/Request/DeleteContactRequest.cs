using MediatR;

namespace AluguelIdeal.Api.Interactors.Contact.Request
{
    public sealed class DeleteContactRequest : IRequest
    {
        public int Id { get; set; }
    }
}
