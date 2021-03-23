using AluguelIdeal.Api.Interactors.Contact.Response;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Contact.Request
{
    public  class InsertContactRequest : IRequest<InsertContactResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
