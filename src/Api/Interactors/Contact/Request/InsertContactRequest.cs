using AluguelIdeal.Api.Interactors.Contact.Response;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Contact.Request
{
    public sealed class InsertContactRequest : IRequest<InsertContactResponse>
    {
        public string Name { get; set; }
    }
}
