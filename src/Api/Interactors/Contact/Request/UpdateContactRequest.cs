using AluguelIdeal.Api.Interactors.Contact.Response;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Contact.Request
{
    public sealed class UpdateContactRequest : IRequest<UpdateContactResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
