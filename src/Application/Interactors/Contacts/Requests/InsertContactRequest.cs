using AluguelIdeal.Application.Interactors.Contacts.Responses;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Contacts.Requests
{
    public  class InsertContactRequest : IRequest<InsertContactResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
